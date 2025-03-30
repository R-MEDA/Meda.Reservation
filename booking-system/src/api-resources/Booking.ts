import { HalResource } from './HalResource';

export interface BookingResource extends HalResource {
    id: string;
    timeSlotId: string;
    customerId: string;
    status: 'Confirmed' | 'Cancelled' | 'CheckedIn';
    reservedAt: string;
    _links: {
        self: {
            href: string;
            method: 'GET';
        };
        cancel?: {
            href: string;
            method: 'DELETE';
        };
    };
}

export type BookingsResponse = BookingResource[];
