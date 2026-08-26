import React from "react";

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

const booleanValue = (value) => {
    return value ? "Yes" : "No";
};

const ServiceDetails = ({ service }) => {
    return (
        <div className="service-details">

            <div className="service-detail-grid">

                <div className="service-detail-item">
                    <span className="service-detail-label">
                        Service ID
                    </span>

                    <span className="service-detail-value">
                        {displayValue(service.serviceId)}
                    </span>
                </div>

                <div className="service-detail-item">
                    <span className="service-detail-label">
                        Service Category ID
                    </span>

                    <span className="service-detail-value">
                        {displayValue(
                            service.serviceCategoryId
                        )}
                    </span>
                </div>

                <div className="service-detail-item">
                    <span className="service-detail-label">
                        Service Name
                    </span>

                    <span className="service-detail-value">
                        {displayValue(
                            service.serviceName
                        )}
                    </span>
                </div>

                <div className="service-detail-item">
                    <span className="service-detail-label">
                        Description
                    </span>

                    <span className="service-detail-value">
                        {displayValue(
                            service.description
                        )}
                    </span>
                </div>

                <div className="service-detail-item service-detail-full">
                    <span className="service-detail-label">
                        Special Instruction
                    </span>

                    <span className="service-detail-value">
                        {displayValue(
                            service.specialInstruction
                        )}
                    </span>
                </div>

                <div className="service-detail-item">
                    <span className="service-detail-label">
                        Branch Permission
                    </span>

                    <span className="service-detail-value">
                        {booleanValue(
                            service.hasBranchPermission
                        )}
                    </span>
                </div>

                <div className="service-detail-item">
                    <span className="service-detail-label">
                        Branch Edit Price
                    </span>

                    <span className="service-detail-value">
                        {booleanValue(
                            service.allowBranchEditPrice
                        )}
                    </span>
                </div>

            </div>

        </div>
    );
};

export default ServiceDetails;