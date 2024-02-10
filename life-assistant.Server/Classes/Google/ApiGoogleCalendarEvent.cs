using Newtonsoft.Json;

public class ApiGoogleCalendarEvent
{
    [JsonProperty("id")]
    public string Id { get; set; }
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("start")]
    public DateTime? Start { get; set; }

    [JsonProperty("end")]
    public DateTime? End { get; set; }

    [JsonProperty("location")]
    public string Location { get; set; }

    public ApiGoogleCalendarEvent(Google.Apis.Calendar.v3.Data.Event calendarEvent)
    {
        this.Id = calendarEvent.Id;
        this.Name = calendarEvent.Summary;
        this.Description = calendarEvent.Description;
        this.Start = calendarEvent.Start.DateTime;
        this.End = calendarEvent.End.DateTime;
        this.Location = calendarEvent.Location;
    }
}