using Newtonsoft.Json;

namespace life_assistant.Server.Classes.Hue
{
    public class ApiHueLight
    {
        [JsonProperty("errors")]
        public List<object> Errors { get; set; }

        [JsonProperty("data")]
        public List<Light> Data { get; set; }
    }

    public class Light
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("id_v1")]
        public string IdV1 { get; set; }

        [JsonProperty("owner")]
        public Owner Owner { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("product_data")]
        public ProductData ProductData { get; set; }

        [JsonProperty("identify")]
        public Identify Identify { get; set; }

        [JsonProperty("on")]
        public On On { get; set; }

        [JsonProperty("dimming")]
        public Dimming Dimming { get; set; }

        [JsonProperty("dimming_delta")]
        public DimmingDelta DimmingDelta { get; set; }

        [JsonProperty("color_temperature")]
        public ColorTemperature ColorTemperature { get; set; }

        [JsonProperty("color_temperature_delta")]
        public ColorTemperatureDelta ColorTemperatureDelta { get; set; }

        [JsonProperty("color")]
        public Color Color { get; set; }

        [JsonProperty("dynamics")]
        public Dynamics Dynamics { get; set; }

        [JsonProperty("alert")]
        public Alert Alert { get; set; }

        [JsonProperty("signaling")]
        public Signaling Signaling { get; set; }

        [JsonProperty("mode")]
        public string Mode { get; set; }

        [JsonProperty("effects")]
        public Effects Effects { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class Metadata
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("archetype")]
        public string Archetype { get; set; }

        [JsonProperty("function")]
        public string Function { get; set; }
    }

    public class MirekSchema
    {
        [JsonProperty("mirek_minimum")]
        public int MirekMinimum { get; set; }

        [JsonProperty("mirek_maximum")]
        public int MirekMaximum { get; set; }
    }

    public class ColorTemperature
    {
        [JsonProperty("mirek")]
        public int Mirek { get; set; }

        [JsonProperty("mirek_valid")]
        public bool MirekValid { get; set; }

        [JsonProperty("mirek_schema")]
        public MirekSchema MirekSchema { get; set; }
    }

    public class XY
    {
        [JsonProperty("x")]
        public double X { get; set; }

        [JsonProperty("y")]
        public double Y { get; set; }
    }

    public class Gamut
    {
        [JsonProperty("red")]
        public XY Red { get; set; }

        [JsonProperty("green")]
        public XY Green { get; set; }

        [JsonProperty("blue")]
        public XY Blue { get; set; }

        [JsonProperty("gamut_type")]
        public string GamutType { get; set; }
    }

    public class Dynamics
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("status_values")]
        public List<string> StatusValues { get; set; }

        [JsonProperty("speed")]
        public int Speed { get; set; }

        [JsonProperty("speed_valid")]
        public bool SpeedValid { get; set; }
    }

    public class Powerup
    {
        [JsonProperty("preset")]
        public string Preset { get; set; }

        [JsonProperty("configured")]
        public bool Configured { get; set; }

        [JsonProperty("on")]
        public On On { get; set; }

        [JsonProperty("dimming")]
        public Dimming Dimming { get; set; }

        [JsonProperty("color")]
        public Color Color { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class On
    {
        [JsonProperty("mode")]
        public string Mode { get; set; }

        [JsonProperty("on")]
        public bool IsOn { get; set; }
    }

    public class Dimming
    {
        [JsonProperty("mode")]
        public string Mode { get; set; }

        [JsonProperty("dimming")]
        public DimmingDimming DimmingDimming { get; set; }
    }

    public class DimmingDimming
    {
        [JsonProperty("brightness")]
        public double Brightness { get; set; }
    }

    public class Color
    {
        [JsonProperty("mode")]
        public string Mode { get; set; }

        [JsonProperty("color_temperature")]
        public ColorTemperature ColorTemperature { get; set; }
    }

    public class DimmingDelta
    {
    }

    public class ColorTemperatureDelta
    {
    }

    public class Signaling
    {
        [JsonProperty("signal_values")]
        public List<string> SignalValues { get; set; }
    }

    public class Effects
    {
        [JsonProperty("status_values")]
        public List<string> StatusValues { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("effect_values")]
        public List<string> EffectValues { get; set; }
    }

    public class Alert
    {
        [JsonProperty("action_values")]
        public List<string> ActionValues { get; set; }
    }



    public class Owner
    {
        [JsonProperty("rid")]
        public string Rid { get; set; }

        [JsonProperty("rtype")]
        public string Rtype { get; set; }
    }

    public class Identify
    {
    }

    public class ProductData
    {
        [JsonProperty("function")]
        public string Function { get; set; }
    }
}
