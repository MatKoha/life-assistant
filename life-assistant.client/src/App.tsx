import { useState } from 'react';
import Dashboard from './components/Dashboard';
import DashboardItem from './components/Dashboard/DashboardItem';
import Weather from './components/Weather';
import './App.css';
import { Grid, ToggleButton, ToggleButtonGroup } from '@mui/material';
import { WeatherDisplayType } from './enums';

const App = () => {

    const [weatherType, setWeatherType] = useState<WeatherDisplayType>(WeatherDisplayType.Hour);

    const handleWeatherDisplayType = (_event: React.MouseEvent<HTMLElement>, value: WeatherDisplayType) => {
        setWeatherType(value);
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
                                <Weather type={weatherType} />
                            </DashboardItem>
                        </Grid>
                        <Grid item xs={12}>
                            New item
                        </Grid>
                    </Grid>
                </Grid>
            </Dashboard>
        </div>
    );
}

export default App;