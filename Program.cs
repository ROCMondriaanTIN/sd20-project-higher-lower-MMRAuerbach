using System;
using System.Threading;
using System.Collections.Generic;

namespace ProjectHigherLower
{
    class Program
    {
        private static int userPoints = 10;
        private static Card firstComputerCard;

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
            Console.WriteLine("Welcome to this game " + playerName);
            List<Card> deck = GenerateDeck();
            firstComputerCard = GetRandomCard(deck);

            //Create while loop, as long as player plays game then loop ;-) 
            bool playGame = true;
            do
            {
                //Start with playing a round (always)
                PlayRound(deck);

                //Check if the player can continue or wants to continue
                Console.Write("Currentpoints = " + userPoints);

                if (userPoints <= 0)
                {
                    playGame = false;
                    Console.WriteLine("You have no points left, you lost.");
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
            Console.WriteLine("Thank you for playing HiLo, you ended with " + userPoints + " points");
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
        static private void PlayRound(List<Card>deck)
        {            
            Card secondComputerCard = GetRandomCard(deck);
            int difference = firstComputerCard.GetPoints() - secondComputerCard.GetPoints();
            Console.Write("The computer has chosen a card, ");
            firstComputerCard.PrintCard();

            Console.Write(" is the next card higher (H) or lower (L)? ");

            //Make sure the answer is uppercase
            string userAnswer = Console.ReadLine().ToUpper();

            while (checkAnswer(userAnswer, new string[] { "H", "L"}))
            {
                Console.WriteLine("That is not a correct answer, please try again");
                Console.WriteLine(" is the next card higher (H) or lower (L)? ");
                secondComputerCard.PrintCard();

                userAnswer = Console.ReadLine().ToUpper();//Make sure the answer is uppercase
            }

            bool guessed = ((difference > 0 && userAnswer == "L") || (difference < 0 && userAnswer == "H"));

            Console.Write("The second card is ");
            secondComputerCard.PrintCard();

            if (guessed)
            {
                Console.WriteLine(" your answer is correct!");
                userPoints++;
            }
            else
            {
                Console.WriteLine(" your answer is wrong!");
                userPoints--;
            }

            firstComputerCard = secondComputerCard;
        }

        /**
         * GetRandomCard - select a random card from a deck
         * @params List<Card>
         * @return Card
         */
        static private Card GetRandomCard(List<Card> deck)
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
        static private List<Card> GenerateDeck()
        {
            string[] suits = Card.GetAllowedCardSuits();
            string[] cardValue = Card.GetAllowedCardValues(); 

            List<Card> deck = new List<Card>();

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

            return deck;
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
    }
}
