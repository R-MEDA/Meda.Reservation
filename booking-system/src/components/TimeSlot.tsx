import { TimeslotResource } from '@/api-resources/TimeSlot';
import { ApiService } from '@/services/api';
import { useRouter } from 'next/navigation';
import { useState } from 'react';
import styles from './TimeSlot.module.css';

export default function TimeSlot({ slot }: { slot: TimeslotResource }) {
    const router = useRouter();
    const [isBooking, setIsBooking] = useState(false);
    const [error, setError] = useState<string>();

    const dateTime = new Date(slot.startTime);
    const canBook = !slot.isFullyBooked && slot._links.some(link => link.rel === 'book-slot');

    const handleBook = async () => {
        const bookLink = slot._links.find(link => link.rel === 'book-slot');
        if (!bookLink) return;

        setIsBooking(true);
        setError(undefined);

        try {
            const response = await ApiService.post(bookLink.href);
            if (response && 'reservationId' in response) {
                router.push(`/success?booking=${response.reservationId}`);
            } else {
                router.push('/reservations');
            }
        } catch (err) {
            console.error('Booking error:', err);
            setError(err instanceof Error ? err.message : 'Failed to book slot');
        } finally {
            setIsBooking(false);
        }
    };

    return (
        <div className={`${styles.card} ${slot.isFullyBooked && styles.unavailable}`}>
            <div className={styles.time}>
                {dateTime.toLocaleString('en-US', {
                    weekday: 'short',
                    month: 'short',
                    day: 'numeric',
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
            
            {canBook && (
                <button 
                    className={styles.bookButton}
                    disabled={isBooking}
                    onClick={handleBook}
                >
                    {isBooking ? "Booking..." : "Book Now"}
                </button>
            )}
        </div>
    );
}