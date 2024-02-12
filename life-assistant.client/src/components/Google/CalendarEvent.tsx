import dayjs from 'dayjs';
import 'dayjs/locale/fi';
import * as React from 'react';
import { ApiGoogleCalendarEvent } from '../../Api/Typings/Google';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import { Place, Description } from '@mui/icons-material';

import Typography from '@mui/material/Typography';
import './styles.scss';
import { CardHeader } from '@mui/material';

dayjs.locale('fi');

interface Properties {
    data: ApiGoogleCalendarEvent;
}

const CalendarEvent: React.FC<Properties> = (props) => {

    const formatDateTimeRange = (): string => {

        if (props.data.start == null) {
            const endDate = dayjs(props.data.startDate, { format: 'YYYY-MM-DD' }).format('D. MMMM YYYY');
            return `${endDate} - Koko päivä`;
        }

        const start = dayjs(props.data.start);
        const end = dayjs(props.data.end);

        const dateFormat = 'D. MMMM YYYY, HH:mm';

        if (start.isSame(end, 'day')) {
            return `${start.format(dateFormat)} - ${end.format('HH:mm')}`;
        } else {
            return `${start.format(dateFormat)} - ${end.format(dateFormat)}`;
        }
    }

    const computedTitle = props.data.name ? props.data.name : 'Ei otsikkoa';

    return (
        <Card key={props.data.id} className='card' elevation={3}>
            <CardHeader title={<Typography variant="h6">{computedTitle}</Typography>} subheader={formatDateTimeRange()} />
            <CardContent>
                {props.data.location && (
                    <Typography className="itemRow">
                        <Place fontSize="small" color="secondary" /> {props.data.location}
                    </Typography>
                )}
                {props.data.description && (
                    <Typography className="itemRow">
                        <Description fontSize="small" /> {props.data.description}
                    </Typography>
                )}
            </CardContent>
        </Card>
    )
}

export default CalendarEvent;