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
        ApiService.followLink('bookings')
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

    const handleCancelSuccess = (bookingId: string) => {
        setBookings(bookings.filter(booking => booking.id !== bookingId));
    };

    if (error) {
        return <div className={styles.error}>{error}</div>;
    }

    return (
        <div className={styles.page}>
            <div className={styles.container}>
                <BackToHome />
                <h1 className={styles.title}>Your Bookings</h1>
                <div className={styles.grid}>
                    {bookings.length === 0 ? (
                        <p className={styles.empty}>No bookings found</p>
                    ) : (
                        bookings.map(booking => (
                            <Booking 
                                key={booking.id} 
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
