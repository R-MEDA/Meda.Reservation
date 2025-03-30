import { HalResource } from './HalResource';

export interface BookingResource extends HalResource {
    reservationId: string; // Changed from id
    timeSlot: {
        timeSlotId: string;
        startTime: string;
        availableSeats: number;
    };
    status: 'Confirmed' | 'Cancelled' | 'CheckedIn';
    reservedAt: string;
    _links: {
        self: {
            href: string;
            rel: string;
            method: 'GET';
        };
        'cancel-reservation'?: {  // Updated name to match API
            href: string;
            rel: string;
            method: 'DELETE';
        };
    }[];
}

export type BookingsResponse = BookingResource[];
