
public class Menu
{
    public void DisplayMenu()
    {
        var mvArt = new Art();
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        mvArt.MovieMenuArt();
        Console.ResetColor();
        Thread.Sleep(2000);
        Console.WriteLine(" -- -- -- -- -- -- -- -- -- -- -- --\n");
        Console.WriteLine("| 1. View Movies                    |\n");
        Console.WriteLine("| 2. View Movies by Category        |\n");
        Console.WriteLine("| 3. View Movies by Title           |\n");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("| 4. Add Movie                      |\n");
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("| 5. Exit Menu                      |\n");
        Console.ResetColor();

        Console.WriteLine("| Select an option from the menu:   |\n");
        Console.WriteLine(" -- -- -- -- -- -- -- -- -- -- -- --\n");
    }
}

