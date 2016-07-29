using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace HPE
{
    public partial class MainForm : Form
    {
        // memory
        private ROM rom;
        private Ini ini;
        private bool mc = false;
        private string iniVersion = "0.9";

        // things we'll need/can edit
        private int selectedPokemon = -1, selectedEvolution = -1, selectedAttack = -1;
        private string[] pokemonNames, itemNames, attackNames;
        private Pokemon pokemon;
        private Evolution[] evolutions;
        private List<AttackEntry> moveset;
        private bool[] tmCompatibility, hmCompatibility;
        private DexEntry pokedex;
        private ushort[] pokedexOrder;
        private bool illegalDex = false;
        private bool[] moveTutorCompat;
        private uint frontSprite, backSprite;
        private uint regularPalette, shinyPalette;
        private uint iconSprite;
        private byte iconPalette;
        private Cry cry;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ini = null;
            rom = new ROM()
            {
                File = string.Empty,
                Code = string.Empty
            };
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        #region Menu

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // get file
            openDialog.Title = "Open ROM";
            openDialog.Filter = "GBA ROMs|*.gba";
            openDialog.FileName = "";
            if (openDialog.ShowDialog() != DialogResult.OK) return;

            // try it out
            string file = openDialog.FileName;
            using (GBABinaryReader br = new GBABinaryReader(File.OpenRead(openDialog.FileName)))
            {
                // open
                br.BaseStream.Seek(0xA0, SeekOrigin.Begin);
                string name = Encoding.UTF8.GetString(br.ReadBytes(12));
                string code = Encoding.UTF8.GetString(br.ReadBytes(4));

                // get ini
                Ini main = new Ini(Path.Combine(Environment.CurrentDirectory, "ROMs.ini"));
                string iniPath = Path.Combine(Path.GetDirectoryName(file), Path.GetFileNameWithoutExtension(file) + ".hpe.ini");
                if (!File.Exists(iniPath))
                {
                    ini = new Ini(); // new ini

                    if (!main.ContainsSection(code))
                    {
                        MessageBox.Show("Uh-oh! You just tried to load an unsupported ROM!", "Uh-oh!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    main.CopySectionTo(code, ini);
                    ini.Save(iniPath); // -- enable later~
                }
                else
                {
                    ini = new Ini(iniPath);

                    // Update
                    if (!ini[code, "Version"].Equals(iniVersion))
                    {
                        // Inform
                        MessageBox.Show("It looks like your .ini for this game is out of date!\nDon't worry, though. I'll update it for you.", "Uh-oh!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Update for real
                        ini[code, "Version"] = iniVersion; // ~:D
                        foreach (string key in main[code])
                        {
                            if (ini[code, key] == string.Empty)
                            {
                                ini[code, key] = main[code, key];
                            }
                        }
                        ini.Save(iniPath);
                    }
                }

                // load
                rom.File = openDialog.FileName;
                rom.Code = code;
                lblROM.Text = "ROM: " + name + "\nCode: " + code;

                try
                {
                    PreLoadROM(br);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There was an error loading the ROM:\n" + ex.Message + "\n" + ex.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rom.File != string.Empty)
            {
                try
                {
                    // let's go~
                    SavePokemon(selectedPokemon, pokemon);
                    SaveEvolutions(selectedPokemon, evolutions);
                    SaveMoveset(selectedPokemon, moveset.ToArray());
                    SaveTMHMCompatibility(selectedPokemon, tmCompatibility, hmCompatibility);
                    if (!illegalDex) // this again
                    {
                        SavePokedexEntry(pokedexOrder[selectedPokemon], ref pokedex);
                    }
                    if (Convert.ToBoolean(ini[rom.Code, "HasMoveTutors"]))
                    {
                        SaveMoveTutorCompatibility(selectedPokemon, moveTutorCompat);
                    }
                    SaveSpriteOffsets(selectedPokemon);
                    SaveIconOffsets(selectedPokemon);
                    SavePokemonNames();

                    mc = true;
                    listPokemon.Items[selectedPokemon].SubItems[1].Text = pokemonNames[selectedPokemon];
                    mc = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n" + ex.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void saveNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rom.File != string.Empty)
            {
                try
                {
                    SavePokemonNames();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n" + ex.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void saveBaseStatsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rom.File != string.Empty)
            {
                try
                {
                    SavePokemon(selectedPokemon, pokemon);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n" + ex.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void saveEvolutionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rom.File != string.Empty)
            {
                try
                {
                    SaveEvolutions(selectedPokemon, evolutions);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n" + ex.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void saveAttacksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rom.File != string.Empty)
            {
                try
                {
                    SaveMoveset(selectedPokemon, moveset.ToArray());
                    SaveTMHMCompatibility(selectedPokemon, tmCompatibility, hmCompatibility);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n" + ex.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void savePokédexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rom.File != string.Empty && !illegalDex)
            {
                try
                {
                    SavePokedexEntry(pokedexOrder[selectedPokemon], ref pokedex);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n" + ex.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void saveMoveTutorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rom.File != string.Empty)
            {
                try
                {
                    if (Convert.ToBoolean(ini[rom.Code, "HasMoveTutors"]))
                    {
                        SaveMoveTutorCompatibility(selectedPokemon, moveTutorCompat);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n" + ex.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void saveSpritesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rom.File != string.Empty)
            {
                try
                {
                    SaveSpriteOffsets(selectedPokemon);
                    SaveIconOffsets(selectedPokemon);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n" + ex.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void expandPokémonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rom.File != string.Empty)
            {
                if (!Convert.ToBoolean(ini[rom.Code, "AllowExpandPokemon"])) return;
                int celebiTreeckoRange = Convert.ToInt32(ini[rom.Code, "NumberOfPokemonBetweenCelebiAndTreecko"]);
                int post411Count = Convert.ToInt32(ini[rom.Code, "NumberOfUnownAfter411"]);

                // Pokemon Length
                int pokemonCount = Convert.ToInt32(ini[rom.Code, "NumberOfPokemon"]);
                //int pokedexLength = pokemonCount;// -celebiTreeckoRange;
                if (pokemonCount > 412) pokemonCount -= post411Count;
                pokemonCount -= celebiTreeckoRange;

                ExpandPokemonDialog exp = new ExpandPokemonDialog(pokemonCount);
                if (exp.ShowDialog() != DialogResult.OK) return;

                if (exp.NewPokemonCount == pokemonCount)
                {
                    MessageBox.Show("Please designate at least 1 Pokémon to add.", "Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // yeah~
                {
                    string format = "FR"; // TODO: Read from INI
                    if (format == "FR")
                        PokemonExpander.ExpandFR(rom, ini, exp.NewPokemonCount + celebiTreeckoRange + post411Count);
                    else if (format == "LG")
                        PokemonExpander.ExpandFR(rom, ini, exp.NewPokemonCount + celebiTreeckoRange + post411Count);
                }

                string iniPath = Path.Combine(Path.GetDirectoryName(rom.File), Path.GetFileNameWithoutExtension(rom.File) + ".hpe.ini");
                ini.Save(iniPath);

                using (GBABinaryReader br = new GBABinaryReader(rom))
                {
                    PreLoadROM(br);
                }

                MessageBox.Show("The expansion was a success~!", "Yay~!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void editMoveTutorAttacksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rom.File != string.Empty)
            {
                // Show
                int count = Convert.ToInt32(ini[rom.Code, "NumberOfMoveTutorAttacks"]);
                MoveTutorDialog mt = new MoveTutorDialog(rom, Convert.ToUInt32(ini[rom.Code, "MoveTutorData"], 16), count, attackNames);
                mt.ShowDialog();

                mc = true;
                using (GBABinaryReader br = new GBABinaryReader(rom))
                {
                    ushort[] mtl = LoadMoveTutorList(br);

                    for (int i = 0; i < count; i++)
                    {
                        listMoveTutor.Items[i].Text = attackNames[mtl[i]];
                        //if (selectedPokemon > -1)
                    }
                }
                mc = false;
            }
        }

        private void showSpriteTransprencyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedPokemon > -1)
            {
                using (GBABinaryReader br = new GBABinaryReader(rom))
                {
                    pFrontSprite.Image = LoadPokemonSprite(frontSprite, regularPalette, br);
                    pFrontSprite2.Image = LoadPokemonSprite(frontSprite, shinyPalette, br);
                    pBackSprite.Image = LoadPokemonSprite(backSprite, regularPalette, br);
                    pBackSprite2.Image = LoadPokemonSprite(backSprite, shinyPalette, br);

                    pIcon.Image = LoadIconSprite(iconSprite, iconPalette, br);
                }
            }
        }

        #endregion

        #region Loading

        private void PreLoadROM(GBABinaryReader br)
        {
            // here, we load everything we'll need on initial load...
            pokemonNames = LoadPokemonNames(br);
            string[] types = LoadTypeNames(br);
            string[] abilities = LoadAbilityNames(br);
            itemNames = LoadItemNames(br);
            attackNames = LoadAttackNames(br);
            ushort[] tmhmList = LoadTMHMList(br);
            pokedexOrder = LoadPokedexOrder(br);

            // fill controls
            expandPokémonToolStripMenuItem.Enabled = Convert.ToBoolean(ini[rom.Code, "AllowExpandPokemon"]);

            // base stats
            cType1.Items.Clear(); cType2.Items.Clear();
            cType1.Items.AddRange(types); cType2.Items.AddRange(types);

            cAbility1.Items.Clear(); cAbility2.Items.Clear();
            cAbility1.Items.AddRange(abilities); cAbility2.Items.AddRange(abilities);

            cHeld1.Items.Clear(); cHeld2.Items.Clear();
            cHeld1.Items.AddRange(itemNames); cHeld2.Items.AddRange(itemNames);

            // evolutions
            string[] evos = ini[rom.Code, "EvolutionTypes"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            cEvolutionType.Items.Clear(); cEvolutionType.Items.AddRange(evos);

            cEvolutionTarget.Items.Clear(); cEvolutionTarget.Items.AddRange(pokemonNames);

            // evolutions
            cAtkAtk.Items.Clear();
            cAtkAtk.Items.AddRange(attackNames);

            bChangeEvolutions.Enabled = Convert.ToBoolean(ini[rom.Code, "AllowExpandedEvolutions"]);

            // attacks
            int tms = Convert.ToInt32(ini[rom.Code, "NumberOfTMs"]);
            int hms = Convert.ToInt32(ini[rom.Code, "NumberOfHMs"]);

            listTMs.Items.Clear();
            for (int i = 0; i < tms; i++)
            {
                var item = new ListViewItem((i + 1).ToString("00"));
                item.SubItems.Add(attackNames[tmhmList[i]]);
                listTMs.Items.Add(item);
            }

            listHMs.Items.Clear();
            for (int i = 0; i < hms; i++)
            {
                var item = new ListViewItem((i + 1).ToString("00"));
                item.SubItems.Add(attackNames[tmhmList[i + tms]]);
                listHMs.Items.Add(item);
            }

            // move tutor
            if (Convert.ToBoolean(ini[rom.Code, "HasMoveTutors"]))
            {
                ushort[] moveTutor = LoadMoveTutorList(br);
                listMoveTutor.Items.Clear();
                for (int i = 0; i < moveTutor.Length; i++)
                {
                    ListViewItem t = new ListViewItem(attackNames[moveTutor[i]]);
                    t.Checked = false;
                    listMoveTutor.Items.Add(t);
                }

                groupBox17.Enabled = true;
                bEditMoveTutor.Enabled = true;
            }
            else
            {
                //tabMoveTutor
                listMoveTutor.Items.Clear();
                groupBox17.Enabled = false;
                bEditMoveTutor.Enabled = false;
            }

            // sprites
            txtFrontSprite.MaxValue = (uint)br.BaseStream.Length - 1;
            txtBackSprite.MaxValue = (uint)br.BaseStream.Length - 1;
            txtRegularPal.MaxValue = (uint)br.BaseStream.Length - 1;
            txtShinyPal.MaxValue = (uint)br.BaseStream.Length - 1;

            txtIcon.MaxValue = (uint)br.BaseStream.Length - 1;

            // populate listview
            listPokemon.Items.Clear();
            for (int i = 0; i < pokemonNames.Length; i++)
            {
                ListViewItem t = new ListViewItem(i.ToString());
                t.SubItems.Add(pokemonNames[i]);
                listPokemon.Items.Add(t);
            }
        }

        private Pokemon LoadPokemon(int id, GBABinaryReader br)
        {
            // goto offset
            uint tableStart = Convert.ToUInt32(ini[rom.Code, "BaseStatsData"], 16);
            br.BaseStream.Seek(tableStart + (id * Pokemon.SIZE), SeekOrigin.Begin);

            // let's load~
            Pokemon pkmn = new Pokemon();
            pkmn.HP = br.ReadByte();
            pkmn.Attack = br.ReadByte();
            pkmn.Defense = br.ReadByte();
            pkmn.Speed = br.ReadByte();
            pkmn.SpAttack = br.ReadByte();
            pkmn.SpDefense = br.ReadByte();

            pkmn.Type1 = br.ReadByte();
            pkmn.Type2 = br.ReadByte();
            pkmn.CatchRate = br.ReadByte();
            pkmn.BaseExperience = br.ReadByte();

            pkmn.EffortYield = br.ReadUInt16();
            pkmn.Item1 = br.ReadUInt16();
            pkmn.Item2 = br.ReadUInt16();

            pkmn.Gender = br.ReadByte();
            pkmn.EggCycles = br.ReadByte();
            pkmn.Friendship = br.ReadByte();
            pkmn.LevelRate = br.ReadByte();

            pkmn.EggGroup1 = br.ReadByte();
            pkmn.EggGroup2 = br.ReadByte();
            pkmn.Ability1 = br.ReadByte();
            pkmn.Ability2 = br.ReadByte();
            pkmn.RunRate = br.ReadByte();
            pkmn.ColorFlip = br.ReadByte();
            return pkmn;
        }

        private Evolution[] LoadEvolutions(int id, GBABinaryReader br)
        {
            // get offset
            int count = Convert.ToInt32(ini[rom.Code, "NumberOfEvolutions"]);
            uint tableStart = Convert.ToUInt32(ini[rom.Code, "EvolutionData"], 16);
            br.BaseStream.Seek(tableStart + (id * count * Evolution.SIZE), SeekOrigin.Begin);

            // load evolutions TODO: enable changing of # of evolutions
            Evolution[] evos = new Evolution[count];
            for (int i = 0; i < count; i++)
            {
                Evolution e = new Evolution();
                e.Type = br.ReadUInt16();
                e.Parameter = br.ReadUInt16();
                e.Target = br.ReadUInt16();
                br.BaseStream.Seek(2L, SeekOrigin.Current); // skip the filler
                evos[i] = e;
            }
            return evos;
        }

        private List<AttackEntry> LoadMoveset(int id, GBABinaryReader br)
        {
            // get offset
            uint tableStart = Convert.ToUInt32(ini[rom.Code, "MovesetData"], 16);
            br.BaseStream.Seek(tableStart + (id * 4), SeekOrigin.Begin);

            // load pointer from table
            uint movesetStart = br.ReadPointer();
            br.BaseStream.Seek(movesetStart, SeekOrigin.Begin);

            // try to load
            List<AttackEntry> attacks = new List<AttackEntry>();
            while (br.BaseStream.Position < br.BaseStream.Length - 1)
            {
                ushort buffer = br.ReadUInt16();
                if (buffer == 0xFFFF) break;
                else
                {
                    AttackEntry atk = new AttackEntry();
                    atk.Attack = (ushort)(buffer & 0x1FF);
                    atk.Level = (ushort)((buffer >> 9) & 0x7F);
                    attacks.Add(atk);
                }
            }
            return attacks;
        }

        private bool[][] LoadTMHMCompatibility(int id, GBABinaryReader br)
        {
            // goto
            int tms = Convert.ToInt32(ini[rom.Code, "NumberOfTMs"]);
            int hms = Convert.ToInt32(ini[rom.Code, "NumberOfHMs"]);
            uint tableStart = Convert.ToUInt32(ini[rom.Code, "TMCompatibilityData"], 16);
            br.BaseStream.Seek(tableStart + id * 8, SeekOrigin.Begin);

            // and load
            bool[][] tmhm = new bool[2][];
            ulong buffer = br.ReadUInt64();

            // understand -- try #1
            tmhm[0] = new bool[tms];
            for (int i = 0; i < tms; i++)
            {
                tmhm[0][i] = (((buffer >> i) & 1) == 1);
            }

            tmhm[1] = new bool[hms];
            for (int i = 0; i < hms; i++)
            {
                tmhm[1][i] = (((buffer >> (tms + i)) & 1) == 1);
            }
            return tmhm;
        }

        private DexEntry LoadPokedexEntry(int id, GBABinaryReader br)
        {
            uint tableStart = Convert.ToUInt32(ini[rom.Code, "PokedexData"], 16);
            string format = ini[rom.Code, "PokedexFormat"];

            // load entry
            DexEntry entry = new DexEntry();
            if (format == "FRLG" || format == "RS")
            {
                br.BaseStream.Seek(tableStart + id * 36, SeekOrigin.Begin);
                entry.Species = TextTable.GetString(br.ReadBytes(12));
                entry.Height = br.ReadUInt16();
                entry.Weight = br.ReadUInt16();
                entry.Page1Offset = br.ReadPointer();
                entry.Page2Offset = br.ReadPointer();
                br.BaseStream.Seek(2L, SeekOrigin.Current);
                entry.PokemonScale = br.ReadUInt16();
                entry.PokemonOffset = br.ReadInt16();
                entry.TrainerScale = br.ReadUInt16();
                entry.TrainerOffset = br.ReadInt16();
            }
            else if (format == "E")
            {
                br.BaseStream.Seek(tableStart + id * 32, SeekOrigin.Begin);
                entry.Species = TextTable.GetString(br.ReadBytes(12));
                entry.Height = br.ReadUInt16();
                entry.Weight = br.ReadUInt16();
                entry.Page1Offset = br.ReadPointer();
                br.BaseStream.Seek(2L, SeekOrigin.Current);
                entry.PokemonScale = br.ReadUInt16();
                entry.PokemonOffset = br.ReadInt16();
                entry.TrainerScale = br.ReadUInt16();
                entry.TrainerOffset = br.ReadInt16();
            }
            else
            {
                throw new Exception("Unknown Pokédex format!");
            }

            // load page 1
            List<byte> page = new List<byte>();
            br.BaseStream.Seek(entry.Page1Offset, SeekOrigin.Begin);
            while (true)
            {
                // read
                byte b = br.ReadByte();
                page.Add(b);

                // exit?
                if (b == 0xFF) break;
            }
            entry.Page1 = TextTable.GetString(page.ToArray());
            entry.Page1OS = page.Count;

            // load page 2
            if (format == "RS")
            {
                page.Clear();
                br.BaseStream.Seek(entry.Page2Offset, SeekOrigin.Begin);
                while (true)
                {
                    // read
                    byte b = br.ReadByte();
                    page.Add(b);

                    // exit?
                    if (b == 0xFF) break;
                }
                entry.Page2 = TextTable.GetString(page.ToArray());
                entry.Page2OS = page.Count;
            }

            return entry;
        }

        private Bitmap LoadFootprint(int id, GBABinaryReader br)
        {
            uint tableStart = Convert.ToUInt32(ini[rom.Code, "FootprintData"], 16);
            br.BaseStream.Seek(tableStart + id * 4, SeekOrigin.Begin);

            uint footprintStart = br.ReadPointer();
            br.BaseStream.Seek(footprintStart, SeekOrigin.Begin);

            byte[] buffer = br.ReadBytes(32);

            Bitmap bmp = new Bitmap(48, 48);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                int num1 = 0, num2 = 0, num3 = 0;
                for (int b = 0; b < 32; b++)
                {
                    for (int bit = 0; bit < 8; bit++)
                    {
                        //int i = (buffer[b] >> bit) & 1;
                        //if (((buffer[b] >> bit) & 1) == 1) bmp.SetPixel(bit + num1 * 8, num2 + num3 * 8, Color.Black);
                        if (((buffer[b] >> bit) & 1) == 1)
                        {
                            g.FillRectangle(Brushes.Black, bit * 3 + num1 * 24, num2 * 3 + num3 * 24, 3, 3);
                        }
                        /*else
                        {
                            g.FillRectangle(Brushes.White, bit * 4 + num1 * 32, num2 * 4 + num3 * 32, 4, 4);
                        }*/
                    }

                    if (num2 < 7)
                    {
                        num2++;
                    }
                    else if (num1 == 1)
                    {
                        num2 = 0;
                        num1 = 0;
                        num3++;
                    }
                    else
                    {
                        num2 = 0;
                        num1++;
                    }
                }
            }
            return bmp;
        }

        private bool[] LoadMoveTutorCompatibility(int id, GBABinaryReader br)
        {
            uint tableStart = Convert.ToUInt32(ini[rom.Code, "MoveTutorCompatibilityData"], 16);
            int len = Convert.ToInt32(ini[rom.Code, "MoveTutorCompatibilityLength"], 16);
            int count = Convert.ToInt32(ini[rom.Code, "NumberOfMoveTutorAttacks"]);
            //MessageBox.Show("Start: 0x" + tableStart.ToString("X"));

            br.BaseStream.Seek(tableStart + id * len, SeekOrigin.Begin);
            byte[] buffer = br.ReadBytes(len);
            bool[] tf = new bool[count];

            //int k = 0;
            for (int b = 0; b < len; b++)
            {
                for (int bit = 0; bit < 8; bit++)
                {
                    int index = bit + b * 8;
                    if (index >= count) break;

                    tf[index] = ((buffer[b] >> bit) & 1) == 1;
                }
            }

            /*int k = 0;
            for (int bit = 0; bit < count; k++)
            {
                tf[bit] = (((buffer[bit / 8] >> (bit % 8)) & 1) == 1);
            }*/
            return tf;
        }

        private string[] LoadPokemonNames(GBABinaryReader br)
        {
            // get number of pokemon
            int count = Convert.ToInt32(ini[rom.Code, "NumberOfPokemon"]);
            string[] names = new string[count];

            // load all names
            br.BaseStream.Seek(Convert.ToUInt32(ini[rom.Code, "PokemonNames"], 16), SeekOrigin.Begin);
            for (int i = 0; i < count; i++)
            {
                names[i] = TextTable.GetString(br.ReadBytes(11));
            }
            return names;
        }

        private string[] LoadTypeNames(GBABinaryReader br)
        {
            int count = Convert.ToInt32(ini[rom.Code, "NumberOfTypes"]);
            string[] names = new string[count];

            // load all names
            br.BaseStream.Seek(Convert.ToUInt32(ini[rom.Code, "TypeNames"], 16), SeekOrigin.Begin);
            for (int i = 0; i < count; i++)
            {
                names[i] = TextTable.GetString(br.ReadBytes(7));
            }
            return names;
        }

        private string[] LoadAbilityNames(GBABinaryReader br)
        {
            int count = Convert.ToInt32(ini[rom.Code, "NumberOfAbilities"]);
            string[] names = new string[count];

            // load all names
            br.BaseStream.Seek(Convert.ToUInt32(ini[rom.Code, "AbilityNames"], 16), SeekOrigin.Begin);
            for (int i = 0; i < count; i++)
            {
                names[i] = TextTable.GetString(br.ReadBytes(13));
            }
            return names;
        }

        private string[] LoadItemNames(GBABinaryReader br)
        {
            int count = Convert.ToInt32(ini[rom.Code, "NumberOfItems"]);
            string[] names = new string[count];

            // load all names
            uint start = Convert.ToUInt32(ini[rom.Code, "ItemData"], 16);
            for (int i = 0; i < count; i++)
            {
                br.BaseStream.Seek(start + i * 44, SeekOrigin.Begin);
                names[i] = TextTable.GetString(br.ReadBytes(14));
            }
            return names;
        }

        private string[] LoadAttackNames(GBABinaryReader br)
        {
            // get number of pokemon
            int count = Convert.ToInt32(ini[rom.Code, "NumberOfAttacks"]);
            string[] names = new string[count];

            // load all names
            br.BaseStream.Seek(Convert.ToUInt32(ini[rom.Code, "AttackNames"], 16), SeekOrigin.Begin);
            for (int i = 0; i < count; i++)
            {
                names[i] = TextTable.GetString(br.ReadBytes(13));
            }
            return names;
        }

        private ushort[] LoadTMHMList(GBABinaryReader br)
        {
            // get number of pokemon
            int count = Convert.ToInt32(ini[rom.Code, "NumberOfTMs"]) + Convert.ToInt32(ini[rom.Code, "NumberOfHMs"]);
            ushort[] list = new ushort[count];

            br.BaseStream.Seek(Convert.ToUInt32(ini[rom.Code, "TMData"], 16), SeekOrigin.Begin);
            for (int i = 0; i < count; i++)
            {
                list[i] = br.ReadUInt16(); // I think this is all it takes
            }
            return list;
        }

        private ushort[] LoadPokedexOrder(GBABinaryReader br)
        {
            // set up
            int count = Convert.ToInt32(ini[rom.Code, "NumberOfPokemon"]);
            //ushort[] order = new ushort[count];
            List<ushort> order = new List<ushort>();
            order.Add(0); // Enrty 0 does not exist...

            // go
            br.BaseStream.Seek(Convert.ToUInt32(ini[rom.Code, "NationalDexOrder"], 16), SeekOrigin.Begin);
            for (int j = 0; j < count; j++)
            {
                order.Add(br.ReadUInt16()); // simple numbers game really
            }
            return order.ToArray();
        }

        private ushort[] LoadMoveTutorList(GBABinaryReader br)
        {
            int count = Convert.ToInt32(ini[rom.Code, "NumberOfMoveTutorAttacks"]);
            ushort[] list = new ushort[count];

            br.BaseStream.Seek(Convert.ToUInt32(ini[rom.Code, "MoveTutorData"], 16), SeekOrigin.Begin);
            for (int i = 0; i < count; i++)
            {
                list[i] = br.ReadUInt16();
            }
            return list;
        }

        private void LoadSpriteOffests(int id, GBABinaryReader br)
        {
            // front sprite
            uint frontTableStart = Convert.ToUInt32(ini[rom.Code, "FrontSpriteData"], 16);
            br.BaseStream.Seek(frontTableStart + (id * 8), SeekOrigin.Begin);
            frontSprite = br.ReadPointer();

            // back sprite
            uint backTableStart = Convert.ToUInt32(ini[rom.Code, "BackSpriteData"], 16);
            br.BaseStream.Seek(backTableStart + (id * 8), SeekOrigin.Begin);
            backSprite = br.ReadPointer();

            // regular palette
            uint regTableStart = Convert.ToUInt32(ini[rom.Code, "RegularPaletteData"], 16);
            br.BaseStream.Seek(regTableStart + (id * 8), SeekOrigin.Begin);
            regularPalette = br.ReadPointer();

            // shiny palette
            uint shinyTableStart = Convert.ToUInt32(ini[rom.Code, "ShinyPaletteData"], 16);
            br.BaseStream.Seek(shinyTableStart + (id * 8), SeekOrigin.Begin);
            shinyPalette = br.ReadPointer();
        }

        private void LoadIconData(int id, GBABinaryReader br)
        {
            // Sprite data
            uint spriteTableStart = Convert.ToUInt32(ini[rom.Code, "IconSpriteData"], 16);
            br.BaseStream.Seek(spriteTableStart + id * 4, SeekOrigin.Begin);
            iconSprite = br.ReadPointer();

            // Palette #
            uint iconTableStart = Convert.ToUInt32(ini[rom.Code, "IconData"], 16);
            br.BaseStream.Seek(iconTableStart + id, SeekOrigin.Begin);
            iconPalette = br.ReadByte();
        }

        #region Pokemon Sprites

        // Works for both front and back sprites~! ;)
        private Bitmap LoadPokemonSprite(uint spriteOffset, uint paletteOffset, GBABinaryReader br)
        {
            // start with data loading
            // get palette
            //uint spriteTableStart = Convert.ToUInt32(ini[rom.Code, "FrontSpriteData"], 16);
            //br.BaseStream.Seek(spriteTableStart + (id * 8), SeekOrigin.Begin);
            //uint spriteOffset = br.ReadPointer();

            // ---
            byte[] sprite;
            try
            {
                br.BaseStream.Seek(spriteOffset, SeekOrigin.Begin);
                sprite = br.ReadLZ77Bytes();
                if (sprite == null) return Properties.Resources.invalid_sprite;
            }
            catch (Exception ex)
            {
                return Properties.Resources.invalid_sprite;
            }
            //if (sprite.Length != 0x2048) MessageBox.Show("Length: " + sprite.Length);

            // get palette
            //uint paletteTableStart = Convert.ToUInt32(ini[rom.Code, (shiny ? "ShinyPaletteData" : "RegularPaletteData")], 16);
            //br.BaseStream.Seek(paletteTableStart + id * 8, SeekOrigin.Begin);
            //uint paletteOffset = br.ReadPointer();

            // ---
            Color[] palette;
            try
            {
                br.BaseStream.Seek(paletteOffset, SeekOrigin.Begin);
                palette = br.ReadLZ77Palette();
            }
            catch (Exception ex)
            {
                br.BaseStream.Seek(paletteOffset, SeekOrigin.Begin);
                palette = br.ReadPalette();
            }
            
            if (palette == null)
            {
                br.BaseStream.Seek(paletteOffset, SeekOrigin.Begin);
                palette = br.ReadPalette();
            }

            // now, we draw~
            int width = 8; // constant width
            int height = sprite.Length / (width * 32); // dynamic height
            return Sprites.DrawSprite16(sprite, width, height, palette, showSpriteTransprencyToolStripMenuItem.Checked); // and draw~
        }

        private Bitmap LoadIconSprite(uint iconOffset, byte paletteNum, GBABinaryReader br)
        {
            // ---
            br.BaseStream.Seek(iconOffset, SeekOrigin.Begin);
            byte[] frame1 = br.ReadBytes(512); // Each icon is 1024 bytes uncompressed
            byte[] frame2 = br.ReadBytes(512); // Each frame is 512 bytes

            // ---
            uint paletteStart = Convert.ToUInt32(ini[rom.Code, "IconPaletteData"], 16);
            br.BaseStream.Seek(paletteStart + paletteNum * 32, SeekOrigin.Begin);
            Color[] palette = br.ReadPalette();

            // The icon is usually 32 x 64, but we want it sideways
            // So we split it into two bitmaps and then combine them
            Bitmap bmp = new Bitmap(64, 32);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.DrawImageUnscaled(Sprites.DrawSprite16(frame1, 4, 4, palette, showSpriteTransprencyToolStripMenuItem.Checked), 0, 0);
                g.DrawImageUnscaled(Sprites.DrawSprite16(frame2, 4, 4, palette, showSpriteTransprencyToolStripMenuItem.Checked), 32, 0);
            }
            return bmp;
        }

        #endregion

        private Cry LoadCry(int id, GBABinaryReader br)
        {
            uint cryTableStart = Convert.ToUInt32(ini[rom.Code, "CryData"], 16);
            br.BaseStream.Seek(cryTableStart + (id * 12), SeekOrigin.Begin);
            uint pitch = br.ReadUInt32(); // A guess
            uint cryStart = br.ReadPointer();

            Cry cry = new Cry();
            cry.TableOffset = (uint)(cryTableStart + (id * 32));
            cry.Offset = cryStart;

            br.BaseStream.Seek(cryStart, SeekOrigin.Begin);
            cry.Compressed = (br.ReadUInt16() == 0x1);
            cry.Looped = (br.ReadUInt16() == 0x4000);

            cry.Pitch = br.ReadUInt32(); // Does this match pitch?
            cry.LoopStart = br.ReadUInt32() + 1; // Should I adjust these with +1?
            cry.OriginalSize = br.ReadUInt32() + 1;

            // Read PCM data
            if (!cry.Compressed)
            {
                // This is uncompressed
                cry.Data = new sbyte[cry.OriginalSize];
                for (int i = 0; i < cry.Data.Length; i++)
                {
                    cry.Data[i] = br.ReadSByte();
                }
            }
            else // Compressed -- uh-oh!
            {
                byte[] lookup_table = { 0x0, 0x1, 0x4, 0x9, 0x10, 0x19, 0x24, 0x31, 0xC0, 0xCF, 0xDC, 0xE7, 0xF0, 0xF7, 0xFC, 0xFF };

                //MessageBox.Show("Sample Data Length: " + sampleData.OriginalSize);s
                int originalSize = (int)cry.OriginalSize;
                List<sbyte> data = new List<sbyte>();
                int blockAlign = 0; sbyte pcmLevel = 0; int mySample = 0;
                for (int i = 0; ; i++)
                {
                    if (blockAlign == 0)
                    {
                        pcmLevel = br.ReadSByte();

                        data.Add(pcmLevel);
                        blockAlign = 32;
                    }
                    else
                    {
                        byte input = br.ReadByte();

                        // Get nybble 1
                        byte delta = lookup_table[input >> 4];
                        if (blockAlign < 32)
                        {
                            pcmLevel += (sbyte)delta;
                            data.Add(pcmLevel);
                        }

                        // Get nybble 2
                        delta = lookup_table[input & 15];
                        pcmLevel += (sbyte)delta;
                        data.Add(pcmLevel);

                        // Moving along
                        mySample += 2;
                        if (mySample >= originalSize) break;

                        blockAlign -= 1;
                    }
                }
                cry.Data = data.ToArray();
                cry.OriginalSize = (uint)data.Count; // This is so we can save correctly
            }

            return cry;
        }

        #endregion

        #region Saving

        private void SavePokemon(int id, Pokemon pkmn)
        {
            using (GBABinaryWriter bw = new GBABinaryWriter(rom))
            {
                // goto offset
                uint tableStart = Convert.ToUInt32(ini[rom.Code, "BaseStatsData"], 16);
                bw.BaseStream.Seek(tableStart + (id * Pokemon.SIZE), SeekOrigin.Begin);

                // save~
                bw.Write(pkmn.HP);
                bw.Write(pkmn.Attack);
                bw.Write(pkmn.Defense);
                bw.Write(pkmn.Speed);
                bw.Write(pkmn.SpAttack);
                bw.Write(pkmn.SpDefense);

                bw.Write(pkmn.Type1);
                bw.Write(pkmn.Type2);
                bw.Write(pkmn.CatchRate);
                bw.Write(pkmn.BaseExperience);

                bw.Write(pkmn.EffortYield);
                bw.Write(pkmn.Item1);
                bw.Write(pkmn.Item2);

                bw.Write(pkmn.Gender);
                bw.Write(pkmn.EggCycles);
                bw.Write(pkmn.Friendship);
                bw.Write(pkmn.LevelRate);

                bw.Write(pkmn.EggGroup1);
                bw.Write(pkmn.EggGroup2);
                bw.Write(pkmn.Ability1);
                bw.Write(pkmn.Ability2);
                bw.Write(pkmn.RunRate);
                bw.Write(pkmn.ColorFlip);
            }
        }

        private void SaveEvolutions(int id, Evolution[] evos)
        {
            using (GBABinaryWriter bw = new GBABinaryWriter(rom))
            {
                // get write to locaton
                uint tableStart = Convert.ToUInt32(ini[rom.Code, "EvolutionData"], 16);
                bw.BaseStream.Seek(tableStart + (id * evos.Length * Evolution.SIZE), SeekOrigin.Begin);

                // and write
                for (int i = 0; i < evos.Length; i++)
                {
                    bw.Write(evos[i].Type);
                    bw.Write(evos[i].Parameter);
                    bw.Write(evos[i].Target);
                    bw.BaseStream.Seek(2, SeekOrigin.Current); // filler
                }
            }
        }

        private void SaveMoveset(int id, AttackEntry[] attacks)
        {
            // get write to locaton
            uint tableStart = Convert.ToUInt32(ini[rom.Code, "MovesetData"], 16);

            // check to see if we have enough space
            uint writeToStart = 0; int originalSize = 0;
            using (GBABinaryReader br = new GBABinaryReader(rom))
            {
                // read from table where to start
                br.BaseStream.Seek(tableStart + id * 4, SeekOrigin.Begin);
                writeToStart = br.ReadPointer();

                // read the size of the existing moveset
                br.BaseStream.Seek(writeToStart, SeekOrigin.Begin);
                while (br.BaseStream.Position < br.BaseStream.Length - 1)
                {
                    originalSize += 2;
                    if (br.ReadUInt16() == 0xFFFF) break;
                }
            }

            // repoint, if necessary
            bool repointed = false;
            if (originalSize < 2 + (attacks.Length * 2) || chkAtkForceRepoint.Checked)
            {
                // free space finder
                FreeSpaceFinderDialog fsf = new FreeSpaceFinderDialog(rom, (uint)(4 + attacks.Length * 2));
                fsf.Text = "Repoint Moveset";

                // get offset
                if (fsf.ShowDialog() != DialogResult.OK) return; // or quit this part
                writeToStart = fsf.FreeSpaceOffset;
                repointed = true;
                // offset will be saved with the rest
            }

            // and now, open for writing
            using (GBABinaryWriter bw = new GBABinaryWriter(rom))
            {
                // write pointer
                bw.BaseStream.Seek(tableStart + id * 4, SeekOrigin.Begin);
                bw.WritePointer(writeToStart);

                // write moveset
                bw.BaseStream.Seek(writeToStart, SeekOrigin.Begin);
                foreach (AttackEntry atk in attacks)
                {
                    bw.Write((ushort)(((atk.Level & 0x7F) << 9) + (atk.Attack & 0x1FF)));
                }
                bw.Write(ushort.MaxValue); // all done~

                if (repointed) bw.Write((ushort)0); // prevents freespace finder from overwriting ending ;)
            }

            // TODO: overwrite old moveset with 0xFF
        }

        private void SaveTMHMCompatibility(int id, bool[] tmCompat, bool[] hmCompat)
        {
            // combine into a single array
            bool[] tmhm = new bool[tmCompat.Length + hmCompat.Length];
            for (int i = 0; i < tmCompat.Length; i++)
            {
                tmhm[i] = tmCompat[i];
            }

            for (int i = 0; i < hmCompat.Length; i++)
            {
                tmhm[tmCompat.Length + i] = hmCompat[i];
            }

            using (GBABinaryWriter bw = new GBABinaryWriter(rom))
            {
                // A really weird way of doing this.
                // It seems to work.
                byte[] buffer = new byte[8];
                for (int i = 0; i < 8; i++) buffer[i] = 0;
                for (int i = tmhm.Length - 1; i >= 0; i--)
                {
                    if (tmhm[i])
                    {
                        buffer[i / 8] = (byte)(buffer[i / 8] | 1 << (i % 8));
                    }
                    else
                    {
                        buffer[i / 8] = (byte)(buffer[i / 8] & ~(1 << (i % 8)));
                    }
                }

                // get write to locaton
                uint tableStart = Convert.ToUInt32(ini[rom.Code, "TMCompatibilityData"], 16);
                bw.BaseStream.Seek(tableStart + (id * 8), SeekOrigin.Begin);
                // and write
                bw.Write(buffer);
            }
        }

        private void SavePokedexEntry(int id, ref DexEntry entry)
        {
            uint tableStart = Convert.ToUInt32(ini[rom.Code, "PokedexData"], 16);
            string format = ini[rom.Code, "PokedexFormat"];

            // start with text
            byte[] page = TextTable.GetBytes(entry.Page1);
            Array.Resize(ref page, page.Length + 1);
            page[page.Length - 1] = 0xFF;

            bool repoint1 = false;
            if (page.Length > entry.Page1OS || chkDexForceRepoint.Checked) // repoint
            {
                FreeSpaceFinderDialog fsf = new FreeSpaceFinderDialog(rom, (uint)page.Length);
                fsf.Text = "Repoint Page 1";

                if (fsf.ShowDialog() != DialogResult.OK) return;
                entry.Page1Offset = fsf.FreeSpaceOffset;
                entry.Page1OS = page.Length;
                repoint1 = true;
            }

            // write page 1
            using (GBABinaryWriter bw = new GBABinaryWriter(rom))
            {
                bw.BaseStream.Seek(entry.Page1Offset, SeekOrigin.Begin);
                bw.Write(page);
                if (repoint1) bw.Write((byte)0);
            }

            if (format == "RS") // page 2 (RS only~!)
            {
                byte[] page2 = TextTable.GetBytes(entry.Page2);
                Array.Resize(ref page2, page2.Length + 1);
                page2[page2.Length - 1] = 0xFF;

                bool repoint2 = false;
                if (page2.Length > entry.Page2OS || chkDexForceRepoint.Checked) // repoint
                {
                    FreeSpaceFinderDialog fsf = new FreeSpaceFinderDialog(rom, (uint)page2.Length);
                    fsf.Text = "Repoint Page 2";

                    if (fsf.ShowDialog() != DialogResult.OK) return;
                    entry.Page2Offset = fsf.FreeSpaceOffset;
                    entry.Page2OS = page.Length;
                    repoint2 = true;
                }

                using (GBABinaryWriter bw = new GBABinaryWriter(rom))
                {
                    // write page 2
                    bw.BaseStream.Seek(entry.Page2Offset, SeekOrigin.Begin);
                    bw.Write(page2);
                    if (repoint2) bw.Write((byte)0);
                }
            }

            using (GBABinaryWriter bw = new GBABinaryWriter(rom))
            {
                // now do to the entry itself
                bw.BaseStream.Seek(tableStart + id * (format == "E" ? 32 : 36), SeekOrigin.Begin);
                byte[] species = TextTable.GetBytes(pokedex.Species, 12);
                bw.Write(species);
                bw.Write(pokedex.Height);
                bw.Write(pokedex.Weight);
                bw.WritePointer(pokedex.Page1Offset);
                if (format != "E") bw.WritePointer(pokedex.Page2Offset);
                bw.BaseStream.Seek(2L, SeekOrigin.Current);
                bw.Write(pokedex.PokemonScale);
                bw.Write(pokedex.PokemonOffset);
                bw.Write(pokedex.TrainerScale);
                bw.Write(pokedex.TrainerOffset);
            }
        }

        private void SaveMoveTutorCompatibility(int id, bool[] compat)
        {
            // Get relevant data
            uint tableStart = Convert.ToUInt32(ini[rom.Code, "MoveTutorCompatibilityData"], 16);
            int len = Convert.ToInt32(ini[rom.Code, "MoveTutorCompatibilityLength"], 16);
            int count = Convert.ToInt32(ini[rom.Code, "NumberOfMoveTutorAttacks"]);

            // Format
            byte[] buffer = new byte[len];
            for (int i = 0; i < len; i++) buffer[i] = 0;
            for (int i = count - 1; i >= 0; i--)
            {
                if (compat[i])
                {
                    buffer[i / 8] = (byte)(buffer[i / 8] | 1 << (i % 8));
                }
                else
                {
                    buffer[i / 8] = (byte)(buffer[i / 8] & ~(1 << (i % 8)));
                }
            }

            // Write
            using (GBABinaryWriter bw = new GBABinaryWriter(rom))
            {
                bw.BaseStream.Seek(tableStart + id * len, SeekOrigin.Begin);
                bw.Write(buffer);
            }
        }

        private void SavePokemonNames()
        {
            using (GBABinaryWriter bw = new GBABinaryWriter(rom))
            {
                // just do it
                bw.BaseStream.Seek(Convert.ToUInt32(ini[rom.Code, "PokemonNames"], 16), SeekOrigin.Begin);
                for (int i = 0; i < pokemonNames.Length; i++)
                {
                    // convert from ASCII to GBA
                    byte[] buffer = TextTable.GetBytes(pokemonNames[i], 11);
                    bw.Write(buffer); // save
                }
            }
        }

        private void SaveSpriteOffsets(int id)
        {
            using (GBABinaryWriter bw = new GBABinaryWriter(rom))
            {
                // front sprite
                uint frontTableStart = Convert.ToUInt32(ini[rom.Code, "FrontSpriteData"], 16);
                bw.BaseStream.Seek(frontTableStart + (id * 8), SeekOrigin.Begin);
                bw.WritePointer(frontSprite);

                // back sprite
                uint backTableStart = Convert.ToUInt32(ini[rom.Code, "BackSpriteData"], 16);
                bw.BaseStream.Seek(backTableStart + (id * 8), SeekOrigin.Begin);
                bw.WritePointer(backSprite);

                // regular palette
                uint regTableStart = Convert.ToUInt32(ini[rom.Code, "RegularPaletteData"], 16);
                bw.BaseStream.Seek(regTableStart + (id * 8), SeekOrigin.Begin);
                bw.WritePointer(regularPalette);

                // shiny palette
                uint shinyTableStart = Convert.ToUInt32(ini[rom.Code, "ShinyPaletteData"], 16);
                bw.BaseStream.Seek(shinyTableStart + (id * 8), SeekOrigin.Begin);
                bw.WritePointer(shinyPalette);
            }
        }

        private void SaveIconOffsets(int id)
        {
            using (GBABinaryWriter bw = new GBABinaryWriter(rom))
            {
                // Sprite data
                uint spriteTableStart = Convert.ToUInt32(ini[rom.Code, "IconSpriteData"], 16);
                bw.BaseStream.Seek(spriteTableStart + id * 4, SeekOrigin.Begin);
                bw.WritePointer(iconSprite);

                // Palette #
                uint iconTableStart = Convert.ToUInt32(ini[rom.Code, "IconData"], 16);
                bw.BaseStream.Seek(iconTableStart + id, SeekOrigin.Begin);
                bw.Write(iconPalette);
            }
        }

        #endregion

        private void listPokemon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rom.File == string.Empty) return;

            // get selection
            int id = -1;
            foreach (int x in listPokemon.SelectedIndices) id = x;
            if (id < 0) return;

            // load
            try
            {
                selectedPokemon = id;
                using (GBABinaryReader br = new GBABinaryReader(rom))
                {
                    // begin a whole slew of loading
                    pokemon = LoadPokemon(selectedPokemon, br);
                    evolutions = LoadEvolutions(selectedPokemon, br);
                    moveset = LoadMoveset(selectedPokemon, br).ToList();
                    bool[][] tmhm = LoadTMHMCompatibility(selectedPokemon, br);
                    tmCompatibility = tmhm[0];
                    hmCompatibility = tmhm[1];

                    //if (selectedPokemon != 0) Text = "Dex Entry: " + pokedexOrder[selectedPokemon - 1];
                    //else Text = "Dex Entry: 0";
                    int dexEntry = pokedexOrder[selectedPokemon];
                    if (dexEntry >= (pokemonNames.Length - 25 - (pokemonNames.Length > 412 ? 28 : 0)))
                    {
                        illegalDex = true;
                    }
                    else
                    {
                        illegalDex = false;
                        pokedex = LoadPokedexEntry(dexEntry, br);
                    }

                    /*if (selectedPokemon == 0)
                    {
                        pokedex = LoadPokedexEntry(selectedPokemon, br);
                        illegalDex = false;
                    }
                    else if (selectedPokemon > 0 && selectedPokemon < 412)
                    {
                        int dexIndex = pokedexOrder[selectedPokemon - 1];
                        int celebiTreeckoRange = Convert.ToInt32(ini[rom.Code, "NumberOfPokemonBetweenCelebiAndTreecko"]);

                        if (dexIndex < pokedexOrder.Length - celebiTreeckoRange)
                        {
                            pokedex = LoadPokedexEntry(dexIndex, br);
                            illegalDex = false;
                        }
                    }
                    else if (selectedPokemon >= 412) // expanded (skip Unown entries!)
                    {
                        //MessageBox.Show("Expanded!");
                        int unownCount = Convert.ToInt32(ini[rom.Code, "NumberOfUnownAfter411"]);

                        if (selectedPokemon > 411 + unownCount)
                        {
                            pokedex = LoadPokedexEntry(pokedexOrder[selectedPokemon - 1], br);
                            illegalDex = false;
                        }
                    }*/

                    bFootprint.Image = LoadFootprint(selectedPokemon, br);

                    if (Convert.ToBoolean(ini[rom.Code, "HasMoveTutors"]))
                    {
                        moveTutorCompat = LoadMoveTutorCompatibility(selectedPokemon, br);
                    }

                    LoadSpriteOffests(selectedPokemon, br);
                    pFrontSprite.Image = LoadPokemonSprite(frontSprite, regularPalette, br);
                    pFrontSprite2.Image = LoadPokemonSprite(frontSprite, shinyPalette, br);
                    pBackSprite.Image = LoadPokemonSprite(backSprite, regularPalette, br);
                    pBackSprite2.Image = LoadPokemonSprite(backSprite, shinyPalette, br);

                    LoadIconData(selectedPokemon, br);
                    pIcon.Image = LoadIconSprite(iconSprite, iconPalette, br);

                    if (selectedPokemon > 0)
                    {
                        cry = LoadCry(selectedPokemon - 1, br);
                    }
                    else cry = null;
                }

                // display
                FillDisplay();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
        }

        private void listPokemon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (selectedPokemon > -1) // between first and last
            {
                if (char.IsLetterOrDigit(e.KeyChar))
                {
                    // format
                    string search = e.KeyChar.ToString().ToLower();

                    // perform search
                    int next = -1; // will be > 0 is found
                    for (int pkmn = selectedPokemon + 1; pkmn < pokemonNames.Length; pkmn++)
                    {
                        if (pokemonNames[pkmn].ToLower().StartsWith(search))
                        {
                            next = pkmn;
                            break;
                        }
                    }

                    if (next > -1)
                    {
                        // Clear current
                        ListViewItem t1 = new ListViewItem(selectedPokemon.ToString());
                        t1.SubItems.Add(pokemonNames[selectedPokemon]);
                        t1.Selected = false;
                        listPokemon.Items[selectedPokemon] = t1;

                        // Get next
                        ListViewItem t2 = new ListViewItem(next.ToString());
                        t2.SubItems.Add(pokemonNames[next]);
                        t2.Selected = true;
                        listPokemon.Items[next] = t2;
                        listPokemon.EnsureVisible(next);
                    }
                }
            }
        }

        private void FillDisplay()
        {
            if (selectedPokemon == -1) return;


            // reset these
            selectedEvolution = -1;
            selectedAttack = -1;

            // and then fill in everything we'll need
            mc = true;
            txtName.Text = pokemonNames[selectedPokemon];

            #region Base Stats.
            // base stats
            txtHP.Value = pokemon.HP;
            txtAtk.Value = pokemon.Attack;
            txtDef.Value = pokemon.Defense;
            txtSpd.Value = pokemon.Speed;
            txtSpAtk.Value = pokemon.SpAttack;
            txtSpDef.Value = pokemon.SpDefense;

            // effort yield
            txtHP2.Value = (uint)pokemon.EffortYield & 3;
            txtAtk2.Value = (uint)(pokemon.EffortYield >> 2) & 3;
            txtDef2.Value = (uint)(pokemon.EffortYield >> 4) & 3;
            txtSpd2.Value = (uint)(pokemon.EffortYield >> 6) & 3;
            txtSpAtk2.Value = (uint)(pokemon.EffortYield >> 8) & 3;
            txtSpDef2.Value = (uint)(pokemon.EffortYield >> 10) & 3;

            cType1.SelectedIndex = pokemon.Type1;
            cType2.SelectedIndex = pokemon.Type2;

            cAbility1.SelectedIndex = pokemon.Ability1;
            cAbility2.SelectedIndex = pokemon.Ability2;

            cHeld1.SelectedIndex = pokemon.Item1;
            cHeld2.SelectedIndex = pokemon.Item2;

            txtBaseExp.Value = pokemon.BaseExperience;
            cGrowthRate.SelectedIndex = pokemon.LevelRate;

            cEggGrp1.SelectedIndex = pokemon.EggGroup1 - 1;
            cEggGrp2.SelectedIndex = pokemon.EggGroup2 - 1;
            txtEggCycles.Value = pokemon.EggCycles;

            txtGender.Value = pokemon.Gender;
            txtFriend.Value = pokemon.Friendship;
            txtCatchRate.Value = pokemon.CatchRate;
            cDexColor.SelectedIndex = pokemon.ColorFlip & 127;
            chkFlip.Checked = (pokemon.ColorFlip > 128);
            #endregion

            #region Evoltions

            listEvolutions.Items.Clear();
            for (int i = 0; i < evolutions.Length; i++)
            {
                listEvolutions.Items.Add(EvolutionToListViewItem(i));
            }

            cEvolutionType.SelectedIndex = 0;
            cEvolutionParam.SelectedIndex = 0;
            cEvolutionTarget.SelectedIndex = 0;

            #endregion

            #region Moveset

            listAttacks.Items.Clear();
            for (int i = 0; i < moveset.Count; i++)
            {
                ListViewItem item = new ListViewItem(attackNames[moveset[i].Attack]);
                //item.Text = ;
                item.SubItems.Add(moveset[i].Level.ToString());
                listAttacks.Items.Add(item);
            }

            cAtkAtk.SelectedIndex = 0;
            txtAtkLevel.Value = 0;

            int tms = Convert.ToInt32(ini[rom.Code, "NumberOfTMs"]);
            int hms = Convert.ToInt32(ini[rom.Code, "NumberOfHMs"]);

            //listTMs.Items.Clear();
            for (int i = 0; i < tms; i++)
            {
                //var item = new ListViewItem((i + 1).ToString("00"));
                //item.SubItems.Add(attackNames[tmhmList[i]]);
                //item.Checked = tmCompatability[i];
                //listTMs.Items.Add(item);

                listTMs.Items[i].Checked = tmCompatibility[i];
            }

            //listHMs.Items.Clear();
            for (int i = 0; i < hms; i++)
            {
                //var item = new ListViewItem((i + 1).ToString("00"));
                //item.SubItems.Add(attackNames[tmhmList[i + tms]]);
                //item.Checked = hmCompatability[i];
                //listHMs.Items.Add(item);

                listHMs.Items[i].Checked = hmCompatibility[i];
            }

            #endregion

            #region Pokedex

            if (!illegalDex)
            {
                string format = ini[rom.Code, "PokedexFormat"];

                txtSpecies.Text = pokedex.Species;
                txtPage1.Text = pokedex.Page1.Replace("\\n", "\n");
                if (format == "RS")
                {
                    txtPage2.Text = pokedex.Page2.Replace("\\n", "\n");
                    txtPage2.Enabled = true;
                }
                else
                {
                    txtPage2.Text = "";
                    txtPage2.Enabled = false;
                }
                txtDexHeight.Value = pokedex.Height;
                txtDexWeight.Value = pokedex.Weight;

                txtPScale.Value = pokedex.PokemonScale;
                txtPOffset.Value = pokedex.PokemonOffset;
                txtTScale.Value = pokedex.TrainerScale;
                txtTOffset.Value = pokedex.TrainerOffset;
                chkDexForceRepoint.Checked = false;

                // enable
                txtSpecies.Enabled = true;
                txtPage1.Enabled = true;
                txtDexHeight.Enabled = true;
                txtDexWeight.Enabled = true;
                txtPScale.Enabled = true;
                txtPOffset.Enabled = true;
                txtTScale.Enabled = true;
                txtTOffset.Enabled = true;
                chkDexForceRepoint.Enabled = true;
            }
            else
            {
                // blank
                txtSpecies.Text = "";
                txtPage1.Text = "- There is no Pokédex entry for this Pokémon -";
                txtPage2.Text = "";

                txtDexHeight.Value = 0;
                txtDexWeight.Value = 0;
                txtPScale.Value = 0;
                txtPOffset.Value = 0;
                txtTScale.Value = 0;
                txtTOffset.Value = 0;

                // disable
                txtSpecies.Enabled = false;
                txtPage1.Enabled = false;
                txtPage2.Enabled = false;
                txtDexHeight.Enabled = false;
                txtDexWeight.Enabled = false;
                txtPScale.Enabled = false;
                txtPOffset.Enabled = false;
                txtTScale.Enabled = false;
                txtTOffset.Enabled = false;
                chkDexForceRepoint.Enabled = false;
            }

            #endregion

            // Ruby & Sapphire don't have move tutors! ;)
            if (Convert.ToBoolean(ini[rom.Code, "HasMoveTutors"]))
            {
                for (int i = 0; i < moveTutorCompat.Length; i++)
                {
                    listMoveTutor.Items[i].Checked = moveTutorCompat[i];
                }
            }

            #region Sprites

            txtFrontSprite.Value = frontSprite;
            txtBackSprite.Value = backSprite;
            txtRegularPal.Value = regularPalette;
            txtShinyPal.Value = shinyPalette;

            txtIcon.Value = iconSprite;
            nIconPal.Value = iconPalette;

            #endregion

            #region Cry

            if (cry != null)
            {
                lblCryInfo.Text = "Table: 0x" + cry.TableOffset.ToString("X") + "\nOffset: 0x" + cry.Offset.ToString("X") + "\nPitch: " + (cry.Pitch / 1024);
                bCryImport.Enabled = true;
                bCryExport.Enabled = true;
            }
            else
            {
                lblCryInfo.Text = "No Cry";
                bCryImport.Enabled = false;
                bCryExport.Enabled = false;
            }
            pCry.Image = DrawCry();

            bCryImport.Enabled = false;

            #endregion

            mc = false;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (mc) return;

            if (selectedPokemon > -1)
            {
                pokemonNames[selectedPokemon] = txtName.Text;
            }
        }

        #region Base Stats. Controls

        private void txtSpd_TextChanged(object sender, EventArgs e)
        {
            uint total = txtHP.Value + txtAtk.Value + txtDef.Value + txtSpAtk.Value + txtSpDef.Value + txtSpd.Value;
            groupBox2.Text = "Base Stats. (" + total + ")";
            
            if (mc) return;

            if (selectedPokemon > -1)
            {
                pokemon.HP = (byte)txtHP.Value;
                pokemon.Attack = (byte)txtAtk.Value;
                pokemon.Defense = (byte)txtDef.Value;
                pokemon.SpAttack = (byte)txtSpAtk.Value;
                pokemon.SpDefense = (byte)txtSpDef.Value;
                pokemon.Speed = (byte)txtSpd.Value;
            }
        }

        private void txtSpd2_TextChanged(object sender, EventArgs e)
        {
            uint total = txtHP2.Value + txtAtk2.Value + txtDef2.Value + txtSpAtk2.Value + txtSpDef2.Value + txtSpd2.Value;
            groupBox3.Text = "Effort Yield (" + total + ")";

            if (mc) return;

            if (selectedPokemon > -1)
            {
                pokemon.EffortYield = (ushort)((txtHP2.Value & 3) + ((txtAtk2.Value & 3) << 2) + ((txtDef2.Value & 3) << 4) +
                    ((txtSpd2.Value & 3) << 6) + ((txtSpAtk2.Value & 3) << 8) + ((txtSpDef2.Value & 3) << 10));
            }
        }

        private void cType2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mc) return;

            if (selectedPokemon > -1)
            {
                pokemon.Type1 = (byte)cType1.SelectedIndex;
                pokemon.Type2 = (byte)cType2.SelectedIndex;
            }
        }

        private void cAbility2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mc) return;

            if (selectedPokemon > -1)
            {
                pokemon.Ability1 = (byte)cAbility1.SelectedIndex;
                pokemon.Ability2 = (byte)cAbility2.SelectedIndex;
            }
        }

        private void cHeld2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mc) return;

            if (selectedPokemon > -1)
            {
                pokemon.Item1 = (ushort)cHeld1.SelectedIndex;
                pokemon.Item2 = (ushort)cHeld2.SelectedIndex;
            }
        }

        private void txtBaseExp_TextChanged(object sender, EventArgs e)
        {
            if (mc) return;

            if (selectedPokemon > -1) pokemon.BaseExperience = (byte)txtBaseExp.Value;
        }

        private void cGrowthRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mc) return;

            if (selectedPokemon > -1) pokemon.LevelRate = (byte)cGrowthRate.SelectedIndex;
        }

        private void cEggGrp2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mc) return;

            if (selectedPokemon > -1)
            {
                pokemon.EggGroup1 = (byte)(cEggGrp1.SelectedIndex + 1);
                pokemon.EggGroup2 = (byte)(cEggGrp2.SelectedIndex + 1);
            }
        }

        private void txtEggCycles_TextChanged(object sender, EventArgs e)
        {
            if (txtEggCycles.Value == 0) lblEggSteps.Text = "Instant";
            else lblEggSteps.Text = ((txtEggCycles.Value + 1) * 255) + " steps";

            if (mc) return;

            if (selectedPokemon > -1) pokemon.EggCycles = (byte)txtEggCycles.Value;
        }

        private void txtGender_TextChanged(object sender, EventArgs e)
        {
            if (txtGender.Value == 255) lblGender.Text = "None";
            else
            {
                lblGender.Text = ((txtGender.Value / 254.0) * 100).ToString("0.0") + "% ♀";
            }

            if (mc) return;

            if (selectedPokemon > -1) pokemon.Gender = (byte)txtGender.Value;
        }

        private void txtFriend_TextChanged(object sender, EventArgs e)
        {
            if (mc) return;

            if (selectedPokemon > -1) pokemon.Friendship = (byte)txtFriend.Value;
        }

        private void txtCatchRate_TextChanged(object sender, EventArgs e)
        {
            if (mc) return;

            if (selectedPokemon > -1)
            {
                pokemon.CatchRate = (byte)txtCatchRate.Value;
            }
        }

        private void cDexColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mc) return;

            if (selectedPokemon > -1)
            {
                pokemon.ColorFlip = (byte)((pokemon.ColorFlip & 128) + (cDexColor.SelectedIndex & 127));
            }
        }

        private void chkFlip_CheckedChanged(object sender, EventArgs e)
        {
            if (mc) return;

            if (selectedPokemon > -1)
            {
                if (chkFlip.Checked)
                {
                    pokemon.ColorFlip = (byte)(128 + (pokemon.ColorFlip & 127));
                }
                else
                {
                    pokemon.ColorFlip = (byte)(pokemon.ColorFlip & 127);
                }
            }
        }

        #endregion

        #region Evolution Controls

        private void listEvolutions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mc) return;

            // get selction
            int id = -1;
            foreach (int i in listEvolutions.SelectedIndices) id = i;
            if (id < 0) return;

            selectedEvolution = id;
            
            // display
            mc = true;
            cEvolutionType.SelectedIndex = evolutions[id].Type;
            cEvolutionTarget.SelectedIndex = evolutions[id].Target;

            cEvolutionParam.Items.Clear();
            switch (evolutions[id].Type)
            {
                case 0x4:
                case 0x8:
                case 0x9:
                case 0xA:
                case 0xB:
                case 0xC:
                case 0xD:
                case 0xE:
                    for (int i = 0; i <= 100; i++)
                    {
                        cEvolutionParam.Items.Add(i.ToString());
                    }
                    cEvolutionParam.SelectedIndex = evolutions[id].Parameter;
                    break;

                case 0x6:
                case 0x7:
                    cEvolutionParam.Items.AddRange(itemNames);
                    cEvolutionParam.SelectedIndex = evolutions[id].Parameter;
                    break;

                default:
                    cEvolutionParam.Items.Add("-----");
                    cEvolutionParam.SelectedIndex = 0;
                    break;
            }
            //cEvolutionParam.SelectedIndex = evolutions[id].Parameter;
            mc = false;
        }

        private ListViewItem EvolutionToListViewItem(int id)
        {
            if (selectedPokemon == -1) return null;
            string[] evolutionTypes = ini[rom.Code, "EvolutionTypes"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            Evolution e = evolutions[id];

            ListViewItem item = new ListViewItem((id + 1).ToString());
            if (e.Type == 0 && e.Parameter == 0 && e.Target == 0)
            {
                item.SubItems.Add("-----");
            }
            else
            {
                item.SubItems.Add(evolutionTypes[e.Type]);
                /*switch (e.Type) // show different stuff depending on parameters
                {
                    case 0x6:
                    case 0x7:
                        item.SubItems.Add(itemNames[e.Parameter]);
                        break;

                    default:
                        item.SubItems.Add(e.Parameter.ToString());
                        break;
                }*/

                switch (evolutions[id].Type)
                {
                    case 0x4: // level
                    case 0x8:
                    case 0x9:
                    case 0xA:
                    case 0xB:
                    case 0xC:
                    case 0xD:
                    case 0xE:
                        item.SubItems.Add(e.Parameter.ToString());
                        break;

                    case 0x6: // item
                    case 0x7:
                        item.SubItems.Add(itemNames[e.Parameter]);
                        break;

                    default: // none
                        item.SubItems.Add("-----");
                        break;
                }

                item.SubItems.Add(pokemonNames[e.Target]);
            }

            return item;
        }

        private void cEvolutionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mc) return;

            if (selectedEvolution > -1)
            {
                evolutions[selectedEvolution].Type = (ushort)cEvolutionType.SelectedIndex;
                evolutions[selectedEvolution].Target = 0;

                mc = true;
                if (cEvolutionType.SelectedIndex == 0) // none
                {
                    evolutions[selectedEvolution].Parameter = 0;
                    cEvolutionParam.SelectedIndex = 0;
                    cEvolutionTarget.SelectedIndex = 0;
                }
                else
                {
                    cEvolutionParam.Items.Clear();
                    switch (evolutions[selectedEvolution].Type)
                    {
                        case 0x4:
                        case 0x8:
                        case 0x9:
                        case 0xA:
                        case 0xB:
                        case 0xC:
                        case 0xD:
                        case 0xE:
                            for (int i = 0; i <= 100; i++)
                            {
                                cEvolutionParam.Items.Add(i.ToString());
                            }
                            break;

                        case 0x6:
                        case 0x7:
                            cEvolutionParam.Items.AddRange(itemNames);
                            break;

                        default:
                            cEvolutionParam.Items.Add("-----");
                            break;
                    }
                    cEvolutionParam.SelectedIndex = 0;
                }

                listEvolutions.Items[selectedEvolution] = EvolutionToListViewItem(selectedEvolution);
                mc = false;

            }
        }

        private void cEvolutionParam_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mc) return;

            if (selectedEvolution > -1 && cEvolutionType.SelectedIndex > 0)
            {
                evolutions[selectedEvolution].Parameter = (ushort)cEvolutionParam.SelectedIndex;

                mc = true;
                listEvolutions.Items[selectedEvolution] = EvolutionToListViewItem(selectedEvolution);
                mc = false;
            }
        }

        private void cEvolutionTarget_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mc) return;

            if (selectedEvolution > -1 && cEvolutionType.SelectedIndex > 0)
            {
                evolutions[selectedEvolution].Target = (ushort)cEvolutionTarget.SelectedIndex;

                mc = true;
                listEvolutions.Items[selectedEvolution] = EvolutionToListViewItem(selectedEvolution);
                mc = false;
            }
        }

        private void bChangeEvolutions_Click(object sender, EventArgs e)
        {
            if (rom.File == string.Empty) return;
            /*if (selectedPokemon == -1) return;

            if (MessageBox.Show("Would you like to save the evolutions first?", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (GBABinaryWriter bw = new GBABinaryWriter(rom))
                {
                    SaveEvolutions(selectedPokemon, evolutions);
                }
            }

            MessageBox.Show("I bet you expected something amazing didn't you?\nThis isn't ready yet.");*/

            // Get new count
            int oldCount = Convert.ToInt32(ini[rom.Code, "NumberOfEvolutions"]);
            int newCount = 0;

            ModifyEvolutionsDialog med = new ModifyEvolutionsDialog(oldCount);
            if (med.ShowDialog() != DialogResult.OK) return;
            newCount = med.NewEvolutionCount;

            // Expan
            EvolutionExpander.ExpandFR(rom, ini, oldCount, newCount);

            // Save the .ini
            string iniPath = Path.Combine(Path.GetDirectoryName(rom.File), Path.GetFileNameWithoutExtension(rom.File) + ".hpe.ini");
            ini.Save(iniPath);

            MessageBox.Show("The change was a success!", "Yay~!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // And now, reload if needed.
            if (selectedPokemon > -1)
            {
                // Reload just this
                using (GBABinaryReader br = new GBABinaryReader(rom))
                {
                    evolutions = LoadEvolutions(selectedPokemon, br);
                }

                // And display it
                mc = true;
                #region Display

                listEvolutions.Items.Clear();
                for (int i = 0; i < evolutions.Length; i++)
                {
                    listEvolutions.Items.Add(EvolutionToListViewItem(i));
                }

                cEvolutionType.SelectedIndex = 0;
                cEvolutionParam.SelectedIndex = 0;
                cEvolutionTarget.SelectedIndex = 0;

                #endregion
                mc = false;
            }
        }

        #endregion

        #region Attacks Controls

        private void listAttacks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mc || selectedPokemon == -1) return;

            // get selection
            int id = -1;
            foreach (int a in listAttacks.SelectedIndices) id = a;
            if (id < 0) return; // not every selection works~
            selectedAttack = id;

            // display
            mc = true;
            cAtkAtk.SelectedIndex = moveset[selectedAttack].Attack;
            txtAtkLevel.Value = moveset[selectedAttack].Level;
            mc = false;
        }

        private void cAtkAtk_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mc) return;

            if (selectedAttack > -1)
            {
                moveset[selectedAttack].Attack = (ushort)cAtkAtk.SelectedIndex;

                mc = true;
                listAttacks.Items[selectedAttack].Text = attackNames[cAtkAtk.SelectedIndex];
                mc = false;
            }
        }

        private void txtAtkLevel_TextChanged(object sender, EventArgs e)
        {
            if (mc) return;

            if (selectedAttack > -1)
            {
                moveset[selectedAttack].Level = (ushort)txtAtkLevel.Value;

                mc = true;
                listAttacks.Items[selectedAttack].SubItems[1].Text = txtAtkLevel.Value.ToString();
                mc = false;
            }
        }

        private void bAtkAdd_Click(object sender, EventArgs e)
        {
            if (selectedPokemon > -1)
            {
                mc = true;
                moveset.Add(new AttackEntry());
                ListViewItem item = new ListViewItem(attackNames[0]);
                //item.Text = ;
                item.SubItems.Add("0");
                listAttacks.Items.Add(item);
                mc = false;
            }
        }

        private void bAtkRemove_Click(object sender, EventArgs e)
        {
            if (selectedAttack > -1 && moveset.Count > 0)
            {
                mc = true;
                moveset.RemoveAt(selectedAttack);
                listAttacks.Items.RemoveAt(selectedAttack);
                
                selectedAttack = -1;
                cAtkAtk.SelectedIndex = 0;
                txtAtkLevel.Value = 0;
                mc = false;
            }
        }

        private void bAtkUp_Click(object sender, EventArgs e)
        {
            if (selectedAttack > 0)
            {
                mc = true;
                AttackEntry atk1 = moveset[selectedAttack];
                AttackEntry atk2 = moveset[selectedAttack - 1];

                moveset[selectedAttack] = atk2;
                moveset[selectedAttack - 1] = atk1;

                selectedAttack--;
                listAttacks.Items.Clear();
                for (int i = 0; i < moveset.Count; i++)
                {
                    ListViewItem item = new ListViewItem(attackNames[moveset[i].Attack]);
                    //item.Text = ;
                    item.SubItems.Add(moveset[i].Level.ToString());
                    if (i == selectedAttack) item.Selected = true;
                    listAttacks.Items.Add(item);
                }
                mc = false;
            }
        }

        private void bAtkDown_Click(object sender, EventArgs e)
        {
            if (selectedAttack > -1 && selectedAttack < moveset.Count - 1)
            {
                mc = true;
                AttackEntry atk1 = moveset[selectedAttack];
                AttackEntry atk2 = moveset[selectedAttack + 1];

                moveset[selectedAttack] = atk2;
                moveset[selectedAttack + 1] = atk1;

                selectedAttack++;
                listAttacks.Items.Clear();
                for (int i = 0; i < moveset.Count; i++)
                {
                    ListViewItem item = new ListViewItem(attackNames[moveset[i].Attack]);
                    //item.Text = ;
                    item.SubItems.Add(moveset[i].Level.ToString());
                    if (i == selectedAttack) item.Selected = true;
                    listAttacks.Items.Add(item);
                }
                mc = false;
            }
        }

        private void listTMs_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (mc) return;

            if (selectedPokemon > -1)
            {
                tmCompatibility[e.Item.Index] = e.Item.Checked;
                /*for (int i = 0; i < tmCompatability.Length; i++)
                {
                    tmCompatability[i] = listTMs.Items[i].Checked;
                }*/
            }
        }

        private void listHMs_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (mc) return;

            if (selectedPokemon > -1)
            {
                hmCompatibility[e.Item.Index] = e.Item.Checked;
                /*for (int i = 0; i < hmCompatability.Length; i++)
                {
                    hmCompatability[i] = listHMs.Items[i].Checked;
                }*/
            }
        }

        private void bTMSelect_Click(object sender, EventArgs e)
        {
            if (selectedPokemon > -1)
            {
                for (int i = 0; i < tmCompatibility.Length; i++)
                {
                    listTMs.Items[i].Checked = true;
                }
            }
        }

        private void bTMDeselect_Click(object sender, EventArgs e)
        {
            if (selectedPokemon > -1)
            {
                for (int i = 0; i < tmCompatibility.Length; i++)
                {
                    listTMs.Items[i].Checked = false;
                }
            }
        }

        #endregion

        #region Pokedex Controls

        private void txtDexHeight_TextChanged(object sender, EventArgs e)
        {
            lblHeightM.Text = (txtDexHeight.Value / 10f).ToString("0.0");
            lblHeightF.Text = (txtDexHeight.Value * 0.3281f).ToString("0.0");

            if (mc) return;

            if (selectedPokemon > -1 && !illegalDex)
            {
                pokedex.Height = (ushort)txtDexHeight.Value;
            }
        }

        private void txtDexWeight_TextChanged(object sender, EventArgs e)
        {
            lblWeightK.Text = (txtDexWeight.Value / 10f).ToString("0.0");
            lblWeightL.Text = (txtDexWeight.Value * 0.2205f).ToString("0.0");

            if (mc) return;

            if (selectedPokemon > -1 && !illegalDex)
            {
                pokedex.Weight = (ushort)txtDexWeight.Value;
            }
        }

        private void txtSpecies_TextChanged(object sender, EventArgs e)
        {
            if (mc) return;

            if (selectedPokemon > -1 && !illegalDex)
            {
                pokedex.Species = txtSpecies.Text;
            }
        }

        private void txtPage1_TextChanged(object sender, EventArgs e)
        {
            if (mc) return;

            if (selectedPokemon > -1 && !illegalDex)
            {
                pokedex.Page1 = txtPage1.Text.Replace("\n", "\\n");
            }
        }

        private void txtPage2_TextChanged(object sender, EventArgs e)
        {
            if (mc) return;

            if (selectedPokemon > -1 && !illegalDex)
            {
                pokedex.Page2 = txtPage2.Text.Replace("\n", "\\n");
            }
        }

        private void txtPScale_TextChanged(object sender, EventArgs e)
        {
            if (txtPScale.Value != 0)
            {
                float scale = (256f / txtPScale.Value);
                int pixels = (int)Math.Floor(64f * scale);

                lblPScale1.Text = scale.ToString("0.000") + "x";
                lblPScale2.Text = pixels + " px";
            }
            else
            {
                lblPScale1.Text = "";
                lblPScale2.Text = "Bad Scale";
            }

            if (mc) return;

            if (selectedPokemon > -1 && !illegalDex)
            {
                pokedex.PokemonScale = (ushort)txtPScale.Value;
            }
        }

        private void txtPOffset_TextChanged(object sender, EventArgs e)
        {
            if (mc) return;

            if (selectedPokemon > -1 && !illegalDex)
            {
                pokedex.PokemonOffset = (short)txtPOffset.Value;
            }
        }

        private void txtTScale_TextChanged(object sender, EventArgs e)
        {
            if (txtTScale.Value != 0)
            {
                float scale = (256f / txtTScale.Value);
                int pixels = (int)Math.Floor(64f * scale);

                lblTScale1.Text = scale.ToString("0.000") + "x";
                lblTScale2.Text = pixels + " px";
            }
            else
            {
                lblTScale1.Text = "";
                lblTScale2.Text = "Bad Scale";
            }

            if (mc) return;

            if (selectedPokemon > -1 && !illegalDex)
            {
                pokedex.PokemonScale = (ushort)txtPScale.Value;
            }
        }

        private void txtTOffset_TextChanged(object sender, EventArgs e)
        {
            if (mc) return;

            if (selectedPokemon > -1 && !illegalDex)
            {
                pokedex.TrainerOffset = (short)txtTOffset.Value;
            }
        }

        private void txtPage1_SelectionChanged(object sender, EventArgs e)
        {
            UpdatePage1Words();
        }

        private void UpdatePage1Words()
        {
            int line = txtPage1.GetLineFromCharIndex(txtPage1.SelectionStart);
            if (txtPage1.Lines.Length == 0)
            {
                lblPage1Words.Text = "0/41";
            }
            else
            {
                string text = txtPage1.Lines[line];
                lblPage1Words.Text = text.Length + "/41";
            }
        }

        private void txtPage2_SelectionChanged(object sender, EventArgs e)
        {
            UpdatePage2Words();
        }

        private void UpdatePage2Words()
        {
            int line = txtPage2.GetLineFromCharIndex(txtPage2.SelectionStart);
            if (txtPage2.Lines.Length == 0)
            {
                lblPage2Words.Text = "0/41";
            }
            else
            {
                string text = txtPage2.Lines[line];
                lblPage2Words.Text = text.Length + "/41";
            }
        }

        private void bFootprint_Click(object sender, EventArgs e)
        {
            if (!mc && selectedPokemon > -1) // Only if a Pokemon was loaded
            {
                // Show editor
                FootprintDialog foot = new FootprintDialog(rom, selectedPokemon, Convert.ToUInt32(ini[rom.Code, "FootprintData"], 16));
                foot.ShowDialog();

                // Load the new footprint
                using (GBABinaryReader br = new GBABinaryReader(rom))
                {
                    bFootprint.Image = LoadFootprint(selectedPokemon, br);
                }
            }
        }
        
        #endregion

        #region Move Tutor Controls

        private void listMoveTutor_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (mc) return;

            if (selectedPokemon > -1)
            {
                moveTutorCompat[e.Item.Index] = e.Item.Checked;
            }
        }

        private void bMTSelect_Click(object sender, EventArgs e)
        {
            if (selectedPokemon > -1)
            {
                for (int i = 0; i < moveTutorCompat.Length; i++)
                {
                    listMoveTutor.Items[i].Checked = true;
                }
            }
        }

        private void bMTDeselect_Click(object sender, EventArgs e)
        {
            if (selectedPokemon > -1)
            {
                for (int i = 0; i < moveTutorCompat.Length; i++)
                {
                    listMoveTutor.Items[i].Checked = false;
                }
            }
        }

        private void bEditMoveTutor_Click(object sender, EventArgs e)
        {
            if (rom.File != string.Empty)
            {
                // Show
                int count = Convert.ToInt32(ini[rom.Code, "NumberOfMoveTutorAttacks"]);
                MoveTutorDialog mt = new MoveTutorDialog(rom, Convert.ToUInt32(ini[rom.Code, "MoveTutorData"], 16), count, attackNames);
                mt.ShowDialog();

                mc = true;
                using (GBABinaryReader br = new GBABinaryReader(rom))
                {
                    ushort[] mtl = LoadMoveTutorList(br);

                    for (int i = 0; i < count; i++)
                    {
                        listMoveTutor.Items[i].Text = attackNames[mtl[i]];
                        //if (selectedPokemon > -1)
                    }
                }
                mc = false;
            }

        }

        #endregion

        #region Sprites Controls

        private void txtFrontSprite_TextChanged(object sender, EventArgs e)
        {
            if (mc) return;

            if (selectedPokemon > -1)
            {
                using(GBABinaryReader br = new GBABinaryReader(rom))
                {
                    if (txtFrontSprite.Value >= br.BaseStream.Length) return;
                    frontSprite = txtFrontSprite.Value;

                    pFrontSprite.Image = LoadPokemonSprite(frontSprite, regularPalette, br);
                    pFrontSprite2.Image = LoadPokemonSprite(frontSprite, shinyPalette, br);
                }
            }
        }

        private void txtBackSprite_TextChanged(object sender, EventArgs e)
        {
            if (mc) return;

            if (selectedPokemon > -1)
            {
                using (GBABinaryReader br = new GBABinaryReader(rom))
                {
                    if (txtBackSprite.Value >= br.BaseStream.Length) return;
                    backSprite = txtBackSprite.Value;

                    pBackSprite.Image = LoadPokemonSprite(backSprite, regularPalette, br);
                    pBackSprite2.Image = LoadPokemonSprite(backSprite, shinyPalette, br);
                }
            }
        }

        private void txtRegularPal_TextChanged(object sender, EventArgs e)
        {
            if (mc) return;

            if (selectedPokemon > -1)
            {
                using (GBABinaryReader br = new GBABinaryReader(rom))
                {
                    if (txtRegularPal.Value >= br.BaseStream.Length) return;
                    regularPalette = txtRegularPal.Value;

                    pFrontSprite.Image = LoadPokemonSprite(frontSprite, regularPalette, br);
                    pBackSprite.Image = LoadPokemonSprite(backSprite, regularPalette, br);
                }
            }
        }

        private void txtShinyPal_TextChanged(object sender, EventArgs e)
        {
            if (mc) return;

            if (selectedPokemon > -1)
            {
                using (GBABinaryReader br = new GBABinaryReader(rom))
                {
                    if (txtShinyPal.Value >= br.BaseStream.Length) return;
                    shinyPalette = txtShinyPal.Value;

                    pFrontSprite2.Image = LoadPokemonSprite(frontSprite, shinyPalette, br);
                    pBackSprite2.Image = LoadPokemonSprite(backSprite, shinyPalette, br);
                }
            }
        }

        private void txtIcon_TextChanged(object sender, EventArgs e)
        {
            if (mc) return;

            if (selectedPokemon > -1)
            {
                using (GBABinaryReader br = new GBABinaryReader(rom))
                {
                    if (txtIcon.Value >= br.BaseStream.Length) return;
                    iconSprite = txtIcon.Value;

                    pIcon.Image = LoadIconSprite(iconSprite, iconPalette, br);
                }
            }
        }

        private void nIconPal_ValueChanged(object sender, EventArgs e)
        {
            if (mc) return;

            if (selectedPokemon > -1)
            {
                iconPalette = (byte)nIconPal.Value;

                using (GBABinaryReader br = new GBABinaryReader(rom))
                {
                    pIcon.Image = LoadIconSprite(iconSprite, iconPalette, br);
                }
            }
        }

        private void bSpritesINI_Click(object sender, EventArgs e)
        {
            if (selectedPokemon == -1) return;
            //MessageBox.Show("Sorry, but this isn't quite ready yet!\n(Once altitude editing is enabled, it will be!)");
            //return;

            saveDialog.Title = "Export .ini for Advanced Series";
            saveDialog.FileName = "";
            saveDialog.Filter = "Configuration Files|*.ini";
            saveDialog.InitialDirectory = Path.GetDirectoryName(rom.File);
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = File.CreateText(saveDialog.FileName))
                {
                    sw.WriteLine("GameCode=" + rom.Code);
                    sw.WriteLine("FrontSpriteTable=" + ini[rom.Code, "FrontSpriteData"]);
                    sw.WriteLine("BackSpriteTable=" + ini[rom.Code, "BackSpriteData"]);
                    sw.WriteLine("FrontPaletteTable=" + ini[rom.Code, "RegularPaletteData"]);
                    sw.WriteLine("BackPaletteTable=" + ini[rom.Code, "ShinyPaletteData"]);
                    sw.WriteLine("EnemyYTable=" + ini[rom.Code, "EnemyYTable"]);
                    sw.WriteLine("PlayerYTable=" + ini[rom.Code, "PlayerYTable"]);
                    sw.WriteLine("EnemyAltitudeTable=" + ini[rom.Code, "EnemyAltitudeTable"]);
                    sw.WriteLine("IconSpriteTable=" + ini[rom.Code, "IconSpriteData"]);
                    sw.WriteLine("IconPaletteTable=" + ini[rom.Code, "IconData"]);
                    sw.WriteLine("IconPalettes=" + ini[rom.Code, "IconPaletteData"]);
                    sw.WriteLine("SpeciesNames=" + ini[rom.Code, "PokemonNames"]);
                    if (pokemonNames.Length == 412)
                        sw.WriteLine("TotalSpecies=440");
                    else
                        sw.WriteLine("TotalSpecies=" + pokemonNames.Length);
                }
            }
        }

        #endregion

        #region Cry Controls

        private void bCryImport_Click(object sender, EventArgs e)
        {

        }

        private void bCryExport_Click(object sender, EventArgs e)
        {
            // Just copied this over from gba2wav in Advanced Song.
            // Works so far.
            if (cry != null)
            {
                if (selectedPokemon > -1)
                    saveDialog.FileName = pokemonNames[selectedPokemon].ToLower() + ".wav";
                else
                    saveDialog.FileName = "cry.wav";
                saveDialog.Filter = "Wave Files|*.wav";
                saveDialog.Title = "Export Cry";
                if (saveDialog.ShowDialog() != DialogResult.OK) return;

                using (BinaryWriter bw = new BinaryWriter(File.Create(saveDialog.FileName)))
                {
                    // Write headers
                    bw.Write(Encoding.UTF8.GetBytes("RIFF"));
                    bw.Write(0); // file size - 8 -- set when finished
                    bw.Write(Encoding.UTF8.GetBytes("WAVE"));
                    bw.Write(Encoding.UTF8.GetBytes("fmt "));
                    bw.Write((uint)16); // fmt size -- constant for us
                    bw.Write((ushort)1);
                    bw.Write((ushort)1);
                    bw.Write((uint)(cry.Pitch / 1024)); // We didn't automatically do this for the cry.
                    bw.Write((uint)((cry.Pitch / 1024) * 1)); // AvgBytesPerSec = SampleRate * BlockAlign
                    bw.Write((ushort)/*(8 / 8 * 1)*/ 1); // BlockAlign = SignificantBitsPerSample / 8 * NumChannels
                    bw.Write((ushort)8); // bits
                    // that's all for now

                    // Write channel
                    bw.Write(Encoding.UTF8.GetBytes("data"));
                    bw.Write((uint)cry.Data.Length); // this should be the one
                    for (int i = 0; i < cry.Data.Length; i++)
                    {
                        bw.Write((byte)(cry.Data[i] + 128)); // Convert to byte from sbyte
                    }

                    // Fix header sizes
                    bw.BaseStream.Seek(4L, SeekOrigin.Begin);
                    bw.Write((uint)(bw.BaseStream.Length - 8));
                }
            }
        }

        private Bitmap DrawCry()
        {
            try
            {
                Bitmap bmp;
                if (cry != null && cry.Data != null)
                    bmp = new Bitmap((int)cry.Data.Length, 272);
                else
                    bmp = new Bitmap(1, 272);
                Graphics g = Graphics.FromImage(bmp);

                // Griding and such
                g.FillRectangle(Brushes.Black, 0, 0, bmp.Width, bmp.Height);
                g.DrawLine(Pens.Red, 0, 136, bmp.Width, 136);
                if (cry.Data != null) // Only with data.
                {
                    for (int i = 8; i < cry.Data.Length; i += 16)
                    {
                        g.DrawLine(Pens.MidnightBlue, i, 0, i, bmp.Height);
                    }
                }
                g.DrawLine(Pens.LightSteelBlue, 0, 8, bmp.Width, 8);
                g.DrawLine(Pens.LightSteelBlue, 0, 264, bmp.Width, 264);

                // Check the mode
                if (cry.Data == null) return bmp;

                // Draw the PCM data -- this is pretty cool looking.
                Pen p = new Pen(new SolidBrush(Color.FromArgb(0, 248, 0)), 1);
                Pen p2 = new Pen(new SolidBrush(Color.FromArgb(164, 248, 164)), 1);
                for (int i = 0; i < cry.Data.Length; i++)
                {
                    // Draw a bar
                    /*g.DrawLine(p, i * scale, 136 + cry.Data[i], (i + 1) * scale, 136 + cry.Data[i]);

                    // Connect the bars
                    if (i > 0)
                    {
                        g.DrawLine(p, i * scale, 136 + cry.Data[i - 1], i * scale, 136 + cry.Data[i]);
                    }*/

                    if (i > 0)
                    {
                        g.DrawLine(p, (i - 1), 136 - cry.Data[i - 1], i, 136 - cry.Data[i]);
                    }
                }

                if (cry.Looped)
                {
                    g.DrawLine(Pens.Pink, cry.LoopStart, 0, cry.LoopStart, bmp.Height);
                }

                g.Dispose(); // Done
                return bmp;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            return new Bitmap(1, 272);
        }

        #endregion




    }

    public struct ROM
    {
        public string File;
        public string Code;
    }
}
