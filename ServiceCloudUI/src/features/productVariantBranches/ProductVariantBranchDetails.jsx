import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";

import {
    getProductVariantBranchById
} from "../../api/product/productVariantBranchApi";

import "./productVariantBranch.css";

const ProductVariantBranchDetails = () => {
    const { id } = useParams();
    const navigate = useNavigate();

    const [data, setData] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");

    useEffect(() => {
        loadData();
    }, [id]);

    const loadData = async () => {
        try {
            setLoading(true);
            setError("");

            const result =
                await getProductVariantBranchById(id);

            setData(result);

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

    if (loading) {
        return (
            <div className="crud-page">
                <div className="loading-message">
                    Loading details...
                </div>
            </div>
        );
    }

    if (error) {
        return (
            <div className="crud-page">
                <div className="error-message">
                    {error}
                </div>
            </div>
        );
    }

    if (!data) {
        return (
            <div className="crud-page">
                <div className="error-message">
                    Product variant branch not found.
                </div>
            </div>
        );
    }

    return (
        <div className="crud-page">

            <div className="crud-header">

                <div>
                    <h1>
                        Product Variant Branch Details
                    </h1>

                    <p>
                        View complete product variant
                        branch information.
                    </p>
                </div>

                <button
                    className="edit-button"
                    onClick={() =>
                        navigate(
                            `/product-variant-branches/${data.productVariantBranchId}/edit`
                        )
                    }
                >
                    Edit
                </button>

            </div>

            <div className="details-card">

                <div className="details-section">

                    <h2>
                        Basic Information
                    </h2>

                    <div className="details-grid">

                        <div className="detail-item">
                            <span>ID</span>
                            <strong>
                                {data.productVariantBranchId}
                            </strong>
                        </div>

                        <div className="detail-item">
                            <span>Product Variant ID</span>
                            <strong>
                                {data.productVariantId}
                            </strong>
                        </div>

                        <div className="detail-item">
                            <span>Branch ID</span>
                            <strong>
                                {data.branchId}
                            </strong>
                        </div>

                        <div className="detail-item">
                            <span>Active</span>
                            <strong>
                                {data.isActive
                                    ? "Yes"
                                    : "No"}
                            </strong>
                        </div>

                        <div className="detail-item">
                            <span>Included</span>
                            <strong>
                                {data.isIncluded
                                    ? "Yes"
                                    : "No"}
                            </strong>
                        </div>

                        <div className="detail-item">
                            <span>Archived</span>
                            <strong>
                                {data.isArchived
                                    ? "Yes"
                                    : "No"}
                            </strong>
                        </div>

                    </div>

                </div>

                <div className="details-section">

                    <h2>
                        Product Information
                    </h2>

                    <div className="details-grid">

                        <div className="detail-item">
                            <span>Barcode</span>
                            <strong>
                                {data.barcode || "-"}
                            </strong>
                        </div>

                        <div className="detail-item">
                            <span>SKU</span>
                            <strong>
                                {data.sku || "-"}
                            </strong>
                        </div>

                        <div className="detail-item">
                            <span>Supplier ID</span>
                            <strong>
                                {data.supplierId ?? "-"}
                            </strong>
                        </div>

                        <div className="detail-item">
                            <span>Supplier Code</span>
                            <strong>
                                {data.supplierCode || "-"}
                            </strong>
                        </div>

                    </div>

                </div>

                <div className="details-section">

                    <h2>
                        Inventory & Pricing
                    </h2>

                    <div className="details-grid">

                        <div className="detail-item">
                            <span>
                                Reorder Threshold
                            </span>

                            <strong>
                                {data.reorderThreshold ?? "-"}
                            </strong>
                        </div>

                        <div className="detail-item">
                            <span>
                                Reorder Quantity
                            </span>

                            <strong>
                                {data.reorderQuantity ?? "-"}
                            </strong>
                        </div>

                        <div className="detail-item">
                            <span>
                                Supplier Price
                            </span>

                            <strong>
                                {data.supplierPrice ?? "-"}
                            </strong>
                        </div>

                        <div className="detail-item">
                            <span>
                                Price
                            </span>

                            <strong>
                                {data.price}
                            </strong>
                        </div>

                        <div className="detail-item">
                            <span>
                                Total Tax Percentage
                            </span>

                            <strong>
                                {data.totalTaxPercentage}
                            </strong>
                        </div>

                        <div className="detail-item">
                            <span>
                                Total Price
                            </span>

                            <strong>
                                {data.totalPrice}
                            </strong>
                        </div>

                    </div>

                </div>

                <div className="details-section">

                    <h2>
                        Audit Information
                    </h2>

                    <div className="details-grid">

                        <div className="detail-item">
                            <span>
                                Created On
                            </span>

                            <strong>
                                {data.createdOn
                                    ? new Date(
                                        data.createdOn
                                    ).toLocaleString()
                                    : "-"}
                            </strong>
                        </div>

                        <div className="detail-item">
                            <span>
                                Created By
                            </span>

                            <strong>
                                {data.createdBy}
                            </strong>
                        </div>

                        <div className="detail-item">
                            <span>
                                Modified On
                            </span>

                            <strong>
                                {data.modifiedOn
                                    ? new Date(
                                        data.modifiedOn
                                    ).toLocaleString()
                                    : "-"}
                            </strong>
                        </div>

                        <div className="detail-item">
                            <span>
                                Modified By
                            </span>

                            <strong>
                                {data.modifiedBy ?? "-"}
                            </strong>
                        </div>

                    </div>

                </div>

            </div>

            <div className="form-actions">

                <button
                    className="secondary-button"
                    onClick={() =>
                        navigate(
                            "/product-variant-branches"
                        )
                    }
                >
                    Back
                </button>

            </div>

        </div>
    );
};

export default ProductVariantBranchDetails;