using System;
using static AutoBattle.Types;

namespace AutoBattle
{
    /// <summary>
    /// This is the class that controls the characters
    /// </summary>
    public class Character
    {
        GameMode gameMode;
        public string Name { private get; set; }
        public float Health { get; set; }
        public float BaseDamage { private get; set; }
        public GridBox currentBox;
        public Character Target { private get; set; }
        bool _canWalk;

        public Character(CharacterClass characterClass)
        {
            WalkTO(true);
            gameMode = new GameMode();
        }

        /// <summary>
        /// Reduces player's health and checks if it's still greater than 0
        /// </summary>
        void TakeDamage()
        {
            Health -= BaseDamage;
            if (Health <= 0)
            {
                Die();
            }
        }

        /// <summary>
        /// Informs that the player has died and prevents movement
        /// </summary>
        public void Die()
        {
            WalkTO(false);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{ValidatePlayerName(Name)} has been defeated!");
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Defines whether players can move
        /// </summary>
        void WalkTO(bool can)
        {
            _canWalk = can;
        }

        /// <summary>
        /// Check when to attack and walk towards the opponent
        /// </summary>
        public void StartTurn(Grid battlefield)
        {
            if (CheckCloseTargets(battlefield) && _canWalk)
            {
                Attack(Target);
                return;
            }
            else
            {
                MoveTowardsOpponent(battlefield);
            }
        }

        /// <summary>
        /// If there is no target close enough, calculates in wich direction this character should move to be closer to a possible target
        /// </summary>
        void MoveTowardsOpponent(Grid battlefield)
        {
            if (currentBox.xIndex > Target.currentBox.xIndex)
            {
                if ((battlefield.grids.Exists(x => x.index == currentBox.index - 1)))
                {
                    currentBox.occupied = false;
                    battlefield.grids[currentBox.index] = currentBox;
                    currentBox = (battlefield.grids.Find(x => x.index == currentBox.index - 1));
                    currentBox.occupied = true;
                    battlefield.grids[currentBox.index] = currentBox;
                    ShowPlayerDirection("left");
                    battlefield.DrawBattlefield(battlefield.LengthX, battlefield.LengthY);
                    return;
                }
            }
            else if (currentBox.xIndex < Target.currentBox.xIndex)
            {
                currentBox.occupied = false;
                battlefield.grids[currentBox.index] = currentBox;
                currentBox = (battlefield.grids.Find(x => x.index == currentBox.index + 1));
                currentBox.occupied = true;
                battlefield.grids[currentBox.index] = currentBox;
                ShowPlayerDirection("right");
                battlefield.DrawBattlefield(battlefield.LengthX, battlefield.LengthY);
                return;
            }

            if (currentBox.yIndex > Target.currentBox.yIndex)
            {
                currentBox.occupied = false;
                battlefield.grids[currentBox.index] = currentBox;
                currentBox = battlefield.grids.Find(x => x.index == currentBox.index - battlefield.LengthX);
                currentBox.occupied = true;
                battlefield.grids[currentBox.index] = currentBox;
                ShowPlayerDirection("up");
                battlefield.DrawBattlefield(battlefield.LengthX, battlefield.LengthY);
                return;
            }
            else if (currentBox.yIndex < Target.currentBox.yIndex)
            {
                currentBox.occupied = false;
                battlefield.grids[currentBox.index] = currentBox;
                currentBox = battlefield.grids.Find(x => x.index == currentBox.index + battlefield.LengthX);
                currentBox.occupied = true;
                battlefield.grids[currentBox.index] = currentBox;
                ShowPlayerDirection("down");
                battlefield.DrawBattlefield(battlefield.LengthX, battlefield.LengthY);
                return;
            }
        }

        /// <summary>
        /// Check in x and y directions if there is any character close enough to be a target
        /// </summary>     
        /// <returns>If it is a target, it returns true, if not, it returns false</returns>    
        bool CheckCloseTargets(Grid battlefield)
        {
            bool left = (battlefield.grids.Find(x => x.index == currentBox.index - battlefield.LengthX)).occupied;
            bool right = (battlefield.grids.Find(x => x.index == currentBox.index + battlefield.LengthX)).occupied;
            bool up = (battlefield.grids.Find(y => y.index == currentBox.index + battlefield.LengthY)).occupied;
            bool down = (battlefield.grids.Find(y => y.index == currentBox.index - battlefield.LengthY)).occupied;

            if (left || right || up || down)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Displays players attack and remaining health
        /// </summary>
        public void Attack(Character target)
        {
            Console.Write($"{ValidatePlayerName(Name)} is attacking the {ValidatePlayerName(Target.Name)} and did {BaseDamage} damage.");
            Console.WriteLine($" {ValidatePlayerName(Target.Name)} has {Health - BaseDamage} health remaining\n");
            target.TakeDamage();
        }

        /// <summary>
        /// Displays the player's name and which direction he is walking
        /// </summary>
        void ShowPlayerDirection(string direction)
        {
            Console.Write(Environment.NewLine + Environment.NewLine);
            Console.WriteLine($"{ValidatePlayerName(Name)} walked {direction}");
        }

        /// <summary>
        /// Detects if it's a player or if it's an enemy
        /// </summary>
        /// <param name="playerName">Player's name</param>
        /// <returns>Returns a string as the player's name</returns>
        String ValidatePlayerName(string playerName)
        {
            return ((String.IsNullOrEmpty(playerName)) ? "Enemy" : playerName);
        }
    }
}
