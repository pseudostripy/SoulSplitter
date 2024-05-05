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

namespace SoulSplitter.Tests
{
    internal static class XmlData
    {


        public const string SekiroMigration1_1_0 =
            @"<MainViewModel xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
  <Version>1.1.0</Version>
  <SelectedGame>Sekiro</SelectedGame>
  <DarkSouls1ViewModel />
  <DarkSouls2ViewModel>
    <StartAutomatically>true</StartAutomatically>
    <Splits />
  </DarkSouls2ViewModel>
  <DarkSouls3ViewModel>
    <StartAutomatically>true</StartAutomatically>
    <Splits />
  </DarkSouls3ViewModel>
  <SekiroViewModel>
    <StartAutomatically>true</StartAutomatically>
    <OverwriteIgtOnStart>false</OverwriteIgtOnStart>
    <Splits>
      <TimingTypeVM>
        <TimingType xmlns=""Sekiro"">Immediate</TimingType>
        <Children xmlns=""Sekiro"">
          <SplitTypeVM>
            <SplitType>Boss</SplitType>
            <Children>
              <SplitParamsVM>
                <Split xsi:type=""Boss"">GyoubuMasatakaOniwa</Split>
              </SplitParamsVM>
            </Children>
          </SplitTypeVM>
          <SplitTypeVM>
            <SplitType>Idol</SplitType>
            <Children>
              <SplitParamsVM>
                <Split xsi:type=""Idol"">DragonspringHirataEstate</Split>
              </SplitParamsVM>
            </Children>
          </SplitTypeVM>
          <SplitTypeVM>
            <SplitType>Position</SplitType>
            <Children>
              <SplitParamsVM>
                <Split xmlns:q1=""SoulMemory"" xsi:type=""q1:Vector3f"">
                  <q1:X>1</q1:X>
                  <q1:Y>2</q1:Y>
                  <q1:Z>3</q1:Z>
                </Split>
              </SplitParamsVM>
            </Children>
          </SplitTypeVM>
        </Children>
      </TimingTypeVM>
      <TimingTypeVM>
        <TimingType xmlns=""Sekiro"">OnLoading</TimingType>
        <Children xmlns=""Sekiro"">
          <SplitTypeVM>
            <SplitType>Flag</SplitType>
            <Children>
              <SplitParamsVM>
                <Split xsi:type=""xsd:unsignedInt"">123456</Split>
              </SplitParamsVM>
            </Children>
          </SplitTypeVM>
        </Children>
      </TimingTypeVM>
    </Splits>
  </SekiroViewModel>
  <EldenRingViewModel>
    <StartAutomatically>true</StartAutomatically>
    <LockIgtToZero>false</LockIgtToZero>
    <EnabledRemoveSplit>false</EnabledRemoveSplit>
    <Splits />
  </EldenRingViewModel>
</MainViewModel>";

