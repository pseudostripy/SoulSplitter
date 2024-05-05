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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using SoulMemory;
using SoulMemory.DarkSouls2;
using SoulSplitter.Splits.DarkSouls2;
using SoulSplitter.UI.Generic;
using LvlAttrType = SoulSplitter.Splits.DarkSouls2.LvlAttrType;

namespace SoulSplitter.UI.DarkSouls2
{
    public class DarkSouls2ViewModel : INotifyPropertyChanged
    {
        public bool StartAutomatically
        {
            get => _startAutomatically;
            set => SetField(ref _startAutomatically, value);
        }
        private bool _startAutomatically = true;

        [XmlIgnore]
        public Vector3f CurrentPosition
        {
            get => _currentPosition;
            set => SetField(ref _currentPosition, value);
        }
        private Vector3f _currentPosition = new Vector3f(0f,0f,0f);

        internal List<Split> Splits = new List<Split>();

        #region add/remove splits ============================================================================================================================================
        private Split CreateSplit()
        {
            // something likely gone wrong:
            if (NewSplitTimingType == null || NewSplitType == null || NewSplitValue == null)
                return null;

            var timing = NewSplitTimingType.Value;
            switch (NewSplitType)
            {
                default:
                    throw new ArgumentException($"Split type not supported: {NewSplitType}");

                case DS2SplitType.Position:
                    return new PositionSplit(timing, (Vector3f)NewSplitValue); // TODO

                case DS2SplitType.BossKill:
                    var BK = (BossKill)NewSplitValue;
                    return new BossKillSplit(timing, BK.BossType, BK.Count);

                case DS2SplitType.LvlAttr:
                    var vmlvlattr = (LvlAttrType)NewSplitValue; // TODO
                    return new LvlAttrSplit(timing, vmlvlattr.AttributeType, 10); // TODO

                case DS2SplitType.Flag:
                    return new FlagSplit(timing, (uint)NewSplitValue);
            }
        }
        private SplitTypeVM FindInsertionParent(Split split)
        {
            // get timing container or make new one if unique
            var timingVM = TimingTypeVMs.Where(v => v.TimingType == split.TimingType).FirstOrDefault() 
                            ?? new TimingTypeVM() { TimingType = split.TimingType };

            // get splitType container or make new one if unique
            var splitTypeVM = timingVM.Children.Where(v => v.SplitType == split.SplitType).FirstOrDefault()
                            ?? new SplitTypeVM() { Parent = timingVM, SplitType = split.SplitType };
            
            // todo check if duplicate
            return splitTypeVM;
        }

        public void AddSplit()
        {
            if (NewSplitTimingType == null || NewSplitType == null || NewSplitValue == null)
                return;

            // Create new split object:
            var split = CreateSplit();

            // Find where to add new split:
            var vm = FindInsertionParent(split);
            var splitpars = new SplitParamsVM() { Parent = vm }; // TODO
            vm.AddChild(splitpars);

            // Add to backend splits:
            Splits.Add(split);

            //var timingTypeVM = TimingTypeVMs.FirstOrDefault(i => i.TimingType == NewSplitTimingType);
            //if (timingTypeVM == null)
            //{
            //    timingTypeVM = new TimingTypeVM() { TimingType = NewSplitTimingType.Value };
            //    TimingTypeVMs.Add(timingTypeVM);
            //}

            //var hierarchicalSplitType = timingTypeVM.Children.FirstOrDefault(i => i.SplitType == NewSplitType);
            //if (hierarchicalSplitType == null)
            //{
            //    hierarchicalSplitType = new SplitTypeVM() { SplitType = NewSplitType.Value, Parent = timingTypeVM };
            //    timingTypeVM.Children.Add(hierarchicalSplitType);
            //}

            //switch (NewSplitType)
            //{
            //    default:
            //        throw new ArgumentException($"split type not supported: {NewSplitType}");

            //    case DS2SplitType.Position:
            //        var position = (Vector3f)NewSplitValue;
            //        if (hierarchicalSplitType.Children.All(i => ((Vector3f)i.Split).ToString() != position.ToString()))
            //        {
            //            hierarchicalSplitType.Children.Add(new SplitParamsVM() { Split = position, Parent = hierarchicalSplitType });
            //        }
            //        break;

            //    case DS2SplitType.BossKill:
            //        var bossKill = (BossKill)NewSplitValue;
            //        if (hierarchicalSplitType.Children.All(i => ((BossKill)i.Split).ToString() != bossKill.ToString()))
            //        {
            //            hierarchicalSplitType.Children.Add(new SplitParamsVM() { Split = bossKill, Parent = hierarchicalSplitType });
            //        }
            //        break;

            //    case DS2SplitType.LvlAttr:
            //        var attribute = (LvlAttrType)NewSplitValue;
            //        if (hierarchicalSplitType.Children.All(i => ((LvlAttrType)i.Split).ToString() != attribute.ToString()))
            //        {
            //            hierarchicalSplitType.Children.Add(new SplitParamsVM() { Split = attribute, Parent = hierarchicalSplitType });
            //        }
            //        break;

            //    case DS2SplitType.Flag:
            //        var flag = (uint)NewSplitValue;
            //        if (hierarchicalSplitType.Children.All(i => (uint)i.Split != flag))
            //        {
            //            hierarchicalSplitType.Children.Add(new SplitParamsVM() { Split = flag, Parent = hierarchicalSplitType });
            //        }
            //        break;
            //}

            NewSplitTimingType = null;
            NewSplitType = null;
            NewSplitValue = null;
        }

