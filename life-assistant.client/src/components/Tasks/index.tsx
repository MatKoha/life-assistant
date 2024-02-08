import * as React from 'react';
import axios from 'axios';
import dayjs from 'dayjs';
import utc from 'dayjs/plugin/utc';
import timezone from 'dayjs/plugin/timezone';

import './styles.scss';

import {
    Alert,
    Checkbox,
    IconButton,
    LinearProgress,
    List,
    ListItem,
    ListItemButton,
    ListItemSecondaryAction,
    ListItemText,
    Snackbar
} from '@mui/material';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import RefreshIcon from '@mui/icons-material/Refresh';
import AddIcon from '@mui/icons-material/PostAdd';

import { GoogleTask } from '../../Api/Typings/Google';
import { TaskStatus } from '../../enums';
import DashboardItem from '../Dashboard/DashboardItem';
import TaskForm from './TaskForm';

dayjs.extend(utc);
dayjs.extend(timezone);

interface Properties {
    children?: React.ReactNode;
}

const Tasks: React.FC<Properties> = (props) => {
    const [loading, setLoading] = React.useState(true);
    const [data, setData] = React.useState<GoogleTask[]>([]);
    const [error, setError] = React.useState<string | null>();
    const [modifyItem, setModifyItem] = React.useState<GoogleTask | null>(null);
    const apiUrl = 'api/google/tasks';

    React.useEffect(() => {
        fetchTasks();
        //axios.get(apiUrl)
        //    .then((response) => {
        //        setData(response.data);
        //    })
        //    .catch((error) => {
        //        setError(error.message);
        //    })
        //    .finally(() => {
        //        setLoading(false);
        //    });
    }, []);


    const fetchTasks = async () => {
        const response = await fetch(apiUrl);
        const data = await response.json();
        console.log(data);
    }


    const formatDate = (date: string): string => {
        const d = dayjs(date).utc();
        const formatString = d.format('HH:mm') === '00:00' ? 'DD.MM.YYYY' : 'DD.MM.YYYY HH:mm';

        return d.format(formatString);
    };

    const handleModifyTask = (task?: GoogleTask): React.MouseEventHandler<HTMLButtonElement> => (event): void => {
        if (task == null) {
            task = {
                title: '',
                notes: '',
                due: dayjs().toISOString(),
                status: TaskStatus.Pending
            }
        }

        setModifyItem(task);
    };

    const handleCheckboxToggle = (task: GoogleTask): React.MouseEventHandler<HTMLButtonElement> => (event): void => {
        task.status = task.status === TaskStatus.Completed ? TaskStatus.Pending : TaskStatus.Completed;
        updateTask(task);
    };

    const updateTask = (task: GoogleTask): void => {
        setLoading(true);

        axios.put(apiUrl, task)
            .then((response) => {
                const updatedData = data.map((item) =>
                    item.id === response.data.id ? response.data : item
                )
                setData(updatedData);
                setModifyItem(null);
            }).catch((error) => {
                setError(error.message);
            })
            .finally(() => {
                setLoading(false);
            });
    }

    const saveTask = (task: GoogleTask): void => {
        if (task.id == null) {
            setLoading(true);
            axios.post(apiUrl, task)
                .then((response) => {
                    setData(response.data);
                    handleCloseDialog();
                }).catch((error) => {
                    setError(error.message);
                })
                .finally(() => {
                    setLoading(false);
                });
        } else {
            updateTask(task);
        }
    };

    const handleCloseDialog = (): void => {
        setModifyItem(null);
    };

    const handleDeleteTask = (taskId: string | undefined): React.MouseEventHandler<HTMLButtonElement> => (event): void => {
        setLoading(true);
        axios.delete(`${apiUrl}/${taskId}`)
            .then(() => {
                const updatedData = data.filter((item) => item.id !== taskId);
                setData(updatedData);
            }).catch((error) => {
                setError(error.message);
            })
            .finally(() => {
                setLoading(false);
            });
    };

    return (
        <DashboardItem title='Tehtävät' headerAction={
            <>
                <IconButton color="primary" disabled={loading} onClick={handleModifyTask()}>
                    <AddIcon />
                </IconButton>
                <IconButton color="primary" disabled={loading}>
                    <RefreshIcon />
                </IconButton>
            </>
        }>
            <List dense>
                <Snackbar open={error != null} autoHideDuration={6000} onClose={() => setError(null)} anchorOrigin={{ vertical: "bottom", horizontal: "center" }}>
                    <Alert onClose={() => setError(null)} severity="error" sx={{ width: '100%' }}>
                        {error}
                    </Alert>
                </Snackbar>
                <LinearProgress className={loading ? '' : 'hidden'} />
                {data && data.map((task, i) => (
                    <ListItem key={task.id} disablePadding>
                        <ListItemButton dense>
                            <Checkbox
                                size="small"
                                edge="start"
                                checked={task.status === TaskStatus.Completed}
                                onClick={handleCheckboxToggle(task)}
                                disabled={loading} />
                            <ListItemText primary={`${task.title} ${task.notes != null ? ' - ' + task.notes : ''}`} secondary={formatDate(task.due)} />
                        </ListItemButton>
                        <ListItemSecondaryAction>
                            {dayjs(task.due) > dayjs() && (
                                <IconButton aria-label="edit" color="primary" disabled={loading} onClick={handleModifyTask(task)}>
                                    <EditIcon />
                                </IconButton>
                            )}
                            <IconButton aria-label="delete" color="error" disabled={loading} onClick={handleDeleteTask(task.id)}>
                                <DeleteIcon />
                            </IconButton>
                        </ListItemSecondaryAction>
                    </ListItem>
                ))}
                {data.length < 1 && !loading && (
                    <ListItem disablePadding>Ei tehtäviä saatavilla</ListItem>
                )}
            </List>
            {modifyItem != null && <TaskForm item={modifyItem} open onSave={saveTask} onClose={handleCloseDialog} loading={loading} />}
        </DashboardItem>
    );
}

export default Tasks;