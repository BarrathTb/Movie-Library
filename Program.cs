using NLog;
using System.Globalization;
using System.Net;
using System.Reflection.Metadata;




string path = Directory.GetCurrentDirectory() + "\\nlog.config";
// create instance of Logger
var logger = LogManager.Setup().LoadConfigurationFromFile(path).GetCurrentClassLogger();
var menu = new Menu();
var mvArt = new Art();


//display movie art and title -- methods in ArtMethods.cs
mvArt.MovieTitleArt();
mvArt.MovieArtOne();
mvArt.MovieArtTwo();



Thread.Sleep(2000);
//Prompt the user for input
Console.WriteLine("Welcome to the Movie Library!\n");
Console.WriteLine("-- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- --\n");
menu.DisplayMenu();//menu in MenuServices/Services.cs


//************************************************************************************************
//main menu loop
bool exitFlag = false;
do
{

  _ = int.TryParse(Console.ReadLine(), out int option);
  switch (option)
  {
    case 1:
      ViewMovies();
      break;
    case 2:
      ViewMoviesByCategory();
      break;
    case 3:
      ViewMoviesByTitle();
      break;
    case 4:
      AddMovie();
      break;
    case 5:
      WriteSuccess("Thanks for visiting the Movie Library!\n");
      Thread.Sleep(2000);
      mvArt.MovieBye();
      exitFlag = true;
      break;
    default:
      if (option == 0)
      {
        WriteError("Please select a valid option!\n");
        continue;
      }
      exitFlag = true;
      break;
  }
} while (!exitFlag);


//************************************************************************************************
//view movies by title

void ViewMoviesByTitle()
{
  string filePath = "MovieMedia/movies.csv";

  // Loop that repeats until correct input is given or user opts to leave
  while (true)
  {
    WriteInfo("Enter a movie title or type 'MENU' to return to the main menu: ");

    string inputTitle = Console.ReadLine().ToLower();

    // Check if user typed 'MENU'
    if (inputTitle.ToLower() == "menu")
    {
      menu.DisplayMenu(); // Assuming Menu() is your method to display main menu.
      return;
    }

    if (string.IsNullOrEmpty(inputTitle))
    {
      WriteError("Please enter a valid movie title!\n");
      continue;
    }

    try
    {
      var movieLines = File.ReadAllLines(filePath);
      bool found = false;

      foreach (var movieLine in movieLines)
      {
        var parts = movieLine.Split(',').Select(e => e.Trim('\"')).ToArray();

        if (parts.Length < 3)
        {
          WriteError("Invalid data format in file.");
          continue;
        }

        var id = parts[0];
        string titleFromCSV = parts[1].Trim().ToLower(); // Convert title from CSV to lower case
        var categories = parts[2].Trim().Split('|');

        if (titleFromCSV.IndexOf(inputTitle, StringComparison.OrdinalIgnoreCase) >= 0)
        {
          Thread.Sleep(2000);
          WriteData($"ID: {id}, Movie Title: {titleFromCSV}");
          WriteData("Categories: ");

          foreach (var category in categories)
          {
            WriteData("\t" + category);
          }

          Console.WriteLine("-- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- --");
          found = true;

        }
      }

      if (found)
      {
        menu.DisplayMenu();
        break;
      }
      else
      {
        if (!found)
        {
          WriteError("Title not found, please try a different movie title.\n");
          continue;
        }
      }
    }
    catch (Exception ex)
    {
      WriteError($"Something went wrong while reading the data. Exception: {ex.Message}");
      break;
    }
  }
}


//************************************************************************************************
//view movies by category

void ViewMoviesByCategory()
{
  // Path to the file
  string filePath = "MovieMedia/movies.csv";

  while (true)
  {
    WriteInfo("Enter a movie category or type 'MENU' to return to the main menu: ");

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
      WriteError("Please enter a valid movie category!\n");
      continue;
    }

    try
    { // Read the file
      var movieLines = File.ReadAllLines(filePath);
      bool found = false;
      //read each line in the file
      foreach (var movieLine in movieLines)
      {//parse movie info and check format
        var parts = movieLine.Split(',');

        if (parts.Length < 3)
        {
          WriteError("Invalid data format in file.");
          continue;
        }

        var id = parts[0];
        var name = parts[1];
        var categories = parts[2].ToLower().Split('|');
        // Create output of movie info
        if (categories.Contains(inputCategory.ToLower()))
        {
          WriteData($"ID: {id}, Movie Title: {name}");
          WriteData("Categories: ");
          //iterate through each category
          foreach (var category in categories)
          {
            WriteData("\t" + category);
          }

          Console.WriteLine("-- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- --");

          found = true;


        }
      }
      //get back to the main menu if successful search
      if (found)
      {
        menu.DisplayMenu();
        break;
      }
      else
      {//prompt user to try a different category
        if (!found)
        {
          WriteError("Category not found, please try a different category.\n");
          continue;
        }

      }
    }//if all else fails ...whoops ...
    catch (Exception ex)
    {
      WriteError($"Something went wrong while reading the data. Exception: {ex.Message}");
      break;
    }
  }
}



