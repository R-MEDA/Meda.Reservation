import { HalResource } from './HalResource';

export interface TimeslotResource extends HalResource {
    id: string;
    startTime: string;
    availableSeats: number;
    isFullyBooked: boolean;
    _links: {
        self: {
            href: string;
            method: 'GET';
        };
        book?: {
            href: string;
            method: 'POST';
        };
    };
}

export type TimeslotsResponse = TimeslotResource[];