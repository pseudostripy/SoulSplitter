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
using System.Security.RightsManagement;
using System.Speech.Recognition;
using System.Threading;
using System.Xml.Serialization;
using SoulMemory;
using SoulMemory.DarkSouls2;
using SoulSplitter.Splits.DarkSouls2;
using SoulSplitter.UI.Generic;
using SoulMemory.Memory;

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

        #region Splits Hierarchy ============================================================================================================================================
        //public ObservableCollection<TimingTypeVM> TimingTypeVMs { get; set; } = new ObservableCollection<TimingTypeVM>();
        public HierarchicalDS2VM RootTreeVM { get; set; } = new RootVM();
        public void RestoreHierarchy()
        {
            //When serializing the model, we can't serialize the parent relation, because that would be a circular reference. Instead, parent's are not serialized.
            //After deserializing, the parent relations must be restored.

            ////var test = Splits.SelectMany(s => s.Children))
            //foreach (var chTimingType in TimingTypeVMs)
            //{
            //    foreach (var chSplitType in chTimingType.Children)
            //    {
            //        chSplitType.Parent = chTimingType;
            //        foreach (var split in chSplitType.Children)
            //        {
            //            split.Parent = chSplitType;
            //        }
            //    }
            //}
        }
        private Split CreateSplit()
        {
            var timing = NewSplitTimingType.Value;
            switch (NewSplitType)
            {
                default:
                    throw new ArgumentException($"Split type not supported: {NewSplitType}");

                case DS2SplitType.Position:
                    return new PositionSplit(timing, NewSplitPosition.Clone());

                case DS2SplitType.BossKill:
                    return new BossKillSplit(timing, NewSplitBossKill.BossType, NewSplitBossKill.Count);

                case DS2SplitType.LvlAttr:
                    var lvldata = NewSplitLvlAttr;
                    return new LvlAttrSplit(timing, lvldata.LvlAttr, lvldata.Level);

                case DS2SplitType.Flag:
                    return new FlagSplit(timing, NewSplitFlag);
            }
        }
        private void InsertSplit(Split split)
        {
            // get timing container or make new one if unique
            var timVM = RootTreeVM.TryFindOrAddNew(split.TimingType);
            var splitTypeVM = timVM.TryFindOrAddNew(split.SplitType);
            splitTypeVM.AddNewChild(split); // known to be unique
        }
        private bool IsUniqueSplit(Split split)
        {
            var dup = Splits.FirstOrDefault(s => s.Equals(split));
            return dup == null; // null because no matches -> unique
        }
        public void AddSplit()
        {
            // create and add
            var split = CreateSplit();
            if (!IsUniqueSplit(split))
                return;
            Splits.Add(split); // Add to backend splits list

            // Add into our ViewModel tree:
            InsertSplit(split);
        }
        public void RemoveSplit()
        {
            if (SelectedSplit == null)
                return;

            SelectedSplit.DeleteRecursive();
            //var parent = SelectedSplit.Parent;
            //parent.Children.Remove(SelectedSplit);
            //if (parent.Children.Count <= 0)
            //{
            //    var nextParent = parent.Parent;
            //    nextParent.Children.Remove(parent);
            //    if (nextParent.Children.Count <= 0)
            //    {
            //        TimingTypeVMs.Remove(nextParent);
            //    }
            //}

            //SelectedSplit = null;

        }
        #endregion

        #region Properties for new splits ===================================================================================================================================
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
                SetField(ref _newSplitType, value);
                ResetHideSubComponents();
                EnableSubComponents(NewSplitType);
                UpdateAddSplitButtonEnable();
            }
        }
        private DS2SplitType? _newSplitType = null;

        private void ResetHideSubComponents()
        {
            NewSplitPositionEnabled = false;
            NewSplitBossKillEnabled = false;
            NewSplitAttributeEnabled = false;
            NewSplitFlagEnabled = false;
        }
        private void EnableSubComponents(DS2SplitType? splitType)
        {
            if (splitType == null)
                return;

            switch (splitType)
            {
                case DS2SplitType.Position:
                    NewSplitPositionEnabled = true;
                    break;

                case DS2SplitType.BossKill:
                    NewSplitBossKillEnabled = true;
                    break;

                case DS2SplitType.LvlAttr:
                    NewSplitAttributeEnabled = true;
                    break;

                case DS2SplitType.Flag:
                    NewSplitFlagEnabled = true;
                    break;

                default:
                    throw new ArgumentException($"Unsupported split type: {splitType}");
            }
        }
        private void UpdateAddSplitButtonEnable() => NewSplitAddEnabled = CheckSplitValidity();
        private bool CheckSplitValidity()
        {
            switch (NewSplitType)
            {
                case DS2SplitType.BossKill:
                    return NSBossKillComboBT != null && NewSplitBossKill.CheckValidity();

                case DS2SplitType.Position:
                    return true; // no conditions yet

                case DS2SplitType.LvlAttr:
                    return NewSplitLvlAttr.CheckValidity();

                case DS2SplitType.Flag:
                    return NewSplitFlag > 0;
                
                default: return false;
            }
        }

        // SubComponent Enables/Visibility
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

        // Add/Remove split buttons:
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
        public SplitVM SelectedSplit
        {
            get => _selectedSplit;
            set
            {
                SetField(ref _selectedSplit, value);
                RemoveSplitEnabled = SelectedSplit != null;
            }
        }
        private SplitVM _selectedSplit = null;
        #endregion

        #region MainBindings
        // BossKill binding
        // Use NewSplitBossKill for current values
        public BossKill NewSplitBossKill
        {
            get
            {
                if (NSBossKillComboBT == null)
                    return null;
                return new BossKill((BossType)NSBossKillComboBT, NSBossKillCount);
            }
        } 
        public object NSBossKillComboObject
        {
            get => _nsBossKillComboObject;
            set
            {
                // should be BossType but could be string or null b/c filtering
                _nsBossKillComboObject = value;
                if (value is BossType bosstype)
                    NSBossKillComboBT = bosstype;
                else
                    NSBossKillComboBT = null;
            }
        }
        public object _nsBossKillComboObject;
        public BossType? NSBossKillComboBT
        {
            get => _nsBossKillComboBT;
            set
            {
                SetField(ref _nsBossKillComboBT, value);
                UpdateAddSplitButtonEnable();
            }
        }
        private BossType? _nsBossKillComboBT = BossType.TheLastGiant; // default
        public int NSBossKillCount
        {
            get => _nsBossKillCount;
            set
            {
                SetField(ref _nsBossKillCount, value);
                UpdateAddSplitButtonEnable();
            }
        }
        private int _nsBossKillCount = 1; // default

        // Position binding
        public float NSPosX
        {
            get => _nsPosX;
            set
            {
                float posxf = Convert.ToSingle(value);
                SetField(ref _nsPosX, posxf);
                NewSplitPosition.X = _nsPosX;
            }
        }
        private float _nsPosX = 0.0f;
        public float NSPosY
        {
            get => _nsPosY;
            set 
            {
                float posyf = Convert.ToSingle(value);
                SetField(ref _nsPosY, posyf);
                NewSplitPosition.Y = _nsPosY;
            }
        }
        private float _nsPosY = 0.0f;
        public float NSPosZ
        {
            get => _nsPosZ;
            set
            {
                float poszf = Convert.ToSingle(value);
                SetField(ref _nsPosZ, poszf);
                NewSplitPosition.Z = poszf;
            }
        }
        private float _nsPosZ = 0.0f;
        public Vector3f NewSplitPosition
        {
            get => _newSplitPosition;
            set
            {
                NSPosX = value.X;
                NSPosY = value.Y;
                NSPosZ = value.Z;
                _newSplitPosition = value;
            }
        }
        private Vector3f _newSplitPosition = new Vector3f(0, 0, 0);

        // LvlAttr binding
        public LvlAttrData NewSplitLvlAttr => new LvlAttrData(NSLvlAttrCombo, NSLvlAttrLevel);
        public LvlAttr NSLvlAttrCombo
        {
            get => _nsLvlAttrCombo;
            set
            {
                SetField(ref _nsLvlAttrCombo, value);
                NewSplitLvlAttr.LvlAttr = _nsLvlAttrCombo; // update main VM property
                UpdateAddSplitButtonEnable();
            }
        }
        private LvlAttr _nsLvlAttrCombo = LvlAttr.SoulLevel; // default
        public int NSLvlAttrLevel
        {
            get => _nsLvlAttrLevel;
            set
            {
                SetField(ref _nsLvlAttrLevel, value);
                NewSplitLvlAttr.Level = _nsLvlAttrLevel;
                UpdateAddSplitButtonEnable();
            }
        }
        private int _nsLvlAttrLevel = 1; // default
        
        // Flag binding
        public uint NewSplitFlag
        {
            get => _nsFlag;
            set
            {
                uint flaguint = Convert.ToUInt32(value);
                SetField(ref _nsFlag, flaguint);
                UpdateAddSplitButtonEnable();
            }
        }
        private uint _nsFlag = 0; // default
        #endregion

        #region Static UI source data ============================================================================================================================================
        public static ObservableCollection<EnumFlagViewModel<BossType>> Bosses { get; set; } = new ObservableCollection<EnumFlagViewModel<BossType>>
        ( 
            Enum.GetValues(typeof(BossType)).Cast<BossType>()
                .Select(i => new EnumFlagViewModel<BossType>(i)) 
        );
        public static ObservableCollection<EnumFlagViewModel<LvlAttr>> LevelAttributes { get; set; } = new ObservableCollection<EnumFlagViewModel<LvlAttr>>
        (
            Enum.GetValues(typeof(LvlAttr)).Cast<LvlAttr>()
                .Select(i => new EnumFlagViewModel<LvlAttr>(i))
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
