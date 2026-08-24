function ConfirmDialog({
    open,
    title = "Confirm Action",
    message,
    confirmText = "Confirm",
    cancelText = "Cancel",
    loading = false,
    onConfirm,
    onCancel
}) {
    if (!open) {
        return null;
    }

    return (
        <div className="dialog-overlay">
            <div className="confirm-dialog">

                <h3>{title}</h3>

                <p>{message}</p>

                <div className="dialog-actions">

                    <button
                        type="button"
                        className="btn-secondary"
                        onClick={onCancel}
                        disabled={loading}
                    >
                        {cancelText}
                    </button>

                    <button
                        type="button"
                        className="btn-danger"
                        onClick={onConfirm}
                        disabled={loading}
                    >
                        {loading ? "Processing..." : confirmText}
                    </button>

                </div>

            </div>
        </div>
    );
}

export default ConfirmDialog;