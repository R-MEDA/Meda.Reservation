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
    const [cancelLink] = useState(booking._links.find(link => link.rel === 'cancel-reservation')?.href);

    const handleCancel = async () => {
        if (!cancelLink) return;

        setIsLoading(true);

        await ApiService.delete(cancelLink);
        onCancelSuccess(booking.reservationId);

        setIsLoading(false);
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

            {cancelLink && (
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