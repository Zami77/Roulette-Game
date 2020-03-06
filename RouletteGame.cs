using System;

namespace RouletteGame
{
    class RouletteGame
    {
        //struct for each bin, has value as a string to display "00" and char for color 'g' = green
        //Actual values are inserted in the fillTable method
        public struct RouletteBin
        {
            public string value;
            public char color;
        }
        //Created this static class to make rouletteTable a global variable
        public static class RouletteTable
        {
            public static RouletteBin[] rouletteTable = new RouletteBin[38];
            public static int[,] bettingTable = new int[12, 3];
            public static int money = 10;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Roulette Game!");
            fillTable(RouletteTable.rouletteTable);
            fillBetTable(RouletteTable.bettingTable);           
            Bets.PlaceBet();
        }
        
        
        //Prints the table, used to test to make sure all values for the table were inserted correctly
        public static void PrintTable(RouletteBin[] table)
        {
            for (int i = 0; i < table.Length; i++)
            {
                Console.WriteLine($"Value:{table[i].value} Color:{table[i].color}\n");
            }
        }
        public static void PrintTable(int[,] arr)
        {
            for (int row = 0; row < arr.GetLength(0); row++)
            {
                for (int col = 0; col < arr.GetLength(1); col++)
                {
                    Console.Write($"{arr[row,col]}\t");
                }
                Console.WriteLine();
            }
        }
        //Fills the table with proper string values and colors. Used Strings to properly display 00
        public static void fillTable(RouletteBin[] table)
        {
            table[0].value = "0";
            table[0].color = 'g';
            int index = 1;
            //Insert Colors and values into the table
            //The roulette table color alternates every 18 bins
            //1-10 of 18 are even = blacks and odd = red
            //11-18 switches to even = red and odd = black
            //This repeats every 18 bins, hence the nested for loop
            for (int i = 0; i < 2; i++)
            {
                for (int j = 1; j <= 18; j++)
                {
                    
                    if(j <= 10)
                    {
                        if(j % 2 == 0)
                        {
                            //Even black
                            table[index].value = index.ToString();
                            table[index].color = 'b';
                        }
                        else
                        {
                            //Odd Red
                            table[index].value = index.ToString();
                            table[index].color = 'r';
                        }

                    }
                    else
                    {
                        if (j % 2 == 0)
                        {
                            //Even Red
                            table[index].value = index.ToString();
                            table[index].color = 'r';
                        }
                        else
                        {
                            //Odd Black
                            table[index].value = index.ToString();
                            table[index].color = 'b';
                        }
                    }
                    index++;
                }
                
            }
            table[37].value = "00";
            table[37].color = 'g';
        }
        public static void fillBetTable(int[,] table)
        {
            int numToFill = 1;
            for (int row = 0; row < table.GetLength(0); row++)
            {
                for (int col = 0; col < table.GetLength(1); col++)
                {
                    table[row, col] = numToFill++;
                }
            }
        }
        
    
    }
}
