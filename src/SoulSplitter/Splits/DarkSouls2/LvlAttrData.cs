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

using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using SoulMemory.DarkSouls2;
using SoulMemory.Memory;

namespace SoulSplitter.Splits.DarkSouls2
{
    [XmlType(Namespace = "DarkSouls2")]
    public class LvlAttrData
    {
        public LvlAttrData(LvlAttr lvlAttr, int level)
        {
            LvlAttr = lvlAttr;
            Level = level;
        }

        public LvlAttr LvlAttr { get; set; }
        public int Level {  get; set; }

        public bool CheckValidity()
        {
            // Add more here as appropriate
            return Level > 0; // && BossType exists
        }

        public override string ToString() => $"{LvlAttr.GetDisplayName()} {Level}";
    }
}
