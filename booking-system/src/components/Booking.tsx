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
        const cancelLink = booking._links['cancel'];
        if (!cancelLink) return;

        setIsLoading(true);
        setError(undefined);

        try {
            await ApiService.delete(cancelLink.href);
            onCancelSuccess(booking.id);
        } catch (err) {
            setError(err instanceof Error ? err.message : 'Failed to cancel booking');
        } finally {
            setIsLoading(false);
        }
    };

    const statusColor = {
        Confirmed: 'confirmed',
        Cancelled: 'cancelled',
        CheckedIn: 'checkedIn'
    }[booking.status];

    const dateTime = new Date(booking.reservedAt);

    return (
        <div className={styles.card}>
            <div className={styles.header}>
                <span className={`${styles.status} ${styles[statusColor]}`}>
                    {booking.status}
                </span>
                <time className={styles.date}>
                    {dateTime.toLocaleDateString()}
                </time>
            </div>
            <div className={styles.info}>
                <p>Booking ID: {booking.reservationId}</p>
                <p>Time Slot: {booking.timeSlotId}</p>
            </div>
            {error && <div className={styles.error}>{error}</div>}
            {booking._links.cancel && (
                <button 
                    className={styles.cancelButton}
                    onClick={handleCancel}
                    disabled={isLoading}
                >
                    {isLoading ? "Canceling..." : "Cancel Booking"}
                </button>
            )}
        </div>
    );
}
