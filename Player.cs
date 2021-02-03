using System;
namespace ProjectHigherLower
{
    public class Player
    {
        public string Name { get; set; } //Makes method Player.Name and Player.Name = ".."
        public int Points { get; set; } //Since this is a getter-setter the variable is PascalCase
        public string Choice { get; set; } //What was the players choice

        public Player(string name)
        {
            Name = name;
            Points = 0;
        }
    }
}
