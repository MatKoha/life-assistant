﻿import * as React from 'react';
import { styled, createTheme, ThemeProvider } from '@mui/material/styles';
import CssBaseline from '@mui/material/CssBaseline';
import MuiDrawer from '@mui/material/Drawer';
import Box from '@mui/material/Box';
import MuiAppBar, { AppBarProps as MuiAppBarProps } from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import List from '@mui/material/List';
import Typography from '@mui/material/Typography';
import Divider from '@mui/material/Divider';
import IconButton from '@mui/material/IconButton';
import Container from '@mui/material/Container';
import Grid from '@mui/material/Grid';
import MenuIcon from '@mui/icons-material/Menu';
import ChevronLeftIcon from '@mui/icons-material/ChevronLeft';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';
import DashboardIcon from '@mui/icons-material/Dashboard';
import CalendarMonthIcon from '@mui/icons-material/CalendarMonth';
import WbSunnyIcon from '@mui/icons-material/WbSunny';
import FavoriteIcon from '@mui/icons-material/Favorite';
import TaskIcon from '@mui/icons-material/Task';
import SettingsIcon from '@mui/icons-material/Settings';
import WifiIcon from '@mui/icons-material/Wifi';
import axios from 'axios';
import './styles.scss';
import dayjs from 'dayjs';

const drawerWidth: number = 240;

interface AppBarProps extends MuiAppBarProps {
    open?: boolean;
}

interface Properties {
    children?: React.ReactNode;
}

const AppBar = styled(MuiAppBar, {
    shouldForwardProp: (prop) => prop !== 'open',
})<AppBarProps>(({ theme, open }) => ({
    zIndex: theme.zIndex.drawer + 1,
    transition: theme.transitions.create(['width', 'margin'], {
        easing: theme.transitions.easing.sharp,
        duration: theme.transitions.duration.leavingScreen,
    }),
    ...(open && {
        marginLeft: drawerWidth,
        width: `calc(100% - ${drawerWidth}px)`,
        transition: theme.transitions.create(['width', 'margin'], {
            easing: theme.transitions.easing.sharp,
            duration: theme.transitions.duration.enteringScreen,
        }),
    }),
}));

const Drawer = styled(MuiDrawer, { shouldForwardProp: (prop) => prop !== 'open' })(
    ({ theme, open }) => ({
        '& .MuiDrawer-paper': {
            position: 'relative',
            whiteSpace: 'nowrap',
            width: drawerWidth,
            transition: theme.transitions.create('width', {
                easing: theme.transitions.easing.sharp,
                duration: theme.transitions.duration.enteringScreen,
            }),
            boxSizing: 'border-box',
            ...(!open && {
                overflowX: 'hidden',
                transition: theme.transitions.create('width', {
                    easing: theme.transitions.easing.sharp,
                    duration: theme.transitions.duration.leavingScreen,
                }),
                width: "60px",
            }),
        },
    }),
);

const defaultTheme = createTheme();

const Dashboard: React.FC<Properties> = (props) => {
    const [open, setOpen] = React.useState(false);
    const [status, setStatus] = React.useState(false);
    const [time, setTime] = React.useState(dayjs().format('DD.MM.YYYY HH:mm'));
    const toggleDrawer = () => {
        setOpen(!open);
    };

    React.useEffect(() => {
        const fetchStatus = async () => {
            axios.get('/api/status')
                .then(() => {
                    setStatus(true);
                })
                .catch(() => {
                    console.log("Could not fetch status");
                })
                .finally(() => {
                    setTimeout(() => {
                        setStatus(false);
                    }, 2000);
                });

            const currentDate = dayjs().format('DD.MM.YYYY HH:mm');
            setTime(currentDate);
        };

        fetchStatus();

        const intervalId = setInterval(() => {
            fetchStatus();
        }, 5000);

        return () => clearInterval(intervalId);
    }, []); 

    return (
        <ThemeProvider theme={defaultTheme}>
            <Box sx={{ display: 'flex' }}>
                <CssBaseline />
                <AppBar position="absolute" open={open}>
                    <Toolbar
                        sx={{
                            pr: '24px', // keep right padding when drawer closed
                        }}
                    >
                        <IconButton
                            edge="start"
                            color="inherit"
                            aria-label="open drawer"
                            onClick={toggleDrawer}
                            sx={{
                                marginRight: '36px',
                                ...(open && { display: 'none' }),
                            }}
                        >
                            <MenuIcon />
                        </IconButton>
                        <Typography
                            component="h1"
                            variant="h6"
                            color="inherit"
                            noWrap
                            sx={{ flexGrow: 1 }}
                            className="title"
                        >
                            {time} 
                            <FavoriteIcon className={status ? "heart heartbeat" : "heart"} />
                        </Typography>
                    </Toolbar>
                </AppBar>
                <Drawer variant="permanent" open={open}>
                    <Toolbar
                        sx={{
                            display: 'flex',
                            alignItems: 'center',
                            justifyContent: 'flex-end',
                            px: [1],
                        }}
                    >
                        <IconButton onClick={toggleDrawer}>
                            <ChevronLeftIcon />
                        </IconButton>
                    </Toolbar>
                    <Divider />
                    <List component="nav">
                        <React.Fragment>
                            <ListItemButton>
                                <ListItemIcon>
                                    <DashboardIcon />
                                </ListItemIcon>
                                <ListItemText primary="Dashboard" />
                            </ListItemButton>
                            <ListItemButton>
                                <ListItemIcon>
                                    <CalendarMonthIcon />
                                </ListItemIcon>
                                <ListItemText primary="Kalenteri" />
                            </ListItemButton>
                            <ListItemButton>
                                <ListItemIcon>
                                    <WbSunnyIcon />
                                </ListItemIcon>
                                <ListItemText primary="Sää" />
                            </ListItemButton>
                            <ListItemButton>
                                <ListItemIcon>
                                    <FavoriteIcon />
                                </ListItemIcon>
                                <ListItemText primary="Hyvinvointi" />
                            </ListItemButton>
                            <ListItemButton>
                                <ListItemIcon>
                                    <TaskIcon />
                                </ListItemIcon>
                                <ListItemText primary="Tehtävät" />
                            </ListItemButton>
                            <ListItemButton>
                                <ListItemIcon>
                                    <WifiIcon />
                                </ListItemIcon>
                                <ListItemText primary="Wifi" />
                            </ListItemButton>
                            <ListItemButton>
                                <ListItemIcon>
                                    <SettingsIcon />
                                </ListItemIcon>
                                <ListItemText primary="Asetukset" />
                            </ListItemButton>
                        </React.Fragment>
                    </List>
                </Drawer>
                <Box
                    component="main"
                    sx={{
                        backgroundColor: (theme) =>
                            theme.palette.mode === 'light'
                                ? theme.palette.grey[100]
                                : theme.palette.grey[900],
                        flexGrow: 1,
                        height: '100vh',
                        overflow: 'auto',
                    }}
                >
                    <Toolbar />
                    <Container maxWidth="lg" sx={{ mt: 4, mb: 4 }}>
                        <Grid container spacing={2}>
                            {props.children}
                        </Grid>
                    </Container>
                </Box>
            </Box>
        </ThemeProvider >
    );
}

export default Dashboard;