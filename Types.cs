namespace AutoBattle
{
    /// <summary>
    /// This is the class that defines the data types
    /// </summary>
    public class Types
    {
        /// <summary>
        /// Defines the coordinates inside the grid if it is busy or not and the index
        /// </summary>
        public struct GridBox
        {
            public int xIndex;
            public int yIndex;
            public bool occupied;
            public int index;

            public GridBox(int x, int y, bool occupied, int index)
            {
                xIndex = x;
                yIndex = y;
                this.occupied = occupied;
                this.index = index;
            }
        }

        /// <summary>
        /// Set the numbers corresponding to each character class
        /// </summary>
        public enum CharacterClass : uint
        {
            Paladin = 1,
            Warrior = 2,
            Cleric = 3,
            Archer = 4
        }
    }
}
