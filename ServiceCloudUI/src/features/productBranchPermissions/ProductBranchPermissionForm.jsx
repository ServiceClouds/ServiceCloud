import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import {
    createProductBranchPermission,
    getProductBranchPermissionById,
    updateProductBranchPermission
} from "../../api/product/productBranchPermissionApi";
import "./productBranchPermission.css";

const initialForm = {
    productBranchPermissionId: "",
    productId: "",
    branchId: "",
    isActive: true,
    isOnline: false,
    isHidePriceOnline: false,
    isFeatured: false,
    hasTrackingventory: false,
    hasShipping: false,
    isIncluded: false,
    businessUseOnly: false,
    isVariantGenerated: false,
    isSharedPrivately: false
};

const ProductBranchPermissionForm = () => {
    const { id } = useParams();
    const navigate = useNavigate();

    const isEdit = Boolean(id);

    const [form, setForm] = useState(initialForm);
    const [loading, setLoading] = useState(isEdit);
    const [saving, setSaving] = useState(false);
    const [error, setError] = useState("");

    useEffect(() => {
        if (!isEdit) return;

        const loadPermission = async () => {
            try {
                setLoading(true);

                const response =
                    await getProductBranchPermissionById(id);

                const data = response.messageData;

                setForm({
                    productBranchermissionId:
                        data.productBranchPermissionId,
                    productId: data.productId,
                    branchId: data.branchId,
                    isActive: data.isActive ?? false,
                    isOnline: data.isOnline ?? false,
                    isHidePriceOnline:
                        data.isHidePriceOnline ?? false,
                    isFeatured: data.isFeatured ?? false,
                    hasTrackingventory:
                        data.hasTrackingventory ?? false,
                    hasShipping: data.hasShipping ?? false,
                    isIncluded: data.isIncluded ?? false,
                    businessUseOnly:
                        data.businessUseOnly ?? false,
                    isVariantGenerated:
                        data.isVariantGenerated ?? false,
                    isSharedPrivately:
                        data.isSharedPrivately ?? false
                });
            } catch (error) {
                setError(
                    error.response?.data?.message ||
                    "Failed to load product branch permission."
                );
            } finally {
                setLoading(false);
            }
        };

        loadPermission();
    }, [id, isEdit]);

    const handleChange = (e) => {
        const { name, value, type, checked } = e.target;

        setForm((previous) => ({
            ...previous,
            [name]: type === "checkbox" ? checked : value
        }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        try {
            setSaving(true);
            setError("");

            if (isEdit) {
                const data = {
                    productBranchPermissionId: Number(id),
                    isActive: form.isActive,
                    isOnline: form.isOnline,
                    isHidePriceOnline:
                        form.isHidePriceOnline,
                    isFeatured: form.isFeatured,
                    hasTrackingventory:
                        form.hasTrackingventory,
                    hasShipping: form.hasShipping,
                    isIncluded: form.isIncluded,
                    businessUseOnly:
                        form.businessUseOnly,
                    isVariantGenerated:
                        form.isVariantGenerated,
                    isSharedPrivately:
                        form.isSharedPrivately
                };

                await updateProductBranchPermission(id, data);
            } else {
                const data = {
                    productBranchermissionId:
                        Number(form.productBranchermissionId),
                    productId: Number(form.productId),
                    branchId: Number(form.branchId),
                    isActive: form.isActive,
                    isOnline: form.isOnline,
                    isHidePriceOnline:
                        form.isHidePriceOnline,
                    isFeatured: form.isFeatured,
                    hasTrackingventory:
                        form.hasTrackingventory,
                    hasShipping: form.hasShipping,
                    isIncluded: form.isIncluded,
                    businessUseOnly:
                        form.businessUseOnly,
                    isVariantGenerated:
                        form.isVariantGenerated,
                    isSharedPrivately:
                        form.isSharedPrivately
                };

                await createProductBranchPermission(data);
            }

            navigate("/product-branch-permissions");
        } catch (error) {
            setError(
                error.response?.data?.message ||
                "Failed to save product branch permission."
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
        <div className="product-branch-permission-page">
            <div className="page-header">
                <div>
                    <h2>
                        {isEdit
                            ? "Edit Product Branch Permission"
                            : "Add Product Branch Permission"}
                    </h2>

                    <p>
                        {isEdit
                            ? "Update product branch permission."
                            : "Create a new product branch permission."}
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
                {!isEdit && (
                    <div className="form-group">
                        <label>
                            Product Branch Permission ID
                        </label>

                        <input
                            type="number"
                            name="productBranchermissionId"
                            value={
                                form.productBranchermissionId
                            }
                            onChange={handleChange}
                            required
                        />
                    </div>
                )}

                <div className="form-group">
                    <label>Product ID</label>

                    <input
                        type="number"
                        name="productId"
                        value={form.productId}
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

                <div className="checkbox-grid">
                    <label>
                        <input
                            type="checkbox"
                            name="isActive"
                            checked={form.isActive}
                            onChange={handleChange}
                        />
                        Active
                    </label>

                    <label>
                        <input
                            type="checkbox"
                            name="isOnline"
                            checked={form.isOnline}
                            onChange={handleChange}
                        />
                        Online
                    </label>

                    <label>
                        <input
                            type="checkbox"
                            name="isHidePriceOnline"
                            checked={form.isHidePriceOnline}
                            onChange={handleChange}
                        />
                        Hide Price Online
                    </label>

                    <label>
                        <input
                            type="checkbox"
                            name="isFeatured"
                            checked={form.isFeatured}
                            onChange={handleChange}
                        />
                        Featured
                    </label>

                    <label>
                        <input
                            type="checkbox"
                            name="hasTrackingventory"
                            checked={form.hasTrackingventory}
                            onChange={handleChange}
                        />
                        Has Tracking Inventory
                    </label>

                    <label>
                        <input
                            type="checkbox"
                            name="hasShipping"
                            checked={form.hasShipping}
                            onChange={handleChange}
                        />
                        Has Shipping
                    </label>

                    <label>
                        <input
                            type="checkbox"
                            name="isIncluded"
                            checked={form.isIncluded}
                            onChange={handleChange}
                        />
                        Included
                    </label>

                    <label>
                        <input
                            type="checkbox"
                            name="businessUseOnly"
                            checked={form.businessUseOnly}
                            onChange={handleChange}
                        />
                        Business Use Only
                    </label>

                    <label>
                        <input
                            type="checkbox"
                            name="isVariantGenerated"
                            checked={form.isVariantGenerated}
                            onChange={handleChange}
                        />
                        Variant Generated
                    </label>

                    <label>
                        <input
                            type="checkbox"
                            name="isSharedPrivately"
                            checked={form.isSharedPrivately}
                            onChange={handleChange}
                        />
                        Shared Privately
                    </label>
                </div>

                <div className="form-actions">
                    <button
                        type="button"
                        className="btn btn-secondary"
                        onClick={() =>
                            navigate(
                                "/product-branch-permissions"
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

export default ProductBranchPermissionForm;