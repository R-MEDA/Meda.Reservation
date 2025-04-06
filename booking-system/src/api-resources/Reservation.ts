import { HalResource } from './HalResource';

interface TimeSlotResource {
    timeSlotId: string;
    startTime: string;
    availableSeats: number;
}

export interface ReservationResource extends HalResource {
    reservationId: string;
    timeSlot: TimeSlotResource;
    status: 'Confirmed' | 'Cancelled' | 'CheckedIn';
    reservedAt: string;
}

export type ReservationsResponse = ReservationResource[];
