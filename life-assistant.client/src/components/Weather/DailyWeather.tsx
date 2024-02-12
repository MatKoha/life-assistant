import * as React from 'react';
import { Card, CardContent, Typography, CardHeader, Grid } from '@mui/material';
import dayjs from 'dayjs';
import AirIcon from '@mui/icons-material/Air';
import WaterDropIcon from '@mui/icons-material/WaterDrop';
import { WiSunrise, WiMoonrise  } from "weather-icons-react";
import { ApiDayWeather } from '../../Api/Typings/Weather';
import ThermostatIcon from '@mui/icons-material/ThermostatOutlined';
import './styles.scss';

interface Properties {
  children?: React.ReactNode;
  data: ApiDayWeather;
  formatTemp: (temp: number) => string;
  getTypeIcon: (type: number, isDayLight: boolean, size: number) => React.ReactNode;
  getTemperatureClasses: (temp: number) => string[]
}

const DailyWeather: React.FC<Properties> = (props) => {

  const formattedDate = dayjs(props.data.date).format('dd D.M');
  const computedTitle = formattedDate.charAt(0).toUpperCase() + formattedDate.slice(1);

  return (
    <Card className='cardMain'>
      <CardHeader title={<Typography variant="h6">{computedTitle}</Typography>} />
      <CardContent>
        <Grid container alignItems="center" justifyContent="space-evenly" direction="column">
          <Grid item>
            <Typography className="bigIcon">
              {props.getTypeIcon(props.data.type, true, 48)}
            </Typography>
          </Grid>
          <Grid item>
            <Typography className="iconValuePair">
              <WiSunrise size={24} />{dayjs(props.data.sunRise).format('HH:mm')}
            </Typography>
          </Grid>
          <Grid item >
            <Typography className="iconValuePair">
              <WiMoonrise size={24} color="#444" /> {dayjs(props.data.sunSet).format('HH:mm')}
            </Typography>
          </Grid>
          <Grid item>
        <Typography  className="iconValuePair">
          <span className="iconText">
            <ThermostatIcon />
            <span>min</span>
          </span>
          <span className={props.getTemperatureClasses(props.data.maxTemp).join(' ')}>{props.formatTemp(props.data.minTemp)}&deg;</span>
        </Typography>
      </Grid>
      <Grid item>
        <Typography  className="iconValuePair">
          <span className="iconText">
            <ThermostatIcon />
            <span>max</span>
          </span>
          <span className={props.getTemperatureClasses(props.data.maxTemp).join(' ')}>{props.formatTemp(props.data.maxTemp)}&deg;</span>
        </Typography>
      </Grid>
          <Grid item style={{ textAlign: 'center' }}>
            <Typography className="iconValuePair">
              <AirIcon /> {props.data.windSpeed} m/s
            </Typography>
          </Grid>
          <Grid item>
            <Typography className="iconValuePair">
              <WaterDropIcon color="primary" /> {props.data.totalLiquid} mm
            </Typography>
          </Grid>
        </Grid>
      </CardContent>
    </Card>
  );
}

export default DailyWeather;