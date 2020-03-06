using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace RouletteGame
{
    class Bets
    {
        //Gets a random rolled number between the indexes of the global table array
        public static int RollBall()
        {
            System.Random rand = new Random();
            int rolledNum = rand.Next(0, 38);
            return rolledNum;
        }
        //Overarching selection of bet types
        public static void PlaceBet()
        {
            Console.WriteLine($"Current Money:{RouletteGame.RouletteTable.money}");
            Console.WriteLine("***************************************");
            Console.WriteLine("Please enter which bet!");
            Console.WriteLine("***************************************");
            Console.WriteLine("1. Numbers: the number of the bin");
            Console.WriteLine("2. Evens/Odds: even or odd numbers");
            Console.WriteLine("3. Reds/Blacks: red or black colored numbers");
            Console.WriteLine("4. Lows/Highs: low (1-18) or high (19-38) numbers.");
            Console.WriteLine("5. Dozens: row thirds, 1-12, 13-24, 25-36");
            Console.WriteLine("6. Columns: First, second, or third columns");
            Console.WriteLine("7. Street: rows, e.g., 1/2/3 or 22/23/24");
            Console.WriteLine("8. 6 Numbers: double rows, e.g., 1/2/3/4/5/6 or 22/23/24/25/26/26");
            Console.WriteLine("9. Split: at the edge of any two contiguous numbers, e.g., 1/2, 11/14, and 35/36");
            Console.WriteLine("10. Corner: at the intersection of any four contiguous numbers, e.g., 1/2/4/5, or 23/24/26/27");
            int bet;
            try { bet = Convert.ToInt32(Console.ReadLine()); }
            catch (Exception ex)
            {
                throw new Exception("Incorrect Input", ex);
            }

            switch (bet)
            {
                case 1:
                    Numbers();
                    break;
                case 2:
                    EvenOdd();
                    break;
                case 3:
                    RedBlack();
                    break;
                case 4:
                    LowHigh();
                    break;
                case 5:
                    Dozen();
                    break;
                case 6:
                    Column();
                    break;
                case 7:
                    Street();
                    break;
                case 8:
                    SixNumbers();
                    break;
                case 9:
                    Split();
                    break;
                case 10:
                    Corner();
                    break;
                default:
                    Console.WriteLine("Wrong Input, please enter a number between 1 and 10...");
                    PlaceBet();
                    return;
            }

        }
        //Bet on a specific number
        public static void Numbers()
        {
            Console.WriteLine("What number are you betting on?");
            string betValue = Console.ReadLine();
            if (betValue == "00") betValue = "37";
            int bet;
            try
            {
                bet = Convert.ToInt32(betValue);
            }
            catch (Exception ex)
            {
                throw new Exception("Incorrect Input", ex);
            }
            if(bet < 0 || bet > 37)
            {
                Console.WriteLine("Incorrect Input, try again");
                Numbers();
            }
            int rolledNum = RollBall();
            Console.WriteLine($"You bet {RouletteGame.RouletteTable.rouletteTable[bet].value} and we rolled {RouletteGame.RouletteTable.rouletteTable[rolledNum].value}");
            if (bet == rolledNum)
                Console.WriteLine("You win! try again? (press y to bet again)");
            else
                Console.WriteLine("You lost. try again? (press y to bet again)");

            if (Console.ReadLine().ToLower() == "y")
                PlaceBet();

        }
        //Bet on whether number is even or odd
        public static void EvenOdd()
        {
            Console.WriteLine("Odd(o) or Even(e)? (type o or e)");
            string bet = Console.ReadLine().ToLower();
            bool betEven = true;
            if (bet == "o")
                betEven = false;
            int rolledNum = RollBall();
            Console.WriteLine($"You bet {(betEven ? "Even" : "Odd")} and we rolled {RouletteGame.RouletteTable.rouletteTable[rolledNum].value} {PrintBinColor(rolledNum)}");
            if ((betEven && (Convert.ToInt32(RouletteGame.RouletteTable.rouletteTable[rolledNum].value) % 2 == 0) && (Convert.ToInt32(RouletteGame.RouletteTable.rouletteTable[rolledNum].value) != 0))
                || (!betEven && (Convert.ToInt32(RouletteGame.RouletteTable.rouletteTable[rolledNum].value) % 2 == 1) && (Convert.ToInt32(RouletteGame.RouletteTable.rouletteTable[rolledNum].value) != 0)))
                Console.WriteLine("You win! try again? (press y to bet again)");
            else
                Console.WriteLine("You lost. try again? (press y to bet again)");

            if (Console.ReadLine().ToLower() == "y")
                PlaceBet();
        }
        //Bet on Red or Black
        public static void RedBlack()
        {
            Console.WriteLine("Red(r) or Black(b)? (type r or b)");
            string bet = Console.ReadLine().ToLower();
            bool betRed = true;
            if (bet == "b")
                betRed = false;
            int rolledNum = RollBall();
            Console.WriteLine($"You bet {(betRed ? "Red" : "Black")} and we rolled {RouletteGame.RouletteTable.rouletteTable[rolledNum].value} {PrintBinColor(rolledNum)}");
            if ((betRed && (RouletteGame.RouletteTable.rouletteTable[rolledNum].color == 'r') )
                || (!betRed && (RouletteGame.RouletteTable.rouletteTable[rolledNum].color == 'b')))
                Console.WriteLine("You win! try again? (press y to bet again)");
            else
                Console.WriteLine("You lost. try again? (press y to bet again)");

            if (Console.ReadLine().ToLower() == "y")
                PlaceBet();
        }
        //This method will print out the color for each bin, for the RedBlack method
        public static string PrintBinColor(int num)
        {
            switch(RouletteGame.RouletteTable.rouletteTable[num].color)
            {
                case 'g':
                    return "Green";
                case 'r':
                    return "Red";
                case 'b':
                    return "Black";
                default:
                    break;
            }
            return "";
        }
        //Lows/Highs
        public static void LowHigh()
        {
            Console.WriteLine("Low(l) or High(h)? (type l or h)");
            string bet = Console.ReadLine().ToLower();
            bool betLow = true;
            if (bet == "h")
                betLow = false;
            int rolledNum = RollBall();
            Console.WriteLine($"You bet {(betLow ? "Low" : "High")} and we rolled {RouletteGame.RouletteTable.rouletteTable[rolledNum].value} {PrintBinColor(rolledNum)}");
            if ((betLow && (rolledNum >= 1 && rolledNum <= 18)) || (!betLow && ((rolledNum >= 19 && rolledNum <= 36))))
                Console.WriteLine("You win! try again? (press y to bet again)");
            else
                Console.WriteLine("You lost. try again? (press y to bet again)");

            if (Console.ReadLine().ToLower() == "y")
                PlaceBet();
        }
        public static void Dozen()
        {
            Console.WriteLine("1-12, 13-24, or 25-36? (type \"1-12\" \"13-24\" or \"25-36\" without quotes)");
            string bet = Console.ReadLine();
            int rolledNum = RollBall();
            Console.WriteLine($"You bet {bet} and we rolled {RouletteGame.RouletteTable.rouletteTable[rolledNum].value} {PrintBinColor(rolledNum)}");
            if ((bet == "1-12" && (rolledNum >= 1 && rolledNum <= 12)) || (bet == "13-24" && (rolledNum >= 13 && rolledNum <= 24)) 
                || (bet == "25-36" && (rolledNum >= 25 && rolledNum <= 36)))
                Console.WriteLine("You win! try again? (press y to bet again)");
            else
                Console.WriteLine("You lost. try again? (press y to bet again)");

            if (Console.ReadLine().ToLower() == "y")
                PlaceBet();
        }

        public static void Column()
        {
            Console.WriteLine("Column 1, 2, or 3? (type 1 2 or 3)");
            string bet = Console.ReadLine();
            int rolledNum = RollBall();
            Console.WriteLine($"You bet {bet} and we rolled {RouletteGame.RouletteTable.rouletteTable[rolledNum].value} {PrintBinColor(rolledNum)}");
            if (InColumn(rolledNum, Convert.ToInt32(bet)))
                Console.WriteLine("You win! try again? (press y to bet again)");
            else
                Console.WriteLine("You lost. try again? (press y to bet again)");

            if (Console.ReadLine().ToLower() == "y")
                PlaceBet();
        }
        public static bool InColumn(int value, int column)
        {
            for (int i = column; i < 12; i+=3)
            {
                if (i == value)
                    return true;
            }
            return false;
        }
        public static void Street()
        {
            Console.WriteLine("Enter street (1/2/3 or 4/5/6 or 7/8/9 etc... just enter first number of street");
            int bet = Convert.ToInt32(Console.ReadLine());
            //check valid street
            for (int i = 1; i < 36; i+=3)
            {
                if (bet == i)
                    goto ValidStreet;
            }
            Console.WriteLine("Invalid Input, try again...");
            Street();
            ValidStreet:
            int rolledNum = RollBall();
            Console.WriteLine($"You bet {bet} street and we rolled {RouletteGame.RouletteTable.rouletteTable[rolledNum].value} {PrintBinColor(rolledNum)}");
            if (InStreet(rolledNum, bet))
                Console.WriteLine("You win! try again? (press y to bet again)");
            else
                Console.WriteLine("You lost. try again? (press y to bet again)");

            if (Console.ReadLine().ToLower() == "y")
                PlaceBet();
        }
        public static bool InStreet(int value, int street)
        {
            for (int i = street; i < street+3; i++)
            {
                if (i == value)
                    return true;
            }
            return false;
        }
        public static void SixNumbers()
        {
            Console.WriteLine("Enter six numbers (1/2/3/4/5/6 or 7/8/9/10/11/12 etc... just enter first number of the six");
            int bet = Convert.ToInt32(Console.ReadLine());
            //check valid six numbers
            for (int i = 1; i < 33; i += 3)
            {
                if (bet == i)
                    goto ValidSixNumbers;
            }
            Console.WriteLine("Invalid Input, try again...");
            SixNumbers();
        ValidSixNumbers:
            int rolledNum = RollBall();
            Console.WriteLine($"You bet {bet} six numbers and we rolled {RouletteGame.RouletteTable.rouletteTable[rolledNum].value} {PrintBinColor(rolledNum)}");
            if (InStreet(rolledNum, bet) && InStreet(rolledNum, bet+3))
                Console.WriteLine("You win! try again? (press y to bet again)");
            else
                Console.WriteLine("You lost. try again? (press y to bet again)");

            if (Console.ReadLine().ToLower() == "y")
                PlaceBet();
        }
        public static void Split()
        {
            Console.WriteLine("Split: at the edge of any two contiguous numbers, e.g., 1/2, 11/14, and 35/36 (Enter values seperated by space)");
            string input = Console.ReadLine();
            int[] bet = new int[2];
            int index = 0;
            foreach(string num in input.Split(" "))
            {
                if (!string.IsNullOrEmpty(num))
                    bet[index++] = Convert.ToInt32(num);
                if (index >= 2)
                    break;
            }
            if (!ValidSplit(bet))
            {
                Console.WriteLine("Not a valid split, try again..");
                Split();
            }
            int rolledNum = RollBall();
            Console.WriteLine($"You bet {bet[0]} and {bet[1]} split and we rolled {RouletteGame.RouletteTable.rouletteTable[rolledNum].value} {PrintBinColor(rolledNum)}");
            if ((rolledNum == bet[0]) || (rolledNum == bet[1]))
                Console.WriteLine("You win! try again? (press y to bet again)");
            else
                Console.WriteLine("You lost. try again? (press y to bet again)");

            if (Console.ReadLine().ToLower() == "y")
                PlaceBet();
        }
        public static bool ValidSplit(int[] nums)
        {
            int[] rowCol = getRouletteIndex(nums[0]);
            //Check all valid surronding spots to see if it's a valid split
            if ((rowCol[1] + 1) < RouletteGame.RouletteTable.bettingTable.GetLength(1)) { 
                if (RouletteGame.RouletteTable.bettingTable[rowCol[0], rowCol[1] + 1] == nums[1])
                    return true;
            }
            if ((rowCol[1] - 1) >= 0) {
                if (RouletteGame.RouletteTable.bettingTable[rowCol[0], rowCol[1] - 1] == nums[1])
                    return true;
            }
            if ((rowCol[0] + 1) < RouletteGame.RouletteTable.bettingTable.GetLength(0)){ 
                if (RouletteGame.RouletteTable.bettingTable[rowCol[0] + 1, rowCol[1]] == nums[1])
                    return true;
            }
            if ((rowCol[0] - 1) >= 0){ 
                if (RouletteGame.RouletteTable.bettingTable[rowCol[0] - 1, rowCol[1]] == nums[1])
                    return true;
            }
            return false;
        }
        public static int[] getRouletteIndex(int value)
        {
            for (int row = 0; row < RouletteGame.RouletteTable.bettingTable.GetLength(0); row++)
            {
                for (int col = 0; col < RouletteGame.RouletteTable.bettingTable.GetLength(1); col++)
                {
                    if (RouletteGame.RouletteTable.bettingTable[row, col] == value)
                        return new int[] { row, col };
                }
            }
            Console.WriteLine("No values found");
            return new int[] { -1 };
        }
        public static void Corner()
        {
            Console.WriteLine(" Corner: at the intersection of any four contiguous numbers (enter top left number for your corner)");
            Console.WriteLine("i.e. 1 for 1/2/4/5 32 for 32/33/35/36");
            int bet = Convert.ToInt32(Console.ReadLine());
            if(!ValidCorner(bet))
            {
                Console.WriteLine("Incorrect input, try again...");
                Corner();
            }
            int rolledNum = RollBall();
            Console.WriteLine($"You bet {bet}/{bet+1}/{bet+3}/{bet+4} corner and we rolled {RouletteGame.RouletteTable.rouletteTable[rolledNum].value} {PrintBinColor(rolledNum)}");
            if ((rolledNum == bet) || (rolledNum == bet+1) || (rolledNum == bet + 3) || (rolledNum == bet + 4))
                Console.WriteLine("You win! try again? (press y to bet again)");
            else
                Console.WriteLine("You lost. try again? (press y to bet again)");

            if (Console.ReadLine().ToLower() == "y")
                PlaceBet();

        }
        //Checks if value is in top left corner of a valid corner
        public static bool ValidCorner(int bet)
        {
            for(int i = 1; i < 33; i++)
            {
                if ((bet == i) || (bet == ++i))
                    return true;
                i++;
            }
            return false;
        }
    }
}
