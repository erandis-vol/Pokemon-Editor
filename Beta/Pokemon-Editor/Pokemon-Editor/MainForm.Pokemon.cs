using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lost.GBA;

namespace Lost
{
    // holds code responsible for loading/saving Pokemon data
    partial class MainForm
    {
        ROM rom;
        Settings romInfo;

        // data that is saved
        Pokemon[] pokemon;
        string[] names;
        Evolution[,] evolutions;
        ushort[] nationalOrder;
        PokedexEntry[] pokedex;

        // data that is not saved
        string[] types;
        string[] abilities;
        string[] items;

        // limits
        int pokemonCount;
        int evolutionCount;

        int typeCount;
        int abilityCount;
        int itemCount;

        Dictionary<int, string> evolutionTypes = new Dictionary<int, string>();
        Dictionary<int, string> evolutionParameters = new Dictionary<int, string>();

        public bool OpenROM(string filename)
        {
            bool success = true;
            ROM temp = null;

            try
            {
                // create a new ROM
                temp = new ROM(filename);

                // check that it is valid
                if (!File.Exists($@"ROMs\{temp.Code}.ini"))
                    throw new Exception($"ROM type {temp.Code} is not supported!");

                // TODO: custom settings
                romInfo = Settings.FromFile($@"ROMs\{temp.Code}.ini", "ini");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\n\nStack Trace:\n{ex.StackTrace}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                success = false;
            }

            // finish
            if (success)
            {
                rom?.Dispose();
                rom = temp;
            }
            else
            {
                temp?.Dispose();
            }
            return success;
        }

        #region Loading

        public void LoadAll()
        {
            if (rom == null) return;

            // get limits from .ini
            pokemonCount = romInfo.GetInt32("pokemon", "Count");
            evolutionCount = romInfo.GetInt32("evolutions", "Count");

            typeCount = 17; // default value, changed during loading
            abilityCount = romInfo.GetInt32("abilities", "Count");
            itemCount = romInfo.GetInt32("items", "Count");

            // get other stuff from ini
            // TODO: should this be moved elsewhere
            evolutionTypes.Clear();
            evolutionParameters.Clear();

            for (int i = 0; i <= 0xFF; i++)
            {
                if (romInfo.ContainsKey("evolution_types", $"{i:X2}"))
                    evolutionTypes[i] = romInfo.GetString("evolution_types", $"{i:X2}");

                if (romInfo.ContainsKey("evolution_parameters", $"{i:X2}"))
                    evolutionParameters[i] = romInfo.GetString("evolution_parameters", $"{i:X2}");                
            }

            if (!evolutionTypes.ContainsKey(0))
                evolutionTypes[0] = "None";

            // load all data needed
            LoadNames();
            LoadPokemon();
            LoadEvolutions();
            LoadPokedexOrder();
            LoadPokedex();
            LoadMovesets();

            LoadTypes();
            LoadAbilities();
            LoadItems();
        }

        // data that will be saved:

        void LoadPokemon()
        {
            // get needed info from ini
            var firstPokemon = romInfo.GetInt32("pokemon", "Data", 16);

            // seek firstPokemon and begin
            rom.Seek(firstPokemon);

            pokemon = new Pokemon[pokemonCount];
            for (int i = 0; i < pokemonCount; i++)
            {
                pokemon[i].HP = rom.ReadByte();
                pokemon[i].Attack = rom.ReadByte();
                pokemon[i].Defense = rom.ReadByte();
                pokemon[i].Speed = rom.ReadByte();
                pokemon[i].SpecialAttack = rom.ReadByte();
                pokemon[i].SpecialDefense = rom.ReadByte();
                pokemon[i].Type = rom.ReadByte();
                pokemon[i].Type2 = rom.ReadByte();
                pokemon[i].CatchRate = rom.ReadByte();
                pokemon[i].BaseExperience = rom.ReadByte();
                pokemon[i].EffortYield = rom.ReadUInt16();
                pokemon[i].HeldItem = rom.ReadUInt16();
                pokemon[i].HeldItem2 = rom.ReadUInt16();
                pokemon[i].GenderRatio = rom.ReadByte();
                pokemon[i].EggCycles = rom.ReadByte();
                pokemon[i].BaseFriendship = rom.ReadByte();
                pokemon[i].LevelRate = rom.ReadByte();
                pokemon[i].EggGroup = rom.ReadByte();
                pokemon[i].EggGroup2 = rom.ReadByte();
                pokemon[i].Ability = rom.ReadByte();
                pokemon[i].Ability2 = rom.ReadByte();
                pokemon[i].RunRate = rom.ReadByte();
                pokemon[i].ColorFlip = rom.ReadByte();
                pokemon[i].Padding = rom.ReadUInt16();
            }
        }

