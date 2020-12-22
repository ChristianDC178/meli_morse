using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MeliMorse.Translator
{
    public static class MorseDecoder
    {

        static Dictionary<string, string> _morseTable = new Dictionary<string, string>();
        static string LetterSeparator = " ";
        static string WordSeparator = "    ";

        static MorseDecoder()
        {
            InitMorseDictionary();
        }

        public static string TranslateMorseToLetter(string morseCode)
        {
            if (!_morseTable.TryGetValue(morseCode, out string letter))
                return "?";

            return letter;
        }

        public static string decodeBits2Morse(string morse)
        {
            int oneTiming = 0, zeroTimming = 0, oneMinSequence = 0, zeroMinSequence = 0;
            var morseBits = morse.ToCharArray();

            List<KeyValuePair<int, int>> bitCount = new List<KeyValuePair<int, int>>();

            for (int i = 0; i < morseBits.Length; i++)
            {

                int number = int.Parse(morseBits[i].ToString());

                if (number == 1)
                {
                    oneTiming++;

                    if (zeroTimming > 0)
                    {

                        if (zeroMinSequence == 0 || zeroMinSequence > zeroTimming)
                            zeroMinSequence = zeroTimming;

                        bitCount.Add(new KeyValuePair<int, int>(0, zeroTimming));

                        zeroTimming = 0;
                    }

                }
                else if (number == 0)
                {

                    zeroTimming++;

                    if (oneTiming > 0)
                    {

                        if (oneMinSequence == 0 || oneMinSequence > oneTiming)
                            oneMinSequence = oneTiming;

                        bitCount.Add(new KeyValuePair<int, int>(1, oneTiming));

                        oneTiming = 0;
                    }

                }
            }

            string morseParagraph = string.Empty;

            foreach (var item in bitCount)
            {
                if (item.Key == 1)
                {
                    if (item.Value == 1 || (item.Value - oneMinSequence < 2))
                        morseParagraph += ".";
                    else
                        morseParagraph += "-";
                }

                if (item.Key == 0 && morseParagraph != string.Empty)
                {
                    if (item.Value > zeroMinSequence + 2)
                    {
                        morseParagraph += LetterSeparator;
                    }
                }
            }

            return morseParagraph;

        }

        public static string translate2Human(string morseParagraph)
        {

            string[] words = morseParagraph.Split(WordSeparator);
            string message = string.Empty;

            foreach (var item in words)
            {

                string[] wordLetters = item.Split(LetterSeparator);

                foreach (var letter in wordLetters)
                {
                    message += TranslateMorseToLetter(letter);
                }

                message += LetterSeparator;

            }

            return message;
        }

        public static string traslate2Morse(string text)
        {
            var letters = text.ToCharArray();
            string morse = string.Empty;

            foreach (var item in letters)
            {

                if (item.ToString() == LetterSeparator)
                    morse += LetterSeparator;
                else
                {
                    KeyValuePair<string, string> found = _morseTable.FirstOrDefault(v => v.Value == item.ToString().ToUpper());

                    if (found.Key != null)
                    {
                        morse += found.Key + LetterSeparator;
                    }
                }
            }

            return morse;

        }

        static Dictionary<string, string> InitMorseDictionary()
        {
            _morseTable = new Dictionary<string, string>()
            {
                [".-"] = "A",
                ["-..."] = "B",
                ["-.-."] = "C",
                ["-.."] = "D",
                ["."] = "E",
                ["..-."] = "F",
                ["--."] = "G",
                ["...."] = "H",
                [".."] = "I",
                [".---"] = "J",
                ["-.-"] = "K",
                [".-.."] = "L",
                ["--"] = "M",
                ["-."] = "N",
                ["---"] = "O",
                [".--."] = "P",
                ["--.-"] = "Q",
                [".-."] = "R",
                ["..."] = "S",
                ["-"] = "T",
                ["..-"] = "U",
                ["...-"] = "V",
                [".--"] = "W",
                ["-..-"] = "X",
                ["-.--"] = "Y",
                ["--.."] = "Z",
                ["-----"] = "0",
                [".----"] = "1",
                ["..---"] = "2",
                ["...--"] = "3",
                ["....-"] = "4",
                ["......"] = "5",
                ["-...."] = "6",
                ["--..."] = "7",
                ["---.."] = "8",
                ["----."] = "9",
                [".-.-.-"] = "stop"
            };

            return _morseTable;
        }

    }
}