        public void RemoveSplit()
        {
            if (SelectedSplit != null)
            {
                var parent = SelectedSplit.Parent;
                parent.Children.Remove(SelectedSplit);
                if (parent.Children.Count <= 0)
                {
                    var nextParent = parent.Parent;
                    nextParent.Children.Remove(parent);
                    if (nextParent.Children.Count <= 0)
                    {
                        TimingTypeVMs.Remove(nextParent);
                    }
                }

                SelectedSplit = null;
            }
        }

        
        public ObservableCollection<TimingTypeVM> TimingTypeVMs { get; set; } = new ObservableCollection<TimingTypeVM>();
        #endregion

        #region Properties for new splits ============================================================================================================================================

        [XmlIgnore]
        public TimingType? NewSplitTimingType
        {
            get => _newSplitTimingType;
            set
            {
                SetField(ref _newSplitTimingType, value);
                NewSplitTypeEnabled = true;
            }
        }
        private TimingType? _newSplitTimingType = null;

        [XmlIgnore]
        public DS2SplitType? NewSplitType
        {
            get => _newSplitType;
            set
            {
                // init all false set the correct value later...
                NewSplitPositionEnabled = false;
                NewSplitBossKillEnabled = false;
                NewSplitAttributeEnabled = false;
                NewSplitFlagEnabled = false;

                SetField(ref _newSplitType, value);
                switch (NewSplitType)
                {
                    case null:
                        break;

                    case DS2SplitType.Position:
                        NewSplitPositionEnabled = true;
                        NewSplitValue = new Vector3f(CurrentPosition.X, CurrentPosition.Y, CurrentPosition.Z);
                        break;

                    case DS2SplitType.BossKill:
                        NewSplitBossKillEnabled = true;
                        NewSplitValue = new BossKill();
                        break;

                    case DS2SplitType.LvlAttr:
                        NewSplitAttributeEnabled = true;
                        NewSplitValue = new SoulMemory.DarkSouls2.LvlAttr();
                        break;

                    case DS2SplitType.Flag:
                        NewSplitFlagEnabled = true;
                        break;

                    default: 
                        throw new ArgumentException($"Unsupported split type: {NewSplitType}");
                }
            }
        }
        private DS2SplitType? _newSplitType = null;

        [XmlIgnore]
        public object NewSplitValue
        {
            get => _newSplitValue;
            set
            {
                SetField(ref _newSplitValue, value);
                NewSplitAddEnabled = NewSplitValue != null;
            }
        }
        private object _newSplitValue = null;
        
        [XmlIgnore]
        public bool NewSplitTypeEnabled
        {
            get => _newSplitTypeEnabled;
            set => SetField(ref _newSplitTypeEnabled, value);
        }
        private bool _newSplitTypeEnabled = false;

        [XmlIgnore]
        public bool NewSplitPositionEnabled
        {
            get => _newSplitPositionEnabled;
            set => SetField(ref _newSplitPositionEnabled, value);
        }
        private bool _newSplitPositionEnabled = false;

        [XmlIgnore]
        public bool NewSplitBossKillEnabled
        {
            get => _newSplitBossKillEnabled;
            set => SetField(ref _newSplitBossKillEnabled, value);
        }
        private bool _newSplitBossKillEnabled = false;

        [XmlIgnore]
        public bool NewSplitAttributeEnabled
        {
            get => _newSplitAttributeEnabled;
            set => SetField(ref _newSplitAttributeEnabled, value);
        }
        private bool _newSplitAttributeEnabled = false;

        [XmlIgnore]
        public bool NewSplitFlagEnabled
        {
            get => _newSplitFlagEnabled;
            set => SetField(ref _newSplitFlagEnabled, value);
        }
        private bool _newSplitFlagEnabled = false;

        [XmlIgnore]
        public bool NewSplitAddEnabled
        {
            get => _newSplitAddEnabled;
            set => SetField(ref _newSplitAddEnabled, value);
        }
        private bool _newSplitAddEnabled = false;

        [XmlIgnore]
        public bool RemoveSplitEnabled
        {
            get => _removeSplitEnabled;
            set => SetField(ref _removeSplitEnabled, value);
        }
        private bool _removeSplitEnabled = false;

        [XmlIgnore]
        public SplitParamsVM SelectedSplit
        {
            get => _selectedSplit;
            set
            {
                SetField(ref _selectedSplit, value);
                RemoveSplitEnabled = SelectedSplit != null;
            }
        }
        private SplitParamsVM _selectedSplit = null;

        #endregion

        #region Splits hierarchy
        public void RestoreHierarchy()
        {
            //When serializing the model, we can't serialize the parent relation, because that would be a circular reference. Instead, parent's are not serialized.
            //After deserializing, the parent relations must be restored.

            //var test = Splits.SelectMany(s => s.Children))
            foreach (var chTimingType in TimingTypeVMs)
            {
                foreach (var chSplitType in chTimingType.Children)
                {
                    chSplitType.Parent = chTimingType;
                    foreach (var split in chSplitType.Children)
                    {
                        split.Parent = chSplitType;
                    }
                }
            }
        }

        #endregion

        #region Static UI source data ============================================================================================================================================

        public static ObservableCollection<EnumFlagViewModel<BossType>> Bosses { get; set; } = new ObservableCollection<EnumFlagViewModel<BossType>>
            ( 
                Enum.GetValues(typeof(BossType)).Cast<BossType>()
                    .Select(i => new EnumFlagViewModel<BossType>(i)) 
            );
        


        #endregion

        #region INotifyPropertyChanged ============================================================================================================================================

        private bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName ?? "");
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName ?? ""));
        }

        #endregion
    }
}
