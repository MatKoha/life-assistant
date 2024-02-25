using life_assistant.Server.Classes.Hue;
using Newtonsoft.Json;

public class SleepData
{
    [JsonProperty("nights")]
    public List<Night> Nights { get; set; }
}

public class Night
{
    [JsonProperty("polar_user")]
    public string PolarUser { get; set; }

    [JsonProperty("date")]
    public string Date { get; set; }

    [JsonProperty("sleep_start_time")]
    public string SleepStartTime { get; set; }

    [JsonProperty("sleep_end_time")]
    public string SleepEndTime { get; set; }

    [JsonProperty("device_id")]
    public string DeviceId { get; set; }

    [JsonProperty("continuity")]
    public double Continuity { get; set; }

    [JsonProperty("continuity_class")]
    public int ContinuityClass { get; set; }

    [JsonProperty("light_sleep")]
    public int LightSleep { get; set; }

    [JsonProperty("deep_sleep")]
    public int DeepSleep { get; set; }

    [JsonProperty("rem_sleep")]
    public int RemSleep { get; set; }

    [JsonProperty("unrecognized_sleep_stage")]
    public int UnrecognizedSleepStage { get; set; }

    [JsonProperty("sleep_score")]
    public int SleepScore { get; set; }

    [JsonProperty("total_interruption_duration")]
    public int TotalInterruptionDuration { get; set; }

    [JsonProperty("sleep_charge")]
    public int SleepCharge { get; set; }

    [JsonProperty("sleep_rating")]
    public int SleepRating { get; set; }

    [JsonProperty("sleep_goal")]
    public int SleepGoal { get; set; }

    [JsonProperty("short_interruption_duration")]
    public int ShortInterruptionDuration { get; set; }

    [JsonProperty("long_interruption_duration")]
    public int LongInterruptionDuration { get; set; }

    [JsonProperty("sleep_cycles")]
    public int SleepCycles { get; set; }

    [JsonProperty("group_duration_score")]
    public double GroupDurationScore { get; set; }

    [JsonProperty("group_solidity_score")]
    public double GroupSolidityScore { get; set; }

    [JsonProperty("group_regeneration_score")]
    public double GroupRegenerationScore { get; set; }

    [JsonProperty("hypnogram")]
    public Dictionary<string, int> Hypnogram { get; set; }

    [JsonProperty("heart_rate_samples")]
    public Dictionary<string, int> HeartRateSamples { get; set; }
}
