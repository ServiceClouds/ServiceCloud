import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import {
    createServiceCategoryBranch,
    getServiceCategoryBranchById,
    updateServiceCategoryBranch
} from "../../api/serviceCategoryBranch/serviceCategoryBranchApi";
import "./serviceCategoryBranch.css";

const initialForm = {
    serviceCategoryId: "",
    branchId: "",
    isActive: true,
    isIncluded: true
};

const ServiceCategoryBranchForm = () => {
    const { id } = useParams();
    const navigate = useNavigate();

    const isEdit = Boolean(id);

    const [form, setForm] = useState(initialForm);
    const [loading, setLoading] = useState(isEdit);
    const [saving, setSaving] = useState(false);
    const [error, setError] = useState("");

    useEffect(() => {
        if (!isEdit) return;

        const loadBranch = async () => {
            try {
                setLoading(true);
                setError("");

                const response =
                    await getServiceCategoryBranchById(id);

                const data = response.messageData;

                setForm({
                    serviceCategoryId:
                        data.serviceCategoryId,
                    branchId: data.branchId,
                    isActive: data.isActive ?? false,
                    isIncluded: data.isIncluded ?? false
                });
            } catch (error) {
                setError(
                    error.response?.data?.message ||
                    "Failed to load service category branch."
                );
            } finally {
                setLoading(false);
            }
        };

        loadBranch();
    }, [id, isEdit]);

    const handleChange = (e) => {
        const { name, value, type, checked } = e.target;

        setForm((previous) => ({
            ...previous,
            [name]:
                type === "checkbox"
                    ? checked
                    : value
        }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        try {
            setSaving(true);
            setError("");

            if (isEdit) {
                const data = {
                    serviceCategoryBranchId: Number(id),
                    isActive: form.isActive,
                    isIncluded: form.isIncluded
                };

                await updateServiceCategoryBranch(
                    id,
                    data
                );
            } else {
                const data = {
                    serviceCategoryId:
                        Number(form.serviceCategoryId),
                    branchId: Number(form.branchId),
                    isIncluded: form.isIncluded
                };

                await createServiceCategoryBranch(data);
            }

            navigate("/service-category-branches");
        } catch (error) {
            setError(
                error.response?.data?.message ||
                "Failed to save service category branch."
            );
        } finally {
            setSaving(false);
        }
    };

    if (loading) {
        return (
            <div className="loading">
                Loading...
            </div>
        );
    }

    return (
        <div className="service-category-branch-page">
            <div className="page-header">
                <div>
                    <h2>
                        {isEdit
                            ? "Edit Service Category Branch"
                            : "Add Service Category Branch"}
                    </h2>

                    <p>
                        {isEdit
                            ? "Update service category branch."
                            : "Create a service category branch assignment."}
                    </p>
                </div>
            </div>

            {error && (
                <div className="error-message">
                    {error}
                </div>
            )}

            <form
                className="form-card"
                onSubmit={handleSubmit}
            >
                <div className="form-group">
                    <label>
                        Service Category ID
                    </label>

                    <input
                        type="number"
                        name="serviceCategoryId"
                        value={form.serviceCategoryId}
                        onChange={handleChange}
                        disabled={isEdit}
                        required
                    />
                </div>

                <div className="form-group">
                    <label>Branch ID</label>

                    <input
                        type="number"
                        name="branchId"
                        value={form.branchId}
                        onChange={handleChange}
                        disabled={isEdit}
                        required
                    />
                </div>

                {isEdit && (
                    <div className="checkbox-group">
                        <label>
                            <input
                                type="checkbox"
                                name="isActive"
                                checked={form.isActive}
                                onChange={handleChange}
                            />
                            Active
                        </label>
                    </div>
                )}

                <div className="checkbox-group">
                    <label>
                        <input
                            type="checkbox"
                            name="isIncluded"
                            checked={form.isIncluded}
                            onChange={handleChange}
                        />
                        Included
                    </label>
                </div>

                <div className="form-actions">
                    <button
                        type="button"
                        className="btn btn-secondary"
                        onClick={() =>
                            navigate(
                                "/service-category-branches"
                            )
                        }
                    >
                        Cancel
                    </button>

                    <button
                        type="submit"
                        className="btn btn-primary"
                        disabled={saving}
                    >
                        {saving
                            ? "Saving..."
                            : isEdit
                            ? "Update"
                            : "Create"}
                    </button>
                </div>
            </form>
        </div>
    );
};

export default ServiceCategoryBranchForm;