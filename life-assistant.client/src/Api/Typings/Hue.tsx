export interface Device {
    id: string;
    id_v1: string;
    product_data: ProductData;
    metadata: Metadata;
    identify: { [key: string]: string };
    services: Service[];
    type: string;
}

export interface ProductData {
    model_id: string;
    manufacturer_name: string;
    product_name: string;
    product_archetype: string;
    certified: boolean;
    software_version: string;
    function: string;
}

export interface Metadata {
    name: string;
    archetype: string;
    function?: string;
}

export interface Service {
    rid: string;
    rtype: string;
}

export interface Light {
    id: string;
    id_v1: string;
    owner: Owner;
    metadata: Metadata;
    product_data: ProductData;
    identify: Identify;
    on: On;
    dimming: Dimming;
    dimming_delta: DimmingDelta;
    color_temperature: ColorTemperature;
    color_temperature_delta: ColorTemperatureDelta;
    color: Color;
    dynamics: Dynamics;
    alert: Alert;
    signaling: Signaling;
    mode: string;
    effects: Effect;
    powerup: Powerup;
    type: string;
}

export interface Owner {
    rid: string;
    rtype: string;
}

export interface Dimming {
    mode: string;
    dimmingValue: Dimming | null;
    brightness: number;
}

export interface Color {
    xy: XY;
    gamut: Gamut;
    gamutType: string;
}

export interface XY {
    x: number;
    y: number;
}

export interface Gamut {
    red: XY;
    green: XY;
    blue: XY;
    gamut_type: string;
}

export interface On {
    isOn: boolean;
}

export interface OnPowerUp {
    mode: string;
    on: On;
}

export interface ColorTemperature {
    mirek: number;
    mirek_valid: boolean;
    mirek_schema: MirekSchema;
}

export interface MirekSchema {
    mirek_minimum: number;
    mirek_maximum: number;
}

export interface Dynamics {
    status: string;
    status_values: string[];
    speed: number;
    speed_valid: boolean;
}

export interface Alert {
    action_values: string[];
}

export interface Signaling {
    signal_values: string[];
}

export interface Effect {
    status_values: string[];
    status: string;
    effect_values: string[];
}

export interface Powerup {
    preset: string;
    configured: boolean;
    on: OnPowerUp;
    dimming: Dimming;
    color: Color;
    type: string;
}

export interface Identify { }
export interface DimmingDelta { }
export interface ColorTemperatureDelta { }