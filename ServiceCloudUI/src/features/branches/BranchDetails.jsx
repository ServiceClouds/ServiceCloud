const BranchDetails = ({
    branch,
    onClose
}) => {

    if (!branch) {

        return null;

    }


    return (

        <div className="branch-modal-overlay">

            <div className="branch-modal branch-details-modal">

                {/* =================================================
                    HEADER
                ================================================== */}

                <div className="branch-modal-header">

                    <div>

                        <h2>
                            Branch Details
                        </h2>

                        <p>
                            View branch information
                        </p>

                    </div>


                    <button
                        type="button"
                        className="branch-close-button"
                        onClick={onClose}
                    >
                        ×
                    </button>

                </div>


                {/* =================================================
                    BASIC INFORMATION
                ================================================== */}

                <div className="branch-detail-section">

                    <h3>
                        Basic Information
                    </h3>


                    <div className="branch-details-grid">

                        <Detail
                            label="Branch ID"
                            value={
                                branch.branchId
                            }
                        />


                        <Detail
                            label="Company ID"
                            value={
                                branch.companyId
                            }
                        />


                        <Detail
                            label="Country ID"
                            value={
                                branch.countryId
                            }
                        />


                        <Detail
                            label="Branch Name"
                            value={
                                branch.branchName
                            }
                        />


                        <Detail
                            label="Branch Code"
                            value={
                                branch.branchCode
                            }
                        />


                        <Detail
                            label="Status"
                            value={
                                branch.isActive
                                    ? "Active"
                                    : "Inactive"
                            }
                        />


                        <Detail
                            label="Archived"
                            value={
                                branch.isArchived
                                    ? "Yes"
                                    : "No"
                            }
                        />


                        <Detail
                            label="Online"
                            value={
                                branch.isOnline
                                    ? "Yes"
                                    : "No"
                            }
                        />

                    </div>

                </div>


                {/* =================================================
                    LOCATION
                ================================================== */}

                <div className="branch-detail-section">

                    <h3>
                        Location
                    </h3>


                    <div className="branch-details-grid">

                        <Detail
                            label="City"
                            value={
                                branch.cityName
                            }
                        />


                        <Detail
                            label="State / Province"
                            value={
                                branch.stateCountryName
                            }
                        />


                        <Detail
                            label="Address Line 1"
                            value={
                                branch.addressLine1
                            }
                        />


                        <Detail
                            label="Address Line 2"
                            value={
                                branch.addressLine2
                            }
                        />


                        <Detail
                            label="Postal Code"
                            value={
                                branch.postalCode
                            }
                        />


                        <Detail
                            label="Time Zone"
                            value={
                                branch.timeZone
                            }
                        />

                    </div>

                </div>


                {/* =================================================
                    CONTACT
                ================================================== */}

                <div className="branch-detail-section">

                    <h3>
                        Contact
                    </h3>


                    <div className="branch-details-grid">

                        <Detail
                            label="Email"
                            value={
                                branch.email
                            }
                        />


                        <Detail
                            label="Phone"
                            value={
                                branch.phone
                            }
                        />


                        <Detail
                            label="Mobile"
                            value={
                                branch.mobile
                            }
                        />


                        <Detail
                            label="Fax"
                            value={
                                branch.fax
                            }
                        />

                    </div>

                </div>


                {/* =================================================
                    REGIONAL / LEGAL
                ================================================== */}

                <div className="branch-detail-section">

                    <h3>
                        Regional / Legal
                    </h3>


                    <div className="branch-details-grid">

                        <Detail
                            label="Currency"
                            value={
                                branch.currency
                            }
                        />


                        <Detail
                            label="Date Format ID"
                            value={
                                branch.dateFormatId
                            }
                        />


                        <Detail
                            label="Terms of Service"
                            value={
                                branch.termsOfServiceUrl
                            }
                        />


                        <Detail
                            label="Privacy Policy"
                            value={
                                branch.privacyPolicyUrl
                            }
                        />

                    </div>

                </div>


                {/* =================================================
                    AUDIT
                ================================================== */}

                <div className="branch-detail-section">

                    <h3>
                        Audit Information
                    </h3>


                    <div className="branch-details-grid">

                        <Detail
                            label="Created By"
                            value={
                                branch.createdBy
                            }
                        />


                        <Detail
                            label="Created On"
                            value={
                                branch.createdOn
                                    ? new Date(
                                        branch.createdOn
                                    ).toLocaleString()
                                    : "-"
                            }
                        />


                        <Detail
                            label="Modified By"
                            value={
                                branch.modifiedBy
                            }
                        />


                        <Detail
                            label="Modified On"
                            value={
                                branch.modifiedOn
                                    ? new Date(
                                        branch.modifiedOn
                                    ).toLocaleString()
                                    : "-"
                            }
                        />

                    </div>

                </div>


                {/* =================================================
                    FOOTER
                ================================================== */}

                <div className="branch-form-actions">

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

        <div className="branch-detail-item">

            <span className="branch-detail-label">

                {label}

            </span>


            <span className="branch-detail-value">

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


export default BranchDetails;