import styles from './ConfirmDialog.module.css';

interface ConfirmDialogProps {
    isOpen: boolean;
    title: string;
    message: string;
    onConfirm: () => void;
    onCancel: () => void;
}

export default function ConfirmDialog({ isOpen, title, message, onConfirm, onCancel }: ConfirmDialogProps) {
    if (!isOpen) return null;

    return (
        <div className={styles.overlay}>
            <div className={styles.dialog}>
                <h2 className={styles.title}>{title}</h2>
                <p className={styles.message}>{message}</p>
                <div className={styles.buttons}>
                    <button className={styles.cancelButton} onClick={onCancel}>
                        Cancel
                    </button>
                    <button className={styles.confirmButton} onClick={onConfirm}>
                        Confirm
                    </button>
                </div>
            </div>
        </div>
    );
}
