import { useEffect, useState } from 'react';
import Dashboard from './components/Dashboard';
import './App.css';
import { Grid } from '@mui/material';

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

    return (
        <div>
            <Dashboard>
                <Grid item xs={12} md={6}>
                    <Grid container spacing={2}>
                        <Grid item xs={12}>
                            Moi
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