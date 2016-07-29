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
    public partial class PokedexDialog : Form
    {
        private ROM rom;
        private Ini ini;

        private int selection = -1;

        private string[] pokemonNames;
        private ushort[] pokedexOrder;
        private string[] entryPreviews; //
        private byte[][] entries, newEntries;

        //private Dictionary<int, byte[]> entries = new Dictionary<int, byte[]>();

        public PokedexDialog(ROM rom, Ini ini, string[] pokemonNames)
        {
            InitializeComponent();

            this.rom = rom;
            this.ini = ini;
            this.pokemonNames = pokemonNames;
        }

        private void PokedexDialog_Load(object sender, EventArgs e)
        {
            int count = Convert.ToInt32(ini[rom.Code, "NumberOfPokemon"]);

            using (GBABinaryReader gb = new GBABinaryReader(File.OpenRead(rom.File)))
            {
                pokedexOrder = LoadPokedexOrder(gb);

                entryPreviews = new string[pokedexOrder.Length];
                entries = new byte[pokedexOrder.Length][];
                newEntries = new byte[pokedexOrder.Length][];

                for (int i = 0; i < count; i++)
                {
                    ushort entry = pokedexOrder[i];

                    if (IsIllegalEntry(entry))
                    {
                        entryPreviews[entry] = "- Not An Entry -";
                        entries[entry] = new byte[0];
                        newEntries[entry] = new byte[0];
                    }
                    else
                    {
                        entryPreviews[entry] = LoadPokedexEntryPreview(entry, gb);
                        entries[entry] = LoadRawPokedexEntry(entry, gb);
                        newEntries[entry] = LoadRawPokedexEntry(entry, gb);
                    }
                }


            }

            txtEntry.MaxValue = (uint)pokedexOrder.Length - 1;

            // display dex order
            for (int i = 0; i < count; i++)
            {
                ushort entry = pokedexOrder[i];

                /*if (IsIllegalEntry(entry))
                {
                    listPokedex.Items.Add(entry + " = " + pokemonNames[i] + "***");
                }
                else
                {
                    listPokedex.Items.Add(entry + " = " + pokemonNames[i]);
                }*/

                var item = new ListViewItem(i.ToString());
                item.SubItems.Add(pokemonNames[i]);
                item.SubItems.Add(entry.ToString());
                item.SubItems.Add(entryPreviews[entry]);

                if (IsIllegalEntry(entry)) item.BackColor = Color.LightSalmon;

                listPokedex.Items.Add(item);
            }
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            // sooooo let's give this a try
            int count = Convert.ToInt32(ini[rom.Code, "NumberOfPokemon"]);

            uint tableStart = Convert.ToUInt32(ini[rom.Code, "PokedexData"], 16);
            string format = ini[rom.Code, "PokedexFormat"];

            using (GBABinaryWriter bw = new GBABinaryWriter(rom))
            {
                // write order
                bw.BaseStream.Seek(Convert.ToUInt32(ini[rom.Code, "NationalDexOrder"], 16), SeekOrigin.Begin);

                // skip entry 0 because it does not exist...
                for (int i = 1; i < count; i++)
                {
                    bw.Write(pokedexOrder[i]);
                }

                // attempt to write entries
                bw.BaseStream.Seek(tableStart, SeekOrigin.Begin);
                for (int i = 0; i < count; i++)
                {
                    //bw.BaseStream.Seek(tableStart, SeekOrigin.Begin);
                    //ushort entry = pokedexOrder[i];
                    if (newEntries[i].Length == 0) continue;
                    
                    //bw.Write(entries[i]);
                    bw.Write(newEntries[i]);
                }

            }
        }

        private void bClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool IsIllegalEntry(int dexEntry)
        {
            return (dexEntry >= (pokemonNames.Length - 25 - (pokemonNames.Length > 412 ? 28 : 0)));
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

        private string LoadPokedexEntryPreview(int id, GBABinaryReader br)
        {
            uint tableStart = Convert.ToUInt32(ini[rom.Code, "PokedexData"], 16);
            string format = ini[rom.Code, "PokedexFormat"];

            // load entry
            uint page1Offset;
            if (format == "FRLG" || format == "RS")
            {
                br.BaseStream.Seek(tableStart + id * 36 + 16, SeekOrigin.Begin);
                page1Offset = br.ReadPointer();
            }
            else if (format == "E")
            {
                br.BaseStream.Seek(tableStart + id * 32 + 16, SeekOrigin.Begin);
                page1Offset = br.ReadPointer();
            }
            else
            {
                throw new Exception("Unknown Pokédex format!");
            }

            // load page 1
            List<byte> page = new List<byte>();
            br.BaseStream.Seek(page1Offset, SeekOrigin.Begin);
            while (true)
            {
                // read
                byte b = br.ReadByte();
                page.Add(b);

                // exit?
                if (b == 0xFF) break;
            }

            return TextTable.GetString(page.ToArray());

            // load page 2
            /*if (format == "RS")
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
            }*/

            //return entry;
        }

        private byte[] LoadRawPokedexEntry(int id, GBABinaryReader br)
        {
            uint tableStart = Convert.ToUInt32(ini[rom.Code, "PokedexData"], 16);
            string format = ini[rom.Code, "PokedexFormat"];

            // load entry
            if (format == "FRLG" || format == "RS")
            {
                br.BaseStream.Seek(tableStart + id * 36, SeekOrigin.Begin);
                return br.ReadBytes(36);
            }
            else if (format == "E")
            {
                br.BaseStream.Seek(tableStart + id * 32 + 16, SeekOrigin.Begin);
                return br.ReadBytes(32);
            }
            else
            {
                throw new Exception("Unknown Pokédex format!");
            }
        }

        private void listPokedex_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = -1;
            foreach (int x in listPokedex.SelectedIndices) i = x;
            if (i == -1) return;

            selection = -1;
            txtEntry.Value = pokedexOrder[i];
            selection = i;
        }

        private void txtEntry_TextChanged(object sender, EventArgs e)
        {
            if (selection > -1)
            {
                pokedexOrder[selection] = (ushort)txtEntry.Value;
                newEntries[selection] = entries[txtEntry.Value];

                listPokedex.Items[selection].SubItems[2].Text = txtEntry.Value.ToString();
                //listPokedex.Items[selection].SubItems[3].Text = entryPreviews[txtEntry.Value];

                if (IsIllegalEntry((int)txtEntry.Value)) listPokedex.Items[selection].BackColor = Color.LightSalmon;
                else listPokedex.Items[selection].BackColor = SystemColors.Window;
            }
        }
    }
}
