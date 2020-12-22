using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MeliMorse.Translator;

namespace MeliMorse.App
{
    class Program
    {

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

                long pause = (stopwatch.ElapsedMilliseconds / 800);

                string zeros = string.Empty;

                for (int i = 0; i < pause; i++)
                {
                    zeros += "0";
                }

                message += inputStr + zeros;

                Console.Clear();
                Console.Write(message);

                continueMessage = pause < 10;

            } while (continueMessage);


            string morseParagraph = MorseDecoder.decodeBits2Morse(message);

            List<string> wordsToTRanslate = morseParagraph.Split(MorseDecoder.LetterSeparator).ToList();

            string traslated = string.Empty;

            foreach (var item in wordsToTRanslate)
            {
                string letter = MorseDecoder.TranslateMorseToLetter(item);
                if (letter == "stop")
                    break;

                traslated += letter;
            }

            Console.WriteLine($"Decoded  : {morseParagraph}");
            Console.WriteLine($"Input Traslated: {traslated}");
            Console.ReadLine();
        }

        public static void TestMeliMessage()
        {
            string meliMessage = "000000001101101100111000001111110001111110011111100000001110111111110111011100000001100011111100000111111001111110000000110000110111111110111011100000011011100000000000";
            Console.WriteLine(MorseDecoder.decodeBits2Morse(meliMessage));
        }

        public static void Test_MessageWithStop()
        {
            string inputWithFullStop = "000000001101101100111000001111110001111110011111100000001110011111110001100111111000110011111100000001110111111110111011100000001100011111100000111111001111110000000110000110111111110111011100000011011100000000000";
            Console.WriteLine(MorseDecoder.decodeBits2Morse(inputWithFullStop));
        }


    }
}
