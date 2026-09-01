import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";

import {
    createProduct,
    getProductById,
    updateProduct
} from "../../api/product/productApi";

import "./product.css";

const initialForm = {
    productId: "",
    productCategoryId: "",
    productName: "",
    description: "",
    isActive: true,
    allowBranchTrackInventory: false,
    hasBranchPermission: false,
    allowBranchEditPrice: false,
    productClassificationId: "",
    brandId: "",
    appSourceTypeId: ""
};

function ProductForm() {

    const navigate = useNavigate();

    const { id } = useParams();

    const isEditMode = Boolean(id);

    const [form, setForm] = useState(initialForm);

    const [loading, setLoading] = useState(false);

    const [saving, setSaving] = useState(false);

    const [error, setError] = useState("");

    // ============================================================
    // LOAD PRODUCT
    // ============================================================

    useEffect(() => {

        if (!isEditMode) {
            return;
        }

        const loadProduct = async () => {

            try {

                setLoading(true);

                const response =
                    await getProductById(id);

                const product =
                    response?.messageData ??
                    response?.data ??
                    response;

                setForm({
                    productCategoryId:
                        product?.productCategoryId ?? "",

                    productName:
                        product?.productName ?? "",

                    description:
                        product?.description ?? "",

                    isActive:
                        product?.isActive ?? true,

                    allowBranchTrackInventory:
                        product?.allowBranchTrackInventory ?? false,

                    hasBranchPermission:
                        product?.hasBranchPermission ?? false,

                    allowBranchEditPrice:
                        product?.allowBranchEditPrice ?? false,

                    productClassificationId:
                        product?.productClassificationId ?? "",

                    brandId:
                        product?.brandId ?? "",

                    appSourceTypeId:
                        product?.appSourceTypeId ?? ""
                });

            }
            catch (err) {

                console.error(err);

                setError(
                    err?.response?.data?.message ||
                    "Unable to load product."
                );

            }
            finally {

                setLoading(false);

            }
        };

        loadProduct();

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
                 productId: Number(form.productId),
                productCategoryId:
                    Number(form.productCategoryId),

                productName:
                    form.productName || null,

                description:
                    form.description || null,

                isActive:
                    form.isActive,

                allowBranchTrackInventory:
                    form.allowBranchTrackInventory,

                hasBranchPermission:
                    form.hasBranchPermission,

                allowBranchEditPrice:
                    form.allowBranchEditPrice,

                productClassificationId:
                    form.productClassificationId
                        ? Number(form.productClassificationId)
                        : null,

                brandId:
                    form.brandId
                        ? Number(form.brandId)
                        : null,

                appSourceTypeId:
                    form.appSourceTypeId
                        ? Number(form.appSourceTypeId)
                        : null
            };

            if (isEditMode) {

                await updateProduct(
                    Number(id),
                    {
                        productId: Number(id),
                        ...payload
                    }
                );

            }
            else {

                await createProduct(
                    payload
                );

            }

            navigate("/dashboard/products");

        }
        catch (err) {

            console.error(err);

            setError(
                err?.response?.data?.message ||
                err?.response?.data?.messageData ||
                "Unable to save product."
            );

        }
        finally {

            setSaving(false);

        }
    };

    if (loading) {

        return (
            <div className="product-form-page">
                <p>Loading product...</p>
            </div>
        );

    }

    return (

        <div className="product-form-page">

            {/* ================================================== */}
            {/* HEADER */}
            {/* ================================================== */}

            <div className="product-page-header">

                <div>

                    <h1>
                        {isEditMode
                            ? "Edit Product"
                            : "Create Product"}
                    </h1>

                    <p>
                        {isEditMode
                            ? "Update product information."
                            : "Add a new product to your catalog."}
                    </p>

                </div>

                <button
                    type="button"
                    className="btn-secondary"
                    onClick={() =>
                        navigate("/dashboard/products")
                    }
                >
                    ← Back
                </button>

            </div>

            {/* ================================================== */}
            {/* ERROR */}
            {/* ================================================== */}

            {error && (

                <div className="product-error">
                    {error}
                </div>

            )}

            {/* ================================================== */}
            {/* FORM */}
            {/* ================================================== */}

            <form
                className="product-form-card"
                onSubmit={handleSubmit}
            >

                <div className="form-section">

                    <h2>Basic Information</h2>

                    <div className="form-grid">

                        <div className="form-group">

                            <label>
                                Product Category
                                <span>*</span>
                            </label>

                            <input
                                type="number"
                                name="productCategoryId"
                                value={form.productCategoryId}
                                onChange={handleChange}
                                required
                                placeholder="Category ID"
                            />

                        </div>

                        <div className="form-group">

                            <label>
                                Product Name
                            </label>
                            <div className="form-group">

    <label>
        Product ID
        <span>*</span>
    </label>

    <input
        type="number"
        name="productId"
        value={form.productId}
        onChange={handleChange}
        required
        min="1"
        placeholder="Enter Product ID"
    />

</div>

                            <input
                                type="text"
                                name="productName"
                                value={form.productName}
                                onChange={handleChange}
                                maxLength={100}
                                placeholder="Enter product name"
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
                            maxLength={1500}
                            rows={5}
                            placeholder="Enter product description"
                        />

                    </div>

                </div>

                <div className="form-section">

                    <h2>Product Settings</h2>

                    <div className="checkbox-grid">

                        <label className="checkbox-option">

                            <input
                                type="checkbox"
                                name="isActive"
                                checked={form.isActive}
                                onChange={handleChange}
                            />

                            <span>
                                Product is active
                            </span>

                        </label>

                        <label className="checkbox-option">

                            <input
                                type="checkbox"
                                name="allowBranchTrackInventory"
                                checked={
                                    form.allowBranchTrackInventory
                                }
                                onChange={handleChange}
                            />

                            <span>
                                Track inventory by branch
                            </span>

                        </label>

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

                        <label className="checkbox-option">

                            <input
                                type="checkbox"
                                name="allowBranchEditPrice"
                                checked={
                                    form.allowBranchEditPrice
                                }
                                onChange={handleChange}
                            />

                            <span>
                                Allow branch price editing
                            </span>

                        </label>

                    </div>

                </div>

                <div className="form-section">

                    <h2>Additional Information</h2>

                    <div className="form-grid">

                        <div className="form-group">

                            <label>
                                Product Classification
                            </label>

                            <input
                                type="number"
                                name="productClassificationId"
                                value={
                                    form.productClassificationId
                                }
                                onChange={handleChange}
                                placeholder="Optional"
                            />

                        </div>

                        <div className="form-group">

                            <label>
                                Brand
                            </label>

                            <input
                                type="number"
                                name="brandId"
                                value={form.brandId}
                                onChange={handleChange}
                                placeholder="Optional"
                            />

                        </div>

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

                <div className="form-actions">

                    <button
                        type="button"
                        className="btn-secondary"
                        onClick={() =>
                            navigate("/products")
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
                                ? "Update Product"
                                : "Create Product"}
                    </button>

                </div>

            </form>

        </div>

    );
}

export default ProductForm;