        void LoadEvolutions()
        {
            evolutions = new Evolution[pokemonCount, evolutionCount];

            rom.Seek(romInfo.GetInt32("evolutions", "Data", 16));
            for (int i = 0; i < pokemonCount; i++)
            {
                for (int j = 0; j < evolutionCount; j++)
                {
                    evolutions[i, j].Method = rom.ReadUInt16();
                    evolutions[i, j].Parameter = rom.ReadUInt16();
                    evolutions[i, j].Target = rom.ReadUInt16();
                    evolutions[i, j].Padding = rom.ReadUInt16();
                }
            }
        }

        void LoadNames()
        {
            var nameTable = romInfo.GetInt32("pokemon", "Names", 16);

            rom.Seek(nameTable);
            names = rom.ReadTextTable(11, pokemonCount, CharacterEncoding.English);
        }

        void LoadPokedexOrder()
        {
            // load national pokedex order
            // !!! missingno. does not have an entry !!!
            rom.Seek(romInfo.GetInt32("pokedex", "Order", 16));
            nationalOrder = new ushort[pokemonCount];

            for (int i = 1; i < pokemonCount; i++)
            {
                nationalOrder[i] = rom.ReadUInt16();
            }
        }

        void LoadPokedex()
        {
            // not working yet

            // load pokedex entries
            // assumes order is loaded
            var firstEntry = romInfo.GetInt32("pokedex", "Data", 16);

            // number of entries is equal to:
            // total - unused_gap - unown_gap (iff expanded)
            var entryCount = pokemonCount - 25 - (pokemonCount > 412 ? 28 : 0);

            pokedex = new PokedexEntry[entryCount];
            switch (romInfo.GetString("pokedex", "Format"))
            {
                case "ruby": // 2 pages, 36 bytes
                    for (int i = 0; i < entryCount; i++)
                    {
                        rom.Seek(firstEntry + i * 36);

                        // read entry
                        pokedex[i].Species = rom.ReadText(12, CharacterEncoding.English);
                        pokedex[i].Height = rom.ReadUInt16();
                        pokedex[i].Weight = rom.ReadUInt16();
                        pokedex[i].Page1.Offset = rom.ReadPointer();
                        pokedex[i].Page2.Offset = rom.ReadPointer();
                        pokedex[i].Padding = rom.ReadUInt16();
                        pokedex[i].PokemonScale = rom.ReadUInt16();
                        pokedex[i].PokemonOffset = rom.ReadUInt16();
                        pokedex[i].TrainerScale = rom.ReadUInt16();
                        pokedex[i].TrainerOffset = rom.ReadUInt16();
                        pokedex[i].Padding2 = rom.ReadUInt16();

                        // read pages
                        if (pokedex[i].Page1.Offset > 0)
                        {
                            rom.Seek(pokedex[i].Page1.Offset);
                            pokedex[i].Page1.Text = rom.ReadText(CharacterEncoding.English);
                        }

                        if (pokedex[i].Page2.Offset > 0)
                        {
                            rom.Seek(pokedex[i].Page2.Offset);
                            pokedex[i].Page2.Text = rom.ReadText(CharacterEncoding.English);
                        }
                    }
                    break;

                case "firered": // 1 page, 36 bytes
                    for (int i = 0; i < entryCount; i++)
                    {
                        rom.Seek(firstEntry + i * 36);

                        pokedex[i].Species = rom.ReadText(12, CharacterEncoding.English);
                        pokedex[i].Height = rom.ReadUInt16();
                        pokedex[i].Weight = rom.ReadUInt16();
                        pokedex[i].Page1.Offset = rom.ReadPointer();
                        pokedex[i].Page2.Offset = rom.ReadPointer();
                        pokedex[i].Padding = rom.ReadUInt16();
                        pokedex[i].PokemonScale = rom.ReadUInt16();
                        pokedex[i].PokemonOffset = rom.ReadUInt16();
                        pokedex[i].TrainerScale = rom.ReadUInt16();
                        pokedex[i].TrainerOffset = rom.ReadUInt16();
                        pokedex[i].Padding2 = rom.ReadUInt16();

                        if (pokedex[i].Page1.Offset > 0)
                        {
                            rom.Seek(pokedex[i].Page1.Offset);
                            pokedex[i].Page1.Text = rom.ReadText(CharacterEncoding.English);
                        }

                        if (pokedex[i].Page2.Offset > 0) // note: on FRLG this *should* always be 0
                        {
                            rom.Seek(pokedex[i].Page2.Offset);
                            pokedex[i].Page2.Text = rom.ReadText(CharacterEncoding.English);
                        }
                    }
                    break;

                case "emerald": // 1 page, 32 bytes
                    for (int i = 0; i < entryCount; i++)
                    {
                        rom.Seek(firstEntry + i * 32);

                        pokedex[i].Species = rom.ReadText(12, CharacterEncoding.English);
                        pokedex[i].Height = rom.ReadUInt16();
                        pokedex[i].Weight = rom.ReadUInt16();
                        pokedex[i].Page1.Offset = rom.ReadPointer();
                        pokedex[i].Padding = rom.ReadUInt16();
                        pokedex[i].PokemonScale = rom.ReadUInt16();
                        pokedex[i].PokemonOffset = rom.ReadUInt16();
                        pokedex[i].TrainerScale = rom.ReadUInt16();
                        pokedex[i].TrainerOffset = rom.ReadUInt16();
                        pokedex[i].Padding2 = rom.ReadUInt16();

                        if (pokedex[i].Page1.Offset > 0)
                        {
                            rom.Seek(pokedex[i].Page1.Offset);
                            pokedex[i].Page1.Text = rom.ReadText(CharacterEncoding.English);
                        }
                    }
                    break;

                default:
                    throw new Exception("Invalid Pokédex format!");
            }
        }

