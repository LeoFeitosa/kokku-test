using System;
using System.Collections.Generic;
using static AutoBattle.Types;

namespace AutoBattle
{
    /// <summary>
    /// This is the class that draws the grids
    /// </summary>
    public class Grid
    {
        public List<GridBox> grids = new List<GridBox>();
        public int LengthX { get; private set; }
        public int LengthY { get; private set; }

        /// <summary>
        /// Create a list of grid positions
        /// </summary>
        public Grid(int Lines, int Columns)
        {
            LengthX = Lines;
            LengthY = Columns;
            Console.WriteLine("The battle field has been created\n");
            for (int i = 0; i < Lines; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    GridBox newBox = new GridBox(j, i, false, (Columns * i + j));
                    grids.Add(newBox);
                }
            }
        }

        /// <summary>
        /// Displays the matrix that indicates the pieces of the battlefield and if it is marked as occupied, the marking has a different color
        /// </summary>
        public void DrawBattlefield(int Lines, int Columns)
        {
            for (int i = 0; i < Lines; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    //in case grid is seated as occupied
                    if (grids[Columns * i + j].occupied)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("[X]\t");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.Write($"[ ]\t");
                    }
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
        }
    }
}
