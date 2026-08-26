import React, { useEffect, useState } from "react";

import {
    getServiceCategories,
    getServiceCategoryById,
    createServiceCategory,
    updateServiceCategory,
    archiveServiceCategory,
} from "../../api/serviceCategory/serviceCategoryApi";

import ServiceCategoryForm from "./ServiceCategoryForm";
import ServiceCategoryDetails from "./ServiceCategoryDetails";

import "./serviceCategory.css";

const ServiceCategoryPage = () => {

    /*
     * Data
     */

    const [items, setItems] = useState([]);

    /*
     * Loading states
     */

    const [loading, setLoading] = useState(false);

    const [saving, setSaving] = useState(false);

    /*
     * Error
     */

    const [error, setError] = useState("");

    /*
     * Pagination / Search
     */

    const [search, setSearch] = useState("");

    const [pageNumber, setPageNumber] = useState(1);

    const [pageSize] = useState(10);

    const [totalPages, setTotalPages] = useState(0);

    const [totalRecords, setTotalRecords] = useState(0);

    /*
     * Modal state
     */

    const [editingItem, setEditingItem] = useState(null);

    const [selectedItem, setSelectedItem] = useState(null);

    const [showForm, setShowForm] = useState(false);

    const [showDetails, setShowDetails] = useState(false);

    /*
     * Extract API response
     *
     * This supports the common response shape:
     *
     * {
     *     data: {
     *         items: [],
     *         totalPages: 1,
     *         totalRecords: 10
     *     }
     * }
     */

    const extractData = (response) => {
        return response?.data ?? response;
    };

    /*
     * API error extraction
     */

    const getErrorMessage = (
        err,
        fallback
    ) => {

        const responseData =
            err?.response?.data;

        if (
            typeof responseData === "string"
        ) {
            return responseData;
        }

        if (
            responseData?.message
        ) {
            return responseData.message;
        }

        if (
            responseData?.error?.message
        ) {
            return responseData.error.message;
        }

        if (
            Array.isArray(
                responseData?.errors
            )
        ) {
            return responseData.errors.join(", ");
        }

        if (
            responseData?.errors &&
            typeof responseData.errors === "object"
        ) {
            return Object.values(
                responseData.errors
            )
                .flat()
                .join(", ");
        }

        return fallback;
    };

    /*
     * Load service categories
     */

    const loadItems = async () => {

        try {

            setLoading(true);

            setError("");

            const response =
                await getServiceCategories({
                    pageNumber,
                    pageSize,
                    search,
                });

            const result =
                extractData(response);

            setItems(
                result?.items ?? []
            );

            setTotalPages(
                result?.totalPages ?? 0
            );

            setTotalRecords(
                result?.totalRecords ?? 0
            );

        } catch (err) {

            console.error(
                "Failed to load service categories:",
                err
            );

            setError(
                getErrorMessage(
                    err,
                    "Failed to load service categories."
                )
            );

        } finally {

            setLoading(false);
        }
    };

    /*
     * Load whenever page/search changes
     */

    useEffect(() => {
        loadItems();
    }, [pageNumber, search]);

    /*
     * Search
     */

    const handleSearchChange = (event) => {

        setSearch(
            event.target.value
        );

        setPageNumber(1);
    };

    /*
     * Create
     */

    const handleCreate = () => {

        setEditingItem(null);

        setError("");

        setShowForm(true);
    };

    /*
     * View
     */

    const handleView = async (item) => {

        try {

            setError("");

            const response =
                await getServiceCategoryById(
                    item.serviceCategoryId
                );

            const category =
                extractData(response);

            setSelectedItem(category);

            setShowDetails(true);

        } catch (err) {

            console.error(
                "Failed to load service category:",
                err
            );

            setError(
                getErrorMessage(
                    err,
                    "Failed to load service category."
                )
            );
        }
    };

    /*
     * Edit
     */

    const handleEdit = async (item) => {

        try {

            setError("");

            const response =
                await getServiceCategoryById(
                    item.serviceCategoryId
                );

            const category =
                extractData(response);

            setEditingItem(category);

            setShowForm(true);

        } catch (err) {

            console.error(
                "Failed to load service category:",
                err
            );

            setError(
                getErrorMessage(
                    err,
                    "Failed to load service category."
                )
            );
        }
    };

    /*
     * Create / Update
     */

    const handleSubmit = async (
        formData
    ) => {

        try {

            setSaving(true);

            setError("");

            if (editingItem) {

                await updateServiceCategory(
                    formData
                );

            } else {

                await createServiceCategory(
                    formData
                );
            }

            /*
             * Close form
             */

            setShowForm(false);

            setEditingItem(null);

            /*
             * Refresh table
             */

            await loadItems();

        } catch (err) {

            console.error(
                "Failed to save service category:",
                err
            );

            setError(
                getErrorMessage(
                    err,
                    "Failed to save service category."
                )
            );

        } finally {

            setSaving(false);
        }
    };

    /*
     * Archive
     */

    const handleArchive = async (
        item
    ) => {

        const confirmed =
            window.confirm(
                `Are you sure you want to archive "${item.serviceCategoryName}"?`
            );

        if (!confirmed) {
            return;
        }

        try {

            setLoading(true);

            setError("");

            await archiveServiceCategory(
                item.serviceCategoryId
            );

            await loadItems();

        } catch (err) {

            console.error(
                "Failed to archive service category:",
                err
            );

            setError(
                getErrorMessage(
                    err,
                    "Failed to archive service category."
                )
            );

            setLoading(false);
        }
    };

    /*
     * Close form
     */

    const closeForm = () => {

        if (saving) {
            return;
        }

        setShowForm(false);

        setEditingItem(null);

        setError("");
    };

    /*
     * Close details
     */

    const closeDetails = () => {

        setShowDetails(false);

        setSelectedItem(null);
    };

    /*
     * Pagination
     */

    const goToPreviousPage = () => {

        if (pageNumber > 1) {

            setPageNumber(
                pageNumber - 1
            );
        }
    };

    const goToNextPage = () => {

        if (
            pageNumber < totalPages
        ) {

            setPageNumber(
                pageNumber + 1
            );
        }
    };

    /*
     * Render
     */

    return (
        <div className="service-category-page">

            {/* Header */}

            <div className="service-category-header">

                <div>

                    <h1>
                        Service Categories
                    </h1>

                    <p>
                        Manage your service categories.
                    </p>

                </div>

                <button
                    className="service-category-button primary"
                    onClick={handleCreate}
                >
                    + Add Service Category
                </button>

            </div>

            {/* Error */}

            {error && !showForm && (
                <div className="service-category-error">
                    {error}
                </div>
            )}

            {/* Toolbar */}

            <div className="service-category-toolbar">

                <div className="service-category-search">

                    <input
                        type="text"
                        value={search}
                        onChange={
                            handleSearchChange
                        }
                        placeholder="Search service categories..."
                    />

                    {search && (
                        <button
                            type="button"
                            onClick={() => {
                                setSearch("");
                                setPageNumber(1);
                            }}
                        >
                            ×
                        </button>
                    )}

                </div>

                <div className="service-category-record-count">
                    Total: {totalRecords}
                </div>

            </div>

            {/* Table */}

            <div className="service-category-table-container">

                {loading ? (

                    <div className="service-category-loading">
                        Loading service categories...
                    </div>

                ) : items.length === 0 ? (

                    <div className="service-category-empty">

                        <h3>
                            No service categories found
                        </h3>

                        <p>
                            Create a service category to get started.
                        </p>

                        <button
                            className="service-category-button primary"
                            onClick={handleCreate}
                        >
                            Add Service Category
                        </button>

                    </div>

                ) : (

                    <table className="service-category-table">

                        <thead>

                            <tr>

                                <th>
                                    ID
                                </th>

                                <th>
                                    Name
                                </th>

                                <th>
                                    Description
                                </th>

                                <th>
                                    Color
                                </th>

                                <th>
                                    Branch Permission
                                </th>

                                <th>
                                    Sort Index
                                </th>

                                <th>
                                    Status
                                </th>

                                <th>
                                    Actions
                                </th>

                            </tr>

                        </thead>

                        <tbody>

                            {items.map(
                                (item) => (

                                    <tr
                                        key={
                                            item.serviceCategoryId
                                        }
                                    >

                                        <td>
                                            {
                                                item.serviceCategoryId
                                            }
                                        </td>

                                        <td>

                                            <strong>
                                                {
                                                    item.serviceCategoryName ||
                                                    "-"
                                                }
                                            </strong>

                                        </td>

                                        <td className="description-cell">

                                            {
                                                item.description ||
                                                "-"
                                            }

                                        </td>

                                        <td>

                                            {item.color ? (

                                                <div className="service-category-color">

                                                    <span
                                                        className="service-category-color-preview"
                                                        style={{
                                                            backgroundColor:
                                                                item.color,
                                                        }}
                                                    />

                                                    {
                                                        item.color
                                                    }

                                                </div>

                                            ) : (
                                                "-"
                                            )}

                                        </td>

                                        <td>

                                            {item.hasBranchPermission
                                                ? "Yes"
                                                : "No"}

                                        </td>

                                        <td>

                                            {
                                                item.sortIndex ??
                                                "-"
                                            }

                                        </td>

                                        <td>

                                            <span
                                                className={`service-category-status ${
                                                    item.isArchived
                                                        ? "archived"
                                                        : "active"
                                                }`}
                                            >
                                                {item.isArchived
                                                    ? "Archived"
                                                    : "Active"}
                                            </span>

                                        </td>

                                        <td>

                                            <div className="service-category-actions">

                                                <button
                                                    className="action-button view"
                                                    onClick={() =>
                                                        handleView(
                                                            item
                                                        )
                                                    }
                                                >
                                                    View
                                                </button>

                                                <button
                                                    className="action-button edit"
                                                    onClick={() =>
                                                        handleEdit(
                                                            item
                                                        )
                                                    }
                                                    disabled={
                                                        item.isArchived
                                                    }
                                                >
                                                    Edit
                                                </button>

                                                <button
                                                    className="action-button archive"
                                                    onClick={() =>
                                                        handleArchive(
                                                            item
                                                        )
                                                    }
                                                    disabled={
                                                        item.isArchived
                                                    }
                                                >
                                                    Archive
                                                </button>

                                            </div>

                                        </td>

                                    </tr>

                                )
                            )}

                        </tbody>

                    </table>

                )}

            </div>

            {/* Pagination */}

            {totalPages > 0 && (

                <div className="service-category-pagination">

                    <button
                        onClick={
                            goToPreviousPage
                        }
                        disabled={
                            pageNumber <= 1 ||
                            loading
                        }
                    >
                        Previous
                    </button>

                    <span>
                        Page {pageNumber} of{" "}
                        {totalPages}
                    </span>

                    <button
                        onClick={
                            goToNextPage
                        }
                        disabled={
                            pageNumber >= totalPages ||
                            loading
                        }
                    >
                        Next
                    </button>

                </div>

            )}

            {/* Create / Edit Modal */}

            {showForm && (

                <ServiceCategoryForm
                    editingItem={
                        editingItem
                    }
                    onSubmit={
                        handleSubmit
                    }
                    onCancel={
                        closeForm
                    }
                    saving={
                        saving
                    }
                    error={
                        error
                    }
                />

            )}

            {/* Details Modal */}

            {showDetails && (

                <ServiceCategoryDetails
                    category={
                        selectedItem
                    }
                    onClose={
                        closeDetails
                    }
                />

            )}

        </div>
    );
};

export default ServiceCategoryPage;