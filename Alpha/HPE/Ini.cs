using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Windows.Forms;

namespace HPE
{
    public class Ini
    {
        private List<Section> sections;

        public Ini()
        {
            sections = new List<Section>();
        }

        public Ini(string file)
        {
            sections = new List<Section>();
            Load(file);
        }

        public void Load(string file)
        {
            using (StreamReader sr = File.OpenText(file))
            {
                sections.Clear();
                int lineNo = 0;
                Section section = null;
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine().Trim();
                    lineNo += 1;

                    // comment
                    if (line.Contains("#"))
                    {
                        int index = line.IndexOf("#");
                        line = line.Remove(index).TrimEnd();
                    }

                    // check if it's empty
                    if (string.IsNullOrEmpty(line) || string.IsNullOrWhiteSpace(line)) continue;

                    // parse
                    if (line.StartsWith("[") && line.EndsWith("]"))
                    {
                        // add current section to collection so we can start anew
                        if (section != null)
                        {
                            sections.Add(section);
                        }

                        // get section name
                        section = new Section(line.Substring(1, line.Length - 2));
                    }
                    else if (line.Contains("="))
                    {
                        /*string[] parts = line.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

                        if (parts.Length != 2) throw new Exception("Line " + lineNo + ": Bad section entry format!");

                        if (section == null) throw new Exception("Line " + lineNo + ": Cannot declare an entry without being in a section!");

                        // add or replace this entry
                        if (section.entries.ContainsKey(parts[0])) section.entries[parts[0]] = parts[1];
                        else section.entries.Add(parts[0], parts[1]);*/

                        // new method:
                        int index1 = line.IndexOf('=');
                        string key = line.Substring(0, index1);
                        string value = line.Substring(index1 + 1);

                        if (section.entries.ContainsKey(key)) section.entries[key] = value;
                        else section.entries.Add(key, value);
                    }
                    else
                    {
                        throw new Exception("Unsure how to parse line " + lineNo + "!");
                    }
                }

                // add final section
                if (section != null)
                {
                    sections.Add(section);
                }
            }
        }

        public void Save(string file)
        {
            using (StreamWriter sw = File.CreateText(file))
            {
                sw.Flush();

                // if there's nothing, it will be a blank file...
                if (sections.Count > 0)
                {
                    // write each section
                    foreach (Section section in sections)
                    {
                        // write section name
                        sw.WriteLine("[" + section.name + "]");

                        // write keys
                        if (section.entries.Count > 0)
                        {
                            foreach (string key in section.entries.Keys)
                            {
                                sw.WriteLine(key + "=" + section.entries[key]);
                            }
                        }

                        // add some buffer space
                        sw.WriteLine();
                    }
                }
            }
        }

        public Dictionary<string, string>.KeyCollection this[string section]
        {
            get
            {
                for (int i = 0; i < sections.Count; i++)
                {
                    if (sections[i].name == section)
                    {
                        return sections[i].entries.Keys;
                    }
                }

                return null;
            }
        }

        public string this[string section, string key]
        {
            get
            {
                for (int i = 0; i < sections.Count; i++)
                {
                    if (sections[i].name == section)
                    {
                        return sections[i].entries[key];
                    }
                }

                return string.Empty;
            }
            set
            {
                bool sectionExists = false;
                for (int i = 0; i < sections.Count; i++)
                {
                    if (sections[i].name == section)
                    {
                        sectionExists = true;
                        if (sections[i].entries.ContainsKey(key))
                            sections[i].entries[key] = value;
                        else
                            sections[i].entries.Add(key, value);
                    }
                }

                if (!sectionExists)
                {
                    Section s = new Section(section);
                    s.entries.Add(key, value);
                    sections.Add(s);
                }
            }
        }

        public TreeNode ToTreeNodes(string parent)
        {
            TreeNode root = new TreeNode(parent);
            for (int i = 0; i < sections.Count; i++)
            {
                TreeNode section = new TreeNode(sections[i].name);
                foreach (string key in sections[i].entries.Keys)
                {
                    TreeNode kEy = new TreeNode(key);
                    kEy.Nodes.Add(new TreeNode(sections[i].entries[key]));
                    section.Nodes.Add(kEy);
                }
                root.Nodes.Add(section);
            }
            return root;
        }

        public string[] GetSectionNames()
        {
            string[] sectionNames = new string[sections.Count];
            for (int i = 0; i < sectionNames.Length; i++)
            {
                sectionNames[i] = sections[i].name;
            }
            return sectionNames;
        }

        public bool CopySectionTo(string section, Ini destination)
        {
            for (int i = 0; i < sections.Count; i++)
            {
                if (sections[i].name == section)
                {
                    /*foreach (var x in sections[i].entries)
                    {
                        destination[section, x.Key] = x.Value;
                    }*/
                    destination.sections.Add(sections[i]);
                    return true;
                }
            }
            return false;
        }

        public bool ContainsSection(string section)
        {
            foreach (Section s in sections)
            {
                if (s.name == section) return true;
            }
            return false;
        }

        public class Section
        {
            public Dictionary<string, string> entries;
            public string name;

            public Section(string name)
            {
                this.name = name;
                this.entries = new Dictionary<string, string>();
            }
        }
    }
}
