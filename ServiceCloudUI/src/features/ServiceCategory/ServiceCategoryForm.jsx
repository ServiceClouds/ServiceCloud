import React, { useEffect, useState } from "react";

const initialForm = {
    serviceCategoryName: "",
    description: "",
    imagePath: "",
    hasBranchPermission: false,
    appSourceTypeId: 1,
    color: "",
    sortIndex: "",
};

const ServiceCategoryForm = ({
    editingItem,
    onSubmit,
    onCancel,
    saving,
    error,
}) => {
    const [form, setForm] = useState(initialForm);
    const [validationError, setValidationError] = useState("");

    useEffect(() => {
        if (editingItem) {
            setForm({
                serviceCategoryName:
                    editingItem.serviceCategoryName ?? "",

                description:
                    editingItem.description ?? "",

                imagePath:
                    editingItem.imagePath ?? "",

                hasBranchPermission:
                    editingItem.hasBranchPermission ?? false,

                appSourceTypeId:
                    editingItem.appSourceTypeId ?? 1,

                color:
                    editingItem.color ?? "",

                sortIndex:
                    editingItem.sortIndex ?? "",
            });
        } else {
            setForm(initialForm);
        }

        setValidationError("");
    }, [editingItem]);

    const handleChange = (event) => {
        const {
            name,
            value,
            type,
            checked,
        } = event.target;

        setForm((previous) => ({
            ...previous,
            [name]:
                type === "checkbox"
                    ? checked
                    : value,
        }));
    };

    const handleSubmit = async (event) => {
        event.preventDefault();

        setValidationError("");

        /*
         * Frontend validation
         */

        if (!form.serviceCategoryName.trim()) {
            setValidationError(
                "Service category name is required."
            );
            return;
        }

        if (
            form.serviceCategoryName.trim().length > 100
        ) {
            setValidationError(
                "Service category name cannot exceed 100 characters."
            );
            return;
        }

        if (form.description.length > 500) {
            setValidationError(
                "Description cannot exceed 500 characters."
            );
            return;
        }

        if (form.imagePath.length > 80) {
            setValidationError(
                "Image path cannot exceed 80 characters."
            );
            return;
        }

        if (
            !form.appSourceTypeId ||
            Number(form.appSourceTypeId) <= 0
        ) {
            setValidationError(
                "App Source Type ID must be greater than 0."
            );
            return;
        }

        if (form.color.length > 50) {
            setValidationError(
                "Color cannot exceed 50 characters."
            );
            return;
        }

        /*
         * Prepare API payload
         */

        const payload = {
            serviceCategoryName:
                form.serviceCategoryName.trim(),

            description:
                form.description.trim() || null,

            imagePath:
                form.imagePath.trim() || null,

            hasBranchPermission:
                Boolean(form.hasBranchPermission),

            appSourceTypeId:
                Number(form.appSourceTypeId),

            color:
                form.color.trim() || null,

            sortIndex:
                form.sortIndex === ""
                    ? null
                    : Number(form.sortIndex),
        };

        /*
         * Edit
         */

        if (editingItem) {
            await onSubmit({
                serviceCategoryId:
                    editingItem.serviceCategoryId,

                ...payload,
            });

            return;
        }

        /*
         * Create
         */

        await onSubmit(payload);
    };

    return (
        <div className="service-category-modal-overlay">

            <div className="service-category-modal">

                {/* Header */}

                <div className="service-category-modal-header">

                    <div>
                        <h2>
                            {editingItem
                                ? "Edit Service Category"
                                : "Create Service Category"}
                        </h2>

                        <p>
                            {editingItem
                                ? "Update service category information."
                                : "Create a new service category."}
                        </p>
                    </div>

                    <button
                        type="button"
                        className="service-category-close-button"
                        onClick={onCancel}
                        disabled={saving}
                    >
                        ×
                    </button>

                </div>

                {/* Error */}

                {(validationError || error) && (
                    <div className="service-category-error">
                        {validationError || error}
                    </div>
                )}

                {/* Form */}

                <form onSubmit={handleSubmit}>

                    <div className="service-category-form-grid">

                        {/* Name */}

                        <div className="service-category-field full-width">

                            <label>
                                Service Category Name
                                <span>*</span>
                            </label>

                            <input
                                type="text"
                                name="serviceCategoryName"
                                value={
                                    form.serviceCategoryName
                                }
                                onChange={handleChange}
                                maxLength={100}
                                placeholder="Enter service category name"
                                disabled={saving}
                            />

                        </div>

                        {/* Description */}

                        <div className="service-category-field full-width">

                            <label>
                                Description
                            </label>

                            <textarea
                                name="description"
                                value={form.description}
                                onChange={handleChange}
                                maxLength={500}
                                rows={4}
                                placeholder="Enter description"
                                disabled={saving}
                            />

                        </div>

                        {/* Image Path */}

                        <div className="service-category-field">

                            <label>
                                Image Path
                            </label>

                            <input
                                type="text"
                                name="imagePath"
                                value={form.imagePath}
                                onChange={handleChange}
                                maxLength={80}
                                placeholder="Enter image path"
                                disabled={saving}
                            />

                        </div>

                        {/* App Source Type */}

                        <div className="service-category-field">

                            <label>
                                App Source Type ID
                            </label>

                            <input
                                type="number"
                                name="appSourceTypeId"
                                value={
                                    form.appSourceTypeId
                                }
                                onChange={handleChange}
                                min="1"
                                disabled={saving}
                            />

                        </div>

                        {/* Color */}

                        <div className="service-category-field">

                            <label>
                                Color
                            </label>

                            <input
                                type="text"
                                name="color"
                                value={form.color}
                                onChange={handleChange}
                                maxLength={50}
                                placeholder="e.g. #FF5733"
                                disabled={saving}
                            />

                        </div>

                        {/* Sort Index */}

                        <div className="service-category-field">

                            <label>
                                Sort Index
                            </label>

                            <input
                                type="number"
                                name="sortIndex"
                                value={form.sortIndex}
                                onChange={handleChange}
                                placeholder="Optional"
                                disabled={saving}
                            />

                        </div>

                        {/* Branch Permission */}

                        <div className="service-category-checkbox-field full-width">

                            <label>

                                <input
                                    type="checkbox"
                                    name="hasBranchPermission"
                                    checked={
                                        form.hasBranchPermission
                                    }
                                    onChange={handleChange}
                                    disabled={saving}
                                />

                                <span>
                                    Has Branch Permission
                                </span>

                            </label>

                        </div>

                    </div>

                    {/* Buttons */}

                    <div className="service-category-modal-actions">

                        <button
                            type="button"
                            className="service-category-button secondary"
                            onClick={onCancel}
                            disabled={saving}
                        >
                            Cancel
                        </button>

                        <button
                            type="submit"
                            className="service-category-button primary"
                            disabled={saving}
                        >
                            {saving
                                ? "Saving..."
                                : editingItem
                                    ? "Update Category"
                                    : "Create Category"}
                        </button>

                    </div>

                </form>

            </div>

        </div>
    );
};

export default ServiceCategoryForm;