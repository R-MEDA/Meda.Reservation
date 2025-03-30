'use client';

import { TimeslotsResponse } from '@/api-resources/TimeSlot';
import BackToHome from '@/components/BackToHome';
import TimeSlot from '@/components/TimeSlot';
import { ApiService } from '@/services/api';
import { useRouter } from 'next/navigation';
import { useEffect, useState } from 'react';
import styles from './page.module.css';

export default function BookPage() {
    const router = useRouter();
    const [slots, setSlots] = useState<TimeslotsResponse>([]);
    const [selectedSlot, setSelectedSlot] = useState<Date | null>(null);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        ApiService.followLink<TimeslotsResponse>('slots')
            .then(response => {
                console.log('API Response:', response); // Debug log
                if (Array.isArray(response)) {
                    setSlots(response);
                } else {
                    setError('Invalid response format');
                }
            })
            .catch(err => {
                console.error('API Error:', err); // Debug log
                setError(err.message)
            });
    }, []);

    const handleConfirmBooking = async () => {
        if (!selectedSlot) return;

        try {
            setLoading(true);
            setError(null);
            
            await ApiService.post("/bookings", {
                dateTime: selectedSlot,
            });

            // Redirect to reservations page after successful booking
            router.push("/reservations");
            
        } catch (err) {
            setError("Failed to make booking. Please try again.");
        } finally {
            setLoading(false);
        }
    };

    if (error) {
        return <div className={styles.error}>{error}</div>;
    }

    return (
        <div className={styles.page}>
            <div className={styles.container}>
                <BackToHome />
                <h1 className={styles.title}>Choose Your Time Slot</h1>
                <div className={styles.grid}>
                    {slots.map(slot => (
                        <TimeSlot key={slot.id} slot={slot} />
                    ))}
                </div>
            </div>
        </div>
    );
}