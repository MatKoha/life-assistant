import { useState } from 'react';
import Dashboard from './components/Dashboard/Dashboard';
import DashboardItem from './components/Dashboard/DashboardItem';
import Weather from './components/Weather/Weather';
import './App.css';
import { Grid, ToggleButton, ToggleButtonGroup } from '@mui/material';
import { WeatherDisplayType } from './enums';
import Tasks from './components/Google/Tasks';
import Calendar from './components/Google/Calendar';
import Hue from './components/Hue/Hue';
import GoogleOAuthProvider from './components/Google/GoogleOAuthProvider';
import { LocalizationProvider } from '@mui/x-date-pickers';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs'

const App = () => {
    const [weatherType, setWeatherType] = useState<WeatherDisplayType>(WeatherDisplayType.Hour);

    const handleWeatherDisplayType = (_event: React.MouseEvent<HTMLElement>, value: WeatherDisplayType) => {
        setWeatherType(value);
    }

    return (
        <LocalizationProvider dateAdapter={AdapterDayjs}>
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
                                <Weather type={weatherType} />
                            </DashboardItem>
                        </Grid>
                        <GoogleOAuthProvider>
                            <Grid item xs={12}>
                                <Tasks />
                            </Grid>
                            <Grid item xs={12}>
                                <DashboardItem title='Tapahtumat'>
                                    <Calendar />
                                </DashboardItem>
                            </Grid>
                        </GoogleOAuthProvider>
                    </Grid>
                </Grid>
                <Grid item xs={12} md={6}>
                    <Grid container spacing={2}>
                        <Grid item xs={12}>
                            <DashboardItem title='Hue'>
                                <Hue />
                            </DashboardItem>
                        </Grid>
                        <Grid item xs={12}>
                            <DashboardItem title='Polar'>
                                Polar
                            </DashboardItem>
                        </Grid>
                    </Grid>
                </Grid>
            </Dashboard>
        </LocalizationProvider>
    );
}

export default App;