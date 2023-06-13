// See https://aka.ms/new-console-template for more information
using MessageLogger;

Console.WriteLine("Welcome to Message Logger!");
Console.WriteLine();
Console.WriteLine("Let's create a user pofile for you.");
Console.Write("What is your name? ");
string name = Console.ReadLine();
Console.Write("What is your username? (one word, no spaces!) ");
string username = Console.ReadLine();
User user = new User(name, username);
//save user here
using (var context = new MessageLoggerContext())
{
    context.Users.Add(user);
    context.SaveChanges();
}
    Console.WriteLine();
Console.WriteLine("To log out of your user profile, enter `log out`.");

Console.WriteLine();
Console.Write("Add a message (or `quit` to exit): ");

string userInput = Console.ReadLine();
List<User> users = new List<User>() { user };///create using statement using MessageLonggerContext
//create message

while (userInput.ToLower() != "quit")
{
    while (userInput.ToLower() != "log out")
    {
      // user.Messages.Add(new Message(userInput));//take the new message and write it to database
        // var message1 = new Message(userInput)
        using (var context = new MessageLoggerContext())
        {
            var message1 = new Message(userInput);
            user.Messages.Add(message1);
            context.Messages.Add(message1);
            context.SaveChanges();
        }
        foreach (var message in user.Messages)
        {
            Console.WriteLine($"{user.Name} {message.CreatedAt:t}: {message.Content}");
        }

        Console.Write("Add a message: ");

        userInput = Console.ReadLine();
        Console.WriteLine();
    }

    Console.Write("Would you like to log in a `new` or `existing` user? Or, `quit`? ");
    userInput = Console.ReadLine();
    if (userInput.ToLower() == "new")
    {
        Console.Write("What is your name? ");
        name = Console.ReadLine();
        Console.Write("What is your username? (one word, no spaces!) ");
        username = Console.ReadLine();              //Add using staetment to save user
        user = new User(name, username);
        using (var context = new MessageLoggerContext())
        {
            context.Users.Add(user);
            context.SaveChanges();
        }
            users.Add(user);
        Console.Write("Add a message: ");

        userInput = Console.ReadLine();

    }
    else if (userInput.ToLower() == "existing")
    {
        Console.Write("What is your username? ");
        username = Console.ReadLine();
        user = null;
        foreach (var existingUser in users)
        {
            if (existingUser.Username == username)
            {
                user = existingUser;
            }
        }
        
        if (user != null)
        {
            Console.Write("Add a message: ");               ///A context holder to save messages to context?!!
            userInput = Console.ReadLine();
        }
        else
        {
            Console.WriteLine("could not find user");
            userInput = "quit";

        }
    }

}

Console.WriteLine("Thanks for using Message Logger!");
foreach (var u in users)
{
    Console.WriteLine($"{u.Name} wrote {u.Messages.Count} messages.");
}
