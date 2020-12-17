using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WindowsInput;
using WindowsInput.Native;

namespace MeliMorse.App
{
    class Program
    {

        //https://morsecode.world/international/morse.html
        static Dictionary<string, string> morseTable = new Dictionary<string, string>();

        static void Main(string[] args)
        {

            bool continueMessage = true;

            string message = string.Empty;

            do
            {

                Stopwatch stopwatch = new Stopwatch();

                stopwatch.Start();
                string inputStr = Console.ReadLine();
                stopwatch.Stop();

                long pause = (stopwatch.ElapsedMilliseconds / 500);

                string zeros = string.Empty;

                for (int i = 0; i < pause; i++)
                {
                    zeros += "0";
                    //Console.Write("0");
                }

                message += inputStr + zeros;

                Console.Clear();
                Console.Write(message);

                continueMessage = pause < 10;
                    

            } while (continueMessage);

            string example1 = ".... --- .-.. .-     -- . .-.. ..";
            string[] spplited = example1.Split(" ");

            InitMorseDictionary();

            string input = "000000001101101100111000001111110001111110011111100000001110111111110111011100000001100011111100000111111001111110000000110000110111111110111011100000011011100000000000";

            //string input = "0000000011100000000";

            string inputWithFullStop = "000000001101101100111000001111110001111110011111100000001110011111110001100111111000110011111100000001110111111110111011100000001100011111100000111111001111110000000110000110111111110111011100000011011100000000000";

            input = message;

            var morseBits = input.ToCharArray();

            //var morseBits = inputWithFullStop.ToCharArray();

            //string morseCode = string.Empty;
            string example = ".... --- .-.. .- -- . .-.. ..";
            string letterSeparator = " ";
            int oneTiming = 0, zeroTimming = 0, oneMinSequence = 0, oneMaxSequence = 0, zeroMinSequence = 0, zeroMaxSequence = 0;

            List<KeyValuePair<int, int>> bitCount = new List<KeyValuePair<int, int>>();

            for (int i = 0; i < morseBits.Length; i++)
            {

                int number = int.Parse(morseBits[i].ToString());

                //if (zeroTimming > zeroMaxDelay)
                //    break;

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

                    //if (zeroMinSequence == 0 && zeroMaxSequence == 0 && zeroTimming > 0)
                    //{
                    //    zeroMinSequence = zeroMaxSequence = zeroTimming;
                    //    bitCount.Add(new KeyValuePair<int, int>(0, zeroTimming));
                    //}
                    //else if (zeroTimming != 0)
                    //{
                    //    if (zeroTimming > zeroMaxSequence)
                    //        zeroMaxSequence = zeroTimming;
                    //    else if (zeroTimming < zeroMinSequence)
                    //        zeroMinSequence = zeroTimming;
                    //
                    //    bitCount.Add(new KeyValuePair<int, int>(0, zeroTimming));
                    //}

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

                    //if (oneMinSequence == 0 && oneMaxSequence == 0 && oneTiming > 0)
                    //{
                    //    oneMinSequence = oneMaxSequence = oneTiming;
                    //    bitCount.Add(new KeyValuePair<int, int>(1, oneTiming));
                    //}
                    //else if (oneTiming != 0)
                    //{
                    //    if (oneTiming > oneMaxSequence)
                    //        oneMaxSequence = oneTiming;
                    //    else if (oneTiming < oneMinSequence)
                    //        oneMinSequence = oneTiming;
                    //
                    //
                    //    bitCount.Add(new KeyValuePair<int, int>(1, oneTiming));
                    //
                    //}

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

                    //if (item.Value == 1 || (oneMaxSequence - item.Value >= oneDifference))

                    if (item.Value == 1 || (item.Value - oneMinSequence < 2))
                        morseParagraph += ".";
                    else
                        morseParagraph += "-";

                }

                if (item.Key == 0 && morseParagraph != string.Empty)
                {
                    if (item.Value > zeroMinSequence + 2)
                    {
                        morseParagraph += letterSeparator;
                    }
                }

            }

            List<string> wordsToTRanslate = morseParagraph.Split(letterSeparator).ToList();

            foreach (var item in wordsToTRanslate)
            {
                string letter = TranslateMorseToLetter(item);
                if (letter == "stop")
                    break;

                message += letter;
            }

            Console.WriteLine($"Original : {example}");
            Console.WriteLine($"Decoded  : {morseParagraph}");
            Console.WriteLine($"Translated: {message}");

            Action ShowResults = () =>
            {
                Console.WriteLine(" -----  One -----");
                Console.WriteLine($"Menor One: { oneMinSequence}");
                Console.WriteLine($"Mayor One: { oneMaxSequence}");
                Console.WriteLine(" -----  Zero -----");
                Console.WriteLine($"Menor Zero: { zeroMinSequence}");
                Console.WriteLine($"Mayor Zero: { zeroMaxSequence}");
            };

            Console.ReadLine();

        }

        static string TranslateMorseToLetter(string morseCode)
        {
            morseTable.TryGetValue(morseCode, out string letter);
            return letter;
        }

        static Dictionary<string, string> InitMorseDictionary()
        {
            morseTable = new Dictionary<string, string>()
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
