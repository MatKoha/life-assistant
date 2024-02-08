export interface ApiHourWeather extends WeatherBase {
    temperature: number;
    realFeelTemperature: number;
    isDayLight: boolean;
}

export interface ApiDayWeather extends WeatherBase {
    sunRise: Date;
    sunSet: Date;
    moonRise: Date;
    moonSet: Date;
    moonPhase: string;
    moonAge: number;
    minTemp: number;
    maxTemp: number;
    realFeelMinTemp: number;
    realFeelMaxTemp: number;
}

interface WeatherBase {
    date: Date;
    type: number;
    typeString: string;
    windSpeed: number;
    totalLiquid: number;
    precipitationProbability: number;
}  