        void LoadMovesets()
        {
            rom.Seek(romInfo.GetInt32("movesets", "Data", 16));

            // What is the best way to store this?
            // Movesets are stored in a table of pointers
            // Meaning a Pokémon could share a moveset with another
            // Dictionary?

            switch (romInfo.GetString("movesets", "Format"))
            {
                case "extended":
                    // three bytes per entry
                    // 16 bits per attack, 8 per level
                    // FFFF terminated
                    // (max attack index is 65534, max level is 255)
                    break;

                case "vanilla":
                default:
                    // two bytes per entry
                    // 9 bits per attack, 7 per level
                    // FFFF terminated
                    // (max attack index is 511, max level is 127)
                    for (int i = 0; i < pokemonCount; i++)
                    {

                    }
                    break;
            }
        }

        // data that will not be saved:

        void LoadTypes()
        {
            var nameTable = romInfo.GetInt32("types", "Names", 16);
            var typeChart = romInfo.GetInt32("types", "Data", 16);

            // first, load the type chart to find the number of types
            var lastType = 0;

            rom.Seek(typeChart);

            switch (romInfo.GetString("types", "Format"))
            {
                case "full-chart":
                    // format where for every type, data is stored
                    // (used by the EM battle engine upgrade)
                    // the idea is to analyze the data by reading until a value of
                    // 00, 05, 10, or 20 is not found again
                    throw new NotImplementedException("This type chart format \"full-chart\" is not implemented yet!");

                case "vanilla":
                    // type chart as stored in a vanilla game
                    // 3 bytes per entry, [attacker] [defender] [effectiveness]
                    // terminated by ff fe 00
                    // second chart follows for foresight, terminated by ff ff 00
                default:
                    // normal type data
                    while (rom.PeekByte() != 0xFF)
                    {
                        var attacker = rom.ReadByte();
                        var defender = rom.ReadByte();
                        var effectiveness = rom.ReadByte();

                        if (lastType < attacker)
                            lastType = attacker;
                        if (lastType < defender)
                            lastType = defender;

                        if (rom.PeekByte() == 0xFE)
                        {
                            rom.Skip(3);
                            break;
                        }
                    }

                    // foresight type data
                    while (rom.PeekByte() != 0xFF)
                    {
                        var attacker = rom.ReadByte();
                        var defender = rom.ReadByte();
                        var effectiveness = rom.ReadByte();

                        if (lastType < attacker)
                            lastType = attacker;
                        if (lastType < defender)
                            lastType = defender;
                    }
                    break;
            }
            
            // get typeCount
            typeCount = lastType + 1;

            // load type names now
            rom.Seek(nameTable);
            types = rom.ReadTextTable(7, typeCount, CharacterEncoding.English);
        }

