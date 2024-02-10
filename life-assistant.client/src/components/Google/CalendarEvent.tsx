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

  const formatDateTimeRange = (start: Date, end: Date): string => {
    const startDayjs = dayjs(start);
    const endDayjs = dayjs(end);

    const dateFormat = 'D. MMMM YYYY, HH:mm';

    if (startDayjs.isSame(endDayjs, 'day')) {
      return `${startDayjs.format(dateFormat)} - ${endDayjs.format('HH:mm')}`;
    } else {
      return `${startDayjs.format(dateFormat)} - ${endDayjs.format(dateFormat)}`;
    }
  }

  const computedTitle = props.data.name ? props.data.name : 'Ei otsikkoa';

  return (
    <Card key={props.data.id} className='card' elevation={3}>
      <CardHeader title={<Typography variant="h6">{computedTitle}</Typography>}  subheader={formatDateTimeRange(props.data.start, props.data.end)} />
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