using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeliMorse.App
{
    class Program
    {
        static void Main(string[] args)
        {

            //https://morsecode.world/international/morse.html
            string input = "000000001101101100111000001111110001111110011111100000001110111111110111011100000001100011111100000111111001111110000000110000110111111110111011100000011011100000000000";
            var morseBits = input.ToCharArray();

            int difference = 3;
            string morseCode = string.Empty;
            string example = ".... --- .-.. .- -- . .-.. ..";
            string trimmed = "....---.-...---..-....";


            int oneTiming = 0, zeroTimming = 0;
            int oneMinSequence = 0, oneMaxSequence = 0, zeroMinSequence = 0, zeroMaxSequence = 0;

            List<KeyValuePair<int, int>> bitCount = new List<KeyValuePair<int, int>>();

            for (int i = 0; i < morseBits.Length; i++)
            {

                int number = int.Parse(morseBits[i].ToString());

                if (number == 1)
                {
                    oneTiming++;
                    
                    if (zeroMinSequence == 0 && zeroMaxSequence == 0 && zeroTimming > 0)
                    {
                        zeroMinSequence = zeroMaxSequence = zeroTimming;
                        bitCount.Add(new KeyValuePair<int, int>(0, zeroTimming));
                    }
                    else if (zeroTimming != 0)
                    {
                        if (zeroTimming > zeroMaxSequence)
                            zeroMaxSequence = zeroTimming;
                        else if (zeroTimming < zeroMinSequence)
                            zeroMinSequence = zeroTimming;

                        bitCount.Add(new KeyValuePair<int, int>(0, zeroTimming));
                    }

                    zeroTimming = 0;

                }
                else if( number == 0)
                {
                    
                    zeroTimming++;

                    if (oneMinSequence == 0 && oneMaxSequence == 0 && oneTiming > 0)
                    {
                        oneMinSequence = oneMaxSequence = oneTiming;
                        bitCount.Add(new KeyValuePair<int, int>(1, oneTiming));
                    }
                    else if(oneTiming != 0)
                    {
                        if (oneTiming > oneMaxSequence)
                            oneMaxSequence = oneTiming;
                        else if (oneTiming < oneMinSequence)
                            oneMinSequence = oneTiming;


                        bitCount.Add(new KeyValuePair<int, int>(1, oneTiming));

                    }

                    oneTiming = 0;
                }
            }

            zeroTimming = 0;
            oneTiming = 0;

            string morseParagraph = string.Empty;

            //Una vez que tengo todas las variantes de timing paso a decodificar 
            foreach (var item in bitCount)
            {

                if (item.Key == 1)
                {
                    //aca sacamos si es punto o letra
                    //punto , letra

                    if (oneMaxSequence - item.Value >= difference)
                        morseParagraph += ".";
                    else
                        morseParagraph += "-";
                }

                //if (item.Key == 0)
                //{ 
                // //aca sacamos la cuenta si es espacio entre caracter, letra o palabra
                //}

            }


            Console.WriteLine($"Original: {example}");
            Console.WriteLine($"Original Trimmed: {trimmed}");
            Console.WriteLine($"Decoded         : {morseParagraph}");
            Console.WriteLine(" -----  One -----");
            Console.WriteLine($"Menor One: { oneMinSequence}");
            Console.WriteLine($"Mayor One: { oneMaxSequence}");
            Console.WriteLine(" -----  Zero -----");
            Console.WriteLine($"Menor Zero: { zeroMinSequence}");
            Console.WriteLine($"Mayor Zero: { zeroMaxSequence}");

            Console.WriteLine("-----  BIT Count ------");

            StringBuilder sb = new StringBuilder();

            foreach (var item in bitCount)
            {
                Console.WriteLine($"Bit: {item.Key} -- Timing: {item.Value} ");
                sb.AppendLine(string.Concat(Enumerable.Repeat(item.Key.ToString(), item.Value)));
            }

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
