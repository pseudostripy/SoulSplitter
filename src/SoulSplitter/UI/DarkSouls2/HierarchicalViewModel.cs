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
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using SoulMemory;
using SoulSplitter.Splits.DarkSouls2;
using SoulSplitter.UI.Generic;
using LvlAttrType = SoulSplitter.Splits.DarkSouls2.LvlAttrType;

namespace SoulSplitter.UI.DarkSouls2
{
    [XmlType(Namespace = "DarkSouls2")]
    public class HierarchicalDS2VM : INotifyPropertyChanged
    {
        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName ?? "");
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName ?? ""));
        }
    }

    [XmlType(Namespace = "DarkSouls2")]
    public class TimingTypeVM : INotifyPropertyChanged
    {
        public TimingType TimingType
        {
            get => _timingType;
            set => SetField(ref _timingType, value);
        }
        private TimingType _timingType;
        
        public ObservableCollection<SplitTypeVM> Children { get; set; } = new ObservableCollection<SplitTypeVM>();

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName ?? "");
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName ?? ""));
        }
    }

    [XmlType(Namespace = "DarkSouls2")]
    public class SplitTypeVM : HierarchicalDS2VM
    {
        [XmlIgnore]
        [NonSerialized]
        public TimingTypeVM Parent;

        public DS2SplitType SplitType
        {
            get => _splitType;
            set => SetField(ref _splitType, value);
        }
        private DS2SplitType _splitType;

        public void AddChild(SplitParamsVM splitparams) => Children.Add(splitparams);
        public ObservableCollection<SplitParamsVM> Children { get; set; } = new ObservableCollection<SplitParamsVM>();
    }


    [XmlType(Namespace = "DarkSouls2")]
    [XmlInclude(typeof(Vector3f)), 
     XmlInclude(typeof(BossKill)), 
     XmlInclude(typeof(LvlAttrType)), 
     XmlInclude(typeof(uint))]
    public class SplitParamsVM : HierarchicalDS2VM
    {
        [XmlIgnore]
        [NonSerialized]
        public SplitTypeVM Parent;

        [XmlElement(Namespace = "DarkSouls2")]
        public object Split
        {
            get => _split;
            set => SetField(ref _split, value);
        }
        private object _split;
    }
}
