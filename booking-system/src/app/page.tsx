import Link from "next/link";
import styles from "./page.module.css";

export default function Home() {
  return (
    <div className={styles.page}>
      <main className={styles.main}>
        <h1>Welcome to the Booking System</h1>
        
        <div className={styles.ctas}>
          <Link 
          
            href="/book" 
            className={styles.primary}
          >
            Make a Reservation
          </Link>
          
          <Link 
            href="/reservations" 
            className={styles.secondary}
          >
            View Your Reservations
          </Link>
        </div>
      </main>
    </div>
  );
}
