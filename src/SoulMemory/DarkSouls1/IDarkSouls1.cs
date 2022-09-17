﻿// This file is part of the SoulSplitter distribution (https://github.com/FrankvdStam/SoulSplitter).
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

namespace SoulMemory.DarkSouls1
{
    internal interface IDarkSouls1
    {
        bool Refresh(out Exception exception);
        bool ReadEventFlag(uint eventFlagId);
        int GetAttribute(Attribute attribute);
        bool IsWarpRequested();
        bool IsPlayerLoaded();
        int GetInGameTimeMilliseconds();
        int NgCount();
        int GetCurrentSaveSlot();
        Vector3f GetPosition();
        void ResetInventoryIndices();
        List<Item> GetInventory();
        BonfireState GetBonfireState(Bonfire bonfire);
        string GetSaveFileLocation();

#if DEBUG
        object GetTestValue();
#endif
    }
}
