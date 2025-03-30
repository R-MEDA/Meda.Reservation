import { HalResource } from './HalResource';
import { TimeslotResource } from './TimeSlot';

export interface ReservationResource extends HalResource {
    // Properties must match exactly what backend returns
    reservationId: string; // Keep as is - matches backend
    timeSlot: TimeslotResource;  
    status: 'Confirmed' | 'Cancelled' | 'Rescheduled' | 'NoShow' | 'CheckedIn'; // Match all enum values
    reservedAt: string;
    _links: {
        self: {
            href: string;
        };
        cancel?: {
            href: string; 
            rel: string;
            method: 'POST';
        };
    };
}

export interface ReservationsCollectionResource extends HalResource {
    _embedded: {
        reservations: ReservationResource[];
    };
    _links: {
        self: { href: string };
    };
}

export type ReservationsResponse = ReservationsCollectionResource;