        public const string ExampleSettings = @"<MainViewModel xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
  <Version>1.0.24</Version>
  <SelectedGameIndex>2</SelectedGameIndex>
  <SekiroViewModel>
    <StartAutomatically>true</StartAutomatically>
    <Splits>
      <TimingTypeVM>
        <TimingType xmlns=""Sekiro"">Immediate</TimingType>
        <Children xmlns=""Sekiro"">
          <SplitTypeVM>
            <SplitType>Position</SplitType>
            <Children>
              <SplitParamsVM>
                <Split xsi:type=""Vector3f"">
                  <X xmlns="""">-304.803772</X>
                  <Y xmlns="""">-53.6740761</Y>
                  <Z xmlns="""">305.3302</Z>
                </Split>
              </SplitParamsVM>
            </Children>
          </SplitTypeVM>
        </Children>
      </TimingTypeVM>
    </Splits>
  </SekiroViewModel>
  <EldenRingViewModel>
    <StartAutomatically>true</StartAutomatically>
    <LockIgtToZero>false</LockIgtToZero>
    <EnabledRemoveSplit>false</EnabledRemoveSplit>
    <Splits>
      <TimingTypeVM>
        <TimingType>Immediate</TimingType>
        <Children>
          <SplitTypeVM>
            <EldenRingSplitType>Flag</EldenRingSplitType>
            <Children>
              <SplitParamsVM>
                <Split xsi:type=""xsd:unsignedInt"">60000</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""xsd:unsignedInt"">1034447900</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""xsd:unsignedInt"">11007175</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""xsd:unsignedInt"">13007150</Split>
              </SplitParamsVM>
            </Children>
          </SplitTypeVM>
          <SplitTypeVM>
            <EldenRingSplitType>Grace</EldenRingSplitType>
            <Children>
              <SplitParamsVM>
                <Split xsi:type=""Grace"">Gatefront</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""Grace"">StormhillShack</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""Grace"">SouthRayaLucariaGate</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""Grace"">MainAcademyGate</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""Grace"">DragonTempleRooftop</Split>
              </SplitParamsVM>
            </Children>
          </SplitTypeVM>
        </Children>
      </TimingTypeVM>
      <TimingTypeVM>
        <TimingType>OnLoading</TimingType>
        <Children>
          <SplitTypeVM>
            <EldenRingSplitType>Grace</EldenRingSplitType>
            <Children>
              <SplitParamsVM>
                <Split xsi:type=""Grace"">LiurniaHighwaySouth</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""Grace"">DragonTemple</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""Grace"">TempleOfEiglay</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""Grace"">TheFourBelfries</Split>
              </SplitParamsVM>
            </Children>
          </SplitTypeVM>
          <SplitTypeVM>
            <EldenRingSplitType>Flag</EldenRingSplitType>
            <Children>
              <SplitParamsVM>
                <Split xsi:type=""xsd:unsignedInt"">1034457100</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""xsd:unsignedInt"">520670</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""xsd:unsignedInt"">16007020</Split>
              </SplitParamsVM>
            </Children>
          </SplitTypeVM>
          <SplitTypeVM>
            <EldenRingSplitType>Boss</EldenRingSplitType>
            <Children>
              <SplitParamsVM>
                <Split xsi:type=""Boss"">GodskinDuoCrumblingFarumAzula</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""Boss"">MalikethTheBlackBladeCrumblingFarumAzula</Split>
              </SplitParamsVM>
            </Children>
          </SplitTypeVM>
        </Children>
      </TimingTypeVM>
    </Splits>
  </EldenRingViewModel>
<DarkSouls1ViewModel />
  <DarkSouls2ViewModel>
    <Splits />
  </DarkSouls2ViewModel>
</MainViewModel>";

        public const string DarkSouls3Migration_1_9_0 = @"<MainViewModel xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
  <Version>1.1.9</Version>
  <SelectedGame>DarkSouls3</SelectedGame>
  <DarkSouls1ViewModel>
    <StartAutomatically>true</StartAutomatically>
    <SplitsViewModel>
      <Splits />
    </SplitsViewModel>
    <ResetInventoryIndices>true</ResetInventoryIndices>
  </DarkSouls1ViewModel>
  <DarkSouls2ViewModel>
    <StartAutomatically>true</StartAutomatically>
    <Splits />
  </DarkSouls2ViewModel>
  <DarkSouls3ViewModel>
    <StartAutomatically>true</StartAutomatically>
    <Splits>
      <TimingTypeVM>
        <TimingType xmlns=""DarkSouls3"">OnLoading</TimingType>
        <Children xmlns=""DarkSouls3"">
          <SplitTypeVM>
            <SplitType>Boss</SplitType>
            <Children>
              <SplitParamsVM>
                <Split xsi:type=""Boss"">IudexGundyr</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""Boss"">VordtOfTheBorealValley</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""Boss"">CurseRottedGreatwood</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""Boss"">DeaconsOfTheDeep</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""Boss"">PontiffSulyvahn</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""Boss"">AldrichDevourerOfGods</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""Boss"">YhormTheGiant</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""Boss"">OceirosTheConsumedKing</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""Boss"">ChampionGundyr</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""Boss"">DragonslayerArmour</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""Boss"">LothricYoungerPrince</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""Boss"">AncientWyvern</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""Boss"">DarkeaterMidir</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""Boss"">HalflightSpearOfTheChurch</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""Boss"">SlaveKnightGael</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""Boss"">SisterFriede</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""Boss"">ChampionsGravetenderAndGravetenderGreatwolf</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""Boss"">SoulOfCinder</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""Boss"">OldDemonKing</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""Boss"">NamelessKing</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""Boss"">HighLordWolnir</Split>
              </SplitParamsVM>
            </Children>
          </SplitTypeVM>
          <SplitTypeVM>
            <SplitType>Flag</SplitType>
            <Children>
              <SplitParamsVM>
                <Split xsi:type=""xsd:unsignedInt"">133</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""xsd:unsignedInt"">70000109</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""xsd:unsignedInt"">25105032</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""xsd:unsignedInt"">25105003</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""xsd:unsignedInt"">64500630</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""xsd:unsignedInt"">14100510</Split>
              </SplitParamsVM>
            </Children>
          </SplitTypeVM>
          <SplitTypeVM>
            <SplitType>Attribute</SplitType>
            <Children>
              <SplitParamsVM>
                <Split xsi:type=""Attribute"">
                  <AttributeType>Dexterity</AttributeType>
                  <Level>39</Level>
                </Split>
              </SplitParamsVM>
            </Children>
          </SplitTypeVM>
        </Children>
      </TimingTypeVM>
      <TimingTypeVM>
        <TimingType xmlns=""DarkSouls3"">Immediate</TimingType>
        <Children xmlns=""DarkSouls3"">
          <SplitTypeVM>
            <SplitType>Flag</SplitType>
            <Children>
              <SplitParamsVM>
                <Split xsi:type=""xsd:unsignedInt"">13000885</Split>
              </SplitParamsVM>
            </Children>
          </SplitTypeVM>
          <SplitTypeVM>
            <SplitType>Bonfire</SplitType>
            <Children>
              <SplitParamsVM>
                <Split xsi:type=""Bonfire"">OceirosTheConsumedKing</Split>
              </SplitParamsVM>
            </Children>
          </SplitTypeVM>
          <SplitTypeVM>
            <SplitType>ItemPickup</SplitType>
            <Children>
              <SplitParamsVM>
                <Split xsi:type=""ItemPickup"">CovenantWarriorOfSunlightRank1SunlightShield</Split>
              </SplitParamsVM>
            </Children>
          </SplitTypeVM>
        </Children>
      </TimingTypeVM>
    </Splits>
  </DarkSouls3ViewModel>
  <SekiroViewModel>
    <StartAutomatically>true</StartAutomatically>
    <SplitsViewModel>
      <Splits />
    </SplitsViewModel>
    <OverwriteIgtOnStart>false</OverwriteIgtOnStart>
  </SekiroViewModel>
  <EldenRingViewModel>
    <StartAutomatically>true</StartAutomatically>
    <LockIgtToZero>false</LockIgtToZero>
    <EnabledRemoveSplit>false</EnabledRemoveSplit>
    <Splits />
  </EldenRingViewModel>
</MainViewModel>";

        public static string ExampleSplits =
            @"<MainViewModel xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
  <_badgeVisibilityInverse>Visible</_badgeVisibilityInverse>
  <Version>1.5.2</Version>
  <SelectedGame>ArmoredCore6</SelectedGame>
  <DarkSouls1ViewModel>
    <StartAutomatically>true</StartAutomatically>
    <SplitsViewModel>
      <Splits>
        <SplitTimingViewModel>
          <TimingType>OnWarp</TimingType>
          <Children>
            <SplitTypeViewModel>
              <SplitType>Flag</SplitType>
              <Children>
                <SplitViewModel>
                  <Split xsi:type=""FlagDescription"">
                    <Flag>11000530</Flag>
                    <Description>Laurentius</Description>
                    <State>false</State>
                  </Split>
                </SplitViewModel>
              </Children>
            </SplitTypeViewModel>
            <SplitTypeViewModel>
              <SplitType>Item</SplitType>
              <Children>
                <SplitViewModel>
                  <Split xmlns:q1=""DarkSouls1"" xsi:type=""q1:ItemState"">
                    <q1:ItemType>ChestersLongCoat</q1:ItemType>
                  </Split>
                </SplitViewModel>
              </Children>
            </SplitTypeViewModel>
            <SplitTypeViewModel>
              <SplitType>Boss</SplitType>
              <Children>
                <SplitViewModel>
                  <Split xmlns:q2=""SoulMemory.DarkSouls1"" xsi:type=""q2:Boss"">ChaosWitchQuelaag</Split>
                </SplitViewModel>
                <SplitViewModel>
                  <Split xmlns:q3=""SoulMemory.DarkSouls1"" xsi:type=""q3:Boss"">BellGargoyles</Split>
                </SplitViewModel>
                <SplitViewModel>
                  <Split xmlns:q4=""SoulMemory.DarkSouls1"" xsi:type=""q4:Boss"">OrnsteinAndSmough</Split>
                </SplitViewModel>
              </Children>
            </SplitTypeViewModel>
          </Children>
        </SplitTimingViewModel>
        <SplitTimingViewModel>
          <TimingType>Immediate</TimingType>
          <Children>
            <SplitTypeViewModel>
              <SplitType>Position</SplitType>
              <Children>
                <SplitViewModel>
                  <Split xsi:type=""VectorSize"">
                    <Position>
                      <X xmlns=""SoulMemory"">177.900177</X>
                      <Y xmlns=""SoulMemory"">207.2</Y>
                      <Z xmlns=""SoulMemory"">131.920151</Z>
                    </Position>
                    <Size>5</Size>
                    <Description />
                  </Split>
                </SplitViewModel>
              </Children>
            </SplitTypeViewModel>
          </Children>
        </SplitTimingViewModel>
      </Splits>
    </SplitsViewModel>
    <ResetInventoryIndices>true</ResetInventoryIndices>
  </DarkSouls1ViewModel>
  <DarkSouls2ViewModel>
    <StartAutomatically>true</StartAutomatically>
    <Splits />
  </DarkSouls2ViewModel>
  <DarkSouls3ViewModel>
    <StartAutomatically>true</StartAutomatically>
    <Splits>
      <TimingTypeVM>
        <TimingType xmlns=""DarkSouls3"">OnLoading</TimingType>
        <Children xmlns=""DarkSouls3"">
          <SplitTypeVM>
            <SplitType>Boss</SplitType>
            <Children>
              <SplitParamsVM>
                <Split xsi:type=""Boss"">IudexGundyr</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""Boss"">VordtOfTheBorealValley</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""Boss"">CurseRottedGreatwood</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""Boss"">DeaconsOfTheDeep</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""Boss"">PontiffSulyvahn</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""Boss"">AldrichDevourerOfGods</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""Boss"">YhormTheGiant</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""Boss"">OceirosTheConsumedKing</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""Boss"">ChampionGundyr</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""Boss"">DragonslayerArmour</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""Boss"">LothricYoungerPrince</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""Boss"">AncientWyvern</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""Boss"">DarkeaterMidir</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""Boss"">HalflightSpearOfTheChurch</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""Boss"">SlaveKnightGael</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""Boss"">SisterFriede</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""Boss"">ChampionsGravetenderAndGravetenderGreatwolf</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""Boss"">SoulOfCinder</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""Boss"">OldDemonKing</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""Boss"">NamelessKing</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""Boss"">HighLordWolnir</Split>
              </SplitParamsVM>
            </Children>
          </SplitTypeVM>
          <SplitTypeVM>
            <SplitType>Flag</SplitType>
            <Children>
              <SplitParamsVM>
                <Split xsi:type=""xsd:unsignedInt"">133</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""xsd:unsignedInt"">70000109</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""xsd:unsignedInt"">25105032</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""xsd:unsignedInt"">25105003</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""xsd:unsignedInt"">64500630</Split>
              </SplitParamsVM>
              <SplitParamsVM>
                <Split xsi:type=""xsd:unsignedInt"">14100510</Split>
              </SplitParamsVM>
            </Children>
          </SplitTypeVM>
          <SplitTypeVM>
            <SplitType>Attribute</SplitType>
            <Children>
              <SplitParamsVM>
                <Split xsi:type=""Attribute"">
                  <AttributeType>Dexterity</AttributeType>
                  <Level>39</Level>
                </Split>
              </SplitParamsVM>
            </Children>
          </SplitTypeVM>
        </Children>
      </TimingTypeVM>
      <TimingTypeVM>
        <TimingType xmlns=""DarkSouls3"">Immediate</TimingType>
        <Children xmlns=""DarkSouls3"">
          <SplitTypeVM>
            <SplitType>Flag</SplitType>
            <Children>
              <SplitParamsVM>
                <Split xsi:type=""xsd:unsignedInt"">13000885</Split>
              </SplitParamsVM>
            </Children>
          </SplitTypeVM>
          <SplitTypeVM>
            <SplitType>Bonfire</SplitType>
            <Children>
              <SplitParamsVM>
                <Split xsi:type=""Bonfire"">OceirosTheConsumedKing</Split>
              </SplitParamsVM>
            </Children>
          </SplitTypeVM>
          <SplitTypeVM>
            <SplitType>ItemPickup</SplitType>
            <Children>
              <SplitParamsVM>
                <Split xsi:type=""ItemPickup"">CovenantWarriorOfSunlightRank1SunlightShield</Split>
              </SplitParamsVM>
            </Children>
          </SplitTypeVM>
        </Children>
      </TimingTypeVM>
    </Splits>
  </DarkSouls3ViewModel>
  <SekiroViewModel>
    <StartAutomatically>true</StartAutomatically>
    <SplitsViewModel>
      <Splits>
        <SplitTimingViewModel>
          <TimingType>Immediate</TimingType>
          <Children>
            <SplitTypeViewModel>
              <SplitType>Boss</SplitType>
              <Children>
                <SplitViewModel>
                  <Split xmlns:q5=""Sekiro"" xsi:type=""q5:Boss"">GyoubuMasatakaOniwa</Split>
                </SplitViewModel>
              </Children>
            </SplitTypeViewModel>
          </Children>
        </SplitTimingViewModel>
        <SplitTimingViewModel>
          <TimingType>OnLoading</TimingType>
          <Children>
            <SplitTypeViewModel>
              <SplitType>Bonfire</SplitType>
              <Children>
                <SplitViewModel>
                  <Split xmlns:q6=""Sekiro"" xsi:type=""q6:Idol"">DragonspringHirataEstate</Split>
                </SplitViewModel>
              </Children>
            </SplitTypeViewModel>
          </Children>
        </SplitTimingViewModel>
      </Splits>
    </SplitsViewModel>
    <OverwriteIgtOnStart>false</OverwriteIgtOnStart>
  </SekiroViewModel>
  <EldenRingViewModel>
    <StartAutomatically>true</StartAutomatically>
    <LockIgtToZero>false</LockIgtToZero>
    <EnabledRemoveSplit>false</EnabledRemoveSplit>
    <Splits />
  </EldenRingViewModel>
  <ArmoredCore6ViewModel>
    <StartAutomatically>true</StartAutomatically>
    <SplitsViewModel>
      <Splits>
        <SplitTimingViewModel>
          <TimingType>Immediate</TimingType>
          <Children>
            <SplitTypeViewModel>
              <SplitType>Flag</SplitType>
              <Children>
                <SplitViewModel>
                  <Split xsi:type=""FlagDescription"">
                    <Flag>123</Flag>
                    <Description>First boss</Description>
                    <State>false</State>
                  </Split>
                </SplitViewModel>
              </Children>
            </SplitTypeViewModel>
          </Children>
        </SplitTimingViewModel>
        <SplitTimingViewModel>
          <TimingType>OnLoading</TimingType>
          <Children>
            <SplitTypeViewModel>
              <SplitType>Flag</SplitType>
              <Children>
                <SplitViewModel>
                  <Split xsi:type=""FlagDescription"">
                    <Flag>5421</Flag>
                    <Description>End mission</Description>
                    <State>false</State>
                  </Split>
                </SplitViewModel>
              </Children>
            </SplitTypeViewModel>
          </Children>
        </SplitTimingViewModel>
      </Splits>
    </SplitsViewModel>
  </ArmoredCore6ViewModel>
  <FlagTrackerViewModel>
    <EventFlagCategories>
      <FlagTrackerCategoryViewModel>
        <CategoryName>Asylum</CategoryName>
        <EventFlags>
          <FlagDescription>
            <Flag>51810000</Flag>
            <Description>Dungeon cell key</Description>
            <State>false</State>
          </FlagDescription>
          <FlagDescription>
            <Flag>51810270</Flag>
            <Description>Cracked round shield</Description>
            <State>false</State>
          </FlagDescription>
          <FlagDescription>
            <Flag>51810260</Flag>
            <Description>Hand axe</Description>
            <State>false</State>
          </FlagDescription>
          <FlagDescription>
            <Flag>11810450</Flag>
            <Description>Estus Flask</Description>
            <State>false</State>
          </FlagDescription>
          <FlagDescription>
            <Flag>11810591</Flag>
            <Description>Undead asylum F2 east key</Description>
            <State>false</State>
          </FlagDescription>
          <FlagDescription>
            <Flag>51810280</Flag>
            <Description>Pyromancy flame</Description>
            <State>false</State>
          </FlagDescription>
          <FlagDescription>
            <Flag>16</Flag>
            <Description>Asylum Demon</Description>
            <State>false</State>
          </FlagDescription>
        </EventFlags>
      </FlagTrackerCategoryViewModel>
      <FlagTrackerCategoryViewModel>
        <CategoryName>Firelink</CategoryName>
        <EventFlags>
          <FlagDescription>
            <Flag>51020000</Flag>
            <Description>Humanity</Description>
            <State>false</State>
          </FlagDescription>
          <FlagDescription>
            <Flag>51020110</Flag>
            <Description>Firebomb</Description>
            <State>false</State>
          </FlagDescription>
        </EventFlags>
      </FlagTrackerCategoryViewModel>
    </EventFlagCategories>
    <WindowWidth>800</WindowWidth>
    <WindowHeight>840</WindowHeight>
    <InputColumnWidth>296</InputColumnWidth>
    <CategoryPercentagesRowHeight>210</CategoryPercentagesRowHeight>
    <CategoryPercentageFontSize>72.061068702290086</CategoryPercentageFontSize>
    <TotalPercentageFontSize>176</TotalPercentageFontSize>
    <BackgroundColor>
      <A>255</A>
      <R>4</R>
      <G>244</G>
      <B>4</B>
      <ScA>1</ScA>
      <ScR>0.001214108</ScR>
      <ScG>0.9046612</ScG>
      <ScB>0.001214108</ScB>
    </BackgroundColor>
    <TextColor>
      <A>255</A>
      <R>0</R>
      <G>0</G>
      <B>0</B>
      <ScA>1</ScA>
      <ScR>0</ScR>
      <ScG>0</ScG>
      <ScB>0</ScB>
    </TextColor>
    <FlagsPerFrame>10</FlagsPerFrame>
  </FlagTrackerViewModel>
  <IgnoreProcessNotRunningErrors>true</IgnoreProcessNotRunningErrors>
</MainViewModel>";
    }
}
