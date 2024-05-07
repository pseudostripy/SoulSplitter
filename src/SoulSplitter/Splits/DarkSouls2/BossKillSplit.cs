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
using SoulMemory.Memory;
using SoulSplitter.UI.Generic;

namespace SoulSplitter.Splits.DarkSouls2
{
    internal class BossKillSplit : Split
    {
        public BossKillSplit(TimingType timingType, BossType bossType, int count) : base(timingType,DS2SplitType.BossKill)
        {
            BossType = bossType;
            Count = count;
        }

        public readonly BossType BossType;
        public readonly int Count;

        // Subclass overrides and concreteness
        private string KillCntDisplay => Count == 1 ? string.Empty : $" [kill {Count}]";
        public override string ToString() => $"{BossType.GetDisplayName()}{KillCntDisplay}";
        public override bool Equals(object obj) => Equals(obj as BossKillSplit);
        public bool Equals(BossKillSplit other)
        {
            return other != null &&
                    TimingType == other.TimingType &&
                    BossType == other.BossType &&
                    Count == other.Count;
        }
        public override int GetHashCode() => (TimingType, BossType, Count).GetHashCode();


        
    }
}
