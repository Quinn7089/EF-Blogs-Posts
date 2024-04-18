using BlogConsole.Migrations;
using NLog;
using NLog.Config;
using System.Linq;

// See https://aka.ms/new-console-template for more information
string path = Directory.GetCurrentDirectory() + "\\nlog.config";

// create instance of Logger
var logger = LogManager.LoadConfiguration(path).GetCurrentClassLogger();
logger.Info("Program started");
string choice = "";

do
{
    Console.WriteLine("Enter your selection");
    Console.WriteLine("1) Display all blogs");
    Console.WriteLine("2) Add Blog");
    Console.WriteLine("3) Create Post");
    Console.WriteLine("4) Display Post");
    Console.WriteLine("Enter q to quit");
    choice = Console.ReadLine();
    var db = new BloggingContext();
    if (choice == "1")
    {
        try
        {

            // Display all Blogs from the database
            var query = db.Blogs.OrderBy(b => b.Name);
            int total = db.Blogs.Count();

            logger.Info($"Option {"1"} selected");
            Console.WriteLine($"{total} Blogs returned:");
            foreach (var item in query)
            {
                Console.WriteLine(item.Name);

            }

            Console.WriteLine("");


        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }


    }
    if (choice == "2")
    {
        try
        {
            // Create and save a new Blog
            logger.Info($"Option {"2"} selected");
            Console.Write("Enter a name for a new Blog: ");
            string name = Console.ReadLine();
            if (string.IsNullOrEmpty(name))
            {
                logger.Error("Blog name cannot be null");
            }
            else
            {
                var blog = new Blog { Name = name };

                db.AddBlog(blog);
                logger.Info("Blog added - {name}", name);
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }




} while (choice == "1" || choice == "2" || choice == "3" || choice == "4");