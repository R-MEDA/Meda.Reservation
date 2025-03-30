import { BookingResource } from '@/api-resources/Booking';
import { ApiService } from '@/services/api';
import { useState } from 'react';
import styles from './Booking.module.css';

interface BookingProps {
    booking: BookingResource;
    onCancelSuccess: (bookingId: string) => void;
}

export default function Booking({ booking, onCancelSuccess }: BookingProps) {
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState<string>();

    const handleCancel = async () => {
        const cancelLink = booking._links.find(link => link.rel === 'cancel');
        if (!cancelLink) return;

        setIsLoading(true);
        setError(undefined);

        try {
            await ApiService.delete(cancelLink.href);
            onCancelSuccess(booking.reservationId);
        } catch (err) {
            setError(err instanceof Error ? err.message : 'Failed to cancel reservation');
        } finally {
            setIsLoading(false);
        }
    };

    const dateTime = new Date(booking.timeSlot.startTime);

    return (
        <div className={styles.card}>
            <div className={styles.header}>
                <span className={`${styles.status} ${styles[booking.status.toLowerCase()]}`}>
                    {booking.status}
                </span>
                <time className={styles.date}>
                    {dateTime.toLocaleDateString()}
                </time>
            </div>
            <div className={styles.info}>
                <p>Reservation ID: {booking.reservationId}</p>
                <p>Time: {dateTime.toLocaleTimeString()}</p>
            </div>
            {error && <div className={styles.error}>{error}</div>}
            {booking._links.some(link => link.rel === 'cancel') && (
                <button 
                    className={styles.cancelButton}
                    onClick={handleCancel}
                    disabled={isLoading}
                >
                    {isLoading ? "Canceling..." : "Cancel Reservation"}
                </button>
            )}
        </div>
    );
}
