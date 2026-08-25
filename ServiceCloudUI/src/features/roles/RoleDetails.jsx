const RoleDetails = ({
    role,
    onClose
}) => {

    if (!role) {

        return null;

    }


    return (

        <div className="role-modal-overlay">

            <div className="role-modal role-details-modal">

                <div className="role-modal-header">

                    <div>

                        <h2>
                            Role Details
                        </h2>

                        <p>
                            View role information
                        </p>

                    </div>


                    <button
                        type="button"
                        className="role-close-button"
                        onClick={onClose}
                    >
                        ×
                    </button>

                </div>


                <div className="role-details-grid">

                    <Detail
                        label="Role ID"
                        value={
                            role.roleId
                        }
                    />


                    <Detail
                        label="Role Name"
                        value={
                            role.roleName
                        }
                    />


                    <Detail
                        label="Status"
                        value={
                            role.isActive
                                ? "Active"
                                : "Inactive"
                        }
                    />


                    <Detail
                        label="Created By"
                        value={
                            role.createdBy
                        }
                    />


                    <Detail
                        label="Created On"
                        value={
                            role.createdOn
                                ? new Date(
                                    role.createdOn
                                ).toLocaleString()
                                : "-"
                        }
                    />


                    <Detail
                        label="Modified By"
                        value={
                            role.modifiedBy
                        }
                    />


                    <Detail
                        label="Modified On"
                        value={
                            role.modifiedOn
                                ? new Date(
                                    role.modifiedOn
                                ).toLocaleString()
                                : "-"
                        }
                    />

                </div>


                <div className="role-form-actions">

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

        <div className="role-detail-item">

            <span className="role-detail-label">
                {label}
            </span>


            <span className="role-detail-value">

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


export default RoleDetails;