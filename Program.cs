namespace AutoBattle
{
    /// <summary>
    /// This is the class that starts the program
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            GameMode gameMode = new GameMode();
            gameMode.Setup();
        }
    }
}
