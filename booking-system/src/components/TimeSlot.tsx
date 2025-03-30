import { TimeslotResource } from '@/api-resources/TimeSlot';
import { ApiService } from '@/services/api';
import { useState } from 'react';
import styles from './TimeSlot.module.css';

export default function TimeSlot({ slot }: { slot: TimeslotResource }) {
    const [canBook, setCanBook] = useState(false);
    const [error, setError] = useState<string>();

    const handleBook = async () => {
        const bookLink = slot._links['book'];
        if (!bookLink) return;

        setCanBook(true);
        setError(undefined);

        try {
            await ApiService.post(bookLink.href);
        } catch (err) {
            setError(err instanceof Error ? err.message : 'Failed to book slot');
        } finally {
            setCanBook(false);
        }
    };

    // Parse the ISO date string
    const dateTime = new Date(slot.startTime);

    return (
        <div className={`${styles.card} ${slot.isFullyBooked && styles.unavailable}`}>
            <div className={styles.time}>
                {dateTime.toLocaleDateString('nl', {
                    weekday: 'short',
                    month: 'short',
                    day: 'numeric'
                })}
                {' '}
                {dateTime.toLocaleTimeString('en-US', { 
                    hour: '2-digit', 
                    minute: '2-digit',
                    hour12: false 
                })}
            </div>
            <div className={styles.availability}>
                <span className={styles.seats}>
                    {slot.availableSeats} seats available
                </span>
                <span className={styles.status}>
                    {slot.isFullyBooked ? "Fully Booked" : "Available"}
                </span>
            </div>
            {error && <div className={styles.error}>{error}</div>}
            <button 
                className={styles.bookButton}
                disabled={slot.isFullyBooked || canBook}
                onClick={handleBook}
            >
                {canBook ? "Booking..." : slot.isFullyBooked ? "Unavailable" : "Book Now"}
            </button>
        </div>
    );
}