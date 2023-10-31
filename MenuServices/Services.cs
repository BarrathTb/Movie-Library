
using System.Globalization;

public class MenuServices
{
    public string filePath { get; set; }
    public List<Movie> Movies { get; set; }
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

    public void ViewMovies()
    {
        var menu = new MenuServices();
        var help = new Helpers();

        Movies = new List<Movie>();
        // Path to the file
        filePath = "MovieMedia/movies.scrubbed.csv";


        //check if the file exists
        if (!File.Exists(filePath))
        {
            help.WriteError($"File '{filePath}' not found.");
            return;
        }

        try
        {
            //read the file
            var movieLines = File.ReadAllLines(filePath);
            int titles = 0;

            //parse the lines
            foreach (var movieLine in movieLines)
            {
                var parts = movieLine.Split(',');
                //check if the line is valid
                if (parts.Length < 5)
                {
                    help.WriteError("Invalid data format in file.");
                    continue;
                }


                //parse the info for text output
                var id = parts[0];
                var title = parts[1];
                var categories = parts[2].Split('|');
                var director = parts[3];
                var runningTime = parts[4];

                //list output helper
                help.WriteData($"ID: {id}, Movie Title: {title}, Director: {director}, Running Time: {runningTime}");
                help.WriteData("Categories: ");


                //iterate over the categories
                foreach (var category in categories)
                {
                    help.WriteData("\t" + category);
                }
                titles++;

                Console.WriteLine("-- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- --");
            }
            help.WriteInfo($"{titles} movies found.");
            menu.DisplayMenu();
        }
        //if all else fails
        catch (Exception ex)
        {
            help.WriteError($"Exception occurred: {ex.Message}");
        }
    }

