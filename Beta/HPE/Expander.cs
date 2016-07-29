using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace HPE
{
    public static class PokemonExpander
    {
        /* List of things to Repoint:
         * 
         * xBaseStatsData -> Copy 0 for all new.
         * xPokemonNames -> Copy first 412. Write BADEGG/UNOWN. Write TEMP for each new.
         * xEvolutionData -> Insert blank for all new.
         * xMovesetData -> Insert blank (x10?) for all new.
         * xTMCompatibilityData -> Insert blank for all new.
         * xPokedexData -> Copy 0 for all new.
         * xNationalDexOrder -> Unown and CelebiTreecko = 0, all others copy/new.
         * xFootprintData -> Insert blank for all new (32 bytes each)
         * xMoveTutorCompatibilityData -> Insert blank. 2 bytes each
         * xFrontSpriteData -> Point 0
         * xBackSpriteData -> Point 0
         * xRegularPaletteData -> Point 0
         * xShinyPaletteData -> Point 0
         * xIconSpriteData -> Point blank
         * xIconData -> 0
         * xEnemyYTable -> 0 TODO: Look up how.
         * xPlayerYTable -> 0 ^^
         * xEnemyAltitudeTable -> 0 ^^
         * CryData -> 1 entry
         * HoennCryData -> +1
         */

        private static byte[] jpanFR1 = { 0x21, 0x68, 0xFF, 0x23, 0x1B, 0x01, 0x5B, 0x18, 0x98, 0x88, 0x00, 0x28, 0x09, 0xD0, 0x04, 0x28, 0x0A, 0xD0, 0x0D, 0x28, 0x0C, 0xD0, 0x0D, 0x2D, 0x1C, 0xDD, 0x01, 0x20, 0x08, 0xBC, 0x98, 0x46, 0xF0, 0xBD, 0xCC, 0x21, 0x08, 0x4A, 0x06, 0xE0, 0x96, 0x21, 0x89, 0x00, 0x07, 0x4A, 0x02, 0xE0, 0xBA, 0x21, 0x09, 0x01, 0x06, 0x4A, 0x04, 0x3B, 0x18, 0x68, 0x10, 0x60, 0x04, 0x3A, 0x04, 0x39, 0x00, 0x29, 0xF8, 0xD1, 0xE7, 0xE7, 0x00, 0x00, 0xC8, 0xC0, 0x03, 0x02, 0x20, 0xC3, 0x03, 0x02, 0xC0, 0xCE, 0x03, 0x02, 0x00, 0x48, 0x00, 0x47, 0x71, 0x9E, 0x0D, 0x08, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0x27, 0x3F, 0x01, 0xCF, 0x19, 0xF8, 0x80, 0xBE, 0x88, 0x00, 0x2E, 0x08, 0xD0, 0x04, 0x2E, 0x09, 0xD0, 0x0D, 0x2E, 0x0B, 0xD0, 0x00, 0x00, 0x00, 0x48, 0x00, 0x47, 0x23, 0x99, 0x0D, 0x08, 0xCC, 0x23, 0x08, 0x4A, 0x06, 0xE0, 0x96, 0x23, 0x9B, 0x00, 0x07, 0x4A, 0x02, 0xE0, 0xBA, 0x23, 0x1B, 0x01, 0x06, 0x4A, 0x04, 0x3F, 0x10, 0x68, 0x38, 0x60, 0x04, 0x3A, 0x04, 0x3B, 0x00, 0x2B, 0xF8, 0xD1, 0xE9, 0xE7, 0xC8, 0xC0, 0x03, 0x02, 0x20, 0xC3, 0x03, 0x02, 0xC0, 0xCE, 0x03, 0x02, 0x00, 0x00, 0x24, 0x0F, 0x00, 0x00, 0xF0, 0x0F, 0xF0, 0x0F, 0xF0, 0x0F, 0xE0, 0x1F, 0xF0, 0x0F, 0xD0, 0x2F, 0x98, 0x0D, 0x00, 0x00, 0xF0, 0x0F, 0xF0, 0x0F, 0xF0, 0x0F, 0xE0, 0x1F, 0xF0, 0x0F, 0xD0, 0x2F, 0xF0, 0x0F, 0xC0, 0x3F, 0xF0, 0x0F, 0xB0, 0x4F, 0xF0, 0x0F, 0xA0, 0x5F, 0xF0, 0x0F, 0x90, 0x6F, 0xF0, 0x0F, 0x80, 0x7F, 0x50, 0x04 };
        private static byte[] saveBlockTableFR = { 0x00, 0x00, 0x24, 0x0F, 0x00, 0x00, 0xF0, 0x0F, 0xF0, 0x0F, 0xF0, 0x0F, 0xE0, 0x1F, 0xF0, 0x0F, 0xD0, 0x2F, 0x98, 0x0D, 0x00, 0x00, 0xF0, 0x0F, 0xF0, 0x0F, 0xF0, 0x0F, 0xE0, 0x1F, 0xF0, 0x0F, 0xD0, 0x2F, 0xF0, 0x0F, 0xC0, 0x3F, 0xF0, 0x0F, 0xB0, 0x4F, 0xF0, 0x0F, 0xA0, 0x5F, 0xF0, 0x0F, 0x90, 0x6F, 0xF0, 0x0F, 0x80, 0x7F, 0x50, 0x04 };

        // TODO: Adjust pointers in code (there are two!)
        private static byte[] jpanLG1 = { 0x21, 0x68, 0xFF, 0x23, 0x1B, 0x01, 0x5B, 0x18, 0x98, 0x88, 0x00, 0x28, 0x09, 0xD0, 0x04, 0x28, 0x0A, 0xD0, 0x0D, 0x28, 0x0C, 0xD0, 0x0D, 0x2D, 0x1C, 0xDD, 0x01, 0x20, 0x08, 0xBC, 0x98, 0x46, 0xF0, 0xBD, 0xCC, 0x21, 0x08, 0x4A, 0x06, 0xE0, 0x96, 0x21, 0x89, 0x00, 0x07, 0x4A, 0x02, 0xE0, 0xBA, 0x21, 0x09, 0x01, 0x06, 0x4A, 0x04, 0x3B, 0x18, 0x68, 0x10, 0x60, 0x04, 0x3A, 0x04, 0x39, 0x00, 0x29, 0xF8, 0xD1, 0xE7, 0xE7, 0x00, 0x00, 0xC8, 0xC0, 0x03, 0x02, 0x20, 0xC3, 0x03, 0x02, 0xC0, 0xCE, 0x03, 0x02, 0x00, 0x48, 0x00, 0x47, 0x71, 0x9E, 0x0D, 0x08, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0x27, 0x3F, 0x01, 0xCF, 0x19, 0xF8, 0x80, 0xBE, 0x88, 0x00, 0x2E, 0x08, 0xD0, 0x04, 0x2E, 0x09, 0xD0, 0x0D, 0x2E, 0x0B, 0xD0, 0x00, 0x00, 0x00, 0x48, 0x00, 0x47, 0x23, 0x99, 0x0D, 0x08, 0xCC, 0x23, 0x08, 0x4A, 0x06, 0xE0, 0x96, 0x23, 0x9B, 0x00, 0x07, 0x4A, 0x02, 0xE0, 0xBA, 0x23, 0x1B, 0x01, 0x06, 0x4A, 0x04, 0x3F, 0x10, 0x68, 0x38, 0x60, 0x04, 0x3A, 0x04, 0x3B, 0x00, 0x2B, 0xF8, 0xD1, 0xE9, 0xE7, 0xC8, 0xC0, 0x03, 0x02, 0x20, 0xC3, 0x03, 0x02, 0xC0, 0xCE, 0x03, 0x02, 0x00, 0x00, 0x24, 0x0F, 0x00, 0x00, 0xF0, 0x0F, 0xF0, 0x0F, 0xF0, 0x0F, 0xE0, 0x1F, 0xF0, 0x0F, 0xD0, 0x2F, 0x98, 0x0D, 0x00, 0x00, 0xF0, 0x0F, 0xF0, 0x0F, 0xF0, 0x0F, 0xE0, 0x1F, 0xF0, 0x0F, 0xD0, 0x2F, 0xF0, 0x0F, 0xC0, 0x3F, 0xF0, 0x0F, 0xB0, 0x4F, 0xF0, 0x0F, 0xA0, 0x5F, 0xF0, 0x0F, 0x90, 0x6F, 0xF0, 0x0F, 0x80, 0x7F, 0x50, 0x04 };
        private static byte[] saveBlockTableLG = { 0x00, 0x00, 0x24, 0x0F, 0x00, 0x00, 0xF0, 0x0F, 0xF0, 0x0F, 0xF0, 0x0F, 0xE0, 0x1F, 0xF0, 0x0F, 0xD0, 0x2F, 0x98, 0x0D, 0x00, 0x00, 0xF0, 0x0F, 0xF0, 0x0F, 0xF0, 0x0F, 0xE0, 0x1F, 0xF0, 0x0F, 0xD0, 0x2F, 0xF0, 0x0F, 0xC0, 0x3F, 0xF0, 0x0F, 0xB0, 0x4F, 0xF0, 0x0F, 0xA0, 0x5F, 0xF0, 0x0F, 0x90, 0x6F, 0xF0, 0x0F, 0x80, 0x7F, 0x50, 0x04 };

        // Let's do this.
        public static void ExpandFR(ROM rom, Ini ini, int newPokemonCount)
        {
            //FreeSpaceFinderDialog fsf = new FreeSpaceFinderDialog(rom, (uint)newPokemonCount, false);
            //if (fsf.ShowDialog() != DialogResult.OK) throw new Exception();

            // We'll adjust the ini as we go.
            int originalPkmnCount = Convert.ToInt32(ini[rom.Code, "NumberOfPokemon"]);
            int addedPkmnCount = newPokemonCount - originalPkmnCount;
            //MessageBox.Show("I will add " + addedPkmnCount);
            ini[rom.Code, "NumberOfPokemon"] = newPokemonCount.ToString();

            #region JPAN's Save Hack
            {
                FreeSpaceFinderDialog fsf = new FreeSpaceFinderDialog(rom, (uint)jpanFR1.Length, false);
                fsf.Text = "Repoint Save Block";
                if (fsf.ShowDialog() != DialogResult.OK) throw new Exception();
                uint jpanOffset = fsf.FreeSpaceOffset;

                using (GBABinaryWriter bw = new GBABinaryWriter(rom))
                {
                    // Write new save block table (by JPAN)
                    bw.BaseStream.Seek(0x3FEC94, SeekOrigin.Begin);
                    bw.Write(saveBlockTableFR);

                    // Write save block hack
                    bw.BaseStream.Seek(jpanOffset, SeekOrigin.Begin);
                    bw.Write(jpanFR1);

                    // Pointer to part 1
                    bw.BaseStream.Seek(0xD995C, SeekOrigin.Begin);
                    bw.WritePointer(jpanOffset + 0x61);

                    // Pointer to part 2
                    bw.BaseStream.Seek(0xD9EDC, SeekOrigin.Begin);
                    bw.Write(new byte[] { 0x0, 0x48, 0x0, 0x47 });
                    bw.WritePointer(jpanOffset + 0x1);

                    // Disable LR help menu
                    bw.BaseStream.Seek(0x13B8C2, SeekOrigin.Begin);
                    bw.Write((ushort)0xE01D);
                }
            }
            #endregion

            #region Repoint Everything
            // Names
            #region Names
            {
                uint tableStart = Convert.ToUInt32(ini[rom.Code, "PokemonNames"], 16);

                byte[] temp = new byte[] { 0xCE, 0xBF, 0xC7, 0xCA, 0xFF, 0, 0, 0, 0, 0, 0 };
                byte[] q = new byte[] { 0xAC, 0xFF, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

                byte[] oldEntries;
                using (GBABinaryReader br = new GBABinaryReader(rom))
                {
                    //br.BaseStream.Seek(tableStart, SeekOrigin.Begin);
                    //entry0 = br.ReadBytes(28);

                    br.BaseStream.Seek(tableStart, SeekOrigin.Begin);
                    oldEntries = br.ReadBytes(11 * originalPkmnCount);
                }

                FreeSpaceFinderDialog fsf = new FreeSpaceFinderDialog(rom, 11u * (uint)newPokemonCount, false);
                fsf.Text = "Repoint Names";
                if (fsf.ShowDialog() != DialogResult.OK) throw new Exception();
                uint newTable = fsf.FreeSpaceOffset;

                using (GBABinaryWriter bw = new GBABinaryWriter(rom))
                {
                    bw.BaseStream.Seek(newTable, SeekOrigin.Begin);
                    bw.Write(oldEntries);
                    if (originalPkmnCount == 412)
                    {
                        for (int i = 0; i < addedPkmnCount; i++)
                        {
                            if (i < 28) bw.Write(q);
                            else bw.Write(temp);
                        }
                    }
                    else
                        for (int i = 0; i < addedPkmnCount; i++) bw.Write(temp);
                }

                Tasks.ReplaceAllPointers(rom, tableStart, newTable);
                ini[rom.Code, "PokemonNames"] = "0x" + newTable.ToString("X");
            }
            #endregion

            // Pokedex Order
            #region Pokedex Order
            int neededDexEntries = 0;
            {
                uint tableStart = Convert.ToUInt32(ini[rom.Code, "NationalDexOrder"], 16);

                ushort[] oldOrder = new ushort[originalPkmnCount];
                ushort[] newOrder = new ushort[newPokemonCount];

                using (GBABinaryReader br = new GBABinaryReader(rom))
                {
                    // Read Old Pokedex Order
                    br.BaseStream.Seek(tableStart, SeekOrigin.Begin);
                    for (int i = 0; i < originalPkmnCount; i++)
                    {
                        oldOrder[i] = br.ReadUInt16();
                    }

                    // Do the ?????? ID's
                    for (int i = 0; i < 25; i++)
                    {
                        oldOrder[i + 251] = 0; // :D
                    }
                }

                // Start New Order Making
                oldOrder.CopyTo(newOrder, 0);
                for (int i = 412; i < 439; i++)
                {
                    newOrder[i] = 0;
                }

                ushort startID = (ushort)(originalPkmnCount - 25 - (originalPkmnCount > 412 ? 28 : 0));
                //MessageBox.Show("Start ID: " + startID);
                for (int i = 439; i < newPokemonCount; i++)
                {
                    newOrder[i] = startID;
                    startID++;
                }
                neededDexEntries = startID + 1;

                FreeSpaceFinderDialog fsf = new FreeSpaceFinderDialog(rom, (uint)(newPokemonCount * 2), false);
                fsf.Text = "Repoint Dex Order";
                if (fsf.ShowDialog() != DialogResult.OK) throw new Exception();
                uint newTable = fsf.FreeSpaceOffset;

                using (GBABinaryWriter bw = new GBABinaryWriter(rom))
                {
                    bw.BaseStream.Seek(newTable, SeekOrigin.Begin);
                    for (int i = 0; i < newPokemonCount; i++)
                    {
                        bw.Write(newOrder[i]);
                    }

                    //bw.Write(buffer);
                }

                Tasks.ReplaceAllPointers(rom, tableStart, newTable);
                ini[rom.Code, "NationalDexOrder"] = "0x" + newTable.ToString("X");
            }
            #endregion

            // Base Stats
            #region Base Stats
            {
                uint tableStart = Convert.ToUInt32(ini[rom.Code, "BaseStatsData"], 16);

                byte[] entry0; byte[] oldEntries;
                using (GBABinaryReader br = new GBABinaryReader(rom))
                {
                    br.BaseStream.Seek(tableStart, SeekOrigin.Begin);
                    entry0 = br.ReadBytes(28);

                    br.BaseStream.Seek(tableStart, SeekOrigin.Begin);
                    oldEntries = br.ReadBytes(28 * originalPkmnCount);
                }

                FreeSpaceFinderDialog fsf = new FreeSpaceFinderDialog(rom, 28u * (uint)newPokemonCount, false);
                fsf.Text = "Repoint Base Stats.";
                if (fsf.ShowDialog() != DialogResult.OK) throw new Exception();
                uint newTable = fsf.FreeSpaceOffset;

                using (GBABinaryWriter bw = new GBABinaryWriter(rom))
                {
                    bw.BaseStream.Seek(newTable, SeekOrigin.Begin);
                    bw.Write(oldEntries);
                    for (int i = 0; i < addedPkmnCount; i++) bw.Write(entry0);
                }

                // Repoint
                Tasks.ReplaceAllPointers(rom, tableStart, newTable);
                ini[rom.Code, "BaseStatsData"] = "0x" + newTable.ToString("X"); // Adjust ini

                // Egg hatching pointers
                Tasks.ReplaceAllPointers(rom, tableStart + 18, newTable + 18); //:(
            }
            #endregion

            // Evolution
            #region Evolutions
            {
                uint tableStart = Convert.ToUInt32(ini[rom.Code, "EvolutionData"], 16);
                int count = Convert.ToInt32(ini[rom.Code, "NumberOfEvolutions"]);

                //byte[] blankEntry = new byte[Evolution.SIZE * count];
                //for (int i = 0; i < blankEntry.Length; i++) blankEntry[i] = 0;

                byte[] entry0; byte[] oldEntries;
                using (GBABinaryReader br = new GBABinaryReader(rom))
                {
                    br.BaseStream.Seek(tableStart, SeekOrigin.Begin);
                    entry0 = br.ReadBytes(Evolution.SIZE * count);

                    br.BaseStream.Seek(tableStart, SeekOrigin.Begin);
                    oldEntries = br.ReadBytes(Evolution.SIZE * count * originalPkmnCount);
                }

                FreeSpaceFinderDialog fsf = new FreeSpaceFinderDialog(rom, (uint)(Evolution.SIZE * count * newPokemonCount), false);
                fsf.Text = "Repoint Evolutions";
                if (fsf.ShowDialog() != DialogResult.OK) throw new Exception();
                uint newTable = fsf.FreeSpaceOffset;

                using (GBABinaryWriter bw = new GBABinaryWriter(rom))
                {
                    bw.BaseStream.Seek(newTable, SeekOrigin.Begin);
                    bw.Write(oldEntries);
                    for (int i = 0; i < addedPkmnCount; i++) bw.Write(entry0);
                }

                Tasks.ReplaceAllPointers(rom, tableStart, newTable);
                ini[rom.Code, "EvolutionData"] = "0x" + newTable.ToString("X");
            }
            #endregion

            // Movesets
            #region Movesets
            {
                uint tableStart = Convert.ToUInt32(ini[rom.Code, "MovesetData"], 16);

                byte[] blankMoveset = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 255, 255 };

                byte[] oldTable;
                using (GBABinaryReader br = new GBABinaryReader(rom))
                {
                    br.BaseStream.Seek(tableStart, SeekOrigin.Begin);
                    oldTable = br.ReadBytes(4 * originalPkmnCount);
                }

                FreeSpaceFinderDialog fsf = new FreeSpaceFinderDialog(rom, (uint)(4 * newPokemonCount + 18 * addedPkmnCount + 18), false);
                fsf.Text = "Repoint Movesets";
                if (fsf.ShowDialog() != DialogResult.OK) throw new Exception();
                uint newTable = fsf.FreeSpaceOffset;
                uint movesStart = (uint)(newTable + 4 * newPokemonCount);

                using (GBABinaryWriter bw = new GBABinaryWriter(rom))
                {
                    // fill new table
                    bw.BaseStream.Seek(newTable, SeekOrigin.Begin);
                    bw.Write(oldTable);
                    for (int i = 0; i <= addedPkmnCount; i++) bw.WritePointer((uint)(movesStart + i * 18));

                    // write blank movesets
                    bw.BaseStream.Seek(movesStart, SeekOrigin.Begin);
                    for (int i = 0; i <= addedPkmnCount; i++) bw.Write(blankMoveset); // 8 moves.
                }

                Tasks.ReplaceAllPointers(rom, tableStart, newTable);
                ini[rom.Code, "MovesetData"] = "0x" + newTable.ToString("X");
            }
            #endregion

            // Pokedex
            #region Pokedex
            {
                uint tableStart = Convert.ToUInt32(ini[rom.Code, "PokedexData"], 16);
                //int count = Convert.ToInt32(ini[rom.Code, "NumberOfEvolutions"]);
                int originalDexEntries = (originalPkmnCount - 25 - (originalPkmnCount > 412 ? 28 : 0));
                //MessageBox.Show("Original Dex #: " + originalDexEntries);

                byte[] entry0; byte[] oldEntries;
                using (GBABinaryReader br = new GBABinaryReader(rom))
                {
                    br.BaseStream.Seek(tableStart, SeekOrigin.Begin);
                    entry0 = br.ReadBytes(36);

                    br.BaseStream.Seek(tableStart, SeekOrigin.Begin);
                    oldEntries = br.ReadBytes(36 * originalDexEntries);
                }

                FreeSpaceFinderDialog fsf = new FreeSpaceFinderDialog(rom, (uint)(neededDexEntries * 36), false);
                fsf.Text = "Repoint Pokédex";
                if (fsf.ShowDialog() != DialogResult.OK) throw new Exception();
                uint newTable = fsf.FreeSpaceOffset;

                using (GBABinaryWriter bw = new GBABinaryWriter(rom))
                {
                    bw.BaseStream.Seek(newTable, SeekOrigin.Begin);
                    bw.Write(oldEntries);
                    for (int i = 0; i < neededDexEntries - originalDexEntries; i++) bw.Write(entry0);
                }

                Tasks.ReplaceAllPointers(rom, tableStart, newTable);
                ini[rom.Code, "PokedexData"] = "0x" + newTable.ToString("X");
            }
            #endregion

            // Footprints
            #region Footprints
            {
                uint tableStart = Convert.ToUInt32(ini[rom.Code, "FootprintData"], 16);

                byte[] blankPrint = new byte[32];
                for (int i = 0; i < 32; i++) blankPrint[i] = 0;

                byte[] oldTable;
                using (GBABinaryReader br = new GBABinaryReader(rom))
                {
                    br.BaseStream.Seek(tableStart, SeekOrigin.Begin);
                    oldTable = br.ReadBytes(4 * originalPkmnCount);
                }

                FreeSpaceFinderDialog fsf = new FreeSpaceFinderDialog(rom, (uint)(4 * newPokemonCount + 32 * addedPkmnCount), false);
                fsf.Text = "Repoint Footprints";
                if (fsf.ShowDialog() != DialogResult.OK) throw new Exception();
                uint newTable = fsf.FreeSpaceOffset;
                uint printsStart = (uint)(newTable + 4 * newPokemonCount);

                using (GBABinaryWriter bw = new GBABinaryWriter(rom))
                {
                    // fill new table
                    bw.BaseStream.Seek(newTable, SeekOrigin.Begin);
                    bw.Write(oldTable);
                    for (int i = 0; i < addedPkmnCount; i++) bw.WritePointer((uint)(printsStart + i * 32));

                    // write blank movesets
                    bw.BaseStream.Seek(printsStart, SeekOrigin.Begin);
                    for (int i = 0; i < addedPkmnCount; i++) bw.Write(blankPrint); // 32 bytes
                }

                Tasks.ReplaceAllPointers(rom, tableStart, newTable);
                ini[rom.Code, "FootprintData"] = "0x" + newTable.ToString("X");
            }
            #endregion

            // TM/HM
            #region TM/HM
            {
                uint tableStart = Convert.ToUInt32(ini[rom.Code, "TMCompatibilityData"], 16);

                byte[] blank = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };
                byte[] oldEntries;
                using (GBABinaryReader br = new GBABinaryReader(rom))
                {
                    br.BaseStream.Seek(tableStart, SeekOrigin.Begin);
                    oldEntries = br.ReadBytes(8 * originalPkmnCount);
                }

                FreeSpaceFinderDialog fsf = new FreeSpaceFinderDialog(rom, 8 * (uint)newPokemonCount, false);
                fsf.Text = "Repoint TM/HM Compat.";
                if (fsf.ShowDialog() != DialogResult.OK) throw new Exception();
                uint newTable = fsf.FreeSpaceOffset;

                using (GBABinaryWriter bw = new GBABinaryWriter(rom))
                {
                    bw.BaseStream.Seek(newTable, SeekOrigin.Begin);
                    bw.Write(oldEntries);
                    for (int i = 0; i < addedPkmnCount; i++) bw.Write(blank);
                }

                Tasks.ReplaceAllPointers(rom, tableStart, newTable);
                ini[rom.Code, "TMCompatibilityData"] = "0x" + newTable.ToString("X");
            }
            #endregion

            // Move Tutor
            #region Move Tutor
            {
                uint tableStart = Convert.ToUInt32(ini[rom.Code, "MoveTutorCompatibilityData"], 16);

                byte[] blank = new byte[] { 0, 0 };
                byte[] oldEntries;
                using (GBABinaryReader br = new GBABinaryReader(rom))
                {
                    br.BaseStream.Seek(tableStart, SeekOrigin.Begin);
                    oldEntries = br.ReadBytes(2 * originalPkmnCount);
                }

                FreeSpaceFinderDialog fsf = new FreeSpaceFinderDialog(rom, 2 * (uint)newPokemonCount, false);
                fsf.Text = "Repoint M. Tutor Compat.";
                if (fsf.ShowDialog() != DialogResult.OK) throw new Exception();
                uint newTable = fsf.FreeSpaceOffset;

                using (GBABinaryWriter bw = new GBABinaryWriter(rom))
                {
                    bw.BaseStream.Seek(newTable, SeekOrigin.Begin);
                    bw.Write(oldEntries);
                    for (int i = 0; i < addedPkmnCount; i++) bw.Write((ushort)0);
                }

                Tasks.ReplaceAllPointers(rom, tableStart, newTable);
                ini[rom.Code, "MoveTutorCompatibilityData"] = "0x" + newTable.ToString("X");
            }
            #endregion

            // Icon Sprites
            #region Icon 1
            {
                uint tableStart = Convert.ToUInt32(ini[rom.Code, "IconSpriteData"], 16);

                byte[] blankPrint = new byte[1024];
                for (int i = 0; i < 1024; i++) blankPrint[i] = 0;

                byte[] oldTable;
                using (GBABinaryReader br = new GBABinaryReader(rom))
                {
                    br.BaseStream.Seek(tableStart, SeekOrigin.Begin);
                    if (originalPkmnCount == 412)
                        oldTable = br.ReadBytes(4 * (originalPkmnCount + 28));
                    else
                        oldTable = br.ReadBytes(4 * originalPkmnCount);
                }

                FreeSpaceFinderDialog fsf = new FreeSpaceFinderDialog(rom, (uint)(4 * newPokemonCount + 1024 * addedPkmnCount), false);
                fsf.Text = "Repoint Icon Sprites";
                if (fsf.ShowDialog() != DialogResult.OK) throw new Exception();
                uint newTable = fsf.FreeSpaceOffset;
                uint printsStart = (uint)(newTable + 4 * newPokemonCount);

                using (GBABinaryWriter bw = new GBABinaryWriter(rom))
                {
                    // fill new table
                    bw.BaseStream.Seek(newTable, SeekOrigin.Begin);
                    bw.Write(oldTable);
                    if (originalPkmnCount == 412)
                        for (int i = 0; i < addedPkmnCount - 28; i++) bw.WritePointer((uint)(printsStart + i * 1024));
                    else
                        for (int i = 0; i < addedPkmnCount; i++) bw.WritePointer((uint)(printsStart + i * 1024));

                    // write blank movesets
                    bw.BaseStream.Seek(printsStart, SeekOrigin.Begin);
                    if (originalPkmnCount == 412)
                        for (int i = 0; i < addedPkmnCount - 28; i++) bw.Write(blankPrint); // 32 bytes
                    else
                        for (int i = 0; i < addedPkmnCount; i++) bw.Write(blankPrint); // 32 bytes
                }

                Tasks.ReplaceAllPointers(rom, tableStart, newTable);
                ini[rom.Code, "IconSpriteData"] = "0x" + newTable.ToString("X");
            }
            #endregion

            // Icon 2 (palette #'s)
            #region Icon Pal.
            {
                uint tableStart = Convert.ToUInt32(ini[rom.Code, "IconData"], 16);

                //byte[] blank = new byte[] { 0, 0 };
                byte[] oldEntries;
                using (GBABinaryReader br = new GBABinaryReader(rom))
                {
                    br.BaseStream.Seek(tableStart, SeekOrigin.Begin);
                    oldEntries = br.ReadBytes(originalPkmnCount);
                }

                FreeSpaceFinderDialog fsf = new FreeSpaceFinderDialog(rom, (uint)newPokemonCount, false);
                fsf.Text = "Repoint Icon Pal. #'s";
                if (fsf.ShowDialog() != DialogResult.OK) throw new Exception();
                uint newTable = fsf.FreeSpaceOffset;

                using (GBABinaryWriter bw = new GBABinaryWriter(rom))
                {
                    bw.BaseStream.Seek(newTable, SeekOrigin.Begin);
                    bw.Write(oldEntries);
                    for (int i = 0; i < addedPkmnCount; i++) bw.Write((byte)0);
                }

                Tasks.ReplaceAllPointers(rom, tableStart, newTable);
                ini[rom.Code, "IconData"] = "0x" + newTable.ToString("X");
            }
            #endregion

            // Front Sprites
            #region Front Sprites
            {
                uint tableStart = Convert.ToUInt32(ini[rom.Code, "FrontSpriteData"], 16);

                //byte[] blank = new byte[] { 0, 0 };
                byte[] oldEntries; byte[] entry0;
                using (GBABinaryReader br = new GBABinaryReader(rom))
                {
                    br.BaseStream.Seek(tableStart, SeekOrigin.Begin);
                    entry0 = br.ReadBytes(8);

                    br.BaseStream.Seek(tableStart, SeekOrigin.Begin);
                    oldEntries = br.ReadBytes(8 * originalPkmnCount);
                }

                FreeSpaceFinderDialog fsf = new FreeSpaceFinderDialog(rom, (uint)(8 * newPokemonCount), false);
                fsf.Text = "Repoint Front Sprites";
                if (fsf.ShowDialog() != DialogResult.OK) throw new Exception();
                uint newTable = fsf.FreeSpaceOffset;

                using (GBABinaryWriter bw = new GBABinaryWriter(rom))
                {
                    // write data
                    bw.BaseStream.Seek(newTable, SeekOrigin.Begin);
                    bw.Write(oldEntries);
                    for (int i = 0; i < addedPkmnCount; i++) bw.Write(entry0);

                    // fix oak intro
                    bw.BaseStream.Seek(0x130FA0, SeekOrigin.Begin);
                    bw.WritePointer(newTable + 232);
                }

                // Repoint
                Tasks.ReplaceAllPointers(rom, tableStart, newTable);
                ini[rom.Code, "FrontSpriteData"] = "0x" + newTable.ToString("X");
            }
            #endregion

            // Back Sprites
            #region Back Sprites
            {
                uint tableStart = Convert.ToUInt32(ini[rom.Code, "BackSpriteData"], 16);

                byte[] oldEntries; byte[] entry0;
                using (GBABinaryReader br = new GBABinaryReader(rom))
                {
                    br.BaseStream.Seek(tableStart, SeekOrigin.Begin);
                    entry0 = br.ReadBytes(8);

                    br.BaseStream.Seek(tableStart, SeekOrigin.Begin);
                    oldEntries = br.ReadBytes(8 * originalPkmnCount);
                }

                FreeSpaceFinderDialog fsf = new FreeSpaceFinderDialog(rom, (uint)(8 * newPokemonCount), false);
                fsf.Text = "Repoint Back Sprites";
                if (fsf.ShowDialog() != DialogResult.OK) throw new Exception();
                uint newTable = fsf.FreeSpaceOffset;

                using (GBABinaryWriter bw = new GBABinaryWriter(rom))
                {
                    bw.BaseStream.Seek(newTable, SeekOrigin.Begin);
                    bw.Write(oldEntries);
                    for (int i = 0; i < addedPkmnCount; i++) bw.Write(entry0);
                }

                Tasks.ReplaceAllPointers(rom, tableStart, newTable);
                ini[rom.Code, "BackSpriteData"] = "0x" + newTable.ToString("X");
            }
            #endregion

            // Reg. Palettes
            #region Regular Palettes
            {
                uint tableStart = Convert.ToUInt32(ini[rom.Code, "RegularPaletteData"], 16);

                byte[] oldEntries; byte[] entry0;
                using (GBABinaryReader br = new GBABinaryReader(rom))
                {
                    br.BaseStream.Seek(tableStart, SeekOrigin.Begin);
                    entry0 = br.ReadBytes(8);

                    br.BaseStream.Seek(tableStart, SeekOrigin.Begin);
                    oldEntries = br.ReadBytes(8 * originalPkmnCount);
                }

                FreeSpaceFinderDialog fsf = new FreeSpaceFinderDialog(rom, (uint)(8 * newPokemonCount), false);
                fsf.Text = "Repoint Regular Pals.";
                if (fsf.ShowDialog() != DialogResult.OK) throw new Exception();
                uint newTable = fsf.FreeSpaceOffset;

                using (GBABinaryWriter bw = new GBABinaryWriter(rom))
                {
                    bw.BaseStream.Seek(newTable, SeekOrigin.Begin);
                    bw.Write(oldEntries);
                    for (int i = 0; i < addedPkmnCount; i++) bw.Write(entry0);

                    // fix oak
                    bw.BaseStream.Seek(0x130FA4, SeekOrigin.Begin);
                    bw.WritePointer(newTable + 232);
                }

                Tasks.ReplaceAllPointers(rom, tableStart, newTable);
                ini[rom.Code, "RegularPaletteData"] = "0x" + newTable.ToString("X");
            }
            #endregion

            // Shiny Palettes
            #region Shiny Palettes
            {
                uint tableStart = Convert.ToUInt32(ini[rom.Code, "ShinyPaletteData"], 16);

                byte[] oldEntries; byte[] entry0;
                using (GBABinaryReader br = new GBABinaryReader(rom))
                {
                    br.BaseStream.Seek(tableStart, SeekOrigin.Begin);
                    entry0 = br.ReadBytes(8);

                    br.BaseStream.Seek(tableStart, SeekOrigin.Begin);
                    oldEntries = br.ReadBytes(8 * originalPkmnCount);
                }

                FreeSpaceFinderDialog fsf = new FreeSpaceFinderDialog(rom, (uint)(8 * newPokemonCount), false);
                fsf.Text = "Repoint Shiny Pals.";
                if (fsf.ShowDialog() != DialogResult.OK) throw new Exception();
                uint newTable = fsf.FreeSpaceOffset;

                using (GBABinaryWriter bw = new GBABinaryWriter(rom))
                {
                    bw.BaseStream.Seek(newTable, SeekOrigin.Begin);
                    bw.Write(oldEntries);
                    for (int i = 0; i < addedPkmnCount; i++) bw.Write(entry0);
                }

                Tasks.ReplaceAllPointers(rom, tableStart, newTable);
                ini[rom.Code, "ShinyPaletteData"] = "0x" + newTable.ToString("X");
            }
            #endregion

            // Enemy Y
            #region Enemy Y
            {
                uint tableStart = Convert.ToUInt32(ini[rom.Code, "EnemyYTable"], 16);

                byte[] blank = new byte[] { 0, 0, 0, 0 };
                byte[] oldEntries;
                using (GBABinaryReader br = new GBABinaryReader(rom))
                {
                    br.BaseStream.Seek(tableStart, SeekOrigin.Begin);
                    oldEntries = br.ReadBytes(4 * originalPkmnCount);
                }

                FreeSpaceFinderDialog fsf = new FreeSpaceFinderDialog(rom, (uint)(4 * newPokemonCount), false);
                fsf.Text = "Repoint Enemy Y Data";
                if (fsf.ShowDialog() != DialogResult.OK) throw new Exception();
                uint newTable = fsf.FreeSpaceOffset;

                using (GBABinaryWriter bw = new GBABinaryWriter(rom))
                {
                    bw.BaseStream.Seek(newTable, SeekOrigin.Begin);
                    bw.Write(oldEntries);
                    for (int i = 0; i < addedPkmnCount; i++) bw.Write(blank);
                }

                Tasks.ReplaceAllPointers(rom, tableStart, newTable);
                ini[rom.Code, "EnemyYTable"] = "0x" + newTable.ToString("X");
            }
            #endregion

            // Player Y
            #region Player Y
            {
                uint tableStart = Convert.ToUInt32(ini[rom.Code, "PlayerYTable"], 16);

                byte[] blank = new byte[] { 0, 0, 0, 0 };
                byte[] oldEntries;
                using (GBABinaryReader br = new GBABinaryReader(rom))
                {
                    br.BaseStream.Seek(tableStart, SeekOrigin.Begin);
                    oldEntries = br.ReadBytes(4 * originalPkmnCount);
                }

                FreeSpaceFinderDialog fsf = new FreeSpaceFinderDialog(rom, (uint)(4 * newPokemonCount), false);
                fsf.Text = "Repoint Player Y Data";
                if (fsf.ShowDialog() != DialogResult.OK) throw new Exception();
                uint newTable = fsf.FreeSpaceOffset;

                using (GBABinaryWriter bw = new GBABinaryWriter(rom))
                {
                    bw.BaseStream.Seek(newTable, SeekOrigin.Begin);
                    bw.Write(oldEntries);
                    for (int i = 0; i < addedPkmnCount; i++) bw.Write(blank);
                }

                Tasks.ReplaceAllPointers(rom, tableStart, newTable);
                ini[rom.Code, "PlayerYTable"] = "0x" + newTable.ToString("X");
            }
            #endregion

            // Altitude
            #region Altitude
            {
                uint tableStart = Convert.ToUInt32(ini[rom.Code, "EnemyAltitudeTable"], 16);

                //byte[] blank = new byte[] { 0, 0, 0, 0 };
                byte[] oldEntries;
                using (GBABinaryReader br = new GBABinaryReader(rom))
                {
                    br.BaseStream.Seek(tableStart, SeekOrigin.Begin);
                    oldEntries = br.ReadBytes(originalPkmnCount);
                }

                FreeSpaceFinderDialog fsf = new FreeSpaceFinderDialog(rom, (uint)(newPokemonCount), false);
                fsf.Text = "Repoint Altitude Data";
                if (fsf.ShowDialog() != DialogResult.OK) throw new Exception();
                uint newTable = fsf.FreeSpaceOffset;

                using (GBABinaryWriter bw = new GBABinaryWriter(rom))
                {
                    bw.BaseStream.Seek(newTable, SeekOrigin.Begin);
                    bw.Write(oldEntries);
                    for (int i = 0; i < addedPkmnCount; i++) bw.Write((byte)0);
                }

                Tasks.ReplaceAllPointers(rom, tableStart, newTable);
                ini[rom.Code, "EnemyAltitudeTable"] = "0x" + newTable.ToString("X");
            }
            #endregion

            // Cries
            #region Cries
            {
                // TODO
                // G3HS doesn't repoint this
                // Should I?
            }
            #endregion

            // Hoenn Cry Stuff
            #region Hoenn Cry Table
            {
                // Get necessary info
                int hoennCryLength = originalPkmnCount - 251 - 25; // NOTE: Yeah~
                int newHoennCryLength = newPokemonCount - 251 - 25;
                uint tableStart = Convert.ToUInt32(ini[rom.Code, "HoennCryData"], 16);

                // Read old data
                ushort[] oldTable = new ushort[hoennCryLength];
                using (GBABinaryReader br = new GBABinaryReader(rom))
                {
                    br.BaseStream.Seek(tableStart, SeekOrigin.Begin); // :(
                    for (int i = 0; i < hoennCryLength; i++)
                    {
                        oldTable[i] = br.ReadUInt16();
                    }
                }

                // Format new data
                ushort[] newTable = new ushort[newHoennCryLength];
                for(int i = 0; i < newHoennCryLength; i++)
                {
                    newTable[i] = 0;
                }

                oldTable.CopyTo(newTable, 0); // copy old stuff too~

                // Find free space
                FreeSpaceFinderDialog fsf = new FreeSpaceFinderDialog(rom, (uint)newHoennCryLength * 2, false);
                fsf.Text = "Repoint Hoenn Cry Table";
                if (fsf.ShowDialog() != DialogResult.OK) throw new Exception();
                uint newTableStart = fsf.FreeSpaceOffset;

                // Saving
                using (GBABinaryWriter bw = new GBABinaryWriter(rom))
                {
                    // Overwrite old table
                    bw.BaseStream.Seek(tableStart, SeekOrigin.Begin);
                    for (int i = 0; i < hoennCryLength - 1; i++)
                    {
                        bw.Write(ushort.MaxValue); // FF FF
                    }

                    // Write new table
                    bw.BaseStream.Seek(newTableStart, SeekOrigin.Begin);
                    for(int i= 0; i < newHoennCryLength; i++)
                    {
                        bw.Write(newTable[i]);
                    }
                }

                // And finally, repoint & adjust ini
                Tasks.ReplaceAllPointers(rom, tableStart, newTableStart);
                ini[rom.Code, "HoennCryData"] = "0x" + newTableStart.ToString("X");

            }
            #endregion

            #endregion

            #region Limiters
            uint ramOffset = 0x0203C400;
            using (GBABinaryWriter bw = new GBABinaryWriter(rom))
            {
                #region Seen/Caught Flags
                uint tempDexSize = (uint)neededDexEntries;
                while (tempDexSize % 8 != 0) tempDexSize += 1;
                uint neededFlags = tempDexSize / 8;

                #region Seen Flags
                // Reading
                bw.BaseStream.Seek(0x104B10, SeekOrigin.Begin);
                bw.Write(ramOffset);

                bw.BaseStream.Seek(0x104B00, SeekOrigin.Begin);
                bw.Write(new byte[] { 0, 0, 0, 0 });

                // Writing
                bw.BaseStream.Seek(0x104B94, SeekOrigin.Begin);
                bw.Write(ramOffset);

                bw.BaseStream.Seek(0x104B6A, SeekOrigin.Begin);
                bw.Write(new byte[] { 0x1, 0x1C, 0, 0 });

                bw.BaseStream.Seek(0x104B78, SeekOrigin.Begin);
                bw.Write(new byte[] { 0x1A, 0xE0 });
                #endregion

                #region Caught Flags
                // Reading
                bw.BaseStream.Seek(0x104B5C, SeekOrigin.Begin);
                bw.Write(ramOffset + neededFlags);

                bw.BaseStream.Seek(0x104B16, SeekOrigin.Begin);
                bw.Write(new byte[] { 0, 0, 0, 0, 0, 0 });

                bw.BaseStream.Seek(0x104B26, SeekOrigin.Begin);
                bw.Write(new byte[] { 0x1A, 0xE0 });

                // Writing
                bw.BaseStream.Seek(0x104BB8, SeekOrigin.Begin);
                bw.Write(ramOffset + neededFlags);

                bw.BaseStream.Seek(0x104BA2, SeekOrigin.Begin);
                bw.Write(new byte[] { 0x1, 0x1C, 0, 0 });

                #endregion

                bw.BaseStream.Seek(0x104B34, SeekOrigin.Begin);
                bw.Write(new byte[] { 0xF, 0xE0 });

                // Clear seen/caught flags on start of game
                bw.BaseStream.Seek(0x549D0, SeekOrigin.Begin);
                bw.Write(ramOffset);

                bw.BaseStream.Seek(0x549B0, SeekOrigin.Begin);
                bw.Write(new byte[] { 0x20, 0x1C, 0, 0 });

                bw.BaseStream.Seek(0x549B6, SeekOrigin.Begin);
                bw.Write(new byte[] { (byte)neededFlags, 0x22 });

                bw.BaseStream.Seek(0x549BC, SeekOrigin.Begin);
                bw.Write(new byte[] { 0x20, 0x1C });

                bw.BaseStream.Seek(0x549BE, SeekOrigin.Begin);
                bw.Write(new byte[] { (byte)neededFlags, 0x30 });

                bw.BaseStream.Seek(0x549C2, SeekOrigin.Begin);
                bw.Write(new byte[] { (byte)neededFlags, 0x22 });
                #endregion

                // Front sprite crap
                bw.BaseStream.Seek(0xED72, SeekOrigin.Begin);
                bw.Write(new byte[] { 0x7, 0xE0 });

                bw.BaseStream.Seek(0xF1B6, SeekOrigin.Begin);
                bw.Write(new byte[] { 0x7, 0xE0 });

                // Palette crap
                bw.BaseStream.Seek(0x44104, SeekOrigin.Begin);
                bw.Write(new byte[] { 0x4, 0xE0 });

                // Altitude
                bw.BaseStream.Seek(0x7472E, SeekOrigin.Begin);
                bw.Write(new byte[] { 0x3, 0xE0 });

                bw.BaseStream.Seek(0x7475E, SeekOrigin.Begin);
                bw.Write(new byte[] { 0x3, 0xE0 });

                bw.BaseStream.Seek(0x74788, SeekOrigin.Begin); // ---
                bw.Write(new byte[] { 0x6, 0xE0 });

                // Icon stuffs
                bw.BaseStream.Seek(0x96F90, SeekOrigin.Begin);
                bw.Write(new byte[] { 0x0, 0x0 });

                bw.BaseStream.Seek(0x96E7A, SeekOrigin.Begin);
                bw.Write(new byte[] { 0x0, 0x0, 0x0, 0x0 });

                bw.BaseStream.Seek(0x970A6, SeekOrigin.Begin);  // ---
                bw.Write(new byte[] { 0x0, 0x0 });

                bw.BaseStream.Seek(0x971DA, SeekOrigin.Begin);
                bw.Write(new byte[] { 0x0, 0x0 });

                // Dex
                bw.BaseStream.Seek(0x43220, SeekOrigin.Begin);
                bw.Write(new byte[] { 0x0, 0x0 });

                bw.BaseStream.Seek(0x88EA4, SeekOrigin.Begin);
                bw.Write((ushort)neededDexEntries - 1);

                bw.BaseStream.Seek(0x104C28, SeekOrigin.Begin);
                bw.Write((ushort)neededDexEntries - 1);

                // Evolution
                bw.BaseStream.Seek(0xEC9A, SeekOrigin.Begin);
                bw.Write(new byte[] { 0x7, 0xE0 });

                bw.BaseStream.Seek(0x97011, SeekOrigin.Begin);
                bw.Write((byte)0xE0);

                // Cries
                bw.BaseStream.Seek(0x720CA, SeekOrigin.Begin);
                bw.Write(new byte[] { 0x1, 0x1C, 0x11, 0xE0 });
            }

            #endregion
        }

        
    }

    public static class EvolutionExpander
    {
        public static void ExpandFR(ROM rom, Ini ini, int oldCount, int newCount)
        {
            // I stole these offests from G3HS ;)
            uint[] offsetslslr0r60x1 = { 0x42f9c, 0x43182, 0x43026, 0x43008, 0x43016, 0x43050, 0x4307A, 0x430A8, 0x430C8, 0x430EC, 0x430FC };
            uint[] offsetscountminus1 = { 0x43116, 0x4319e, 0x459A2 };
            uint shedinjaFix = 0xCE766;
            uint[] offsetscounttimes8 = { 0x4598A, 0x459C0, 0x4598E, 0x459C2 };
            int numPokemon = Convert.ToInt32(ini[rom.Code, "NumberOfPokemon"]);

            // Get free space
            FreeSpaceFinderDialog fsf = new FreeSpaceFinderDialog(rom, (uint)(Evolution.SIZE * newCount * numPokemon));
            if (fsf.ShowDialog() != DialogResult.OK) return;

            uint oldStartOffset = Convert.ToUInt32(ini[rom.Code, "EvolutionData"], 16);
            uint newStartOffset = fsf.FreeSpaceOffset;

            // Now, copy evolution data from old offest...
            
            byte[][] entries = new byte[numPokemon][];
            using (GBABinaryReader br = new GBABinaryReader(rom))
            {
                br.BaseStream.Seek(oldStartOffset, SeekOrigin.Begin);
                for (int i = 0; i < numPokemon; i++)
                {
                    // Create a blank entry
                    entries[i] = new byte[Evolution.SIZE * newCount];
                    for (int a = 0; a < Evolution.SIZE * newCount; a++) entries[i][a] = 0;

                    byte[] temp = br.ReadBytes(Evolution.SIZE * oldCount);
                    temp.CopyTo(entries[i], 0); // and then mem it
                }
            }

            // Write to ROM
            using (GBABinaryWriter bw = new GBABinaryWriter(rom))
            {
                // Overwrite with FF
                bw.BaseStream.Seek(oldStartOffset, SeekOrigin.Begin);
                for (int i = 0; i < numPokemon; i++)
                {
                    for (int a = 0; a < Evolution.SIZE * oldCount; a++)
                    {
                        bw.Write((byte)255);
                    }
                }

                // Write new data
                bw.BaseStream.Seek(newStartOffset, SeekOrigin.Begin);
                for (int i = 0; i < numPokemon; i++)
                {
                    bw.Write(entries[i]);
                }

                // This instruction is lsl stuff
                foreach (uint offset in offsetslslr0r60x1)
                {
                    bw.BaseStream.Seek(offset, SeekOrigin.Begin);
                    if (newCount == 4) bw.Write((ushort)0x3000);
                    else if (newCount == 8) bw.Write((ushort)0x7000);
                    else if (newCount == 16) bw.Write((ushort)0xB000);
                    else if (newCount == 32) bw.Write((ushort)0xF000);
                    else bw.Write((ushort)0xF019);
                }

                // New size - 1
                foreach (uint offset in offsetscountminus1)
                {
                    bw.BaseStream.Seek(offset, SeekOrigin.Begin);
                    bw.Write((byte)(newCount - 1)); // ~~~ I guess this is how it works.
                }

                // New size * 8
                foreach (uint offset in offsetscounttimes8) // size in bytes for an entry (8 * #)
                {
                    bw.BaseStream.Seek(offset, SeekOrigin.Begin);
                    bw.Write((byte)offset);

                    // We used a byte for both of those...
                    // Because they were mov commands.
                    // Or something like that.
                }

                // And finally, the dreaded Shedinja Fix.
                // I dunno what this is.
                bw.BaseStream.Seek(shedinjaFix, SeekOrigin.Begin);
            }

            // And adjust the ini
            ini[rom.Code, "EvolutionData"] = "0x" + newStartOffset.ToString("X");
            ini[rom.Code, "NumberOfEvolutions"] = newCount.ToString();
        }
    }
}
