import { TimeslotResource } from '@/api-resources/TimeSlot';
import { ApiService } from '@/services/api';
import { useRouter } from 'next/navigation';
import { useState } from 'react';
import styles from './TimeSlot.module.css';

export default function TimeSlot({ slot }: { slot: TimeslotResource }) {
    const router = useRouter();
    const [bookLink] = useState(slot._links.find(link => link.rel === 'book-slot')?.href);

    const dateTime = new Date(slot.startTime);

    const handleBook = async () => {
        if (!bookLink) return;

        const response = await ApiService.post(bookLink);
        if (response && 'reservationId' in response) {
            router.push(`/success?booking=${response.reservationId}`);
        } else {
            router.push('/reservations');
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

            {bookLink && (
                <button className={styles.bookButton} onClick={handleBook}>
                    Book now
                </button>
            )}
        </div>
    );
}