        void LoadAbilities()
        {
            var firstAbility = romInfo.GetInt32("abilities", "Names", 16);

            rom.Seek(firstAbility);
            abilities = rom.ReadTextTable(13, abilityCount, CharacterEncoding.English);
        }

        void LoadItems()
        {
            var firstItem = romInfo.GetInt32("items", "Data", 16);

            items = new string[itemCount];
            for (int i = 0; i < itemCount; i++)
            {
                rom.Seek(firstItem + i * 44);
                items[i] = rom.ReadText(14, CharacterEncoding.English);
            }
        }

        #endregion

        #region Saving

        public void SaveAll()
        {
            if (rom == null) return;

            // save everything
            SaveNames();
            SavePokemon();
            SaveEvolutions();

            // write ROM buffer to disk
            rom.Save();
        }

        void SavePokemon()
        {
            var firstPokemon = romInfo.GetInt32("pokemon", "Data", 16);

            rom.Seek(firstPokemon);
            for (int i = 0; i < pokemonCount; i++)
            {
                rom.WriteByte(pokemon[i].HP);
                rom.WriteByte(pokemon[i].Attack);
                rom.WriteByte(pokemon[i].Defense);
                rom.WriteByte(pokemon[i].Speed);
                rom.WriteByte(pokemon[i].SpecialAttack);
                rom.WriteByte(pokemon[i].SpecialDefense);
                rom.WriteByte(pokemon[i].Type);
                rom.WriteByte(pokemon[i].Type2);
                rom.WriteByte(pokemon[i].CatchRate);
                rom.WriteByte(pokemon[i].BaseExperience);
                rom.WriteUInt16(pokemon[i].EffortYield);
                rom.WriteUInt16(pokemon[i].HeldItem);
                rom.WriteUInt16(pokemon[i].HeldItem2);
                rom.WriteByte(pokemon[i].GenderRatio);
                rom.WriteByte(pokemon[i].EggCycles);
                rom.WriteByte(pokemon[i].BaseFriendship);
                rom.WriteByte(pokemon[i].LevelRate);
                rom.WriteByte(pokemon[i].EggGroup);
                rom.WriteByte(pokemon[i].EggGroup2);
                rom.WriteByte(pokemon[i].Ability);
                rom.WriteByte(pokemon[i].Ability2);
                rom.WriteByte(pokemon[i].RunRate);
                rom.WriteByte(pokemon[i].ColorFlip);
                rom.WriteUInt16(pokemon[i].Padding);
            }
        }

