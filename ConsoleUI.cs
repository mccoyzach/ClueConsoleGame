// Created by Zach McCoy and Zach Nichols
// Date: September 18, 2021

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClueGame
{
    public class ConsoleUI
    {
        // Variables for storing the murder's information
        private Character _murderer;
        private Weapon _murderWeapon;
        private Room _crimeScene;

        // Variable for storing the player's character
        private Player _playerCharacter;

        // List of virtual players at the "table" (and an eventual winner)
        private List<Player> _listOfPlayers = new List<Player>();
        //static Character winner;

        public ConsoleUI() { }

        public void Run()
        {
            // Ask user to pick a player
            GenerateVirtualPlayers();
            SelectCharacter();

            // Randomize the killer/weapon/room and store in "envelope"
            RandomizeMurderer();

            // Randomly distrubte remaining characters/weapons/rooms to virtual players at the table
            // For testing, display a table of values 
            RandomizeClues();

            // Ask player to make a suggestion or assertion
            KeepInvestigating();

        }

        private void SelectCharacter()
        {
            bool keepLooping = true;

            while (keepLooping)
            {
                Console.Clear();

                // Welcome the user
                Console.WriteLine("Welcome to the game of CLUE\n \n");

                Console.WriteLine("Please pick a player:\n" +
               "\t1. Colonel Mustard\n" +
               "\t2. Professor Plum\n" +
               "\t3. Mr. Green\n" +
               "\t4. Mrs. Peacock\n" +
               "\t5. Miss Scarlet\n" +
               "\t6. Mrs. White\n \n" +
               "\t0. Quit");
                string response = Console.ReadLine();
                switch (response)
                {
                    case "1":
                        _playerCharacter = _listOfPlayers[0];
                        keepLooping = false;
                        break;
                    case "2":
                        _playerCharacter = _listOfPlayers[1];
                        keepLooping = false;
                        break;
                    case "3":
                        _playerCharacter = _listOfPlayers[2];
                        keepLooping = false;
                        break;
                    case "4":
                        _playerCharacter = _listOfPlayers[3];
                        keepLooping = false;
                        break;
                    case "5":
                        _playerCharacter = _listOfPlayers[4];
                        keepLooping = false;
                        break;
                    case "6":
                        _playerCharacter = _listOfPlayers[5];
                        keepLooping = false;
                        break;
                    case "0":
                        Environment.Exit(0);
                        //keepLooping = false;
                        break;
                    default:
                        Console.WriteLine("Please choose a valid selection. Press any key to continue.");
                        Console.ReadLine();
                        break;
                }

            }

            // Print their selection
            Console.WriteLine($"Welcome {_playerCharacter.Name}! Press any key to continue.");
            Console.ReadLine();
        }

        private void RandomizeMurderer()
        {
            // Generate some random numbers
            Random random = new Random();
            int murdererInt = random.Next(0, 5);
            int weaponInt = random.Next(10, 15);
            int roomInt = random.Next(20, 28);

            // Store the _murderer's information
            _murderer = (Character)murdererInt;
            _murderWeapon = (Weapon)weaponInt;
            _crimeScene = (Room)roomInt;
        }

        private void GenerateVirtualPlayers()
        {
            _listOfPlayers.Add(new Player(Character.Mustard));
            _listOfPlayers.Add(new Player(Character.Plum));
            _listOfPlayers.Add(new Player(Character.Green));
            _listOfPlayers.Add(new Player(Character.Peacock));
            _listOfPlayers.Add(new Player(Character.Scarlet));
            _listOfPlayers.Add(new Player(Character.White));
        }

        private void RandomizeClues()
        {
            // Put all of the clues in a single list
            List<int> listOfClues = new List<int>();
            foreach (int i in Enum.GetValues(typeof(Character)))
            {
                listOfClues.Add(i);
            }
            foreach (int i in Enum.GetValues(typeof(Weapon)))
            {
                listOfClues.Add(i);
            }
            foreach (int i in Enum.GetValues(typeof(Room)))
            {
                listOfClues.Add(i);
            }

            // Remove the clues that are in the envelope
            listOfClues.Remove((int)_murderer);
            listOfClues.Remove((int)_murderWeapon);
            listOfClues.Remove((int)_crimeScene);

            // Now randomly distrubte to players
            Random random = new Random();
            int currentPlayer = 0;  // Start with Miss Scarlet
            while (listOfClues.Count > 0)
            {
                // Choose which clue to give away
                int randomIndex;
                if (listOfClues.Count == 1)
                {
                    randomIndex = 0;      // Just get the only item left in the list
                }
                else
                {
                    randomIndex = random.Next(0, listOfClues.Count - 1);
                }

                // Get the current player
                Player virtualPlayer = _listOfPlayers[currentPlayer];

                // Add the clue (from the random index) to the list of clues for that player
                virtualPlayer.Clues.Add(listOfClues[randomIndex]);

                // Remove that clue from the list of clues
                listOfClues.RemoveAt(randomIndex);

                // Go to next player
                if (currentPlayer == _listOfPlayers.Count - 1)
                {
                    currentPlayer = 0;
                }
                else
                {
                    currentPlayer++;
                }

            }

            return;
        }

        private void KeepInvestigating()
        {
            bool keepLooping = true;

            while (keepLooping)
            {
                Console.Clear();
                Console.WriteLine("Keep investigating:\n" +
               "\t1. Make a assertion (you know for sure)\n" +
               "\t2. Make a suggestion (you want to take a guess)\n" +
               "\t3. Cheat (see answers)\n" +
               "\t4. Quit and throw the gameboard across the room");
                string response = Console.ReadLine();
                switch (response)
                {
                    case "1":
                        //...
                        MakeAssertion();
                        break;
                    case "2":
                        //...
                        AskForSuggestion();
                        break;
                    case "3":
                        //...
                        Console.WriteLine($"\n\nMurderer: {_murderer}\n" +
                            $"Weapon: {_murderWeapon}\n" +
                            $"Crime scene: {_crimeScene}\n\n" +
                            "Press any key to continue.");
                        Console.ReadLine();
                        break;
                    case "4":
                        //...
                        //keepLooping = false;
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Please choose a valid selection. Press any key to continue.");
                        Console.ReadLine();
                        break;
                }

            }

        }

        private void AskForSuggestion()
        {
            Console.WriteLine("Pick a player:\n" +
               "\t1. Colonel Mustard\n" +
               "\t2. Professor Plum\n" +
               "\t3. Mr. Green\n" +
               "\t4. Mrs. Peacock\n" +
               "\t5. Miss Scarlet\n" +
               "\t6. Mrs. White");

            int player_response = int.Parse(Console.ReadLine());
            Character suspect = GetCharacterForNumber(player_response - 1);

            Console.WriteLine("Pick a weapon:\n" +
               "\t1. Knife\n" +
               "\t2. Candlestick\n" +
               "\t3. Revolver\n" +
               "\t4. Rope\n" +
               "\t5. Lead Pipe\n" +
               "\t6. Wrench");

            int weapon_response = int.Parse(Console.ReadLine());
            Weapon weapon = GetWeaponForNumber(weapon_response - 1 + 10);

            Console.WriteLine("Pick a room:\n" +
              "\t1. Study\n" +
              "\t2. Hall\n" +
              "\t3. Loung\n" +
              "\t4. Dining Room\n" +
              "\t5. Kitchen\n" +
              "\t6. Ball Room\n" +
              "\t7. Billard Room\n" +
              "\t8. Library\n" +
              "\t9. Conservatory");

            int room_response = int.Parse(Console.ReadLine());
            Room room = GetRoomForNumber(room_response - 1 + 20);

            // Store the result of any A) clues or B) -1 if it couldn't be proven wrong
            int result = MakeSuggestion(suspect, weapon, room);

            if (result == -1)
            {
                Console.WriteLine("\nNo one was able to prove that suggestion false.");
            }
            else if (result >= 0 && result < 10)
            {
                // Character
                Console.WriteLine("\nNo, that person is innocent!");
            }
            else if (result >= 10 && result < 20)
            {
                // Weapon
                Console.WriteLine("\nNo, that weapon wasn't used!");
            }
            else if (result >= 20 && result < 30)
            {
                // Rooms
                Console.WriteLine("\nNo, that room wasn't where it happened!");
            }

            Console.ReadLine();

        }
        private int MakeSuggestion(Character character, Weapon weapon, Room room)
        {
            foreach (Player vp in _listOfPlayers)
            {
                foreach (int clue in vp.Clues)
                {
                    if (clue == (int)character)
                    {
                        // Someone has a clue that proves the character suggested was innocent
                        return clue;
                    }

                    if (clue == (int)weapon)
                    {
                        // Someone has a clue that proves the weapon suggested was incorrect
                        return clue;
                    }

                    if (clue == (int)room)
                    {
                        // Someone has a clue that proves the room suggested was incorrect
                        return clue;
                    }
                }
            }

            // Otherwise, nobody could prove this suggestion false
            return -1;
        }

        private void MakeAssertion()
        {
            Console.WriteLine("Pick a player:\n" +
              "\t1. Colonel Mustard\n" +
              "\t2. Professor Plum\n" +
              "\t3. Mr. Green\n" +
              "\t4. Mrs. Peacock\n" +
              "\t5. Miss Scarlet\n" +
              "\t6. Mrs. White");

            int player_response = int.Parse(Console.ReadLine());
            Character suspect = GetCharacterForNumber(player_response - 1);

            Console.WriteLine("Pick a weapon:\n" +
               "\t1. Knife\n" +
               "\t2. Candlestick\n" +
               "\t3. Revolver\n" +
               "\t4. Rope\n" +
               "\t5. Lead Pipe\n" +
               "\t6. Wrench");

            int weapon_response = int.Parse(Console.ReadLine());
            Weapon weapon = GetWeaponForNumber(weapon_response - 1 + 10);

            Console.WriteLine("Pick a room:\n" +
              "\t1. Study\n" +
              "\t2. Hall\n" +
              "\t3. Loung\n" +
              "\t4. Dinning Room\n" +
              "\t5. Kitchen\n" +
              "\t6. Ball Room\n" +
              "\t7. Billard Room\n" +
              "\t8. Library\n" +
              "\t9. Conservatory");

            int room_response = int.Parse(Console.ReadLine());
            Room room = GetRoomForNumber(room_response - 1 + 20);
            if (suspect == _murderer && weapon == _murderWeapon && room == _crimeScene)
            {
                Console.WriteLine("Congratulations you win!");
            }
            else
            {
                Console.WriteLine("Sorry, that was incorrect, you lose!");
            }

            Console.ReadLine();
            Environment.Exit(0);
        }


        // Helper methods
        private Character GetCharacterForNumber(int number)
        {
            switch (number)
            {
                case 0:
                    return Character.Mustard;
                case 1:
                    return Character.Plum;
                case 2:
                    return Character.Green;
                case 3:
                    return Character.Peacock;
                case 4:
                    return Character.Scarlet;
                case 5:
                    return Character.White;
                default:
                    break;
            }

            return Character.Mustard;   // Non-nullable, work on this later

        }

        private Weapon GetWeaponForNumber(int number)
        {
            switch (number)
            {
                case 10:
                    return Weapon.Knife;
                case 11:
                    return Weapon.Candlestick;
                case 12:
                    return Weapon.Revolver;
                case 13:
                    return Weapon.Rope;
                case 14:
                    return Weapon.LeadPipe;
                case 15:
                    return Weapon.Wrench;
                default:
                    break;
            }

            return Weapon.Knife;   // Non-nullable, work on this later

        }

        private Room GetRoomForNumber(int number)
        {
            switch (number)
            {
                case 20:
                    return Room.Study;
                case 21:
                    return Room.Hall;
                case 22:
                    return Room.Lounge;
                case 23:
                    return Room.DiningRoom;
                case 24:
                    return Room.Kitchen;
                case 25:
                    return Room.BallRoom;
                case 26:
                    return Room.BilliardRoom;
                case 27:
                    return Room.Library;
                case 28:
                    return Room.Conservatory;
                default:
                    break;
            }

            return Room.Study;   // Non-nullable, work on this later

        }
    }
}
