const CompanyDetails = ({
    company,
    onClose
}) => {

    if (!company) {
        return null;
    }

    return (

        <div className="company-modal-overlay">

            <div className="company-modal company-details-modal">

                <div className="company-modal-header">

                    <div>

                        <h2>
                            Company Details
                        </h2>

                        <p>
                            View company information
                        </p>

                    </div>

                    <button
                        type="button"
                        className="company-close-button"
                        onClick={onClose}
                    >
                        ×
                    </button>

                </div>


                <div className="company-details-grid">

                    <Detail
                        label="Company ID"
                        value={
                            company.companyId
                        }
                    />

                    <Detail
                        label="Company Name"
                        value={
                            company.companyName
                        }
                    />

                    <Detail
                        label="Company Code"
                        value={
                            company.companyCode
                        }
                    />

                    <Detail
                        label="Country ID"
                        value={
                            company.countryId
                        }
                    />

                    <Detail
                        label="Currency ID"
                        value={
                            company.currencyId
                        }
                    />

                    <Detail
                        label="NTN"
                        value={
                            company.ntn
                        }
                    />

                    <Detail
                        label="Registration Number"
                        value={
                            company.registrationNumber
                        }
                    />

                    <Detail
                        label="Email"
                        value={
                            company.email
                        }
                    />

                    <Detail
                        label="Website"
                        value={
                            company.website
                        }
                    />

                    <Detail
                        label="Phone"
                        value={
                            company.phone
                        }
                    />

                    <Detail
                        label="Fax"
                        value={
                            company.fax
                        }
                    />

                    <Detail
                        label="Address Line 1"
                        value={
                            company.addressLine1
                        }
                    />

                    <Detail
                        label="Address Line 2"
                        value={
                            company.addressLine2
                        }
                    />

                    <Detail
                        label="City"
                        value={
                            company.cityName
                        }
                    />

                    <Detail
                        label="State / Country"
                        value={
                            company.stateCountryName
                        }
                    />

                    <Detail
                        label="Postal Code"
                        value={
                            company.postalCode
                        }
                    />

                    <Detail
                        label="Image Path"
                        value={
                            company.imagePath
                        }
                    />

                    <Detail
                        label="Apple Store URL"
                        value={
                            company.appleStoreUrl
                        }
                    />

                    <Detail
                        label="Google Play URL"
                        value={
                            company.googlePlayStoreUrl
                        }
                    />

                    <Detail
                        label="Status"
                        value={
                            company.isActive
                                ? "Active"
                                : "Inactive"
                        }
                    />

                    <Detail
                        label="Created On"
                        value={
                            company.createdOn
                                ? new Date(
                                    company.createdOn
                                ).toLocaleString()
                                : "-"
                        }
                    />

                    <Detail
                        label="Modified On"
                        value={
                            company.modifiedOn
                                ? new Date(
                                    company.modifiedOn
                                ).toLocaleString()
                                : "-"
                        }
                    />

                </div>


                <div className="company-form-actions">

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

        <div className="company-detail-item">

            <span className="company-detail-label">
                {label}
            </span>

            <span className="company-detail-value">
                {value === null ||
                value === undefined ||
                value === ""
                    ? "-"
                    : value}
            </span>

        </div>

    );
};


export default CompanyDetails;