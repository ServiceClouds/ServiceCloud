const StateCountryDetails = ({
    stateCountry,
    onClose
}) => {

    if (!stateCountry) {

        return null;

    }


    return (

        <div className="state-country-modal-overlay">

            <div className="state-country-modal state-country-details-modal">

                {/* =================================================
                    HEADER
                ================================================== */}

                <div className="state-country-modal-header">

                    <div>

                        <h2>
                            State / Country Details
                        </h2>

                        <p>
                            View state or province information
                        </p>

                    </div>


                    <button
                        type="button"
                        className="state-country-close-button"
                        onClick={onClose}
                    >
                        ×
                    </button>

                </div>


                {/* =================================================
                    DETAILS
                ================================================== */}

                <div className="state-country-details-grid">

                    <Detail
                        label="State / Country ID"
                        value={
                            stateCountry.stateCountryId
                        }
                    />


                    <Detail
                        label="State / Country Name"
                        value={
                            stateCountry.stateCountryName
                        }
                    />


                    <Detail
                        label="Country ID"
                        value={
                            stateCountry.countryId
                        }
                    />


                    <Detail
                        label="Status"
                        value={
                            stateCountry.isActive
                                ? "Active"
                                : "Inactive"
                        }
                    />


                    <Detail
                        label="Created By"
                        value={
                            stateCountry.createdBy
                        }
                    />


                    <Detail
                        label="Created On"
                        value={
                            stateCountry.createdOn
                                ? new Date(
                                    stateCountry.createdOn
                                ).toLocaleString()
                                : "-"
                        }
                    />


                    <Detail
                        label="Modified By"
                        value={
                            stateCountry.modifiedBy
                        }
                    />


                    <Detail
                        label="Modified On"
                        value={
                            stateCountry.modifiedOn
                                ? new Date(
                                    stateCountry.modifiedOn
                                ).toLocaleString()
                                : "-"
                        }
                    />

                </div>


                {/* =================================================
                    FOOTER
                ================================================== */}

                <div className="state-country-form-actions">

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

        <div className="state-country-detail-item">

            <span className="state-country-detail-label">

                {label}

            </span>


            <span className="state-country-detail-value">

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


export default StateCountryDetails;