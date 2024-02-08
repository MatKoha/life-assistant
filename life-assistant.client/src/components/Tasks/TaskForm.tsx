import * as React from 'react';
import { Button, Dialog, DialogActions, DialogContent, DialogTitle, TextField } from '@mui/material';
import './TaskForm.scss';
import { GoogleTask } from '../../Api/Typings/Google';
import { DatePicker } from '@mui/x-date-pickers';
import dayjs, { Dayjs } from 'dayjs';

interface Properties {
  item: GoogleTask;
  open: boolean;
  loading: boolean;
  onSave: (task: GoogleTask) => void;
  onClose: () => void;
}

const TaskForm: React.FC<Properties> = (props) => {
  const [title, setTitle] = React.useState<string>(props.item.title);
  const [notes, setNotes] = React.useState<string>(props.item.notes || '');
  const [date, setDate] = React.useState<Dayjs>(dayjs(props.item.due));
  const [valid, setValid] = React.useState<boolean | null>(null);

  const handleDateSelect = (value: Dayjs | null) => {
    if (value != null) {
      setDate(value);
    } else {
      setDate(dayjs());
    }
  }

  const handleSave = () => {
    if (title.trim() === '') {
      setValid(false);
      return;
    }

    const saveItem = { ...props.item };
    const localDate = dayjs(date).tz(dayjs.tz.guess());

    saveItem.title = title;
    saveItem.notes = notes;
    saveItem.due = localDate.toISOString();
    props.onSave(saveItem);
  }

  const handleTitle = (e: React.ChangeEvent<HTMLInputElement>) => {
    setTitle(e.target.value);
    setValid(e.target.value !== "");
  }

  const handleNotes = (e: React.ChangeEvent<HTMLInputElement>) => {
    setNotes(e.target.value);
  }

  return (
    <Dialog open={props.open} onClose={props.onClose}>
      <DialogTitle>{props.item?.id == null ? 'Uusi tehtävä' : 'Muokkaa'}</DialogTitle>
      <DialogContent className='dialog'>
          <TextField
            autoFocus
            margin="dense"
            label="Otsikko"
            fullWidth
            variant="outlined"
            name="title"
            value={title}
            required
            onChange={handleTitle}
            disabled={props.loading}
            error={valid === false}
            helperText={valid === false ? 'Otsikko on pakollinen.' : '\u200B'}
          />
          <DatePicker
            disablePast
            timezone='Europe/Helsinki'
            label="Päivämäärä"
            value={date}
            onChange={handleDateSelect}
            disabled={props.loading}
            className='datePicker'
          />
          <TextField
          className='description'
            margin="dense"
            label="Kuvaus"
            fullWidth
            variant="outlined"
            multiline
            rows={2}
            name="description"
            value={notes}
            onChange={handleNotes}
            disabled={props.loading}
          />
      </DialogContent>

      <DialogActions>
        <Button onClick={props.onClose} color="primary" disabled={props.loading}>
          Peruuta
        </Button>
        <Button onClick={handleSave} color="success" variant="contained" disabled={props.loading}>
          Tallenna
        </Button>
      </DialogActions>
    </Dialog>
  );
}

export default TaskForm;