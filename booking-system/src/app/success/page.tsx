'use client';

import { BookingResource } from '@/api-resources/Booking';
import BackToHome from '@/components/BackToHome';
import LoadingSpinner from '@/components/LoadingSpinner';
import { ApiService } from '@/services/api';
import { useSearchParams } from 'next/navigation';
import { useEffect, useState } from 'react';
import styles from './page.module.css';

export default function SuccessPage() {
    const searchParams = useSearchParams();
    const bookingId = searchParams.get('booking');
    const [booking, setBooking] = useState<BookingResource | null>(null);
    const [error, setError] = useState<string>();

    useEffect(() => {
        if (!bookingId) {
            setError('No booking reference provided');
            return;
        }

        ApiService.get<BookingResource>(`/reservations/${bookingId}`)
            .then(setBooking)
            .catch(() => setError('Could not find booking details'));
    }, [bookingId]);

    if (error) return <div className={styles.error}>{error}</div>;
    if (!booking) return <LoadingSpinner />;

    const dateTime = new Date(booking.timeSlot.startTime);

    return (
        <div className={styles.page}>
            <div className={styles.container}>
                <BackToHome />
                <div className={styles.card}>
                    <div className={styles.icon}>✓</div>
                    <h1 className={styles.title}>Booking Confirmed!</h1>
                    <div className={styles.details}>
                        <p>Your appointment is scheduled for:</p>
                        <p className={styles.date}>
                            {dateTime.toLocaleDateString('en-US', {
                                weekday: 'long',
                                year: 'numeric',
                                month: 'long',
                                day: 'numeric'
                            })}
                        </p>
                        <p className={styles.time}>
                            {dateTime.toLocaleTimeString('en-US', {
                                hour: '2-digit',
                                minute: '2-digit',
                                hour12: false
                            })}
                        </p>
                        <p className={styles.confirmation}>
                            Booking Reference: {booking.reservationId}
                        </p>
                    </div>
                </div>
            </div>
        </div>
    );
}
