using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GBAHL;
using GBAHL.IO;
using GBAHL.Drawing;
using GBAHL.Text;

namespace Lost
{
    partial class MainForm
    {
        private Settings romSettings;

        // Editable data
        private Pokemon baseStats = new Pokemon();
        private Evolution[] evolutions;
        private Move[] moveset;

        // Cached data
        private string[] names;
        private string[] types;
        private string[] abilities;
        private string[] items;

        // Limits
        private int pokemonCount;
        private int evolutionCount;

        /// <summary>
        /// Try to open a ROM file and ROM settings.
        /// </summary>
        private void OpenROM(string filename)
        {
            // Try to open new ROM
            ROM temp = null;
            try
            {
                temp = new ROM(filename);

                // Load ROM settings
                if (!File.Exists(Path.Combine(Program.GetPath(), "ROMs", temp.Code + ".ini")))
                    throw new Exception($"Could not find settings for {temp.Code}.");

                // TODO: Create custom settings file
                romSettings = Settings.FromFile(Path.Combine(Program.GetPath(), "ROMs", temp.Code + ".ini"), Settings.Format.INI);
            }
            catch //(Exception ex)
            {
                temp?.Dispose();
                throw;
            }

            // ROM opened successfully, close old ROM
            rom?.Dispose();
            rom = temp;
        }

        private void LoadFirst()
        {
            pokemonCount = romSettings.GetInt32("pokemon", "Count");
            evolutionCount = romSettings.GetInt32("evolutions", "Count");

            // Load any data that we need to start editing
            LoadNames();
            LoadTypes();
            LoadItems();
            LoadAbilities();
        }

        #region Loading

        /// <summary>
        /// Loads the specified base stat data.
        /// </summary>
        /// <param name="index">The index of the Pokémon.</param>
        private void LoadBaseStats(int index)
        {
            rom.Seek(romSettings.GetInt32("pokemon", "Data", 16) + index * 28);

            baseStats = new Pokemon();
            baseStats.HP = rom.ReadByte();
            baseStats.Attack = rom.ReadByte();
            baseStats.Defense = rom.ReadByte();
            baseStats.Speed = rom.ReadByte();
            baseStats.SpecialAttack = rom.ReadByte();
            baseStats.SpecialDefense = rom.ReadByte();
            baseStats.Type = rom.ReadByte();
            baseStats.Type2 = rom.ReadByte();
            baseStats.CatchRate = rom.ReadByte();
            baseStats.BaseExperience = rom.ReadByte();
            baseStats.EffortYield = rom.ReadUInt16();
            baseStats.HeldItem = rom.ReadUInt16();
            baseStats.HeldItem2 = rom.ReadUInt16();
            baseStats.GenderRatio = rom.ReadByte();
            baseStats.EggCycles = rom.ReadByte();
            baseStats.BaseFriendship = rom.ReadByte();
            baseStats.LevelRate = rom.ReadByte();
            baseStats.EggGroup = rom.ReadByte();
            baseStats.EggGroup2 = rom.ReadByte();
            baseStats.Ability = rom.ReadByte();
            baseStats.Ability2 = rom.ReadByte();
            baseStats.RunRate = rom.ReadByte();
            baseStats.ColorFlip = rom.ReadByte();
            baseStats.Padding = rom.ReadUInt16();
        }

        /// <summary>
        /// Loads the specified evolution data.
        /// </summary>
        /// <param name="index">The index of the Pokémon.</param>
        private void LoadEvolutions(int index)
        {
            evolutions = new Evolution[evolutionCount];

            rom.Seek(romSettings.GetInt32("evolutions", "Data", 16) + index * 8);
            for (int i = 0; i < evolutionCount; i++)
            {
                evolutions[i].Method = rom.ReadUInt16();
                evolutions[i].Parameter = rom.ReadUInt16();
                evolutions[i].Target = rom.ReadUInt16();
                evolutions[i].Padding = rom.ReadUInt16();
            }
        }

        /// <summary>
        /// Loads the specified moveset data.
        /// </summary>
        /// <param name="index">The index of the Pokémon.</param>
        private void LoadMoveset(int index)
        {
            // Read offset of moves in pointer table
            rom.Seek(romSettings.GetInt32("movesets", "Data", 16) + index * 4);
            rom.ReadPointerAndSeek();

            // Load moveset based on format
            List<Move> entries = new List<Move>();
            switch (romSettings.GetString("movesets", "Format"))
            {
                case "extended":
                    // Three bytes per entry
                    // 16 bits for the attack, 8 for the level
                    // FFFF termianted
                    while (rom.Position < rom.Length - 3)
                    {
                        var atk = rom.ReadUInt16();
                        var lvl = rom.ReadByte();
                        if (atk == 0xFFFF)
                            break;

                        entries.Add(new Move {
                            Attack = atk,
                            Level = lvl,
                        });
                    }
                    break;

                case "vanilla":
                default:
                    // Two bytes per entry
                    // 9 bits for the attack, 7 for the level
                    // FFFF terminated (unfortunately)
                    while (rom.Position < rom.Length - 2)
                    {
                        var buffer = rom.ReadUInt16();
                        if (buffer == 0xFFFF)
                            break;

                        entries.Add(new Move {
                            Attack = (ushort)(buffer & 0x1FF),
                            Level = (byte)((buffer >> 9) & 0x7F),
                        });
                    }
                    break;
            }
            moveset = entries.ToArray();
        }

