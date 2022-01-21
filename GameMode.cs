using System;
using System.Collections.Generic;
using System.Linq;
using static AutoBattle.Types;

namespace AutoBattle
{
    /// <summary>
    /// This is the class that creates players and defines your data
    /// </summary>
    public class GameMode
    {
        Grid _grid;
        Character _playerCharacter;
        Character _enemyCharacter;
        List<Character> AllPlayers = new List<Character>();
        int _currentTurn;
        int _numberOfPossibleTiles;

        /// <summary>
        /// Set variables and call necessary methods to start the game
        /// </summary>
        public void Setup()
        {
            _grid = new Grid(12, 12);
            _currentTurn = 0;
            _numberOfPossibleTiles = _grid.grids.Count;
            GetPlayerChoice();
        }

        /// <summary>
        /// Displays and records class options for the player
        /// </summary>
        void GetPlayerChoice()
        {
            Console.WriteLine("Choose Between One of this Classes:\n");
            Console.WriteLine("[1] Paladin, [2] Warrior, [3] Cleric, [4] Archer");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreatePlayerCharacter((int)CharacterClass.Paladin);
                    break;
                case "2":
                    CreatePlayerCharacter((int)CharacterClass.Warrior);
                    break;
                case "3":
                    CreatePlayerCharacter((int)CharacterClass.Cleric);
                    break;
                case "4":
                    CreatePlayerCharacter((int)CharacterClass.Archer);
                    break;
                default:
                    GetPlayerChoice();
                    break;
            }
        }

        /// <summary>
        /// Choose the player's class based on the parameter received and set a name for him
        /// </summary>
        /// <param name="classIndex">Player class id</param> 
        void CreatePlayerCharacter(int classIndex)
        {
            CharacterClass characterClass = (CharacterClass)classIndex;
            Console.WriteLine($"Player Class Choice: {characterClass}");
            _playerCharacter = GetDefaultCharacterData(characterClass);

            //receives and stores player name
            Console.Write("Your character's name is: ");
            string characterName = Console.ReadLine();
            _playerCharacter.Name = characterName.Trim();

            CreateEnemyCharacter();
        }

        /// <summary>
        /// Randomly choose the enemy class and set up vital variables
        /// </summary>
        void CreateEnemyCharacter()
        {
            int randomInteger = GetRandomInt(1, Enum.GetNames(typeof(CharacterClass)).Length);
            CharacterClass enemyClass = (CharacterClass)randomInteger;
            Console.WriteLine($"Enemy Class Choice: {enemyClass}");
            _enemyCharacter = GetDefaultCharacterData(enemyClass);
            StartGame();
        }

        /// <summary>
        /// Default dice set for player
        /// </summary>
        /// <returns>Returns a player with standard dice</returns>
        Character GetDefaultCharacterData(CharacterClass characterId)
        {
            Character character = new Character(characterId);
            character.Health = 100;
            character.BaseDamage = 20;

            AllPlayers.Add(character);

            return character;
        }

        /// <summary>
        /// Create a list with all players
        /// </summary>
        void StartGame()
        {
            _enemyCharacter.Target = _playerCharacter;
            _playerCharacter.Target = _enemyCharacter;
            AlocatePlayers();
            StartTurn();
        }

        /// <summary>
        /// Start the round with the random starting player
        /// </summary>
        void StartTurn()
        {
            if (_currentTurn == 0)
            {
                Random rnd = new Random();
                AllPlayers = AllPlayers.OrderBy(player => rnd.Next()).ToList();
            }

            foreach (Character character in AllPlayers)
            {
                character.StartTurn(_grid);
            }

            _currentTurn++;
            HandleTurn();
        }

        /// <summary>
        /// Checks if one of the players still has health greater than 0
        /// </summary>
        /// <remarks>If the condition is positive, the StartTurn() method is called</remarks>
        void HandleTurn()
        {
            if (_playerCharacter.Health > 0 && _enemyCharacter.Health > 0)
            {
                Console.Write(Environment.NewLine + Environment.NewLine);
                Console.WriteLine("Click on any key to start the next turn...\n");

                ConsoleKeyInfo key = Console.ReadKey();
                StartTurn();
            }
        }

        /// <summary>
        /// Generate a random integer value
        /// </summary>
        /// <param name="min">Minimum integer value</param>
        /// <param name="max">Maximum integer value</param>
        /// <returns>Random number between Min and Max</returns>
        /// <remarks>Whole numbers must be greater than zero</remarks>
        int GetRandomInt(int min, int max)
        {
            Random rand = new Random();
            int index = rand.Next(min, max);
            return index;
        }

        /// <summary>
        /// Calls the functions that will position the players
        /// </summary>
        void AlocatePlayers()
        {
            AlocatePlayerCharacter();
            AlocateEnemyCharacter();
        }

        /// <summary>
        /// Repositions player on the grid
        /// </summary>
        void AlocatePlayerCharacter()
        {
            int random = GetRandomInt(0, _numberOfPossibleTiles);
            GridBox randomLocation = _grid.grids.ElementAt(random);

            if (!randomLocation.occupied)
            {
                randomLocation.occupied = true;
                _grid.grids[random] = randomLocation;
                _playerCharacter.currentBox = randomLocation;
            }
            else
            {
                AlocatePlayerCharacter();
            }
        }

        /// <summary>
        /// Repositions enemy on the grid
        /// </summary>
        void AlocateEnemyCharacter()
        {
            int random = GetRandomInt(0, _numberOfPossibleTiles);
            GridBox randomLocation = _grid.grids.ElementAt(random);

            if (!randomLocation.occupied)
            {
                randomLocation.occupied = true;
                _grid.grids[random] = randomLocation;
                _enemyCharacter.currentBox = randomLocation;
            }
            else
            {
                AlocateEnemyCharacter();
            }
        }
    }
}
