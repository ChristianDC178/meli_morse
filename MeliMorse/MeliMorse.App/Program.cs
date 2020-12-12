using System;
using System.Collections.Generic;

namespace MeliMorse.App
{
    class Program
    {
        static void Main(string[] args)
        {

            //https://morsecode.world/international/morse.html

            //Input: 1111 
            //Calculation: 4  + 2 > 4 
            //Yes then dot 
            //No then line

            var diccio = GetMorseCharacters();

            string input = "000000001101101100111000001111110001111110011111100000001110111111110111011100000001100011111100000111111001111110000000110000110111111110111011100000011011100000000000";

            int bitsCorrector = 2;

            var morseBits = input.ToCharArray();


            int oneTiming = 0;
            string morseCode = string.Empty;
            string example = ".... --- .-.. .- -- . .-.. ..";

            for (int i = 0; i < morseBits.Length; i++)
            {

                int number = int.Parse(morseBits[i].ToString());

                if (number == 1)
                {
                    oneTiming++;
                }
                else
                {
                    morseCode += GetMorseChar(oneTiming);
                    oneTiming = 0;
                }
            }

            Console.WriteLine($"Example: {example}");
            Console.WriteLine($"Decoded: {morseCode}");
            Console.ReadLine();

        }

         static string GetMorseChar(int timing)
        {
            if (timing < 2)
                return "";

            if (timing >= 1 && timing <= 3)
                return ".";

            return "-";

        }


        static Dictionary<string, string> GetMorseCharacters()
        {
            Dictionary<string, string> morseTable = new Dictionary<string, string>()
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

            return morseTable;
        }


    }
}
