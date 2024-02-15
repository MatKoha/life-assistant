import * as React from 'react';
import './styles.scss';
import { CircularProgress, Grid } from '@mui/material';
import dayjs, { Dayjs } from 'dayjs';
import SunnyIcon from '@mui/icons-material/WbSunnyOutlined';
import MoonIcon from '@mui/icons-material/ModeNightOutlined';
import { WiNightAltCloudy, WiDaySunnyOvercast, WiNightAltPartlyCloudy, WiDayCloudy, WiCloudy, WiDayRainMix, WiNightAltRainMix, WiRain, WiRainMix, WiDaySunny, WiShowers, WiDayCloudyHigh } from "weather-icons-react";
import { WeatherDisplayType, WeatherType } from '../../enums';
import { ApiDayWeather, ApiHourWeather } from '../../Api/Typings/Weather';
import CakeIcon from '@mui/icons-material/Cake';
import DailyWeather from './DailyWeather';
import HourlyWeather from './HourlyWeather';
import Slider from "react-slick";
import { useEffect } from 'react';
import axios from 'axios';

interface Properties {
    children?: React.ReactNode;
    type: WeatherDisplayType;
}

interface StoredWeatherData {
    data: ApiDayWeather[] | ApiHourWeather[],
    lastUpdate: Dayjs,
}

const Weather: React.FC<Properties> = (props) => {
    const [loading, setLoading] = React.useState<boolean>(true);
    const [hourlyData, setHourlyData] = React.useState<ApiHourWeather[]>([]);
    const [dailyData, setDailyData] = React.useState<ApiDayWeather[]>([]);
    const [error, setError] = React.useState<string>();

    useEffect(() => {
        setLoading(true);
        const storageKey = props.type === WeatherDisplayType.Day ? 'dailyWeatherData' : 'hourlyWeatherData';
        const storedData = localStorage.getItem(storageKey);
        const weatherData: StoredWeatherData = storedData ? JSON.parse(storedData) : null;

        if (weatherData) {
            const now = dayjs();
            const timeDifference = now.diff(weatherData.lastUpdate, 'minute');
            if (timeDifference < 15) {
                if (props.type === WeatherDisplayType.Day) {
                    setDailyData(weatherData.data as ApiDayWeather[]);
                } else {
                    setHourlyData(weatherData.data as ApiHourWeather[]);
                }
                setLoading(false);
            } else {
                fetchWeatherData(props.type === WeatherDisplayType.Day ? 'daily' : 'hourly');
            }
        } else {
            fetchWeatherData(props.type === WeatherDisplayType.Day ? 'daily' : 'hourly');
        }

        const intervalId = setInterval(pollWeatherData, 60000);

        return () => clearInterval(intervalId);
    }, [props.type]);

    const fetchWeatherData = (timePeriod: string) => {
        setLoading(true);
        const apiUrl = `/api/weather/${timePeriod}/133091`;

        axios.get(apiUrl)
            .then((response) => {
                if (timePeriod === 'daily') {
                    setDailyData(response.data);
                    localStorage.setItem('dailyWeatherData', JSON.stringify({ data: response.data, lastUpdate: dayjs() }));
                } else {
                    setHourlyData(response.data);
                    localStorage.setItem('hourlyWeatherData', JSON.stringify({ data: response.data, lastUpdate: dayjs()}));
                }
            })
            .catch((error) => {
                setError(error.response?.data || 'An error occurred.');
            })
            .finally(() => {
                setLoading(false);
            });
    };

    const pollWeatherData = () => {
        console.log("jeah");
        const now = dayjs();

        if (now.minute() === 1 && now.minute() === 3) {
            fetchWeatherData(props.type === WeatherDisplayType.Day ? 'daily' : 'hourly');
        }
    };

    const getTypeIcon = (type: WeatherType, isDayLight: boolean, size: number = 24) => {
        switch (type) {
            case WeatherType.Sunny:
                return <WiDaySunny size={size} />;
            case WeatherType.MostlyCloudyWithShowers:
                return <WiRainMix size={size} />;
            case WeatherType.PartlySunnyWithShowers:
                return isDayLight
                    ? <WiDayRainMix size={size} />
                    : <WiNightAltRainMix size={size} />;
            case WeatherType.Clear:
                return isDayLight
                    ? <SunnyIcon />
                    : <MoonIcon />;
            case WeatherType.MostlyClear:
                return isDayLight
                    ? <WiDaySunnyOvercast size={size} />
                    : <WiNightAltPartlyCloudy size={size} />;
            case WeatherType.PartlyCloudy:
            case WeatherType.VariableCloudiness:
            case WeatherType.MostlyCloudy:
            case WeatherType.IntermittentClouds:
                return isDayLight
                    ? <WiDayCloudy size={size} />
                    : <WiNightAltCloudy size={size} />;
            case WeatherType.Cloudy:
            case WeatherType.Overcast:
                return <WiCloudy size={size} />
            case WeatherType.Rain:
                return <WiRain size={size} />
            case WeatherType.Showers:
                return <WiShowers size={size} />
            case WeatherType.PartlySunny:
                return <WiDayCloudyHigh size={size} />
            default:
                return <CakeIcon />;
        }
    };

    const getTemperatureClasses = (temp: number): string[] => {
        const classNames = ["degrees"];
        if (temp < 0) classNames.push("negative");
        if (temp > 0) classNames.push("positive");

        return classNames;
    }

    const formatTemperatureString = (temp: number): string => {
        let str = "";
        if (temp > 0) {
            str += "+";
        }

        return str += temp.toString();
    }

    const settings = {
        infinite: false,
        slidesToShow: 3,
        slidesToScroll: 1,
    };

    if (loading) return <CircularProgress />;
    if (error) return <div>{error}</div>
    return (
        <div>
            {props.type === WeatherDisplayType.Hour && (
                <HourlyWeather
                    data={hourlyData}
                    formatTemp={formatTemperatureString}
                    getTypeIcon={getTypeIcon}
                    getTemperatureClasses={getTemperatureClasses} />
            )}
            {props.type === WeatherDisplayType.Day && (
                <Slider {...settings}>
                    {dailyData.map(x => (
                        <Grid key={dayjs(x.date).toString()} item sx={{ flex: 1, textAlign: 'center', height: '100%' }} style={{ marginRight: '8px' }}>
                            <DailyWeather data={x}
                                formatTemp={formatTemperatureString}
                                getTypeIcon={getTypeIcon}
                                getTemperatureClasses={getTemperatureClasses} />
                        </Grid>
                    ))}
                </Slider>
            )}
        </div>
    );
}

export default Weather;