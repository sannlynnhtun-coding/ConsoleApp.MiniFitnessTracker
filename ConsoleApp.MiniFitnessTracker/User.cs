namespace ConsoleApp.MiniFitnessTracker;

internal class User
{
    private string username;
    private string password;
    private int calorieGoal;
    private int loginAttempts = 0; // Track login attempts

    public User(string username, string password)
    {
        if (!ValidateUsername(username))
        {
            throw new ArgumentException("Username must contain only letters and numbers.");
        }

        if (!ValidatePassword(password))
        {
            throw new ArgumentException("Password must be 12 characters long with at least 1 lowercase and 1 uppercase letter.");
        }

        this.username = username;
        this.password = password;
        calorieGoal = 0; // Default goal
    }

    public void SetCalorieGoal(int goal)
    {
        if (goal < 0)
        {
            throw new ArgumentException("Calorie goal cannot be negative.");
        }

        calorieGoal = goal;
    }

    public int GetCalorieGoal()
    {
        return calorieGoal;
    }

    public string GetUsername()
    {
        return username;
    }

    public string GetPassword()
    {
        return password;
    }

    private bool ValidateUsername(string username)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(username, "^[a-zA-Z0-9]+$");
    }

    private bool ValidatePassword(string password)
    {
        return password.Length == 12 &&
               password.Any(char.IsLower) &&
               password.Any(char.IsUpper);
    }

    public bool CheckLoginAttempts()
    {
        return loginAttempts < 3; // Allow 3 login attempts
    }

    public void ResetLoginAttempts()
    {
        loginAttempts = 0;
    }

    public void IncrementLoginAttempts()
    {
        loginAttempts++;
    }
}