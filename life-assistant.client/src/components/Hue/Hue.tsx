import * as React from 'react';
import './styles.scss';
import { Button } from '@mui/material';
import axios from 'axios';
interface Properties {
    children?: React.ReactNode;
}

const Hue: React.FC<Properties> = () => {
    const [loading, setLoading] = React.useState<boolean>(false);

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

    return (
        <div>
            <Button disabled={loading} color="secondary" variant="contained" onClick={() => shutLights()}>
                Sammuta valot
            </Button>
        </div>
    );
}

export default Hue;