        /// <summary>
        /// Loads the number of entries in the specified moveset data.
        /// </summary>
        /// <param name="index">The index of the Pokémon.</param>
        /// <returns>The number of entries currently in the ROM.</returns>
        private int LoadMovesetLength(int index)
        {
            // Read offset of moves in pointer table
            rom.Seek(romSettings.GetInt32("movesets", "Data", 16) + index * 4);
            rom.ReadPointerAndSeek();

            // Load moveset based on format
            int entries = 0;
            switch (romSettings.GetString("movesets", "Format"))
            {
                case "extended":
                    while (rom.Position < rom.Length - 3)
                    {
                        var atk = rom.ReadUInt16();
                        var lvl = rom.ReadByte();
                        if (atk == 0xFFFF)
                            break;

                        entries++;
                    }
                    break;

                case "vanilla":
                default:
                    while (rom.Position < rom.Length - 2)
                    {
                        var buffer = rom.ReadUInt16();
                        if (buffer == 0xFFFF)
                            break;

                        entries++;
                    }
                    break;
            }
            return entries;
        }

        /// <summary>
        /// Loads all Pokémon names.
        /// </summary>
        private void LoadNames()
        {
            rom.Seek(romSettings.GetInt32("pokemon", "Names", 16));
            names = rom.ReadTextTable(11, pokemonCount, Table.Encoding.English);
        }

        /// <summary>
        /// Loads all type names.
        /// </summary>
        private void LoadTypes()
        {
            int typeCount = 0;

            // Load the number of types based on the type chart
            rom.Seek(romSettings.GetInt32("types", "Data", 16));
            switch (romSettings.GetString("types", "Format"))
            {
                case "enhanced":
                    // The enhanced type chart used by Dizzy's Emerald Engine Upgrade
                    // This will require an extra Count field in the .ini
                    typeCount = romSettings.GetInt32("types", "Count");
                    break;

                case "vanilla":
                    // The type chart used by an unmodified game
                    // 3 byte entries: [attacker][defender][multiplier]
                    // We can easily analyze this table to dynamically load the number of types
                default:
                    // Normal type data
                    while (rom.PeekByte() != 0xFF)
                    {
                        var attacker = rom.ReadByte();
                        var defender = rom.ReadByte();
                        var effectiveness = rom.ReadByte();

                        if (typeCount < attacker)
                            typeCount = attacker;
                        if (typeCount < defender)
                            typeCount = defender;

                        if (rom.PeekByte() == 0xFE)
                        {
                            rom.Skip(3);
                            break;
                        }
                    }

                    // Foresight type data
                    while (rom.PeekByte() != 0xFF)
                    {
                        var attacker = rom.ReadByte();
                        var defender = rom.ReadByte();
                        var effectiveness = rom.ReadByte();

                        if (typeCount < attacker)
                            typeCount = attacker;
                        if (typeCount < defender)
                            typeCount = defender;
                    }

                    typeCount++;
                    break;
            }

            // Load the type names
            rom.Seek(romSettings.GetInt32("types", "Names", 16));
            types = rom.ReadTextTable(7, typeCount, Table.Encoding.English);
        }

        /// <summary>
        /// Loads all ability names.
        /// </summary>
        private void LoadAbilities()
        {
            rom.Seek(romSettings.GetInt32("abilities", "Names", 16));
            abilities = rom.ReadTextTable(13, romSettings.GetInt32("abilities", "Count"), Table.Encoding.English);
        }

        /// <summary>
        /// Loads all item names (not data).
        /// </summary>
        private void LoadItems()
        {
            var firstItem = romSettings.GetInt32("items", "Data", 16);

            items = new string[romSettings.GetInt32("items", "Count")];
            for (int i = 0; i < items.Length; i++)
            {
                rom.Seek(firstItem + i * 44);
                items[i] = rom.ReadText(14, Table.Encoding.English);
            }
        }

        #endregion

        #region Saving

        /// <summary>
        /// Writes the current base stats to the ROM.
        /// </summary>
        /// <param name="index">The index of the Pokémon.</param>
        private void SaveBaseStats(int index)
        {
            rom.Seek(romSettings.GetInt32("pokemon", "Data", 16) + index * 28);

            rom.WriteByte(baseStats.HP);
            rom.WriteByte(baseStats.Attack);
            rom.WriteByte(baseStats.Defense);
            rom.WriteByte(baseStats.Speed);
            rom.WriteByte(baseStats.SpecialAttack);
            rom.WriteByte(baseStats.SpecialDefense);
            rom.WriteByte(baseStats.Type);
            rom.WriteByte(baseStats.Type2);
            rom.WriteByte(baseStats.CatchRate);
            rom.WriteByte(baseStats.BaseExperience);
            rom.WriteUInt16(baseStats.EffortYield);
            rom.WriteUInt16(baseStats.HeldItem);
            rom.WriteUInt16(baseStats.HeldItem2);
            rom.WriteByte(baseStats.GenderRatio);
            rom.WriteByte(baseStats.EggCycles);
            rom.WriteByte(baseStats.BaseFriendship);
            rom.WriteByte(baseStats.LevelRate);
            rom.WriteByte(baseStats.EggGroup);
            rom.WriteByte(baseStats.EggGroup2);
            rom.WriteByte(baseStats.Ability);
            rom.WriteByte(baseStats.Ability2);
            rom.WriteByte(baseStats.RunRate);
            rom.WriteByte(baseStats.ColorFlip);
            rom.WriteUInt16(baseStats.Padding);
        }

        #endregion
    }
}
