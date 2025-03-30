'use client';

import { BookingsResponse } from '@/api-resources/Booking';
import BackToHome from '@/components/BackToHome';
import Booking from '@/components/Booking';
import { ApiService } from '@/services/api';
import { useEffect, useState } from 'react';
import styles from './page.module.css';

export default function Bookings() {
    const [bookings, setBookings] = useState<BookingsResponse>([]);
    const [error, setError] = useState<string>();

    const fetchBookings = () => {
        ApiService.followLink('reservations')
            .then(response => {
                if (Array.isArray(response)) {
                    setBookings(response);
                } else {
                    setError('Invalid response format');
                }
            })
            .catch(err => setError(err.message));
    };

    useEffect(() => {
        fetchBookings();
    }, []);

    const handleCancelSuccess = async (bookingId: string) => {
        try {
            const updatedBooking = await ApiService.get<BookingResource>(`/reservations/${bookingId}`);
            setBookings(bookings.map(booking => 
                booking.reservationId === bookingId ? updatedBooking : booking
            ));
        } catch (err) {
            setError('Failed to refresh booking data');
        }
    };

    if (error) {
        return <div className={styles.error}>{error}</div>;
    }

    return (
        <div className={styles.page}>
            <div className={styles.container}>
                <BackToHome />
                <h1 className={styles.title}>Your Reservations</h1>
                <div className={styles.grid}>
                    {bookings.length === 0 ? (
                        <p className={styles.empty}>No reservations found</p>
                    ) : (
                        bookings.map(booking => (
                            <Booking 
                                key={booking.reservationId} 
                                booking={booking} 
                                onCancelSuccess={handleCancelSuccess}
                            />
                        ))
                    )}
                </div>
            </div>
        </div>
    );
}
