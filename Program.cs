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
string postFind = " ";

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
    if (choice == "3")
    {
        logger.Info($"Option {"3"} selected");
        Console.WriteLine("Select the blog you want to post to:");
        var query = db.Blogs.OrderBy(b => b.BlogId);
        foreach (var item in query)
        {

            Console.WriteLine($"{item.BlogId}) {item.Name}");

        }
        var blogFind = Console.ReadLine();
        int ID = int.Parse(blogFind);
        var blogID = new Blog { BlogId = ID };
        List<Blog> blogs = db.Blogs.Where(b => b.Name.Contains(blogFind)).ToList();
        Blog blog = new Blog();

        Console.WriteLine("Enter the Post title");
        string postTitle = Console.ReadLine();
        if (string.IsNullOrEmpty(postTitle))
        {
            logger.Error("Blog name cannot be null");
            { break; }
        }
        Console.WriteLine("Enter the Post content");
        string postContent = Console.ReadLine();
        if (string.IsNullOrEmpty(postContent))
        {
            logger.Error("Blog name cannot be null");
            { break; }
        }
        var PostTitle = new Post { Title = postTitle, Content = postContent, BlogId = ID };

        db.addPost(PostTitle);
    }

    if (choice == "4")
    {
        logger.Info($"Option {"4"} selected");
        Console.WriteLine("Select the blog's posts to display");
        Console.WriteLine("0) Post from all blogs");
        var query = db.Blogs.OrderBy(b => b.BlogId);
        foreach (var item in query)
        {
            Console.WriteLine($"{item.BlogId}) {item.Name}");

        }
        postFind = Console.ReadLine();
        Post post = new Post();
        int ID = int.Parse(postFind);
        var blogID = new Blog { BlogId = ID };
        string BlogID = post.BlogId.ToString();
        List<Post> posts = db.Posts.Where(b => BlogID.Contains(postFind)).ToList();
        var queryPost = db.Posts.OrderBy(b => b.BlogId);
        Blog blog = new Blog();
        foreach (var item in queryPost)
        {
            if (item.BlogId == ID)
            {
                Console.WriteLine($"Blog: {item.BlogId}\nTitle: {item.Title}\nContent: {item.Content}");
                Console.WriteLine("");
            }
        }


        if (blog.BlogId != ID)
        {
            logger.Info("Invaild Blog ID");
        }


        if (postFind == "0")
        {

            var queryName = db.Blogs.OrderBy(b => b.Name);
            var queryPost0 = db.Posts.OrderBy(b => b.PostId);
            int total1 = db.Posts.Count();
            Console.WriteLine($"{total1} post(s) returned");
            foreach (var item0 in queryPost)
            {
                Console.WriteLine("");
                Console.WriteLine($"Blog: {item0.BlogId}\nTitle: {item0.Title}\nContent: {item0.Content}");
                Console.WriteLine("");
            }

        }


    }

    if (choice == "q")
    {
        logger.Info("Program ended");
    }


} while (choice == "1" || choice == "2" || choice == "3" || choice == "4");