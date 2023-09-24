using System.Runtime.InteropServices;
using System.Reflection;

public class Art
{
    //Helper Methods for art display
    public void MovieTitleArt()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            try
            {
                Console.SetWindowSize(153, 25);

                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = assembly.GetManifestResourceNames()
                  .FirstOrDefault(str => str.EndsWith(".ConsoleStyle.movieTitle.txt"));

                // Check if the resource exists.
                if (resourceName == null)
                {
                    throw new InvalidOperationException("Resource .ConsoleStyle.movieTitle.txt not found.");
                }

                using Stream stream = assembly.GetManifestResourceStream(resourceName);

                // Check if the stream is valid.
                if (stream == null)
                {
                    throw new InvalidOperationException("Unable to get resource stream.");
                }

                using StreamReader reader = new StreamReader(stream);
                string result = reader.ReadToEnd();

                var defaultForgroundColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Thread.Sleep(2000);
                Console.WriteLine(result);

                Console.ForegroundColor = defaultForgroundColor;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }

    public void MovieMenuArt()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            try
            {
                Console.SetWindowSize(153, 25);

                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = assembly.GetManifestResourceNames()
                  .FirstOrDefault(str => str.EndsWith(".ConsoleStyle.movieMenu.txt"));

                // Check if the resource exists.
                if (resourceName == null)
                {
                    throw new InvalidOperationException("Resource .ConsoleStyle.movieTitle.txt not found.");
                }

                using Stream stream = assembly.GetManifestResourceStream(resourceName);

                // Check if the stream is valid.
                if (stream == null)
                {
                    throw new InvalidOperationException("Unable to get resource stream.");
                }

                using StreamReader reader = new StreamReader(stream);
                string result = reader.ReadToEnd();

                var defaultForgroundColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Thread.Sleep(2000);
                Console.WriteLine(result);

                Console.ForegroundColor = defaultForgroundColor;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }


    public void MovieArtOne()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            try
            {
                Console.SetWindowSize(153, 25);

                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = assembly.GetManifestResourceNames()
                  .FirstOrDefault(str => str.EndsWith(".ConsoleStyle.movieArt1.txt"));

                // Check if the resource exists.
                if (resourceName == null)
                {
                    throw new InvalidOperationException("Resource .ConsoleStyle.movieTitle.txt not found.");
                }

                using Stream stream = assembly.GetManifestResourceStream(resourceName);

                // Check if the stream is valid.
                if (stream == null)
                {
                    throw new InvalidOperationException("Unable to get resource stream.");
                }

                using StreamReader reader = new StreamReader(stream);
                string result = reader.ReadToEnd();

                var defaultForgroundColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Thread.Sleep(2000);
                Console.WriteLine(result);

                Console.ForegroundColor = defaultForgroundColor;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }


    public void MovieArtTwo()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            try
            {
                Console.SetWindowSize(153, 25);

                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = assembly.GetManifestResourceNames()
                  .FirstOrDefault(str => str.EndsWith(".ConsoleStyle.movieArt2.txt"));

                // Check if the resource exists.
                if (resourceName == null)
                {
                    throw new InvalidOperationException("Resource .ConsoleStyle.movieTitle.txt not found.");
                }

                using Stream stream = assembly.GetManifestResourceStream(resourceName);

                // Check if the stream is valid.
                if (stream == null)
                {
                    throw new InvalidOperationException("Unable to get resource stream.");
                }

                using StreamReader reader = new StreamReader(stream);
                string result = reader.ReadToEnd();

                var defaultForgroundColor = Console.ForegroundColor;
                var backgroundColor = Console.BackgroundColor;
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Thread.Sleep(2000);
                Console.WriteLine(result);

                Console.ForegroundColor = defaultForgroundColor;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }

    public void MovieBye()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            try
            {
                Console.SetWindowSize(153, 25);

                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = assembly.GetManifestResourceNames()
                  .FirstOrDefault(str => str.EndsWith(".ConsoleStyle.movieBye.txt"));

                // Check if the resource exists.
                if (resourceName == null)
                {
                    throw new InvalidOperationException("Resource .ConsoleStyle.movieTitle.txt not found.");
                }

                using Stream stream = assembly.GetManifestResourceStream(resourceName);

                // Check if the stream is valid.
                if (stream == null)
                {
                    throw new InvalidOperationException("Unable to get resource stream.");
                }

                using StreamReader reader = new StreamReader(stream);
                string result = reader.ReadToEnd();

                var defaultForgroundColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Thread.Sleep(2000);
                Console.WriteLine(result);

                Console.ForegroundColor = defaultForgroundColor;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }


}