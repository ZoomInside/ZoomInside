﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoomInside
{
    public class ExtractingCyrillic
    {
        public string Format(string text)
        {
            var item_to_find = "annotations";
            var index = text.IndexOf(item_to_find);

            var substract = text.Substring(index);
            substract = substract.Replace("[", "");
            substract = substract.Replace("]", "");
            substract = substract.Replace("\"", "");
            substract = substract.Replace("\\", "");
            substract = substract.Replace("\r\n", "");

            substract = substract.Remove(0, 13);
            substract = substract.Remove(substract.Length - 1);

            return substract;
        }

        public string ToCyrillic(string substract)
        {
            char[] splitSymbols = { ',', '-', '!', '?', '/', 'u' };
            string[] result = substract.Split(splitSymbols);

            // The data with the unicode for the different letters
            var unicodeDictionary = new Dictionary<string, string>()
            {
                {"0410", "А" },
                {"0411", "Б" },
                {"0412", "В" },
                {"0413", "Г" },
                {"0414", "Д" },
                {"0415", "Е" },
                {"0416", "Ж" },
                {"0417", "З" },
                {"0418", "И" },
                {"0419", "Й" },
                {"041a", "К" },
                {"041b", "Л" },
                {"041c", "М" },
                {"041d", "Н" },
                {"041e", "О" },
                {"041f", "П" },
                {"0420", "Р" },
                {"0421", "С" },
                {"0422", "Т" },
                {"0423", "у" },
                {"0424", "Ф" },
                {"0425", "Х" },
                {"0426", "Ц" },
                {"0427", "Ч" },
                {"0428", "Ш" },
                {"0429", "Щ" },
                {"042a", "Ъ" },
                {"042c", "Ь" },
                {"042e", "Ю" },
                {"042f", "Я" },
            };

            // Printing the phrase
            string final = "";
            int i = 0;
            foreach (var item in result)
            {
                // Check where to put a space or dot
                if (item.Contains(' '))
                {
                    var spaceIndex = result[i].IndexOf(' ');
                    result[i] = result[i].Remove(spaceIndex, 1);
                    //Console.Write(' ');
                    final += ' ';
                }
                if (item.Contains('.'))
                {
                    var spaceIndex = result[i].IndexOf('.');
                    result[i] = result[i].Remove(spaceIndex, 1);
                    //Console.Write('.');
                    final += '.';
                }

                // Print the word
                if (unicodeDictionary.ContainsKey(result[i]))
                    final += unicodeDictionary[result[i]];
                    //Console.Write(unicodeDictionary[result[i]]);

                i++;
            }

            return final;
        }
    }
}