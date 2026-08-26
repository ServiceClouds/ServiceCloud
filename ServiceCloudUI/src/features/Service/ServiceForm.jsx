import React, { useEffect, useState } from "react";
import { getServiceCategories } from "../../api/serviceCategory/serviceCategoryApi";

const initialForm = {
    serviceCategoryId: "",
    serviceName: "",
    description: "",
    specialInstruction: "",
    hasBranchPermission: false,
    allowBranchEditPrice: false,
    appSourceTypeId: ""
};

const ServiceForm = ({
    editingService,
    onSubmit,
    onCancel,
    saving
}) => {
    const [form, setForm] = useState(initialForm);

    const [categories, setCategories] = useState([]);
    const [loadingCategories, setLoadingCategories] =
        useState(false);

    const [formError, setFormError] = useState("");

    useEffect(() => {
        if (editingService) {
            setForm({
                serviceCategoryId:
                    editingService.serviceCategoryId ?? "",

                serviceName:
                    editingService.serviceName ?? "",

                description:
                    editingService.description ?? "",

                specialInstruction:
                    editingService.specialInstruction ?? "",

                hasBranchPermission:
                    editingService.hasBranchPermission ?? false,

                allowBranchEditPrice:
                    editingService.allowBranchEditPrice ?? false,

                /*
                 * GetById currently does not return AppSourceTypeId.
                 * Therefore this will be empty when editing.
                 */
                appSourceTypeId:
                    editingService.appSourceTypeId ?? ""
            });
        } else {
            setForm(initialForm);
        }
    }, [editingService]);

    useEffect(() => {
        const loadCategories = async () => {
            try {
                setLoadingCategories(true);

                /*
                 * Use the existing ServiceCategory API.
                 * If your API requires pagination, request a sufficiently
                 * large page here or use its lookup endpoint.
                 */
                const response = await getServiceCategories({
                    pageNumber: 1,
                    pageSize: 1000,
                    search: ""
                });

                const data = response?.data ?? response;

                const items =
                    data?.items ??
                    data?.Items ??
                    [];

                setCategories(items);
            } catch (error) {
                console.error(
                    "Failed to load service categories:",
                    error
                );

                setFormError(
                    "Unable to load service categories."
                );
            } finally {
                setLoadingCategories(false);
            }
        };

        loadCategories();
    }, []);

    const handleChange = (e) => {
        const { name, value, type, checked } = e.target;

        setForm((previous) => ({
            ...previous,
            [name]:
                type === "checkbox"
                    ? checked
                    : value
        }));

        setFormError("");
    };

    const handleSubmit = (e) => {
        e.preventDefault();

        if (!form.serviceCategoryId) {
            setFormError(
                "Service category is required."
            );
            return;
        }

        if (!form.serviceName.trim()) {
            setFormError(
                "Service name is required."
            );
            return;
        }

        /*
         * AppSourceTypeId is required by CreateServiceCommand.
         */
        if (!editingService && !form.appSourceTypeId) {
            setFormError(
                "App Source Type is required."
            );
            return;
        }

        onSubmit(form);
    };

    return (
        <form
            className="service-form"
            onSubmit={handleSubmit}
        >

            {formError && (
                <div className="service-alert service-alert-error">
                    {formError}
                </div>
            )}

            <div className="service-form-grid">

                {/* Service Category */}
                <div className="service-field">
                    <label htmlFor="serviceCategoryId">
                        Service Category
                        <span>*</span>
                    </label>

                    <select
                        id="serviceCategoryId"
                        name="serviceCategoryId"
                        value={form.serviceCategoryId}
                        onChange={handleChange}
                        disabled={
                            saving ||
                            loadingCategories
                        }
                    >
                        <option value="">
                            {loadingCategories
                                ? "Loading categories..."
                                : "Select category"}
                        </option>

                        {categories.map((category) => (
                            <option
                                key={category.serviceCategoryId}
                                value={
                                    category.serviceCategoryId
                                }
                            >
                                {
                                    category.serviceCategoryName
                                }
                            </option>
                        ))}
                    </select>
                </div>

                {/* Service Name */}
                <div className="service-field">
                    <label htmlFor="serviceName">
                        Service Name
                        <span>*</span>
                    </label>

                    <input
                        id="serviceName"
                        name="serviceName"
                        type="text"
                        maxLength="100"
                        value={form.serviceName}
                        onChange={handleChange}
                        placeholder="Enter service name"
                        disabled={saving}
                    />
                </div>

                {/* Description */}
                <div className="service-field service-field-full">
                    <label htmlFor="description">
                        Description
                    </label>

                    <textarea
                        id="description"
                        name="description"
                        maxLength="100"
                        rows="3"
                        value={form.description}
                        onChange={handleChange}
                        placeholder="Enter service description"
                        disabled={saving}
                    />
                </div>

                {/* Special Instruction */}
                <div className="service-field service-field-full">
                    <label htmlFor="specialInstruction">
                        Special Instruction
                    </label>

                    <textarea
                        id="specialInstruction"
                        name="specialInstruction"
                        maxLength="1000"
                        rows="4"
                        value={form.specialInstruction}
                        onChange={handleChange}
                        placeholder="Enter special instructions"
                        disabled={saving}
                    />
                </div>

                {/* App Source Type */}
                <div className="service-field">
                    <label htmlFor="appSourceTypeId">
                        App Source Type
                        {!editingService && <span>*</span>}
                    </label>

                    <input
                        id="appSourceTypeId"
                        name="appSourceTypeId"
                        type="number"
                        min="1"
                        value={form.appSourceTypeId}
                        onChange={handleChange}
                        placeholder="Enter App Source Type ID"
                        disabled={saving}
                    />

                    {editingService && (
                        <small>
                            AppSourceTypeId is not returned by
                            the current GetById API.
                        </small>
                    )}
                </div>

                {/* Branch Permission */}
                <div className="service-checkbox-field">

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

                {/* Branch Edit Price */}
                <div className="service-checkbox-field">

                    <label>
                        <input
                            type="checkbox"
                            name="allowBranchEditPrice"
                            checked={
                                form.allowBranchEditPrice
                            }
                            onChange={handleChange}
                            disabled={saving}
                        />

                        <span>
                            Allow Branch Edit Price
                        </span>
                    </label>

                </div>

            </div>

            {/* Buttons */}
            <div className="service-form-actions">

                <button
                    type="button"
                    className="service-secondary-btn"
                    onClick={onCancel}
                    disabled={saving}
                >
                    Cancel
                </button>

                <button
                    type="submit"
                    className="service-primary-btn"
                    disabled={saving}
                >
                    {saving
                        ? "Saving..."
                        : editingService
                            ? "Update Service"
                            : "Create Service"}
                </button>

            </div>

        </form>
    );
};

export default ServiceForm;