        void SaveNames()
        {
            var nameTable = romInfo.GetInt32("pokemon", "Names", 16);

            rom.Seek(nameTable);
            rom.WriteTextTable(names, 11, CharacterEncoding.English);
        }

        void SaveEvolutions()
        {
            var firstEvolution = romInfo.GetInt32("evolutions", "Data", 16);

            rom.Seek(firstEvolution);
            for (int i = 0; i < pokemonCount; i++)
            {
                for (int j = 0; j < evolutionCount; j++)
                {
                    rom.WriteUInt16(evolutions[i, j].Method);
                    rom.WriteUInt16(evolutions[i, j].Parameter);
                    rom.WriteUInt16(evolutions[i, j].Target);
                    rom.WriteUInt16(evolutions[i, j].Padding);
                }
            }
        }

        #endregion

        void FixPokemon()
        {
            // the aim of this function is to search all Pokemon data
            // and ensure they have valid data
            // TODO
        }

        void DisplayPokemon(int index)
        {
            ignore = true;

            // Base stats
            tBaseHealth.Value = pokemon[index].HP;
            tBaseAttack.Value = pokemon[index].Attack;
            tBaseDefense.Value = pokemon[index].Defense;
            tBaseSpecialAttack.Value = pokemon[index].SpecialAttack;
            tBaseSpecialDefense.Value = pokemon[index].SpecialDefense;
            tBaseSpeed.Value = pokemon[index].Speed;

            tBaseHealth2.Value = pokemon[index].EffortYield & 3;
            tBaseAttack2.Value = pokemon[index].EffortYield >> 2 & 3;
            tBaseDefense2.Value = pokemon[index].EffortYield >> 4 & 3;
            tBaseSpecialAttack2.Value = pokemon[index].EffortYield >> 6 & 3;
            tBaseSpecialDefense2.Value = pokemon[index].EffortYield >> 8 & 3;
            tBaseSpeed2.Value = pokemon[index].EffortYield >> 10 & 3;

            cBaseType.SelectedIndex = pokemon[index].Type;
            cBaseType2.SelectedIndex = pokemon[index].Type2;

            cBaseAbility.SelectedIndex = pokemon[index].Ability;
            cBaseAbility2.SelectedIndex = pokemon[index].Ability2;

            cBaseItem.SelectedIndex = pokemon[index].HeldItem;
            cBaseItem2.SelectedIndex = pokemon[index].HeldItem2;

            cBaseEggGroup.SelectedIndex = pokemon[index].EggGroup;
            cBaseEggGroup2.SelectedIndex = pokemon[index].EggGroup2;
            tBaseHatchTime.Value = pokemon[index].EggCycles;

            cBaseLevelRate.SelectedIndex = pokemon[index].LevelRate;
            tBaseExperienceYield.Value = pokemon[index].BaseExperience;

            tBaseGenderRatio.Value = pokemon[index].GenderRatio;

            tBaseCatchRate.Value = pokemon[index].CatchRate;
            tBaseRunRate.Value = pokemon[index].RunRate;

            chkBaseFlipSprite.Checked = (pokemon[index].ColorFlip & 0x80) == 0x80;
            tBasePadding.Value = pokemon[index].Padding;

            // Evolutions
            listEvolutions.Items.Clear();
            for (int i = 0; i < evolutionCount; i++)
            {
                listEvolutions.Items.Add(DisplayEvolutionItem(ref evolutions[index, i], i));
            }

            // Pokedex
            var pokedexIndex = nationalOrder[index];
            if (pokedexIndex < pokedex.Length)
            {
                tDexPage1.Text = pokedex[pokedexIndex].Page1.Text;
                tDexPage2.Text = pokedex[pokedexIndex].Page2.Text;

                tDexPage1.Enabled = true;
                if (string.IsNullOrEmpty(pokedex[pokedexIndex].Page2.Text))
                    tDexPage2.Enabled = false;
                else
                    tDexPage2.Enabled = true;
            }
            else
            {
                tDexPage1.Text = "- No Pokédex entry. -";
                tDexPage2.Text = string.Empty;

                tDexPage1.Enabled = false;
                tDexPage2.Enabled = false;
            }

            ignore = false;
        }

