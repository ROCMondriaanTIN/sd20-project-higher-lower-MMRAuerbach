using System;
using System.Threading;
using System.Collections.Generic;

namespace ProjectHigherLower
{
    class Program
    {
        private static Card firstComputerCard;
        private static List<Card> deck;
        private static List<Player> players;

        /**
         * Main - start of the game
         * @params string[]
         * @return void
         */
        static void Main(string[] args)
        {
            Console.WriteLine("-=> Project Higher Lower - The card guessing game. <=-");
            Console.WriteLine("How many players will be playing? (1-8)");
            string noOfPlayers = Console.ReadLine();
            while (CheckAnswer(noOfPlayers, new string[] { "1", "2", "3", "4", "5", "6", "7", "8" }))
            {
                Console.WriteLine("That is not a correct number, how many players will be playing? (1-8)");
                noOfPlayers = Console.ReadLine();
            }


            int numberOfPlayers;
            Int32.TryParse(noOfPlayers, out numberOfPlayers);

            AskPlayersName(numberOfPlayers);

            Console.WriteLine("Welcome to this game, we start of with " + players.Count + " players");
            GenerateDeck();
            firstComputerCard = GetRandomCard(deck);

            //Create while loop, as long as player plays game then loop ;-) 
            bool playGame = true;
            do
            {
                //Start with playing a round (always)
                PlayRound(deck);

                //Check if the player can continue or wants to continue
                Console.Write(" Do you want to continue Y/N? ");

                string userAnswer = Console.ReadLine().ToUpper();
                while (CheckAnswer(userAnswer, new string[] { "Y", "N" }))
                {
                    Console.Write("That is not a correct answer, please try again. Do you want to continue Y/N? ");
                    userAnswer = Console.ReadLine().ToUpper();//Make sure the answer is uppercase
                }

                if (userAnswer == "N")
                    playGame = false;

            } while (playGame);

            //End the game
            Console.WriteLine("Thank you for playing HiLo.");
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
            string winningString = "HIGHER";
            if (difference > 0)
                winningString = "LOWER";

            Console.Write("The computer has chosen a card, ");
            firstComputerCard.PrintCard();

            Console.WriteLine(" every player can now choose");

            for (int i = 0; i < players.Count; i++)
            {
                Player player = players[i];
                Console.Write(player.Name + " is the next card higher (H) or lower (L)? ");

                string userAnswer = Console.ReadLine().ToUpper();

                while (CheckAnswer(userAnswer, new string[] { "H", "L" }))
                {
                    Console.WriteLine("That is not a correct answer, please try again");
                    Console.WriteLine(player.Name + " is the next card higher (H) or lower (L)?");

                    userAnswer = Console.ReadLine().ToUpper();//Make sure the answer is uppercase
                }

                player.Choice = userAnswer;

            }

            Console.Write("The second card is ");
            secondComputerCard.PrintCard();
            Console.WriteLine(" that means the correct answer is: " + winningString);

            for (int i = 0; i < players.Count; i++)
            {
                Player player = players[i];
                bool guessed = ((difference > 0 && player.Choice == "L") || (difference < 0 && player.Choice == "H"));
                string message = "right";

                if (guessed)
                {
                    player.Points++;
                    
                }
                else
                {
                    player.Points--;
                    message = "wrong";
                }

                Console.WriteLine(player.Name + " your answer is " + message + "! You now have " + player.Points + " points.");
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
         * CheckAnswer - check answer from user against correct options
         * @params string, string[]
         * @return bool
         */
        public static bool CheckAnswer(string answer, string[] options)
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

        static private void AskPlayersName(int numberOfPlayers)
        {
            players = new List<Player>();
            for (int i = 0; i < numberOfPlayers; i++)
            {
                Console.WriteLine("Player " + (i + 1) + " what is your name?");
                string useName = Console.ReadLine();
                Player newPlayer = new Player(useName);
                players.Add(newPlayer);
            }
        }
    }
}
