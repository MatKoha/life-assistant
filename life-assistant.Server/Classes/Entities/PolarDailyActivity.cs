public class PolarDailyActivity
{
    public long Id { get; set; }
    public int UserId { get; set; }
    public DateTime Date { get; set; }
    public DateTime Created { get; set; }
    public int Calories { get; set; }
    public int ActiveCalories { get; set; }
    public int Duration { get; set; }
    public int ActiveSteps { get; set; }
    public virtual PolarUser User { get; set; }
}