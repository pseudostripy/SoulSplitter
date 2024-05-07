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
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using SoulMemory;
using SoulSplitter.Splits.DarkSouls2;
using SoulSplitter.UI.EldenRing;
using SoulSplitter.UI.Generic;

namespace SoulSplitter.UI.DarkSouls2
{
    [XmlType(Namespace = "DarkSouls2")]
    public class HierarchicalDS2VM : INotifyPropertyChanged
    {
        // Hierarchy relations
        public HierarchicalDS2VM Parent;
        public ObservableCollection<HierarchicalDS2VM> Children { get; set; } = new ObservableCollection<HierarchicalDS2VM>();
        public virtual bool IsRootNode => Parent == null;

        public void AddChild(HierarchicalDS2VM childVM) => Children.Add(childVM);
        public void RemoveChild(HierarchicalDS2VM childVM) => Children.Remove(childVM);
        public void DeleteRecursive()
        {
            // root will never need to access parents for removal of itself
            while (!IsRootNode)
            {
                Parent.RemoveChild(this);
                if (Parent.Children.Count == 0)
                    Parent.DeleteRecursive();
            }
        }
        public HierarchicalDS2VM TryFindOrAddNew(object obj)
        {
            var child = Children.Where(vm => vm.MatchesValue(obj)).FirstOrDefault();
            return child ?? AddNewChild(obj);
        }

        public virtual bool MatchesValue(object other) => false; // overridden
        public virtual HierarchicalDS2VM AddNewChild(object other) { return null; }

        // INotify Interface:
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
    public class RootVM : HierarchicalDS2VM
    {
        public override HierarchicalDS2VM AddNewChild(object obj)
        {
            // fix object uncasting
            if (!(obj is TimingType)) return null;
            TimingType timingType = (TimingType)obj;

            var timingTypeVM = new TimingTypeVM() { Parent = this, TimingType = timingType };
            AddChild(timingTypeVM);
            return timingTypeVM;
        }
        //public void AddChild(SplitTypeVM splitTypeVM) => Children.Add(splitTypeVM);
        //public ObservableCollection<SplitTypeVM> Children { get; set; } = new ObservableCollection<SplitTypeVM>();

    }

    [XmlType(Namespace = "DarkSouls2")]
    public class TimingTypeVM : HierarchicalDS2VM
    {
        public TimingType TimingType
        {
            get => _timingType;
            set => SetField(ref _timingType, value);
        }
        private TimingType _timingType;

        public override bool MatchesValue(object obj)
        {
            if (!(obj is TimingType))
                return false;
            return TimingType == (TimingType)obj;
        }
        public override HierarchicalDS2VM AddNewChild(object obj)
        {
            // fix object uncasting
            if (!(obj is DS2SplitType)) return null;
            DS2SplitType splitType = (DS2SplitType)obj;

            var splitTypeVM = new SplitTypeVM() { Parent = this, SplitType = splitType };
            AddChild(splitTypeVM);
            return splitTypeVM;
        }
        //public void AddChild(SplitTypeVM splitTypeVM) => Children.Add(splitTypeVM);
        //public ObservableCollection<SplitTypeVM> Children { get; set; } = new ObservableCollection<SplitTypeVM>();

    }

    [XmlType(Namespace = "DarkSouls2")]
    public class SplitTypeVM : HierarchicalDS2VM
    {
        //[XmlIgnore]
        //[NonSerialized]
        //public TimingTypeVM Parent;

        public DS2SplitType SplitType
        {
            get => _splitType;
            set => SetField(ref _splitType, value);
        }
        private DS2SplitType _splitType;

        public override bool MatchesValue(object obj)
        {
            if (!(obj is DS2SplitType)) 
                return false;
            return SplitType == (DS2SplitType)obj;
        }
        public override HierarchicalDS2VM AddNewChild(object obj)
        {
            // fix object uncasting
            if (!(obj is Split)) return null;
            Split split = (Split)obj;

            var splitParamVM = new SplitVM() { Parent = this, Split = split };
            AddChild(splitParamVM);
            return splitParamVM;
        }
        
        //public void AddChild(SplitParamsVM splitparams) => Children.Add(splitparams);
        //public ObservableCollection<SplitParamsVM> Children { get; set; } = new ObservableCollection<SplitParamsVM>();
    }


    [XmlType(Namespace = "DarkSouls2")]
    [XmlInclude(typeof(Vector3f)), 
     XmlInclude(typeof(BossKill)), 
     XmlInclude(typeof(uint))]
    public class SplitVM : HierarchicalDS2VM
    {
        //[XmlIgnore]
        //[NonSerialized]
        //public SplitTypeVM Parent;

        [XmlElement(Namespace = "DarkSouls2")]
        public Split Split
        {
            get => _split;
            set => SetField(ref _split, value);
        }
        private Split _split;
        public override bool MatchesValue(object obj)
        {
            if (!(obj is Split))
                return false;
            return Split.Equals((Split)obj);
        }
    }
}
