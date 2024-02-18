export enum WeatherDisplayType {
    Hour = 0,
    Day = 1
}

export enum WeatherType {
    Sunny = 1,
    PartlySunny = 3,
    IntermittentClouds = 4,
    Cloudy = 6,
    Overcast = 7,
    Showers = 12,
    MostlyCloudyWithShowers = 13,
    PartlySunnyWithShowers = 14,
    Rain = 18,
    Snow = 22,
    FreezingRain = 26,
    RainAndSnow = 29,
    Clear = 33,
    MostlyClear = 34,
    PartlyCloudy = 35,
    VariableCloudiness = 36,
    MostlyCloudy = 38,
}

export enum TaskStatus {
    Completed = "completed",
    Pending = "needsAction"
}