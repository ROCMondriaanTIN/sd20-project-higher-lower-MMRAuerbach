using System;
using System.Threading;
using System.Collections.Generic;

namespace ProjectHigherLower
{
    class Program
    {
        private static double userFlorins = 10;
        private static Card firstComputerCard;
        private static List<Card> deck;
        private static int streak = 0;
        private static int currentBet = 0;
        private static double[] streakMultiplier = { 0.5, 1, 2, 3, 5, 8, 13, 20, 40, 100 }; //Seems familier? 

        /**
         * Main - start of the game
         * @params string[]
         * @return void
         */
        static void Main(string[] args)
        {
            Console.WriteLine("-=> Project Higher Lower - The card guessing game. <=-");
            Console.Beep();
            Console.Write("What is your name? ");
            string playerName = Console.ReadLine();
            Console.WriteLine("Welcome to this game " + playerName + " you start with " + userFlorins + " Florins");

            GenerateDeck(); //Generate a new deck
            firstComputerCard = GetRandomCard(); //Select a random card

            //Create while loop, as long as player plays game then loop ;-) 
            bool playGame = true;
            do
            {
                //Start with playing a round (always)
                PlayRound();

                //Check if the player can continue or wants to continue
                Console.Write("You currently have = " + userFlorins);

                if (userFlorins <= 0)
                {
                    playGame = false;
                    Console.WriteLine(" Your money is gone, you can not continue.");
                }
                else
                {
                    Console.Write(" Do you want to continue Y/N? ");
                    string userAnswer = Console.ReadLine().ToUpper();
                    while (checkAnswer(userAnswer, new string[] { "Y", "N" }))
                    {
                        Console.Write("That is not a correct answer, please try again. Do you want to continue Y/N? ");
                        userAnswer = Console.ReadLine().ToUpper();//Make sure the answer is uppercase
                    }

                    if (userAnswer == "N")
                        playGame = false;

                }

            } while (playGame);

            //End the game
            Console.WriteLine("Thank you for playing HiLo, you ended with " + userFlorins + " points");
            ShowAllCards(deck);
            Console.WriteLine("Press any key to quit...");
            Console.ReadLine();
        }

        /**
         * ShowAllCards - Show all the cards in the deck
         * @params List<Card>
         * @return void
         */
        static private void ShowAllCards(List<Card> deck)
        {
            for (int i = 0; i < deck.Count; i++)
            {
                deck[i].PrintCard();
            }
            Console.WriteLine();
        }

        /**
         * PlayRound
         * @params List<Card>
         * @return void
         */
        static private void PlayRound()
        {
            Console.Write("First make your bet (1/10/100) Florins. ");
            string userBet = Console.ReadLine();
            bool canNotBet = true;
            while (canNotBet)
            {
                while (checkAnswer(userBet, new string[] { "1", "10", "100" }))
                {
                    Console.Write("That is not a good bet. Please bet 1/10 or 100. ");
                    userBet = Console.ReadLine();
                }

                currentBet = Convert.ToInt32(userBet);
                double newUserFlorins = userFlorins - currentBet;
                Console.WriteLine(newUserFlorins);

                if (newUserFlorins < 0)
                {
                    userBet = "0";
                    Console.WriteLine("You can not bet more money than you have...");
                }
                else
                {
                    canNotBet = false;
                    userFlorins = newUserFlorins;
                    Console.WriteLine("You have " + userFlorins + " Florins left, starting the game");
                }
            }

            do
            {
                Card secondComputerCard = GetRandomCard();
                int difference = firstComputerCard.GetPoints() - secondComputerCard.GetPoints();
                Console.Write("The computer card is:");
                firstComputerCard.PrintCard();

                Console.Write("is the next card higher (H) or lower (L)? ");
                secondComputerCard.PrintCard();

                //Make sure the answer is uppercase
                string userAnswer = Console.ReadLine().ToUpper();

                while (checkAnswer(userAnswer, new string[] { "H", "L" }))
                {
                    Console.WriteLine("That is not a correct answer, please try again.");
                    Console.Write(" is the next card higher (H) or lower (L)? ");
                    

                    userAnswer = Console.ReadLine().ToUpper();//Make sure the answer is uppercase
                }

                bool guessed = ((difference > 0 && userAnswer == "L") || (difference < 0 && userAnswer == "H"));

                Console.Write("The second card is ");
                secondComputerCard.PrintCard();

                if (guessed)
                {
                    streak++;
                    Console.Write(" your answer is correct! Your current streak is " + streak + ". Do you wish to continue this streak? (Y/N) ");
                    //Make sure the answer is uppercase
                    string userContinue = Console.ReadLine().ToUpper();

                    while (checkAnswer(userContinue, new string[] { "Y", "N" }))
                    {
                        Console.Write("That is not a correct answer. Do you wish to continue your streak? (Y/N) ");
                        userContinue = Console.ReadLine().ToUpper();
                    }

                    if (userContinue == "N")
                    {
                        PayUser(streak);
                        streak = 0;
                    }

                    firstComputerCard = secondComputerCard;
                }
                else
                {
                    Console.WriteLine(" your answer is wrong, you lose your initial bet of " + currentBet);
                    streak = 0;
                }
            } while (streak > 0);
        }

        /**
         * GetRandomCard - select a random card from a deck
         * @params List<Card>
         * @return Card
         */
        static private Card GetRandomCard()
        {
            Random rnd = new Random();
            int cardIndex = rnd.Next(deck.Count);

            return deck[cardIndex];
        }

        /**
         * GenerateDeck
         * @params none
         * @return List<Card>
         */
        static private void GenerateDeck()
        {
            string[] suits = Card.GetAllowedCardSuits();
            string[] cardValue = Card.GetAllowedCardValues();

            deck = new List<Card>();

            for (int i = 0; i < cardValue.Length; i++)
            {
                string useValue = cardValue[i];

                for (int j = 0; j < suits.Length; j++)
                {
                    string useSuit = suits[j];
                    int usePoints = i + 2;
                    deck.Add(new Card(useValue, useSuit, usePoints));
                }
            }
        }

        /**
         * checkAnswer - check answer from user against correct options
         * @params string, string[]
         * @return bool
         */
        public static bool checkAnswer(string answer, string[] options)
        {
            bool returnBoolean = true;

            for (int i = 0; i < options.Length; i++)
            {
                if (answer == options[i])
                {
                    returnBoolean = false;
                }
            }

            return returnBoolean;
        }

        public static void PayUser(int streak)
        {
            if (streak >= streakMultiplier.Length)
                streak = streakMultiplier.Length;

            double payOut = streakMultiplier[(streak - 1)];
            double winning = (payOut * currentBet);
            userFlorins = userFlorins + winning;

            Console.WriteLine("Allright, time to payup. Your streak is " + streak + " your bet was " + currentBet + " you win a total of " + winning);
        }
    }
}