        void DisplayBlankPokemon()
        {
            ignore = true;

            // Base stats

            tBaseHealth.Value = 0;
            tBaseAttack.Value = 0;
            tBaseDefense.Value = 0;
            tBaseSpecialAttack.Value = 0;
            tBaseSpecialDefense.Value = 0;
            tBaseSpeed.Value = 0;
            tBaseHealth2.Value = 0;
            tBaseAttack2.Value = 0;
            tBaseDefense2.Value = 0;
            tBaseSpecialAttack2.Value = 0;
            tBaseSpecialDefense2.Value = 0;
            tBaseSpeed2.Value = 0;
            cBaseType.SelectedIndex = 0;
            cBaseType2.SelectedIndex = 0;
            cBaseAbility.SelectedIndex = 0;
            cBaseAbility2.SelectedIndex = 0;
            cBaseItem.SelectedIndex = 0;
            cBaseItem2.SelectedIndex = 0;
            cBaseEggGroup.SelectedIndex = 0;
            cBaseEggGroup2.SelectedIndex = 0;
            tBaseHatchTime.Value = 0;
            tBaseGenderRatio.Value = 0;
            tBaseCatchRate.Value = 0;
            tBaseRunRate.Value = 0;
            chkBaseFlipSprite.Checked = false;
            tBasePadding.Value = 0;

            // Evolutions
            listEvolutions.Items.Clear();
            for (int i = 0; i < evolutionCount; i++)
            {
                var item = new ListViewItem();
                item.Tag = i;

                item.Text = evolutionTypes[0];
                item.SubItems.Add("00");
                item.SubItems.Add(names[0]);
                item.SubItems.Add($"{0:X2} {0:X2}");

                listEvolutions.Items.Add(item);
            }

            ignore = false;
        }

        ListViewItem DisplayEvolutionItem(ref Evolution e, int index)
        {
            // returns a ListViewItem representing this evolution
            // index is provided for ease of editing

            var item = new ListViewItem();
            item.Tag = index;

            // set method name or value
            if (evolutionTypes.ContainsKey(e.Method))
                item.Text = evolutionTypes[e.Method];
            else
                item.Text = e.Method.ToString("X2");

            // set parameter text or value
            if (evolutionParameters.ContainsKey(e.Method))
            {
                switch (evolutionParameters[e.Method])
                {
                    // level and item are in the only parameters needed by the base game
                    case "level":
                        item.SubItems.Add(e.Parameter.ToString());
                        break;
                    case "item":
                        item.SubItems.Add(items[e.Parameter]);
                        break;

                    // other parameters supported for extensibility
                    case "pokemon":
                        item.SubItems.Add(names[e.Parameter]);
                        break;
                    case "ability":
                        item.SubItems.Add(abilities[e.Parameter]);
                        break;
                    case "type":
                        item.SubItems.Add(types[e.Parameter]);
                        break;
                    //case "weather":
                        // TODO

                    default:
                        item.SubItems.Add($"{e.Parameter >> 8 & 0xFF:X2} {e.Parameter & 0xFF:X2}");
                        break;
                }
            }
            else
            {
                item.SubItems.Add($"{e.Parameter >> 8 & 0xFF:X2} {e.Parameter & 0xFF:X2}");
            }

            // set target
            item.SubItems.Add(names[e.Target]);

            // set padding as bytes
            item.SubItems.Add($"{e.Padding >> 8 & 0xFF:X2} {e.Padding & 0xFF:X2}");

            return item;
        }
    }
}
