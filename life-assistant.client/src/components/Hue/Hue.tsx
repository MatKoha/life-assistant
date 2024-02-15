import { useState, useEffect } from 'react';
import { Button, Link } from '@mui/material';
import axios from 'axios';
import './styles.scss';

interface Properties {
    children?: React.ReactNode;
}

const Hue: React.FC<Properties> = () => {
    const [loading, setLoading] = useState<boolean>(false);
    const [needsAuth, setNeedsAuth] = useState<boolean>(true);

    useEffect(() => {
        setLoading(true);
        axios.get('/api/hue/devices')
            .then(() => {
                setNeedsAuth(false);
            })
            .catch((error) => {
                if (error.response && error.response.status === 401) {
                    setNeedsAuth(true);
                } else {
                    console.error("Error:", error); // ?
                }
            })
            .finally(() => {
                setLoading(false);
            });
    }, []);

    useEffect(() => {
        const urlSearchParams = new URLSearchParams(window.location.search);
        const code = urlSearchParams.get('code');
        if (code) {
            setNeedsAuth(false);
            generateToken(code);
        }
    }, []);

    const shutLights = () => {
        setLoading(true);
        axios.put('/api/hue/home-state')
            .then(() => {

            })
            .catch(() => {
                console.log("Fail");
            })
            .finally(() => {
                setLoading(false);
            });
    };

    const getDevices = () => {
        setLoading(true);
        axios.get('/api/hue/devices')
            .then(() => {

            })
            .catch(() => {
                console.log("Fail");
            })
            .finally(() => {
                setLoading(false);
            });
    };

    const generateToken = (code: string) => {
        axios.post("/api/hue/token/" + code,)
            .then(() => {
                setNeedsAuth(false)
            })
            .catch((error) => {
                console.log(error.response.data);
            })
            .finally(() => {
                setLoading(false);
            });
    }

    return (
        <div>
            {needsAuth && (
                <Link href="https://api.meethue.com/v2/oauth2/authorize?client_id=yrgHveO7J2aA0G77lYYA5c82BG5cng7B&response_type=code" underline="none">
                    Authorize
                </Link>
            )}

            <Button disabled={loading} color="secondary" variant="contained" onClick={() => getDevices()}>Get devices</Button>
            <Button disabled={loading} color="secondary" variant="contained" onClick={() => shutLights()}>Lights off</Button>
        </div>
    );
}

export default Hue;