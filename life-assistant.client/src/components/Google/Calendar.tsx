import axios from 'axios';
import 'dayjs/locale/fi';
import * as React from 'react';
import { ApiGoogleCalendarEvent } from '../../Api/Typings/Google';
import CalendarEvent from './CalendarEvent';
import './styles.scss';

const Calendar: React.FC = () => {
  const [loading, setLoading] = React.useState(true);
  const [data, setData] = React.useState<ApiGoogleCalendarEvent[]>([]);
  const [error, setError] = React.useState<string>();

  React.useEffect(() => {

    const apiUrl = '/api/google/calendar-events';

    axios.get(apiUrl)
      .then((response) => {
        setData(response.data);
      })
      .catch((error) => {
        setError(error.response.data);
      })
      .finally(() => {
        setLoading(false);
      });
  }, []);

  return (
    <div className="container">
      {!loading && data.map(e => (
        <CalendarEvent data={e} key={e.id} />
      ))}
      {!loading && data.length < 1 && error == null && 
        <div>Ei tapahtumia kalenterissa.</div>
      }
    </div>

  );
}

export default Calendar;