import React, { useEffect, useState, useCallback } from "react";
import {
    getServices,
    createService,
    updateService,
    archiveService,
    getServiceById
} from "../../api/service/serviceApi";

import ServiceForm from "./ServiceForm";
import ServiceDetails from "./ServiceDetails";
import "./service.css";

const ServicePage = () => {
    const [services, setServices] = useState([]);

    const [loading, setLoading] = useState(false);
    const [saving, setSaving] = useState(false);

    const [error, setError] = useState("");
    const [successMessage, setSuccessMessage] = useState("");

    const [search, setSearch] = useState("");
    const [pageNumber, setPageNumber] = useState(1);
    const [pageSize] = useState(10);
    const [totalPages, setTotalPages] = useState(0);
    const [totalRecords, setTotalRecords] = useState(0);

    const [showForm, setShowForm] = useState(false);
    const [showDetails, setShowDetails] = useState(false);

    const [editingService, setEditingService] = useState(null);
    const [selectedService, setSelectedService] = useState(null);

    const loadServices = useCallback(async () => {
        try {
            setLoading(true);
            setError("");

            const response = await getServices({
                pageNumber,
                pageSize,
                search
            });

            /*
             * Adjust these two lines only if your project's
             * ToApiResponse() returns a different wrapper.
             */
            const data = response?.data ?? response;

            const items =
                data?.items ??
                data?.Items ??
                [];

            setServices(items);

            setTotalPages(
                data?.totalPages ??
                data?.TotalPages ??
                0
            );

            setTotalRecords(
                data?.totalRecords ??
                data?.TotalRecords ??
                0
            );
        } catch (err) {
            console.error("Failed to load services:", err);

            setError(
                err?.response?.data?.message ||
                err?.response?.data?.Message ||
                "Failed to load services."
            );
        } finally {
            setLoading(false);
        }
    }, [pageNumber, pageSize, search]);

    useEffect(() => {
        loadServices();
    }, [loadServices]);

    const handleSearchChange = (e) => {
        setSearch(e.target.value);
        setPageNumber(1);
    };

    const handleAdd = () => {
        setEditingService(null);
        setError("");
        setSuccessMessage("");
        setShowForm(true);
    };

    const handleEdit = async (service) => {
        try {
            setError("");

            const response = await getServiceById(service.serviceId);

            const data = response?.data ?? response;

            setEditingService(data);
            setShowForm(true);
        } catch (err) {
            console.error("Failed to load service:", err);

            setError(
                err?.response?.data?.message ||
                err?.response?.data?.Message ||
                "Failed to load service details."
            );
        }
    };

    const handleView = async (service) => {
        try {
            setError("");

            const response = await getServiceById(service.serviceId);

            const data = response?.data ?? response;

            setSelectedService(data);
            setShowDetails(true);
        } catch (err) {
            console.error("Failed to load service:", err);

            setError(
                err?.response?.data?.message ||
                err?.response?.data?.Message ||
                "Failed to load service details."
            );
        }
    };

    const handleSubmit = async (formData) => {
        try {
            setSaving(true);
            setError("");
            setSuccessMessage("");

            if (editingService) {
                await updateService({
                    serviceId: editingService.serviceId,
                    serviceCategoryId: Number(formData.serviceCategoryId),
                    serviceName: formData.serviceName,
                    description: formData.description || null,
                    specialInstruction:
                        formData.specialInstruction || null,
                    hasBranchPermission:
                        formData.hasBranchPermission,
                    allowBranchEditPrice:
                        formData.allowBranchEditPrice
                });

                setSuccessMessage("Service updated successfully.");
            } else {
                await createService({
                    serviceCategoryId: Number(formData.serviceCategoryId),
                    serviceName: formData.serviceName,
                    description: formData.description || null,
                    specialInstruction:
                        formData.specialInstruction || null,
                    hasBranchPermission:
                        formData.hasBranchPermission,
                    allowBranchEditPrice:
                        formData.allowBranchEditPrice,
                    appSourceTypeId:
                        Number(formData.appSourceTypeId)
                });

                setSuccessMessage("Service created successfully.");
            }

            setShowForm(false);
            setEditingService(null);

            await loadServices();
        } catch (err) {
            console.error("Failed to save service:", err);

            setError(
                err?.response?.data?.message ||
                err?.response?.data?.Message ||
                "Failed to save service."
            );
        } finally {
            setSaving(false);
        }
    };

    const handleArchive = async (service) => {
        const confirmed = window.confirm(
            `Are you sure you want to archive "${service.serviceName}"?`
        );

        if (!confirmed) {
            return;
        }

        try {
            setLoading(true);
            setError("");
            setSuccessMessage("");

            await archiveService(service.serviceId);

            setSuccessMessage("Service archived successfully.");

            /*
             * If the last item on the current page was archived,
             * move back one page when necessary.
             */
            if (services.length === 1 && pageNumber > 1) {
                setPageNumber((previous) => previous - 1);
            } else {
                await loadServices();
            }
        } catch (err) {
            console.error("Failed to archive service:", err);

            setError(
                err?.response?.data?.message ||
                err?.response?.data?.Message ||
                "Failed to archive service."
            );
        } finally {
            setLoading(false);
        }
    };

    const handleCloseForm = () => {
        if (saving) {
            return;
        }

        setShowForm(false);
        setEditingService(null);
    };

    const handleCloseDetails = () => {
        setShowDetails(false);
        setSelectedService(null);
    };

    return (
        <div className="service-page">

            {/* Header */}
            <div className="service-header">
                <div>
                    <h1>Services</h1>
                    <p>
                        Manage services offered by your business.
                    </p>
                </div>

                <button
                    type="button"
                    className="service-primary-btn"
                    onClick={handleAdd}
                >
                    + Add Service
                </button>
            </div>

            {/* Messages */}
            {error && (
                <div className="service-alert service-alert-error">
                    {error}
                </div>
            )}

            {successMessage && (
                <div className="service-alert service-alert-success">
                    {successMessage}
                </div>
            )}

            {/* Toolbar */}
            <div className="service-toolbar">
                <div className="service-search">
                    <input
                        type="text"
                        placeholder="Search services..."
                        value={search}
                        onChange={handleSearchChange}
                    />
                </div>

                <div className="service-record-count">
                    Total: {totalRecords}
                </div>
            </div>

            {/* Table */}
            <div className="service-table-wrapper">
                <table className="service-table">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Service Name</th>
                            <th>Description</th>
                            <th>Actions</th>
                        </tr>
                    </thead>

                    <tbody>
                        {loading ? (
                            <tr>
                                <td
                                    colSpan="4"
                                    className="service-empty"
                                >
                                    Loading services...
                                </td>
                            </tr>
                        ) : services.length === 0 ? (
                            <tr>
                                <td
                                    colSpan="4"
                                    className="service-empty"
                                >
                                    No services found.
                                </td>
                            </tr>
                        ) : (
                            services.map((service) => (
                                <tr key={service.serviceId}>
                                    <td>
                                        {service.serviceId}
                                    </td>

                                    <td className="service-name-cell">
                                        {service.serviceName || "-"}
                                    </td>

                                    <td>
                                        {service.description || "-"}
                                    </td>

                                    <td>
                                        <div className="service-actions">
                                            <button
                                                type="button"
                                                className="service-action-btn view"
                                                onClick={() =>
                                                    handleView(service)
                                                }
                                            >
                                                View
                                            </button>

                                            <button
                                                type="button"
                                                className="service-action-btn edit"
                                                onClick={() =>
                                                    handleEdit(service)
                                                }
                                            >
                                                Edit
                                            </button>

                                            <button
                                                type="button"
                                                className="service-action-btn archive"
                                                onClick={() =>
                                                    handleArchive(service)
                                                }
                                            >
                                                Archive
                                            </button>
                                        </div>
                                    </td>
                                </tr>
                            ))
                        )}
                    </tbody>
                </table>
            </div>

            {/* Pagination */}
            {totalPages > 0 && (
                <div className="service-pagination">

                    <button
                        type="button"
                        disabled={pageNumber <= 1 || loading}
                        onClick={() =>
                            setPageNumber((previous) =>
                                Math.max(previous - 1, 1)
                            )
                        }
                    >
                        Previous
                    </button>

                    <span>
                        Page {pageNumber} of {totalPages}
                    </span>

                    <button
                        type="button"
                        disabled={
                            pageNumber >= totalPages ||
                            loading
                        }
                        onClick={() =>
                            setPageNumber((previous) =>
                                Math.min(
                                    previous + 1,
                                    totalPages
                                )
                            )
                        }
                    >
                        Next
                    </button>

                </div>
            )}

            {/* Create / Edit Modal */}
            {showForm && (
                <div className="service-modal-overlay">
                    <div className="service-modal">

                        <div className="service-modal-header">
                            <h2>
                                {editingService
                                    ? "Edit Service"
                                    : "Add Service"}
                            </h2>

                            <button
                                type="button"
                                className="service-modal-close"
                                onClick={handleCloseForm}
                                disabled={saving}
                            >
                                ×
                            </button>
                        </div>

                        <ServiceForm
                            editingService={editingService}
                            onSubmit={handleSubmit}
                            onCancel={handleCloseForm}
                            saving={saving}
                        />

                    </div>
                </div>
            )}

            {/* Details Modal */}
            {showDetails && selectedService && (
                <div className="service-modal-overlay">
                    <div className="service-modal service-details-modal">

                        <div className="service-modal-header">
                            <h2>Service Details</h2>

                            <button
                                type="button"
                                className="service-modal-close"
                                onClick={handleCloseDetails}
                            >
                                ×
                            </button>
                        </div>

                        <ServiceDetails
                            service={selectedService}
                        />

                    </div>
                </div>
            )}

        </div>
    );
};

export default ServicePage;