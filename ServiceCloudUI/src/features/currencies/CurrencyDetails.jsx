const CurrencyDetails = ({
    currency,
    onClose
}) => {

    if (!currency) {

        return null;

    }


    return (

        <div className="currency-modal-overlay">

            <div className="currency-modal currency-details-modal">

                <div className="currency-modal-header">

                    <div>

                        <h2>
                            Currency Details
                        </h2>

                        <p>
                            View currency information
                        </p>

                    </div>


                    <button
                        type="button"
                        className="currency-close-button"
                        onClick={onClose}
                    >
                        ×
                    </button>

                </div>


                <div className="currency-details-grid">

                    <Detail
                        label="Currency ID"
                        value={
                            currency.currencyId
                        }
                    />


                    <Detail
                        label="Currency Name"
                        value={
                            currency.currencyName
                        }
                    />


                    <Detail
                        label="Currency Code"
                        value={
                            currency.currencyCode
                        }
                    />


                    <Detail
                        label="Status"
                        value={
                            currency.isActive
                                ? "Active"
                                : "Inactive"
                        }
                    />


                    <Detail
                        label="Created By"
                        value={
                            currency.createdBy
                        }
                    />


                    <Detail
                        label="Created On"
                        value={
                            currency.createdOn
                                ? new Date(
                                    currency.createdOn
                                ).toLocaleString()
                                : "-"
                        }
                    />


                    <Detail
                        label="Modified By"
                        value={
                            currency.modifiedBy
                        }
                    />


                    <Detail
                        label="Modified On"
                        value={
                            currency.modifiedOn
                                ? new Date(
                                    currency.modifiedOn
                                ).toLocaleString()
                                : "-"
                        }
                    />

                </div>


                <div className="currency-form-actions">

                    <button
                        type="button"
                        onClick={onClose}
                    >
                        Close
                    </button>

                </div>

            </div>

        </div>

    );

};


const Detail = ({
    label,
    value
}) => {

    return (

        <div className="currency-detail-item">

            <span className="currency-detail-label">
                {label}
            </span>


            <span className="currency-detail-value">

                {
                    value === null ||
                    value === undefined ||
                    value === ""
                        ? "-"
                        : value
                }

            </span>

        </div>

    );

};


export default CurrencyDetails;