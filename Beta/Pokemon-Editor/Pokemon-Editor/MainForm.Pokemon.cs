using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
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

        // data that is not saved
        string[] types;

        public bool OpenROM(string filename)
        {
            bool success = true;
            ROM temp = null;

            try
            {
                // create a new ROM
                temp = new ROM(filename);

                // check that it is valid
                if (!romInfo.ContainsSection(temp.Code))
                    throw new Exception($"ROM type {temp.Code} is not supported!");

                // TODO: create custom settings
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

            Console.WriteLine("Loading...\n> names");
            LoadNames();

            Console.WriteLine("> base stats");
            LoadPokemon();

            Console.WriteLine("> evolutions");
            LoadEvolutions();

            Console.WriteLine("> types");
            LoadTypes();
        }

        // loads all Pokemon
        void LoadPokemon()
        {
            // get needed info from ini
            var pokemonCount = romInfo.GetInt32(rom.Code, "NumberOfPokemon");
            var firstPokemon = romInfo.GetInt32(rom.Code, "BaseStatsData", 16);

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

        // loads all Pokemon names
        void LoadNames()
        {
            var pokemonCount = romInfo.GetInt32(rom.Code, "NumberOfPokemon");
            var nameTable = romInfo.GetInt32(rom.Code, "PokemonNames", 16);

            rom.Seek(nameTable);
            names = rom.ReadTextTable(11, pokemonCount, CharacterEncoding.English);
        }

        void LoadEvolutions()
        {
            var pokemonCount = romInfo.GetInt32(rom.Code, "NumberOfPokemon");
            var evolutionCount = romInfo.GetInt32(rom.Code, "NumberOfEvolutions");
            var firstEvolution = romInfo.GetInt32(rom.Code, "EvolutionData", 16);

            rom.Seek(firstEvolution);

            evolutions = new Evolution[pokemonCount, evolutionCount];
            for (int i = 0; i < evolutionCount; i++)
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

        void LoadTypes()
        {
            var nameTable = romInfo.GetInt32(rom.Code, "TypeNames", 16);
            var typeChart = romInfo.GetInt32(rom.Code, "TypeChart", 16);

            // first, load the type chart to find the number of types
            var typeCount = 0;

            rom.Seek(typeChart);
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

            Console.WriteLine("number of types: {0}", typeCount);

            // load type names now
            rom.Seek(nameTable);
            types = rom.ReadTextTable(7, typeCount, CharacterEncoding.English);
        }

        #endregion

        #region Saving

        public void SaveAll()
        {
            if (rom == null) return;

            Console.WriteLine("Saving...\n> names");
            SaveNames();

            Console.WriteLine("> base stats");
            SavePokemon();

            Console.WriteLine("> evolutions");
            SaveEvolutions();

            Console.WriteLine("Writing to file.");
            rom.Save();
        }

        unsafe void SavePokemon()
        {
            var firstPokemon = romInfo.GetInt32(rom.Code, "BaseStatsData", 16);

            rom.Seek(firstPokemon);
            for (int i = 0; i < pokemon.Length; i++)
            {
                fixed (Pokemon* pk = &pokemon[i])
                {
                    rom.WriteByte(pk->HP);
                    rom.WriteByte(pk->Attack);
                    rom.WriteByte(pk->Defense);
                    rom.WriteByte(pk->Speed);
                    rom.WriteByte(pk->SpecialAttack);
                    rom.WriteByte(pk->SpecialDefense);
                    rom.WriteByte(pk->Type);
                    rom.WriteByte(pk->Type2);
                    rom.WriteByte(pk->CatchRate);
                    rom.WriteByte(pk->BaseExperience);
                    rom.WriteUInt16(pk->EffortYield);
                    rom.WriteUInt16(pk->HeldItem);
                    rom.WriteUInt16(pk->HeldItem2);
                    rom.WriteByte(pk->GenderRatio);
                    rom.WriteByte(pk->EggCycles);
                    rom.WriteByte(pk->BaseFriendship);
                    rom.WriteByte(pk->LevelRate);
                    rom.WriteByte(pk->EggGroup);
                    rom.WriteByte(pk->EggGroup2);
                    rom.WriteByte(pk->Ability);
                    rom.WriteByte(pk->Ability2);
                    rom.WriteByte(pk->RunRate);
                    rom.WriteByte(pk->ColorFlip);
                    rom.WriteUInt16(pk->Padding);
                }
            }
        }

        void SaveNames()
        {
            var nameTable = romInfo.GetInt32(rom.Code, "PokemonNames", 16);

            rom.Seek(nameTable);
            rom.WriteTextTable(names, 11, CharacterEncoding.English);
        }

        unsafe void SaveEvolutions()
        {
            var pokemonCount = romInfo.GetInt32(rom.Code, "NumberOfPokemon");
            var evolutionCount = romInfo.GetInt32(rom.Code, "NumberOfEvolutions");
            var firstEvolution = romInfo.GetInt32(rom.Code, "EvolutionData", 16);

            rom.Seek(firstEvolution);
            for (int i = 0; i < pokemonCount; i++)
            {
                for (int j = 0; j < evolutionCount; j++)
                {
                    fixed (Evolution* e = &evolutions[i, j])
                    {
                        rom.WriteUInt16(e->Method);
                        rom.WriteUInt16(e->Parameter);
                        rom.WriteUInt16(e->Target);
                        rom.WriteUInt16(e->Padding);
                    }
                }
            }
        }

        #endregion
    }
}
