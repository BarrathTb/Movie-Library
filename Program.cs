using NLog;
using System.Globalization;
using System.Net;
using System.Reflection.Metadata;
using System.Linq;




string path = Directory.GetCurrentDirectory() + "\\nlog.config";
// create instance of Logger
var logger = LogManager.Setup().LoadConfigurationFromFile(path).GetCurrentClassLogger();

logger.Info("Program started");

// //instance of movie
// Movie movie = new Movie
// {
//   mediaId = 123,
//   title = "Greatest Movie Ever, The (2023)",
//   director = "Jeff Grissom",
//   // timespan (hours, minutes, seconds)
//   runningTime = new TimeSpan(2, 21, 23),
//   genres = { "Comedy", "Romance" }
// };
// Console.WriteLine(movie.Display());

// //instance of album
// Album album = new Album
// {
//   mediaId = 321,
//   title = "Greatest Album Ever, The (2020)",
//   artist = "Jeff's Awesome Band",
//   recordLabel = "Universal Music Group",
//   genres = { "Rock" }
// };
// Console.WriteLine(album.Display());

// //instance of book 
// Book book = new Book
// {
//   mediaId = 111,
//   title = "Super Cool Book",
//   author = "Jeff Grissom",
//   pageCount = 101,
//   publisher = "",
//   genres = { "Suspense", "Mystery" }
// };
// Console.WriteLine(book.Display());
string scrubbedFile = FileScrubber.ScrubMovies("MovieMedia/movies.csv");
logger.Info(scrubbedFile);





var help = new Helpers();
var menu = new MenuServices();
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
      menu.ViewMovies();
      break;
    case 2:
      menu.ViewMoviesByCategory();
      break;
    case 3:
      menu.ViewMoviesByTitle();
      break;
    case 4:
      menu.AddMovie();
      break;
    case 5:
      help.WriteSuccess("Thanks for visiting the Movie Library!\n");
      Thread.Sleep(2000);
      mvArt.MovieBye();
      exitFlag = true;
      break;
    default:
      if (option == 0)
      {
        help.WriteError("Please select a valid option!\n");
        continue;
      }
      exitFlag = true;
      break;
  }
} while (!exitFlag);
logger.Info("Program ended");
