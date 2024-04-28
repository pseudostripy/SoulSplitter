// This file is part of the SoulSplitter distribution (https://github.com/FrankvdStam/SoulSplitter).
// Copyright (c) 2022 Frank van der Stam.
// https://github.com/FrankvdStam/SoulSplitter/blob/main/LICENSE
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, version 3.
//
// This program is distributed in the hope that it will be useful, but
// WITHOUT ANY WARRANTY without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program. If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Linq;
using LiveSplit.Model;
using SoulMemory;
using SoulMemory.DarkSouls2;
using SoulMemory.EldenRing;
using SoulSplitter.Splits.DarkSouls2;
using SoulSplitter.UI;
using SoulSplitter.UI.DarkSouls2;
using SoulSplitter.UI.Generic;

namespace SoulSplitter.Splitters
{
    public class DarkSouls2Splitter : ISplitter
    {
        private readonly DarkSouls2 _darkSouls2;
        private DarkSouls2ViewModel _darkSouls2ViewModel;
        private MainViewModel _mainViewModel;
        private readonly LiveSplitState _liveSplitState;

        // Shorthand for common properties
        private bool IsLoading => _darkSouls2.IsLoading();
        private Vector3f Pos => _darkSouls2.GetPosition(); // current position

        public DarkSouls2Splitter(LiveSplitState state, DarkSouls2 darkSouls2)
        {
            _darkSouls2 = darkSouls2;
            _liveSplitState = state;
            _liveSplitState.OnStart += OnStart;
            _liveSplitState.OnReset += OnReset;
            _liveSplitState.IsGameTimePaused = true;

            _timerModel = new TimerModel();
            _timerModel.CurrentState = state;
        }

        public void SetViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        #region 

        private void OnStart(object sender, EventArgs e)
        {
            StartTimer();
            StartAutoSplitting();
        }

        private void OnReset(object sender, TimerPhase timerPhase)
        {
            ResetTimer();
            ResetAutoSplitting();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _liveSplitState.OnStart -= OnStart;
                _liveSplitState.OnReset -= OnReset;
            }
        }


        public ResultErr<RefreshError> Update(MainViewModel mainViewModel)
        {
            _darkSouls2ViewModel = mainViewModel.DarkSouls2ViewModel;

            _darkSouls2.TryRefresh();

            _darkSouls2ViewModel.CurrentPosition = _darkSouls2.GetPosition();

            UpdateTimer();

            UpdateAutoSplitter();

            mainViewModel.TryAndHandleError(() =>
            {
                mainViewModel.FlagTrackerViewModel.Update(_darkSouls2);
            });

            return Result.Ok();
        }

        #endregion

        #region Timer

        private bool _previousIsLoading;
        private readonly TimerModel _timerModel;
        private TimerState _timerState = TimerState.WaitForStart;

        private void StartTimer()
        {
            _timerState = TimerState.Running;
            _liveSplitState.IsGameTimePaused = false;
            _previousIsLoading = _darkSouls2.IsLoading();
            _timerModel.Start();
        }

        private void ResetTimer()
        {
            _timerState = TimerState.WaitForStart;
            _liveSplitState.IsGameTimePaused = true;
            _timerModel.Reset();
        }

        private bool InsideGameStartBox(Vector3f pos)
        {
            return pos.X < -213.0f && pos.X > -214.0f &&
                   pos.Y < -322.0f && pos.Y > -323.0f;
        }
        private void UpdateTimer()
        {
            switch (_timerState)
            {
                case TimerState.WaitForStart:
                    if (!_darkSouls2ViewModel.StartAutomatically)
                        return;

                    if (!IsLoading && InsideGameStartBox(Pos))
                        _timerModel.Start();
                    break;

                case TimerState.Running:
                    //Pause on loads
                    if (_previousIsLoading != IsLoading)
                        _liveSplitState.IsGameTimePaused = IsLoading;
                    _previousIsLoading = IsLoading;
                    break;
            }
        }

        #endregion

        #region Autosplitting

        private List<Split> _splits = new List<Split>();

        public void ResetAutoSplitting()
        {
            _splits.Clear();
        }

        public void StartAutoSplitting()
        {
            _splits = (
                from timingType in _darkSouls2ViewModel.Splits
                from splitType in timingType.Children
                from split in splitType.Children
                select new Split(timingType.TimingType, splitType.SplitType, split.Split)
            ).ToList();
        }


        private const float _boxSize = 5.0f;
        public void UpdateAutoSplitter()
        {
            if (_timerState != TimerState.Running)
                return;

            foreach (var s in _splits.Where(sp => !sp.SplitTriggered))
            {
                // Condition must first be met ONCE
                if (!SplitConditionMetEver(s))
                    continue;

                // passed at least once
                s.SplitConditionMet = true;

                // SplitTrigger condition:
                if (SplitTriggerMet(s))
                    TriggerSplit(s);
            }
        }

        private bool SplitConditionMetEver(Split s)
        {
            // Met but not triggered:
            if (s.SplitConditionMet)
                return true;

            switch (s.SplitType)
            {
                default:
                    throw new ArgumentException($"Unsupported split type {s.SplitType}");

                case DarkSouls2SplitType.Flag:
                    return _darkSouls2.ReadEventFlag(s.Flag);

                case DarkSouls2SplitType.BossKill:
                    return _darkSouls2.GetBossKillCount(s.BossKill.BossType) == s.BossKill.Count;

                case DarkSouls2SplitType.Attribute:
                    return _darkSouls2.GetAttribute(s.Attribute.AttributeType) >= s.Attribute.Level;

                case DarkSouls2SplitType.Position:
                    return IsPositionWithinBox(s, _darkSouls2ViewModel.CurrentPosition);
            }
        }
        private bool IsPositionWithinBox(Split s, Vector3f currpos)
        {
            return s.Position.X - _boxSize < currpos.X && s.Position.X + _boxSize > currpos.X &&
                    s.Position.Y - _boxSize < currpos.Y && s.Position.Y + _boxSize > currpos.Y &&
                    s.Position.Z - _boxSize < currpos.Z && s.Position.Z + _boxSize > currpos.Z;

        }

        private void TriggerSplit(Split s)
        {
            _timerModel.Split();
            s.SplitTriggered = true;
        }
        private bool SplitTriggerMet(Split s)
        {
            switch (s.TimingType)
            {
                case TimingType.Immediate: return true;
                case TimingType.OnLoading: return _darkSouls2.IsLoading();
                default: throw new ArgumentException($"Unsupported timing type {s.TimingType}");
            }
        }
        #endregion

    }
}
