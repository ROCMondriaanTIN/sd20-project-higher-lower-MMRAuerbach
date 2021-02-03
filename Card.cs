using System;
namespace ProjectHigherLower
{
    public class Card
    {
        // Attributes
        private string suit;
        private string colour;
        private string value;
        private int points;
        private static string[] allowedSuits = { "♡", "♢", "♧", "♤" };
        private static string[] allowedValues = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };

        /**
         * Constructor
         * @params string, string, int
         */
        public Card(string value, string suit, int points)
        {
            this.SetValue(value);
            this.SetSuit(suit);
            this.points = points;
        }

        /**
         * PrintCard - shows a card in a console.write
         * @params - 
         * @return void
         */
        public void PrintCard()
        {
            //Store the old colours in a var
            ConsoleColor oldBackground = Console.BackgroundColor;
            ConsoleColor oldForeground = Console.ForegroundColor;

            //Change the console colour to new colour
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;

            //When the card colour is red, then change foreground to red
            if (this.GetColour() == "red")
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }

            //Write the suit en value
            Console.Write(this.suit+this.value);

            //Change the console colours back to the old values (as stored in begin of method)
            Console.ForegroundColor = oldForeground;
            Console.BackgroundColor = oldBackground;
        }

        /**
         * CheckValue - Check a value against the allowed values for the card
         * @params string
         * @return bool
         */
        public bool CheckValue(string value)
        {
            bool isAllowed = false;
            for (int i = 0; i < allowedValues.Length; i++)
            {
                if (value == allowedValues[i])
                {
                    isAllowed = true;
                }
            }

            return isAllowed;
        }

        /**
         * This method returns the points of this card.
         * @params - 
         * @return int
         */
        public int GetPoints()
        {
            return points;
        }

        /**
         * SetSuit - Sets the Suit of the card AND the colour that represents that suit.
         * @params string
         * @return void
         */
        public void SetSuit(string suit)
        {
            if (suit == "♡" || suit == "♢" || suit == "♧" || suit == "♤")
            {
                this.suit = suit;
                this.colour = GetColour(suit);
            }
            else
            {
                throw new Exception("Could not find the specified suit.");
            }
        }

        /**
         * SetValue -   This method checks the value and if the value is allowed it sets the value.
         *              This method method uses the CheckValue method to check if the value is correct.
         * @params string
         * @return void
         */
        public void SetValue(string value)
        {
            if (CheckValue(value))
            {
                this.value = value;
            }
            else
            {
                throw new Exception("This value of a card is not allowed");
            }
        }

        /**
         * GetColour - Get the colour of the card
         * @params - 
         * @return string
         */
        public string GetColour()
        {
            return this.colour;
        }

        /**
         * GetColour - Get the colour according to the given Suit
         * @params string
         * @return string
         */
        public string GetColour(string suit)
        {
            if (suit == "♡" || suit == "♢")
            {
                return "red";
            }
            else if (suit == "♧" || suit == "♤")
            {
                return "black";
            }
            else
            {
                throw new Exception("Could not find the specified suit.");
            }
        }

        /**
         * GetAllowedCardValues - Returns all the allowed Card Values
         * @params - 
         * @return string[]
         */
        public static string[] GetAllowedCardValues()
        {
            return allowedValues;
        }

        /**
         * GetAllowedCardSuits - Returns all the allowed Card Suits
         * @params - 
         * @return string[]
         */
        public static string[] GetAllowedCardSuits()
        {
            return allowedSuits;
        }
    }
}