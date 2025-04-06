import Link from 'next/link';
import styles from './BackToHome.module.css';

export default function BackToHome() {
    return (
        <Link href="/" className={styles.backLink}>
            ← Back to Home
        </Link>
    );
}