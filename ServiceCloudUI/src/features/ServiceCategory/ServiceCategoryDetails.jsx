import React from "react";

const ServiceCategoryDetails = ({
    category,
    onClose,
}) => {
    if (!category) {
        return null;
    }

    const displayValue = (value) => {
        if (
            value === null ||
            value === undefined ||
            value === ""
        ) {
            return "-";
        }

        return value;
    };

    return (
        <div className="service-category-modal-overlay">

            <div className="service-category-modal details-modal">

                {/* Header */}

                <div className="service-category-modal-header">

                    <div>
                        <h2>
                            Service Category Details
                        </h2>

                        <p>
                            View service category information.
                        </p>
                    </div>

                    <button
                        type="button"
                        className="service-category-close-button"
                        onClick={onClose}
                    >
                        ×
                    </button>

                </div>

                {/* Details */}

                <div className="service-category-details-grid">

                    <div className="service-category-detail">
                        <span>ID</span>

                        <strong>
                            {displayValue(
                                category.serviceCategoryId
                            )}
                        </strong>
                    </div>

                    <div className="service-category-detail">
                        <span>Name</span>

                        <strong>
                            {displayValue(
                                category.serviceCategoryName
                            )}
                        </strong>
                    </div>

                    <div className="service-category-detail full-width">
                        <span>Description</span>

                        <strong>
                            {displayValue(
                                category.description
                            )}
                        </strong>
                    </div>

                    <div className="service-category-detail">
                        <span>Image Path</span>

                        <strong>
                            {displayValue(
                                category.imagePath
                            )}
                        </strong>
                    </div>

                    <div className="service-category-detail">
                        <span>App Source Type ID</span>

                        <strong>
                            {displayValue(
                                category.appSourceTypeId
                            )}
                        </strong>
                    </div>

                    <div className="service-category-detail">
                        <span>Color</span>

                        <strong>
                            {displayValue(
                                category.color
                            )}
                        </strong>
                    </div>

                    <div className="service-category-detail">
                        <span>Sort Index</span>

                        <strong>
                            {displayValue(
                                category.sortIndex
                            )}
                        </strong>
                    </div>

                    <div className="service-category-detail">
                        <span>Branch Permission</span>

                        <strong>
                            {category.hasBranchPermission
                                ? "Yes"
                                : "No"}
                        </strong>
                    </div>

                    <div className="service-category-detail">
                        <span>Status</span>

                        <strong
                            className={
                                category.isArchived
                                    ? "status archived"
                                    : "status active"
                            }
                        >
                            {category.isArchived
                                ? "Archived"
                                : "Active"}
                        </strong>
                    </div>

                    <div className="service-category-detail">
                        <span>Company ID</span>

                        <strong>
                            {displayValue(
                                category.companyId
                            )}
                        </strong>
                    </div>

                </div>

                {/* Footer */}

                <div className="service-category-modal-actions">

                    <button
                        type="button"
                        className="service-category-button secondary"
                        onClick={onClose}
                    >
                        Close
                    </button>

                </div>

            </div>

        </div>
    );
};

export default ServiceCategoryDetails;