import { HalResource, Link } from './HalResource';

export interface BookingResource extends HalResource {
    reservationId: string;
    timeSlot: {
        timeSlotId: string;
        startTime: string;
        availableSeats: number;
    };
    status: string;
    reservedAt: string;
    _links: Link[];
}

export type BookingsResponse = BookingResource[];
