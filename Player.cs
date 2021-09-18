using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClueGame
{
    public class Player
    {
        public Character CharacterField { get; set; }
        public List<int> Clues { get; set; }
        public string Name { 
            get
            {
                switch (CharacterField)
                {
                    case Character.Mustard:
                        return "Colonel Mustard";
                    case Character.Plum:
                        return "Professor Plum";
                    case Character.Green:
                        return "Mr. Green";
                    case Character.Peacock:
                        return "Mrs. Peacock";
                    case Character.Scarlet:
                        return "Miss Scarlet";
                    case Character.White:
                        return "Mrs. White";
                    default:
                        break;
                }

                return "";
            }
        }
        public Player()
        {
            Clues = new List<int>();
        }
        public Player(Character character)
        {
            CharacterField = character;
            Clues = new List<int>();

        }
    }
}
