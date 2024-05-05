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
using SoulSplitter.UI.Generic;

namespace SoulSplitter.Splits.DarkSouls2
{
    internal class LvlAttrSplit : Split
    {
        public LvlAttrSplit(TimingType timingType, LvlAttr lvlAttrType, int level) : base(timingType,DS2SplitType.LvlAttr)
        {
            LvlAttrType = lvlAttrType;
            Level = level;
        }

        public readonly LvlAttr LvlAttrType;
        public readonly int Level;
    }
}
