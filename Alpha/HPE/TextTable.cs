using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPE
{
    public class TextTable
    {
        private static string[] table = new string[] { " ", "À", "Á", "Â", "Ç", "È", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "é", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "&", "+", "", "", "", "", "", "[Lv]", "=", "よ", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "[pk]", "[mn]", "[po]", "[ké]", "[bl]", "[oc]", "[k]", "", "%", "(", ")", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "[U]", "[D]", "[L]", "[R]", "", "", "", "", "", "", "", "", "<", ">", "", "", "", "ゲ", "ゴ", "ザ", "ジ", "ズ", "ゼ", "ゾ", "ダ", "ヂ", "ヅ", "デ", "ド", "バ", "ビ", "ブ", "ベ", "ボ", "パ", "ピ", "プ", "ペ", "ポ", "ッ", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "!", "?", ".", "-", "·", "[...]", "“", "”", "‘", "'", "♂", "♀", "$", ",", "*", "/", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "►", ":", "Ä", "Ö", "Ü", "ä", "ö", "ü", "↑", "↓", "←", "→", "[nb2]", "[FC]", "[FD]", "\\n", "\\x" };
        //                                                                                                                                                               1B                                                                                                              35                                                                                                                          53                                                                                                                                                                                                                                                                                                                               94                                                               9F    A0   A1                                           AA                                     B1  B2   B3   B4    B5   B6   B7                  BB                                                                                                                           D4   D5                                                                                                                           EE   EF   F0   F1   F2   F3   F4   F5   F6   F7   F8   F9   FA    FB      FC      FD      FE     FF

        public static string GetString(byte[] bytes)
        {
            string s = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                if (bytes[i] == 0xFF) break;
                s += table[bytes[i]];
            }
            return s;
        }

        public static byte[] GetBytes(string s)
        {
            List<string> trans = new List<string>();
            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                if (c == '[')
                {
                    string ss = "";// = c.ToString();
                    while (i < s.Length && c != ']')
                    {
                        ss += c.ToString();

                        i++;
                        c = s[i];
                    }
                    trans.Add(ss + c);
                }
                else if (c == '\\')
                {
                    i++;
                    trans.Add("\\" + s[i]);
                }
                else
                {
                    trans.Add(c.ToString());
                }
            }

            byte[] buffer = new byte[trans.Count];
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = GetByte(trans[i]);
            }

            return buffer;
        }

        public static byte[] GetBytes(string s, int length)
        {
            // get bytes
            byte[] b = GetBytes(s);

            // adjust to desired size
            if (b.Length == length)
            {
                // if already there, make sure the last byte is 0xFF
                b[length - 1] = 0xFF;
            }
            else if (b.Length > length)
            {
                // if greater, shrink and make 0xFF last
                Array.Resize(ref b, length);
                b[length - 1] = 0xFF;
            }
            else if (b.Length < length)
            {
                // if less, grow and fill with 0xFF
                int original = b.Length;
                Array.Resize(ref b, length);
                b[original] = 0xFF;
                //for (int i = original; i < length; i++) b[i] = 0xFF;
            }
            return b;

            /*if (b.Length != length)
            {
                List<byte> buffer = b.ToList();
                if (b.Length > length)
                {
                    Array.Resize(ref b, length);
                    return b;
                }
                else if (b.Length < length)
                {
                    buffer.Add(0xFF);
                    for (int i = 1; i < length - buffer.Count - 1; i++)
                    {
                        buffer.Add(00);
                    }
                    return buffer.ToArray();
                }

                return buffer.ToArray();
            }
            else
            {
                return b;
            }*/
        }

        public static byte GetByte(string s)
        {
            byte b = 0;
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i] == s)
                {
                    b = (byte)i;
                    break;
                }
            }
            return b;
        }
    }
}
