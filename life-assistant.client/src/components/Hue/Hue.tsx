import { useState, useEffect } from 'react';
import { Button, ButtonGroup, Link, Slider, Stack, Switch, Typography } from '@mui/material';
import axios from 'axios';
import './styles.scss';
import { Color, Light } from '../../Api/Typings/Hue';
import ColorConverter from 'ts-cie1931-rgb';
import LightModeIcon from '@mui/icons-material/LightMode';
import FlareIcon from '@mui/icons-material/Flare';
//import Wheel from '@uiw/react-color-wheel';
//import { hsvaToHex, ColorResult, rgbStringToHsva } from '@uiw/color-convert';


interface Properties {
    children?: React.ReactNode;
}

const Hue: React.FC<Properties> = () => {
    const [loading, setLoading] = useState<boolean>(false);
    const [needsAuth, setNeedsAuth] = useState<boolean>(true);
    const [lights, setLights] = useState<Light[]>([]);
    //const [hsva, setHsva] = useState({ h: 214, s: 43, v: 90, a: 1 });

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

    const getLights = () => {
        setLoading(true);
        axios.get('/api/hue/lights')
            .then((response) => {
                setLights(response.data)
            })
            .catch((error) => {
                console.log(error);
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

    const xyToHex = (color: Color, brightness: number): string => {
        brightness = 15 + ((brightness - 1) / (100 - 1)) * (100 - 15); // for style only
        const rgb = ColorConverter.xyBriToRgb(color.xy.x, color.xy.y, brightness);
        return rgbToHex(rgb);
    }

    //const xyToRgbString = (color: Color, brightness: number): string => {
    //    if (brightness < 20) {
    //        brightness = 20;
    //    }
    //    const rgb = ColorConverter.xyBriToRgb(color.xy.x, color.xy.y, brightness);
    //    return `rgb(${rgb.r}, ${rgb.g}, ${rgb.b})`;
    //}

    const rgbToHex = (rgb: { r: number, g: number, b: number }): string => {
        const toHex = (value: number): string => {
            const hex = value.toString(16);
            return hex.length === 1 ? '0' + hex : hex;
        };

        const r = Math.round(rgb.r);
        const g = Math.round(rgb.g);
        const b = Math.round(rgb.b);

        return `#${toHex(r)}${toHex(g)}${toHex(b)}`;
    };

    const handleBrightness = (idx: number) => (_event: Event, value: number | number[]): void => {
        const updatedLights = [...lights];

        updatedLights[idx].dimming.brightness = value as number;
        setLights(updatedLights);
    };

    const handleToggleLight = (idx: number) => (_event: React.ChangeEvent<HTMLInputElement>, checked: boolean): void => {
        const updatedLights = [...lights];

        updatedLights[idx].on.isOn = checked;
        setLights(updatedLights);
    };

    const formatValueLabel = (value: number): string => {
        return `${value}%`;
    };

    return (
        <div>
            {needsAuth && (
                <Link href="https://api.meethue.com/v2/oauth2/authorize?client_id=yrgHveO7J2aA0G77lYYA5c82BG5cng7B&response_type=code" underline="none">
                    Authorize
                </Link>
            )}

            {!needsAuth && (
                <ButtonGroup size="small">
                    <Button disabled={loading} color="secondary" onClick={() => getDevices()}>Get devices</Button>
                    <Button disabled={loading} color="secondary" onClick={() => shutLights()}>Lights off</Button>
                    <Button disabled={loading} color="secondary" onClick={() => getLights()}>Get lights</Button>
                </ButtonGroup>
            )}
            {lights && lights.map((light, i) => (
                <div className="light-container" key={light.id} >
                    <div className="title-container">
                        <Typography variant="h6" className="light-title">
                            {light.metadata.name}
                        </Typography>
                        <Switch checked={light.on.isOn} onChange={handleToggleLight(i)}></Switch>
                    </div>
                    <div className="brightness" style={{
                        background: light.on.isOn ? `linear-gradient(90deg, ${xyToHex(light.color, light.dimming.brightness)}33 0%, ${xyToHex(light.color, light.dimming.brightness)} 100%)` : '#f2f2f2',
                        boxShadow: light.on.isOn ? `${xyToHex(light.color, light.dimming.brightness)} 0px 4px 12px` : 'none'
                    }}>
                        {/*<Wheel color={hsva} onChange={(color) => setHsva({ ...hsva, ...color.hsva })} />*/}
                        <Stack spacing={2} direction="row" sx={{ mb: 1 }} alignItems="center">
                            <FlareIcon className="brightness-icon" />
                            <Slider
                                disabled={!light.on.isOn}
                                color="primary"
                                min={1}
                                value={light.dimming.brightness}
                                onChange={handleBrightness(i)}
                                valueLabelDisplay="on"
                                valueLabelFormat={formatValueLabel}
                            />
                            <LightModeIcon className="brightness-icon" />
                        </Stack>
                    </div>
                </div>
            ))}
        </div>
    );
}

export default Hue;