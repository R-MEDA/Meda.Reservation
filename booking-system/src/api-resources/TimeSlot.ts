import { HalResource } from './HalResource';

export interface TimeslotResource extends HalResource {
    id: string;
    startTime: string;
    availableSeats: number;
    isFullyBooked: boolean;
    _links: {
        href: string;
        rel: string;
        method: string;
    }[];
}

export type TimeslotsResponse = TimeslotResource[];