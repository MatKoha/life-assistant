import { TaskStatus } from "../../enums";

export interface ApiGoogleCalendarEvent {
    id: number;
    name: string;
    description: string;
    start: Date;
    end: Date
    endDate: string;
    startDate: string;
    location: string;
}

export interface GoogleTask {
    completed?: string,
    deleted?: boolean,
    due: string,
    eTag?: string,
    hidden?: boolean,
    id?: string,
    kind?: string,
    links?: string[],
    notes?: string,
    parent?: string,
    position?: string,
    selfLink?: string,
    status: TaskStatus,
    title: string,
    updated?: Date
}