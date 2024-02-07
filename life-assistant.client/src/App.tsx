import { useEffect, useState } from 'react';
import Dashboard from './components/Dashboard';
import DashboardItem from './components/Dashboard/DashboardItem';
import './App.css';
import { Grid, ToggleButton, ToggleButtonGroup } from '@mui/material';

//interface Forecast {
//    date: string;
//    temperatureC: number;
//    temperatureF: number;
//    summary: string;
//}

function App() {
    //const [forecasts, setForecasts] = useState<Forecast[]>();

    //useEffect(() => {
    //    populateWeatherData();
    //}, []);
    enum WeatherDisplayType {
        Hour = 0,
        Day = 1
    }

    const [weatherType, setWeatherType] = useState<WeatherDisplayType>(WeatherDisplayType.Hour);

    const handleWeatherDisplayType = (event: React.MouseEvent<HTMLElement>, value: WeatherDisplayType) => {
        setWeatherType(value);
        console.log(event);
    }

    return (
        <div>
            <Dashboard>
                <Grid item xs={12} md={6}>
                    <Grid container spacing={2}>
                        <Grid item xs={12}>
                            <DashboardItem title='Sää' headerAction={
                                <ToggleButtonGroup value={weatherType} size="small" color="primary" onChange={handleWeatherDisplayType} exclusive>
                                    <ToggleButton value={WeatherDisplayType.Hour}>12 tuntia</ToggleButton>
                                    <ToggleButton value={WeatherDisplayType.Day}>5 vrk</ToggleButton>
                                </ToggleButtonGroup>
                            }>
                            </DashboardItem>
                        </Grid>
                        <Grid item xs={12}>
                            Hei
                        </Grid>
                    </Grid>
                </Grid>
            </Dashboard>
        </div>
    );

    //async function populateWeatherData() {
    //    const response = await fetch('weatherforecast');
    //    const data = await response.json();
    //    setForecasts(data);
    //}
}

export default App;