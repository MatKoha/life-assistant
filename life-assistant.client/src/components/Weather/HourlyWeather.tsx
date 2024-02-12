import * as React from 'react';
import { Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from '@mui/material';
import dayjs from 'dayjs';
import { ApiHourWeather } from '../../Api/Typings/Weather';
import './styles.scss';

interface Properties {
    children?: React.ReactNode;
    data: ApiHourWeather[],
    formatTemp: (temp: number) => string;
    getTypeIcon: (type: number, isDayLight: boolean, size?: number) => React.ReactNode;
    getTemperatureClasses: (temp: number) => string[]
}

const HourlyWeather: React.FC<Properties> = (props) => {

    const tableHeaders = [
        'Kello',
        'Sää',
        'Lämpötila',
        'Tuuli',
        'Sade (todennäköisyys)',
    ]

    return (
        <TableContainer className='hourlyWeather'>           
            <Table size="small">
                <TableHead>
                    <TableRow>
                        {tableHeaders.map((header) => (
                            <TableCell key={header} align="center">
                                {header}
                            </TableCell>
                        ))}
                    </TableRow>
                </TableHead>
                <TableBody>
                    {props.data.map((x) => (
                        <TableRow key={dayjs(x.date).toString()}>
                            <TableCell >
                                {dayjs(x.date).format('HH:mm')}
                            </TableCell>
                            <TableCell >
                                {props.getTypeIcon(x.type, x.isDayLight)}
                            </TableCell>
                            <TableCell className={`${props.getTemperatureClasses(x.temperature).join(' ')}`} align="center">
                                {props.formatTemp(x.temperature)}&deg;
                            </TableCell>
                            <TableCell style={{ whiteSpace: 'nowrap' }}>
                                {x.windSpeed} m/s
                            </TableCell>
                            <TableCell align="center">
                                {x.totalLiquid} mm ({x.precipitationProbability}%)
                            </TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
    );
}

export default HourlyWeather;