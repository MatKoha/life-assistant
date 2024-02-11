public class PolarUser
{
    public int Id { get; set; }
    public int MemberId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string AccessToken { get; set; }
    public virtual ICollection<PolarDailyActivity> PolarDailyActivities { get; set; } = new List<PolarDailyActivity>();

}