using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;


namespace Cripto
{
    class Program
    {
        static bool monobitTest(string binarystring)
        {
            int count = 0;
            for (int i = 0; i < 20000; i++)
            {
                if (binarystring[i] == '1') count++;
            }
            if (count > 9654 && count < 10346) return true;
            else return false;
        }

        static bool pokerTest(string binarystring)
        {
            int[] count = new int[16];
            int countTotal = 0;
            for (int i = 0; i < 16; i++)
            {
                count[i] = 0;
            }
            for (int i = 0; i < 20000; i+=4)
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append($"{binarystring[i]}");
                stringBuilder.Append($"{binarystring[i + 1]}");
                stringBuilder.Append($"{binarystring[i + 2]}");
                stringBuilder.Append($"{binarystring[i + 3]}");
                int number = Convert.ToInt32(Convert.ToString(stringBuilder), 2);
                count[number]++;
            }
            for (int i = 0; i < 16; i++)
            {
                countTotal += Convert.ToInt32(Math.Pow(count[i], 2));
            }
            double x = (16.0 / 5000.0) * (countTotal * 1.0) - 5000.0;
            if (x > 1.03 && x < 57.4) return true;
            else return false;
        }

        static bool runsTest(string binarystring)
        {
            int countZero = 0;
            int countUm = 0;
            int[] countZeroVetor = new int[6];
            int[] countUmVetor = new int[6];
            for (int i = 0; i < 20000; i ++)
            {
                if (binarystring[i] == '0')
                {
                    countZero++;
                    if(i > 0 && countUm > 0)
                    {
                        if (countUm > 6) countUmVetor[5]++;
                        else countUmVetor[countUm - 1]++;
                    }
                    countUm = 0;
                }
                else
                {
                    countUm++;
                    if (i > 0 && countZero > 0)
                    {
                        if (countZero > 6) countZeroVetor[5]++;
                        else countZeroVetor[countZero - 1]++;
                    }
                    countZero = 0;
                }
            }
            if (countZeroVetor[0] > 2267 && countZeroVetor[0] < 2733 && countZeroVetor[1] > 1079 && countZeroVetor[1] < 1421 
                && countZeroVetor[2] > 502 && countZeroVetor[2] < 748 && countZeroVetor[3] > 223 && countZeroVetor[3] < 402 
                && countZeroVetor[4] > 90 && countZeroVetor[4] < 223 && countZeroVetor[5] > 90 && countZeroVetor[5] < 223 
                && countUmVetor[0] > 2267 && countUmVetor[0] < 2733 && countUmVetor[1] > 1079 && countUmVetor[1] < 1421
                && countUmVetor[2] > 502 && countUmVetor[2] < 748 && countUmVetor[3] > 223 && countUmVetor[3] < 402
                && countUmVetor[4] > 90 && countUmVetor[4] < 223 && countUmVetor[5] > 90 && countUmVetor[5] < 223) return true;
            else return false;
        }

        static bool longRunTest(string binarystring)
        {
            int countZero = 0;
            int countUm = 0;
            for (int i = 0; i < 20000; i++)
            {
                if (binarystring[i] == '0')
                {
                    countZero++;
                    if (countZero == 34) return false;
                    countUm = 0;
                }
                else
                {
                    countUm++;
                    if (countUm == 34) return false;
                    countZero = 0;
                }
            }
            return true;
        }

        static void Main(string[] args) { 


            string textFile = @"D:\RepositorioTestes\Cripto\Cripto\Chaves.txt";
            string[] hexstring = File.ReadAllLines(textFile);
            string[] binarystring = new string[20];
            for (int i = 0; i < 20; i++) {
                binarystring[i] = String.Join(String.Empty,hexstring[i].Trim('\'').Select(
                  c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
                Console.WriteLine( (i+1) + "- Monobit Test: " + monobitTest(binarystring[i]) + "   - Poker Test: " + pokerTest(binarystring[i]) + "   - Runs Test: " + runsTest(binarystring[i]) + "   - LongRun Test: " + longRunTest(binarystring[i]));
                if (monobitTest(binarystring[i]) && pokerTest(binarystring[i]) && runsTest(binarystring[i]) && longRunTest(binarystring[i])) Console.WriteLine("APROVADO");
                else Console.WriteLine("REPROVADO");
            }

        }
    }
}
