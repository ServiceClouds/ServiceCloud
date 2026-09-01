import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";

import {
    createProductCategory,
    getProductCategoryById,
    updateProductCategory
} from "../../api/product/productCategoryApi";

import "./productCategory.css";

const initialForm = {
    productCategoryId: "",
    productCategoryName: "",
    description: "",
    imagePath: "",
    hasBranchPermission: false,
    appSourceTypeId: ""
};

function ProductCategoryForm() {

    const navigate = useNavigate();

    const { id } = useParams();

    const isEditMode = Boolean(id);

    const [form, setForm] = useState(initialForm);

    const [loading, setLoading] = useState(false);

    const [saving, setSaving] = useState(false);

    const [error, setError] = useState("");

    // ============================================================
    // LOAD CATEGORY
    // ============================================================

    useEffect(() => {

        if (!isEditMode) {
            return;
        }

        const loadCategory = async () => {

            try {

                setLoading(true);
                setError("");

                const response =
                    await getProductCategoryById(id);

                const category =
                    response?.messageData ??
                    response?.data ??
                    response;

                setForm({
                    productCategoryId:
                        category?.productCategoryId ?? id,

                    productCategoryName:
                        category?.productCategoryName ?? "",

                    description:
                        category?.description ?? "",

                    imagePath:
                        category?.imagePath ?? "",

                    hasBranchPermission:
                        category?.hasBranchPermission ?? false,

                    appSourceTypeId:
                        category?.appSourceTypeId ?? ""
                });

            }
            catch (err) {

                console.error(err);

                setError(
                    err?.response?.data?.message ||
                    "Unable to load product category."
                );

            }
            finally {

                setLoading(false);

            }
        };

        loadCategory();

    }, [id, isEditMode]);

    // ============================================================
    // CHANGE HANDLER
    // ============================================================

    const handleChange = (event) => {

        const {
            name,
            value,
            type,
            checked
        } = event.target;

        setForm((previous) => ({
            ...previous,

            [name]:
                type === "checkbox"
                    ? checked
                    : value
        }));
    };

    // ============================================================
    // SUBMIT
    // ============================================================

    const handleSubmit = async (event) => {

        event.preventDefault();

        try {

            setSaving(true);
            setError("");

            const payload = {
                productCategoryId:
                    Number(form.productCategoryId),

                productCategoryName:
                    form.productCategoryName || null,

                description:
                    form.description || null,

                imagePath:
                    form.imagePath || null,

                hasBranchPermission:
                    form.hasBranchPermission,

                appSourceTypeId:
                    form.appSourceTypeId
                        ? Number(form.appSourceTypeId)
                        : null
            };

            if (isEditMode) {

                await updateProductCategory(
                    Number(id),
                    {
                        productCategoryId: Number(id),
                        productCategoryName:
                            payload.productCategoryName,
                        description:
                            payload.description,
                        imagePath:
                            payload.imagePath,
                        hasBranchPermission:
                            payload.hasBranchPermission,
                        appSourceTypeId:
                            payload.appSourceTypeId
                    }
                );

            }
            else {

                await createProductCategory(payload);

            }

            navigate("/dashboard/product-categories");

        }
        catch (err) {

            console.error(err);

            setError(
                err?.response?.data?.message ||
                err?.response?.data?.messageData ||
                "Unable to save product category."
            );

        }
        finally {

            setSaving(false);

        }
    };

    if (loading) {

        return (
            <div className="product-category-form-page">
                <p>Loading product category...</p>
            </div>
        );

    }

    return (

        <div className="product-category-form-page">

            {/* ================================================== */}
            {/* HEADER */}
            {/* ================================================== */}

            <div className="product-category-page-header">

                <div>

                    <h1>
                        {isEditMode
                            ? "Edit Product Category"
                            : "Create Product Category"}
                    </h1>

                    <p>
                        {isEditMode
                            ? "Update product category information."
                            : "Add a new product category."}
                    </p>

                </div>

                <button
                    type="button"
                    className="btn-secondary"
                    onClick={() =>
                        navigate("/dashboard/product-categories")
                    }
                >
                    ← Back
                </button>

            </div>

            {/* ================================================== */}
            {/* ERROR */}
            {/* ================================================== */}

            {error && (
                <div className="product-category-error">
                    {error}
                </div>
            )}

            {/* ================================================== */}
            {/* FORM */}
            {/* ================================================== */}

            <form
                className="product-category-form-card"
                onSubmit={handleSubmit}
            >

                <div className="form-section">

                    <h2>Basic Information</h2>

                    <div className="form-grid">

                        <div className="form-group">

                            <label>
                                Category ID
                                <span>*</span>
                            </label>

                            <input
                                type="number"
                                name="productCategoryId"
                                value={form.productCategoryId}
                                onChange={handleChange}
                                required
                                min="1"
                                disabled={isEditMode}
                                placeholder="Enter category ID"
                            />

                        </div>

                        <div className="form-group">

                            <label>
                                Category Name
                            </label>

                            <input
                                type="text"
                                name="productCategoryName"
                                value={form.productCategoryName}
                                onChange={handleChange}
                                maxLength={100}
                                placeholder="Enter category name"
                            />

                        </div>

                    </div>

                    <div className="form-group">

                        <label>
                            Description
                        </label>

                        <textarea
                            name="description"
                            value={form.description}
                            onChange={handleChange}
                            maxLength={500}
                            rows={5}
                            placeholder="Enter category description"
                        />

                    </div>

                </div>

                {/* ================================================== */}
                {/* IMAGE */}
                {/* ================================================== */}

                <div className="form-section">

                    <h2>Category Image</h2>

                    <div className="form-group">

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
                        />

                        <small>
                            Image upload can be added separately later.
                        </small>

                    </div>

                </div>

                {/* ================================================== */}
                {/* SETTINGS */}
                {/* ================================================== */}

                <div className="form-section">

                    <h2>Category Settings</h2>

                    <div className="checkbox-grid">

                        <label className="checkbox-option">

                            <input
                                type="checkbox"
                                name="hasBranchPermission"
                                checked={
                                    form.hasBranchPermission
                                }
                                onChange={handleChange}
                            />

                            <span>
                                Enable branch permission
                            </span>

                        </label>

                    </div>

                </div>

                {/* ================================================== */}
                {/* ADDITIONAL */}
                {/* ================================================== */}

                <div className="form-section">

                    <h2>Additional Information</h2>

                    <div className="form-grid">

                        <div className="form-group">

                            <label>
                                App Source Type
                            </label>

                            <input
                                type="number"
                                name="appSourceTypeId"
                                value={form.appSourceTypeId}
                                onChange={handleChange}
                                placeholder="Optional"
                            />

                        </div>

                    </div>

                </div>

                {/* ================================================== */}
                {/* ACTIONS */}
                {/* ================================================== */}

                <div className="form-actions">

                    <button
                        type="button"
                        className="btn-secondary"
                        onClick={() =>
                            navigate("/dashboard/product-categories")
                        }
                        disabled={saving}
                    >
                        Cancel
                    </button>

                    <button
                        type="submit"
                        className="btn-primary"
                        disabled={saving}
                    >
                        {saving
                            ? "Saving..."
                            : isEditMode
                                ? "Update Category"
                                : "Create Category"}
                    </button>

                </div>

            </form>

        </div>

    );
}

export default ProductCategoryForm;