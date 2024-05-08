using static ConsoleApp.MiniFitnessTracker.Activity;

namespace ConsoleApp.MiniFitnessTracker;

internal class FitnessTrackerApp
{
    private User? currentUser;
    private const int MAX_LOGIN_ATTEMPTS = 3; // Maximum allowed login attempts

    public FitnessTrackerApp()
    {
        currentUser = null;
    }

    public void RegisterUser(string username, string password)
    {
        currentUser = new User(username, password);
        Console.WriteLine("Registration successful!");
    }

    public bool LoginUser(string username, string password)
    {
        if (currentUser != null || !currentUser.CheckLoginAttempts())
        {
            Console.WriteLine("Maximum login attempts reached. Please try again later.");
            return false;
        }

        if (currentUser == null || !currentUser.GetUsername().Equals(username) || !currentUser.GetPassword().Equals(password))
        {
            Console.WriteLine("Invalid username or password.");
            currentUser.IncrementLoginAttempts();
            return false;
        }

        currentUser.ResetLoginAttempts();
        Console.WriteLine("Login successful!");
        return true;
    }

    public void SetCalorieGoal()
    {
        Console.Write("Please Set Goal: ");
        int goal = Convert.ToInt32(Console.ReadLine());
        if (currentUser == null)
        {
            Console.WriteLine("Please log in to set a calorie goal.");
            return;
        }

        currentUser.SetCalorieGoal(goal);
    }

    public void RecordActivity(ActivityType type, double metric1, double metric2, double metric3)
    {
        if (currentUser == null)
        {
            Console.WriteLine("Please log in to record activities.");
            return;
        }

        var activity = new Activity(type, metric1, metric2, metric3);
        double caloriesBurned = activity.CalculateCaloriesBurned();
        Console.WriteLine("You burned approximately {0:0.0} calories from {1}.", caloriesBurned, type.ToString());
    }

    public void ShowProgress()
    {
        if (currentUser == null)
        {
            Console.WriteLine("Please log in to view your progress.");
            return;
        }

        Console.WriteLine("Username: {0}", currentUser.GetUsername());
        Console.WriteLine("Calorie Goal: {0} calories", currentUser.GetCalorieGoal());
        // Add logic to calculate and display total calories burned from recorded activities (exercise for the user)
    }

    public void Run()
    {
        int choice;

        do
        {
            Console.Clear();
            Console.WriteLine("Fitness Tracker Menu");
            Console.WriteLine("1. Register");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Set Calorie Goal");
            Console.WriteLine("4. Record Activity");
            Console.WriteLine("5. Show Progress");
            Console.WriteLine("6. Exit");
            Console.Write("Enter your choice: ");

            choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    RegisterUser();
                    break;
                case 2:
                    LoginUser();
                    break;
                case 3:
                    SetCalorieGoal();
                    break;
                case 4:
                    RecordActivity();
                    break;
                case 5:
                    ShowProgress();
                    break;
                case 6:
                    Console.WriteLine("Exiting Fitness Tracker...");
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }

        } while (choice != 6);
    }

    private void RegisterUser()
    {
        string username, password;

        Console.Write("Enter username: ");
        username = Console.ReadLine();

        Console.Write("Enter password: ");
        password = Console.ReadLine();

        try
        {
            RegisterUser(username, password);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine("Error: {0}", e.Message);
        }
    }

    private void LoginUser()
    {
        string username, password;

        Console.Write("Enter username: ");
        username = Console.ReadLine();

        Console.Write("Enter password: ");
        password = Console.ReadLine();

        if (LoginUser(username, password))
        {
            currentUser = GetUserByUsername(username); // Update currentUser for access control
        }
    }

    private User GetUserByUsername(string username)
    {
        // Implement logic to retrieve user from a data store (e.g., database) based on username
        // This is a simplified example assuming in-memory storage
        if (currentUser != null && currentUser.GetUsername().Equals(username))
        {
            return currentUser;
        }
        return null;
    }

    private void RecordActivity()
    {
        if (currentUser == null)
        {
            return;
        }

        int activityChoice;
        double metric1, metric2, metric3;

        Console.WriteLine("Select activity:");
        Console.WriteLine("1. Walking");
        Console.WriteLine("2. Swimming");
        // Add options for other activities

        activityChoice = Convert.ToInt32(Console.ReadLine());

        switch (activityChoice)
        {
            case 1:
                Console.Write("Enter steps walked: ");
                metric1 = Convert.ToDouble(Console.ReadLine());
                Console.Write("Enter distance walked (km): ");
                metric2 = Convert.ToDouble(Console.ReadLine());
                Console.Write("Enter time taken (minutes): ");
                metric3 = Convert.ToDouble(Console.ReadLine());
                RecordActivity(ActivityType.Walking, metric1, metric2, metric3);
                break;
            case 2:
                Console.Write("Enter number of laps: ");
                metric1 = Convert.ToDouble(Console.ReadLine());
                Console.Write("Enter time taken (minutes)");
                metric2 = Convert.ToDouble(Console.ReadLine());
                Console.Write("Enter average heart rate (bpm): ");
                metric3 = Convert.ToDouble(Console.ReadLine());
                RecordActivity(ActivityType.Swimming, metric1, metric2, metric3);
                break;
            // Add logic for other activities
            default:
                Console.WriteLine("Invalid activity choice.");
                break;
        }
    }
}