    public void ViewMoviesByTitle()
    {
        var menu = new MenuServices();
        var help = new Helpers();
        var movieFilePath = "MovieMedia/movies.scrubbed.csv";

        while (true)
        {
            help.WriteInfo("Enter a movie title or type 'MENU' to return to the main menu: ");
            string inputTitle = Console.ReadLine().ToLower();

            if (inputTitle == "menu")
            {
                menu.DisplayMenu();
                return;
            }

            if (string.IsNullOrEmpty(inputTitle))
            {
                help.WriteError("Please enter a valid movie title!\n");
                continue;
            }

            try
            {
                var movieLines = File.ReadAllLines(movieFilePath);
                bool found = false;
                int matches = 0;

                foreach (var movieLine in movieLines)
                {
                    // Create a new instance of Movie class for each movie
                    Movie movie = new Movie();

                    var quoteIndex = movieLine.IndexOf('"');
                    bool success;

                    if (quoteIndex == -1)
                    {
                        string[] movieDetails = movieLine.Split(',');

                        success = UInt64.TryParse(movieDetails[0], out ulong mediaId);
                        if (success) movie.mediaId = mediaId;

                        movie.title = movieDetails[1];

                        movie.genres = movieDetails[2].Split('|').ToList();

                        movie.director = movieDetails[3];

                        success = TimeSpan.TryParse(movieDetails[4], out TimeSpan runningTime);
                        if (success) movie.runningTime = runningTime;
                    }
                    else
                    {
                        // similar changes are made here as above statement block
                        success = UInt64.TryParse(movieLine[..quoteIndex], out ulong mediaId);
                        if (success) movie.mediaId = mediaId;

                        var remainingString = movieLine[(quoteIndex + 1)..];
                        quoteIndex = remainingString.LastIndexOf('"');

                        movie.title = remainingString[..(quoteIndex + 1)];
                        remainingString = remainingString[(quoteIndex + 2)..];

                        string[] details = remainingString.Split(',');

                        if (details.Length > 0) movie.genres = details[0].Split('|').ToList();
                        if (details.Length > 1) movie.director = details[1];

                        if (details.Length > 2)
                        {
                            success = TimeSpan.TryParse(details[2], out TimeSpan runningTime);
                            if (success) movie.runningTime = runningTime;
                        }
                    }

                    if (movie.title.IndexOf(inputTitle, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        Thread.Sleep(2000);
                        help.WriteData($"ID: {movie.mediaId}, Movie Title: {movie.title}, Director: {movie.director}, Running Time: {movie.runningTime}");
                        help.WriteData("Genres: ");

                        foreach (var category in movie.genres)
                        {
                            help.WriteData("\t" + category);
                        }

                        Console.WriteLine("-- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- --");
                        found = true;
                        matches++;
                    }
                }

                if (!found)
                {
                    help.WriteError("Title not found, please try a different movie title.\n");
                }
                else
                {
                    help.WriteInfo($"Found {matches} matching titles for '{inputTitle}'.");
                    menu.DisplayMenu();
                    break;
                }
            }
            catch (Exception ex)
            {
                help.WriteError($"Something went wrong while reading the data. Exception: {ex.Message}");
                break;
            }
        }
    }



    public void ViewMoviesByCategory()
    {

        var menu = new MenuServices();
        var help = new Helpers();

        Movies = new List<Movie>();
        // Path to the file
        filePath = "MovieMedia/movies.scrubbed.csv";

        while (true)
        {
            help.WriteInfo("Enter a movie category or type 'MENU' to return to the main menu: ");

            string inputCategory = Console.ReadLine().ToLower();

            // Check if user typed 'MENU'
            if (inputCategory.ToLower() == "menu")
            {
                menu.DisplayMenu(); // Assuming Menu() is your method to display main menu.
                return;
            }
            else
            // Check if the input is empty or contains numbers
            if (string.IsNullOrWhiteSpace(inputCategory) || inputCategory.Any(char.IsDigit))
            {
                help.WriteError("Please enter a valid movie category!\n");
                continue;
            }

            try
            { // Read the file
                var movieLines = File.ReadAllLines(filePath);
                bool found = false;
                int matches = 0;
                //read each line in the file
                foreach (var movieLine in movieLines)
                {//parse movie info and check format
                    var parts = movieLine.Split(',');

                    if (parts.Length < 3)
                    {
                        help.WriteError("Invalid data format in file.");
                        continue;
                    }

                    var id = parts[0];
                    var name = parts[1];
                    var categories = parts[2].ToLower().Split('|');
                    // Create output of movie info
                    if (categories.Contains(inputCategory.ToLower()))
                    {
                        help.WriteData($"ID: {id}, Movie Title: {name}");
                        help.WriteData("Categories: ");
                        //iterate through each category
                        foreach (var category in categories)
                        {
                            help.WriteData("\t" + category);
                        }

                        Console.WriteLine("-- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- --");

                        found = true;
                        matches++;


                    }
                }
                //get back to the main menu if successful search
                if (found)
                {
                    help.WriteInfo($"Found {matches} matching categories for '{inputCategory}'.");
                    menu.DisplayMenu();
                    break;
                }
                else
                {//prompt user to try a different category
                    if (!found)
                    {
                        help.WriteError("Category not found, please try a different category.\n");
                        continue;
                    }

                }
            }//if all else fails ...whoops ...
            catch (Exception ex)
            {
                help.WriteError($"Something went wrong while reading the data. Exception: {ex.Message}");
                break;
            }
        }
    }
    const string MENU_COMMAND = "menu";
    public void AddMovie()
    {
        var menu = new MenuServices();
        var help = new Helpers();

        Movies = new List<Movie>();
        // Path to the file
        filePath = "MovieMedia/movies.scrubbed.csv";
        //check for file to read

        if (!File.Exists(filePath))
        {
            help.WriteError($"File '{filePath}' not found.");
            return;
        }

        string[] lines;

        try//read the file
        {
            lines = File.ReadAllLines(filePath);
        }
        catch
        {
            help.WriteError($"Cannot open the file at '{filePath}'.");
            return;
        }
        //create a new movie id
        int newId = lines.Length > 0 ? (int.TryParse(lines[^1].Split(',')[0], out var id) ? id + 1 : 0) : 1;

        if (newId == 0)
        {
            help.WriteError("Last movie ID is not a number.");
            return;
        }

        while (true)
        {//prompt for new movie
            help.WriteInfo($"Enter movie name or type '{MENU_COMMAND}' to return to the main menu: ");
            string name = Console.ReadLine().Trim();

            //Check for MENU input
            if (name.ToLower() == MENU_COMMAND)
            {
                menu.DisplayMenu();  //assuming you have a DisplayMenu function
                return;
            }

            //check for null or empty string from user input
            if (string.IsNullOrWhiteSpace(name))
            {
                help.WriteError("Please enter a valid movie name!\n");
                continue;
            }

            //check if movie name already exists
            if (lines.Any(line => line.Split(',')[1].ToLower().Trim() == name.ToLower()))
            {
                help.WriteError("That Movie already exists! Try Again.\n");
                continue;
            }

            //convert to proper case for csv insert
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            name = textInfo.ToTitleCase(name);

            //get categories from user
            help.WriteInfo($"Enter movie categories (separated by '|') or type '{MENU_COMMAND}' to return to the main menu: ");
            string categories = Console.ReadLine().Trim();

            //Check for menu input
            if (categories.ToLower() == MENU_COMMAND)
            {
                menu.DisplayMenu();  //assuming you have a DisplayMenu function
                return;
            }
            //check for null or empty string from user input
            if (string.IsNullOrWhiteSpace(categories) || categories.Any(char.IsDigit) || categories.Contains(','))
            {
                help.WriteError("Categories cannot be empty, contain digits or commas.\n");
                continue;
            }//correct input for csv insert
            categories = textInfo.ToTitleCase(categories);

            help.WriteInfo($"Enter movie Director or type '{MENU_COMMAND}' to return to the main menu: ");
            string director = Console.ReadLine().Trim();

            //Check for MENU input
            if (director.ToLower() == MENU_COMMAND)
            {
                menu.DisplayMenu();  //assuming you have a DisplayMenu function
                return;
            }

            //check for null or empty string from user input
            if (string.IsNullOrWhiteSpace(director) || director.Any(char.IsDigit) || director.Contains(','))
            {
                help.WriteError("Please enter a valid movie director's name!\n");
                continue;
            }

            TimeSpan movieRunTime = new TimeSpan();
            while (true)
            {
                //help to create timespan variable for user input

                help.WriteInfo($"Please enter the running time as six digits (HHMMSS) without any separators, or type '{MENU_COMMAND}' to return to the main menu: ");
                string runningTimeString = Console.ReadLine().Trim();

                //Check for MENU input
                if (runningTimeString.ToLower() == MENU_COMMAND)
                {
                    menu.DisplayMenu();  //assuming you have a DisplayMenu function
                    return;
                }



                if (runningTimeString.Length != 6 || !int.TryParse(runningTimeString, out _))
                {
                    Console.WriteLine("Invalid input! Please enter the running time as six digits (HHMMSS) without any separators.");
                    return;
                }

                int runningTimeHours = int.Parse(runningTimeString.Substring(0, 2));
                int runningTimeMinutes = int.Parse(runningTimeString.Substring(2, 2));
                int runningTimeSeconds = int.Parse(runningTimeString.Substring(4, 2));



                //if all checks pass and TimeSpan parsing was successful, break the loop
                break;
            }

            //parse the info for text output
            string newMovieLine = $"{newId},{name},{categories},{director},{movieRunTime.Hours:D2}:{movieRunTime.Minutes:D2}:{movieRunTime.Seconds:D2}";



            //write to file
            using (StreamWriter sw = File.AppendText(filePath))
            {
                sw.WriteLine(newMovieLine);
            }

            //pause for console ui cause its nice
            Thread.Sleep(2000);

            //success message
            help.WriteSuccess("-- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- --");
            help.WriteSuccess("New movie added successfully!");
            help.WriteSuccess("-- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- --");

            break;
        }
        menu.DisplayMenu();
    }


}

