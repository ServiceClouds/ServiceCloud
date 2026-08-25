import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";

import {
    createProductVariantBranch,
    getProductVariantBranchById,
    updateProductVariantBranch
} from "../../api/product/productVariantBranchApi";

import "./productVariantBranch.css";

const ProductVariantBranchForm = () => {
    const { id } = useParams();
    const navigate = useNavigate();

    const isEditMode = Boolean(id);

    const [loading, setLoading] = useState(false);
    const [saving, setSaving] = useState(false);
    const [error, setError] = useState("");

    const [formData, setFormData] = useState({
        productVariantBranchId: "",
        productVariantId: "",
        branchId: "",
        isActive: true,
        isIncluded: true,
        barcode: "",
        sku: "",
        supplierId: "",
        supplierCode: "",
        reorderThreshold: "",
        reorderQuantity: "",
        supplierPrice: "",
        price: "",
        totalTaxPercentage: "",
        totalPrice: ""
    });

    useEffect(() => {
        if (isEditMode) {
            loadData();
        }
    }, [id]);

    const loadData = async () => {
        try {
            setLoading(true);
            setError("");

            const result =
                await getProductVariantBranchById(id);

            setFormData({
                productVariantBranchId:
                    result.productVariantBranchId ?? "",

                productVariantId:
                    result.productVariantId ?? "",

                branchId:
                    result.branchId ?? "",

                isActive:
                    result.isActive ?? true,

                isIncluded:
                    result.isIncluded ?? true,

                barcode:
                    result.barcode ?? "",

                sku:
                    result.sku ?? "",

                supplierId:
                    result.supplierId ?? "",

                supplierCode:
                    result.supplierCode ?? "",

                reorderThreshold:
                    result.reorderThreshold ?? "",

                reorderQuantity:
                    result.reorderQuantity ?? "",

                supplierPrice:
                    result.supplierPrice ?? "",

                price:
                    result.price ?? "",

                totalTaxPercentage:
                    result.totalTaxPercentage ?? "",

                totalPrice:
                    result.totalPrice ?? ""
            });

        } catch (err) {
            console.error(err);

            setError(
                err.response?.data?.message ||
                "Failed to load product variant branch."
            );
        } finally {
            setLoading(false);
        }
    };

    const handleChange = (event) => {
        const {
            name,
            value,
            type,
            checked
        } = event.target;

        setFormData((previous) => ({
            ...previous,
            [name]:
                type === "checkbox"
                    ? checked
                    : value
        }));
    };

    const nullableInt = (value) => {
        return value === ""
            ? null
            : Number(value);
    };

    const nullableDecimal = (value) => {
        return value === ""
            ? null
            : Number(value);
    };

    const handleSubmit = async (event) => {
        event.preventDefault();

        try {
            setSaving(true);
            setError("");

            if (isEditMode) {

                const updatePayload = {
                    productVariantBranchId:
                        Number(
                            formData.productVariantBranchId
                        ),

                    isActive:
                        formData.isActive,

                    isIncluded:
                        formData.isIncluded,

                    barcode:
                        formData.barcode || null,

                    sku:
                        formData.sku || null,

                    supplierId:
                        nullableInt(
                            formData.supplierId
                        ),

                    supplierCode:
                        formData.supplierCode || null,

                    reorderThreshold:
                        nullableInt(
                            formData.reorderThreshold
                        ),

                    reorderQuantity:
                        nullableInt(
                            formData.reorderQuantity
                        ),

                    supplierPrice:
                        nullableDecimal(
                            formData.supplierPrice
                        ),

                    price:
                        Number(formData.price),

                    totalTaxPercentage:
                        Number(
                            formData.totalTaxPercentage
                        ),

                    totalPrice:
                        Number(
                            formData.totalPrice
                        )
                };

                await updateProductVariantBranch(
                    formData.productVariantBranchId,
                    updatePayload
                );

            } else {

                const createPayload = {
                    productVariantBranchId:
                        Number(
                            formData.productVariantBranchId
                        ),

                    productVariantId:
                        Number(
                            formData.productVariantId
                        ),

                    branchId:
                        Number(
                            formData.branchId
                        ),

                    isActive:
                        formData.isActive,

                    isIncluded:
                        formData.isIncluded,

                    barcode:
                        formData.barcode || null,

                    sku:
                        formData.sku || null,

                    supplierId:
                        nullableInt(
                            formData.supplierId
                        ),

                    supplierCode:
                        formData.supplierCode || null,

                    reorderThreshold:
                        nullableInt(
                            formData.reorderThreshold
                        ),

                    reorderQuantity:
                        nullableInt(
                            formData.reorderQuantity
                        ),

                    supplierPrice:
                        nullableDecimal(
                            formData.supplierPrice
                        ),

                    price:
                        Number(formData.price),

                    totalTaxPercentage:
                        Number(
                            formData.totalTaxPercentage
                        ),

                    totalPrice:
                        Number(
                            formData.totalPrice
                        )
                };

                await createProductVariantBranch(
                    createPayload
                );
            }

            navigate(
                "/product-variant-branches"
            );

        } catch (err) {
            console.error(err);

            setError(
                err.response?.data?.message ||
                "Failed to save product variant branch."
            );
        } finally {
            setSaving(false);
        }
    };

    if (loading) {
        return (
            <div className="crud-page">
                <div className="loading-message">
                    Loading product variant branch...
                </div>
            </div>
        );
    }

    return (
        <div className="crud-page">

            <div className="crud-header">

                <div>
                    <h1>
                        {isEditMode
                            ? "Edit Product Variant Branch"
                            : "Create Product Variant Branch"}
                    </h1>

                    <p>
                        {isEditMode
                            ? "Update product variant branch information."
                            : "Create a new product variant branch."}
                    </p>
                </div>

            </div>

            {error && (
                <div className="error-message">
                    {error}
                </div>
            )}

            <form
                className="crud-form"
                onSubmit={handleSubmit}
            >

                <div className="form-grid">

                    {!isEditMode && (
                        <>
                            <div className="form-group">

                                <label>
                                    Product Variant Branch ID
                                </label>

                                <input
                                    type="number"
                                    name="productVariantBranchId"
                                    value={
                                        formData.productVariantBranchId
                                    }
                                    onChange={handleChange}
                                    required
                                />

                            </div>

                            <div className="form-group">

                                <label>
                                    Product Variant ID
                                </label>

                                <input
                                    type="number"
                                    name="productVariantId"
                                    value={
                                        formData.productVariantId
                                    }
                                    onChange={handleChange}
                                    required
                                />

                            </div>

                            <div className="form-group">

                                <label>
                                    Branch ID
                                </label>

                                <input
                                    type="number"
                                    name="branchId"
                                    value={
                                        formData.branchId
                                    }
                                    onChange={handleChange}
                                    required
                                />

                            </div>
                        </>
                    )}

                    {isEditMode && (
                        <div className="form-group">

                            <label>
                                Product Variant Branch ID
                            </label>

                            <input
                                type="number"
                                value={
                                    formData.productVariantBranchId
                                }
                                disabled
                            />

                        </div>
                    )}

                    <div className="form-group checkbox-group">

                        <label>
                            <input
                                type="checkbox"
                                name="isActive"
                                checked={
                                    formData.isActive
                                }
                                onChange={handleChange}
                            />

                            Active
                        </label>

                    </div>

                    <div className="form-group checkbox-group">

                        <label>
                            <input
                                type="checkbox"
                                name="isIncluded"
                                checked={
                                    formData.isIncluded
                                }
                                onChange={handleChange}
                            />

                            Included
                        </label>

                    </div>

                    <div className="form-group">

                        <label>
                            Barcode
                        </label>

                        <input
                            type="text"
                            name="barcode"
                            value={
                                formData.barcode
                            }
                            onChange={handleChange}
                            maxLength={20}
                        />

                    </div>

                    <div className="form-group">

                        <label>
                            SKU
                        </label>

                        <input
                            type="text"
                            name="sku"
                            value={
                                formData.sku
                            }
                            onChange={handleChange}
                            maxLength={20}
                        />

                    </div>

                    <div className="form-group">

                        <label>
                            Supplier ID
                        </label>

                        <input
                            type="number"
                            name="supplierId"
                            value={
                                formData.supplierId
                            }
                            onChange={handleChange}
                        />

                    </div>

                    <div className="form-group">

                        <label>
                            Supplier Code
                        </label>

                        <input
                            type="text"
                            name="supplierCode"
                            value={
                                formData.supplierCode
                            }
                            onChange={handleChange}
                            maxLength={20}
                        />

                    </div>

                    <div className="form-group">

                        <label>
                            Reorder Threshold
                        </label>

                        <input
                            type="number"
                            name="reorderThreshold"
                            value={
                                formData.reorderThreshold
                            }
                            onChange={handleChange}
                        />

                    </div>

                    <div className="form-group">

                        <label>
                            Reorder Quantity
                        </label>

                        <input
                            type="number"
                            name="reorderQuantity"
                            value={
                                formData.reorderQuantity
                            }
                            onChange={handleChange}
                        />

                    </div>

                    <div className="form-group">

                        <label>
                            Supplier Price
                        </label>

                        <input
                            type="number"
                            step="0.01"
                            name="supplierPrice"
                            value={
                                formData.supplierPrice
                            }
                            onChange={handleChange}
                        />

                    </div>

                    <div className="form-group">

                        <label>
                            Price
                        </label>

                        <input
                            type="number"
                            step="0.01"
                            name="price"
                            value={
                                formData.price
                            }
                            onChange={handleChange}
                            required
                        />

                    </div>

                    <div className="form-group">

                        <label>
                            Total Tax Percentage
                        </label>

                        <input
                            type="number"
                            step="0.000001"
                            name="totalTaxPercentage"
                            value={
                                formData.totalTaxPercentage
                            }
                            onChange={handleChange}
                            required
                        />

                    </div>

                    <div className="form-group">

                        <label>
                            Total Price
                        </label>

                        <input
                            type="number"
                            step="0.01"
                            name="totalPrice"
                            value={
                                formData.totalPrice
                            }
                            onChange={handleChange}
                            required
                        />

                    </div>

                </div>

                <div className="form-actions">

                    <button
                        type="button"
                        className="secondary-button"
                        onClick={() =>
                            navigate(
                                "/product-variant-branches"
                            )
                        }
                    >
                        Cancel
                    </button>

                    <button
                        type="submit"
                        className="primary-button"
                        disabled={saving}
                    >
                        {saving
                            ? "Saving..."
                            : isEditMode
                                ? "Update"
                                : "Create"}
                    </button>

                </div>

            </form>

        </div>
    );
};

export default ProductVariantBranchForm;