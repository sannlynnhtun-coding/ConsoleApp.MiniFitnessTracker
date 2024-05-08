namespace ConsoleApp.MiniFitnessTracker;

internal class Activity
{
    public enum ActivityType { Walking, Swimming, Cycling, Running, Yoga, Weights }
    public ActivityType Type { get; private set; }
    public double Metric1 { get; private set; }
    public double Metric2 { get; private set; }
    public double Metric3 { get; private set; }

    public Activity(ActivityType type, double metric1, double metric2, double metric3)
    {
        Type = type;
        Metric1 = metric1;
        Metric2 = metric2;
        Metric3 = metric3;
    }

    public double CalculateCaloriesBurned()
    {
        switch (Type)
        {
            case ActivityType.Walking:
                // Sample formula (replace with more accurate if needed)
                return Metric1 * 0.035;
            case ActivityType.Swimming:
                // Sample formula (replace with more accurate if needed)
                return Metric2 * 8;
            default:
                // Implement calculations for other activities
                throw new NotImplementedException("Calories burned calculation not implemented for this activity type.");
        }
    }
}