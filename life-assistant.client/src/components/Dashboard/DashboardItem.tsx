import * as React from 'react';
import Typography from '@mui/material/Typography';
import { Card, CardContent, CardHeader, Collapse, IconButton } from '@mui/material';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import './styles.scss';

interface Properties {
    children?: React.ReactNode;
    title: string,
    headerAction?: React.ReactNode
}

const DashboardItem: React.FC<Properties> = (props) => {
    const [expanded, setExpanded] = React.useState(true);

    const handleExpandClick = () => {
        setExpanded(!expanded);
    };


    return (
        <Card>
            <CardHeader title={<Typography color="primary" variant="h5">{props.title}</Typography>} action={
                <>
                    {expanded && props.headerAction}
                    <IconButton aria-label="settings" color="primary" onClick={handleExpandClick}>
                        <ExpandMoreIcon className={`icon ${expanded ? 'expanded' : ''}`} />
                    </IconButton>
                </>
            } />
            <Collapse in={expanded} timeout="auto" unmountOnExit>
                <CardContent>
                    {props.children}
                </CardContent>
            </Collapse>
        </Card>
    );
}

export default DashboardItem;