//******************************************************************************************************
//view movie list method

void ViewMovies()
{

  //grab the movie file path
  string filePath = "MovieMedia/movies.csv";

  //check if the file exists
  if (!File.Exists(filePath))
  {
    WriteError($"File '{filePath}' not found.");
    return;
  }

  try
  {
    //read the file
    var movieLines = File.ReadAllLines(filePath);

    //parse the lines
    foreach (var movieLine in movieLines)
    {
      var parts = movieLine.Split(',');
      //check if the line is valid
      if (parts.Length < 3)
      {
        WriteError("Invalid data format in file.");
        continue;
      }

      //parse the info for text output
      var id = parts[0];
      var title = parts[1];
      var categories = parts[2].Split('|');

      //list output helper
      WriteData($"ID: {id}, Movie Title: {title}");
      WriteData("Categories: ");


      //iterate over the categories
      foreach (var category in categories)
      {
        WriteData("\t" + category);
      }

      Console.WriteLine("-- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- --");
    }
    menu.DisplayMenu();
  }
  //if all else fails
  catch (Exception ex)
  {
    WriteError($"Exception occurred: {ex.Message}");
  }
}


//*********************************************************************************************************
//add movie method

void AddMovie()
{
  string filePath = "MovieMedia/movies.csv";
  //check for file to read
  if (!File.Exists(filePath))
  {
    WriteError($"File '{filePath}' not found.");
    return;
  }

  string[] lines;

  try//read the file
  {
    lines = File.ReadAllLines(filePath);
  }
  catch
  {
    WriteError($"Cannot open the file at '{filePath}'.");
    return;
  }
  //create a new movie id
  int newId = lines.Length > 0 ? (int.TryParse(lines[^1].Split(',')[0], out var id) ? id + 1 : 0) : 1;

  if (newId == 0)
  {
    WriteError("Last movie ID is not a number.");
    return;
  }

  while (true)
  {//prompt for new movie
    WriteInfo("Enter movie name or type 'menu' to return to the main menu: ");
    string name = Console.ReadLine().Trim();

    //Check for MENU input
    if (name.ToLower() == "menu")
    {
      menu.DisplayMenu();  //assuming you have a DisplayMenu function
      return;
    }

    //check for null or empty string from user input
    if (string.IsNullOrWhiteSpace(name))
    {
      WriteError("Please enter a valid movie name!\n");
      continue;
    }

    //check if movie name already exists
    if (lines.Any(line => line.Split(',')[1].ToLower().Trim() == name.ToLower()))
    {
      WriteError("That Movie already exists! Try Again.\n");
      continue;
    }

    //convert to proper case for csv insert
    TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
    name = textInfo.ToTitleCase(name);

    //get categories from user
    WriteInfo("Enter movie categories (separated by '|'): ");
    string categories = Console.ReadLine().Trim();

    //Check for menu input
    if (categories.ToLower() == "menu")
    {
      menu.DisplayMenu();  //assuming you have a DisplayMenu function
      return;
    }
    //check for null or empty string from user input
    if (string.IsNullOrWhiteSpace(categories) || categories.Any(char.IsDigit) || categories.Contains(','))
    {
      WriteError("Categories cannot be empty, contain digits or commas.\n");
      continue;
    }//correct input for csv insert
    categories = textInfo.ToTitleCase(categories);

    //parse the info for text output
    string newMovieLine = $"{newId},{name},{categories}";

    //write to file
    using (StreamWriter sw = File.AppendText(filePath))
    {
      sw.WriteLine(newMovieLine);
    }

    //pause for console ui cause its nice
    Thread.Sleep(2000);

    //success message
    WriteSuccess("-- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- --");
    WriteSuccess("New movie added successfully!");
    WriteSuccess("-- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- --");

    break;
  }
  menu.DisplayMenu();
}


//************************************************************************************************
//Helper Methods
//************************************************************************************************

//user prompts for input
void WriteInfo(string message)
{
  Console.ForegroundColor = ConsoleColor.Blue;
  Console.WriteLine(message);
  Console.ResetColor();
}
//movie list ooutput helper
void WriteData(string message)
{
  Console.ForegroundColor = ConsoleColor.Yellow;
  Console.WriteLine(message);
  Console.ResetColor();
}
//error message
void WriteError(string message)
{
  Console.ForegroundColor = ConsoleColor.Red;
  Console.WriteLine(message);
  Console.ResetColor();
}
//success message
void WriteSuccess(string message)
{
  Console.ForegroundColor = ConsoleColor.Green;
  Console.WriteLine(message);
  Console.ResetColor();
}




