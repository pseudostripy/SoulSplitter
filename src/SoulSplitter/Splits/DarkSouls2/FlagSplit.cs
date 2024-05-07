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
using SoulMemory;
using SoulMemory.DarkSouls2;
using SoulMemory.EldenRing;
using SoulSplitter.UI.Generic;

namespace SoulSplitter.Splits.DarkSouls2
{
    internal class FlagSplit : Split
    {
        public FlagSplit(TimingType timingType, uint flag) : base(timingType,DS2SplitType.Flag)
        {
            Flag = flag;
        }

        public readonly uint Flag;

        // Subclass overrides and concreteness
        public override string ToString() => Flag.ToString();
        public override bool Equals(object obj) => Equals(obj as FlagSplit);
        public bool Equals(FlagSplit other)
        {
            return other != null &&
                    TimingType == other.TimingType &&
                    Flag == other.Flag;
        }
        public override int GetHashCode() => (TimingType, Flag).GetHashCode();
    }
}
