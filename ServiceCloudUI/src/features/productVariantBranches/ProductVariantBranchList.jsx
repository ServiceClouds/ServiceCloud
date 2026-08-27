import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

import {
    getPagedProductVariantBranches,
    archiveProductVariantBranch
} from "../../api/product/productVariantBranchApi";

import ConfirmDialog from "../../components/common/ConfirmDialog";

import "./productVariantBranch.css";

const ProductVariantBranchList = () => {
    const navigate = useNavigate();

    const [items, setItems] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");

    const [pageNumber, setPageNumber] = useState(1);
    const [pageSize] = useState(10);
    const [search, setSearch] = useState("");

    const [totalPages, setTotalPages] = useState(1);

    const [showConfirm, setShowConfirm] = useState(false);
    const [selectedId, setSelectedId] = useState(null);

    const loadData = async () => {
        try {
            setLoading(true);
            setError("");

            const result = await getPagedProductVariantBranches(
                pageNumber,
                pageSize,
                search
            );

            setItems(result.items || []);
            setTotalPages(result.totalPages || 1);
        } catch (err) {
            console.error(err);

            setError(
                err.response?.data?.message ||
                "Failed to load product variant branches."
            );
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        loadData();
    }, [pageNumber, search]);

    const handleSearchChange = (event) => {
        setSearch(event.target.value);
        setPageNumber(1);
    };

    const handleArchiveClick = (id) => {
        setSelectedId(id);
        setShowConfirm(true);
    };

    const handleArchiveConfirm = async () => {
        try {
            await archiveProductVariantBranch(selectedId);

            setShowConfirm(false);
            setSelectedId(null);

            if (items.length === 1 && pageNumber > 1) {
                setPageNumber((previous) => previous - 1);
            } else {
                await loadData();
            }
        } catch (err) {
            console.error(err);

            setError(
                err.response?.data?.message ||
                "Failed to archive product variant branch."
            );

            setShowConfirm(false);
        }
    };

    if (loading) {
        return (
            <div className="crud-page">
                <div className="loading-message">
                    Loading product variant branches...
                </div>
            </div>
        );
    }

    return (
        <div className="crud-page">

            <div className="crud-header">

                <div>
                    <h1>Product Variant Branches</h1>

                    <p>
                        Manage product variant branch information.
                    </p>
                </div>

                <button
                    className="primary-button"
                    onClick={() =>
                        navigate(
                            "/dashboard/product-variant-branches/new"
                        )
                    }
                >
                    + Add Variant Branch
                </button>

            </div>

            {error && (
                <div className="error-message">
                    {error}
                </div>
            )}

            <div className="search-container">

                <input
                    type="text"
                    placeholder="Search product variant branches..."
                    value={search}
                    onChange={handleSearchChange}
                    className="search-input"
                />

            </div>

            <div className="table-container">

                <table className="crud-table">

                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Variant ID</th>
                            <th>Branch ID</th>
                            <th>SKU</th>
                            <th>Barcode</th>
                            <th>Active</th>
                            <th>Included</th>
                            <th>Price</th>
                            <th>Supplier Price</th>
                            <th>Total Tax %</th>
                            <th>Total Price</th>
                            <th>Actions</th>
                        </tr>
                    </thead>

                    <tbody>

                        {items.length === 0 ? (
                            <tr>
                                <td
                                    colSpan="12"
                                    className="empty-row"
                                >
                                    No product variant branches found.
                                </td>
                            </tr>
                        ) : (
                            items.map((item) => (
                                <tr
                                    key={
                                        item.productVariantBranchId
                                    }
                                >

                                    <td>
                                        {
                                            item.productVariantBranchId
                                        }
                                    </td>

                                    <td>
                                        {item.productVariantId}
                                    </td>

                                    <td>
                                        {item.branchId}
                                    </td>

                                    <td>
                                        {item.sku || "-"}
                                    </td>

                                    <td>
                                        {item.barcode || "-"}
                                    </td>

                                    <td>
                                        <span
                                            className={
                                                item.isActive
                                                    ? "status-active"
                                                    : "status-inactive"
                                            }
                                        >
                                            {item.isActive
                                                ? "Active"
                                                : "Inactive"}
                                        </span>
                                    </td>

                                    <td>
                                        {item.isIncluded
                                            ? "Yes"
                                            : "No"}
                                    </td>

                                    <td>
                                        {item.price}
                                    </td>

                                    <td>
                                        {item.supplierPrice ?? "-"}
                                    </td>

                                    <td>
                                        {item.totalTaxPercentage}
                                    </td>

                                    <td>
                                        {item.totalPrice}
                                    </td>

                                    <td>

                                        <div className="action-buttons">

                                            <button
                                                className="view-button"
                                                onClick={() =>
                                                    navigate(
                                                        `/dashboard/product-variant-branches/${item.productVariantBranchId}`
                                                    )
                                                }
                                            >
                                                View
                                            </button>

                                            <button
                                                className="edit-button"
                                                onClick={() =>
                                                    navigate(
                                                        `/dashboard/product-variant-branches/${item.productVariantBranchId}/edit`
                                                    )
                                                }
                                            >
                                                Edit
                                            </button>

                                            <button
                                                className="archive-button"
                                                onClick={() =>
                                                    handleArchiveClick(
                                                        item.productVariantBranchId
                                                    )
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

            <div className="pagination-container">

                <button
                    className="pagination-button"
                    disabled={pageNumber <= 1}
                    onClick={() =>
                        setPageNumber(
                            (previous) => previous - 1
                        )
                    }
                >
                    Previous
                </button>

                <span>
                    Page {pageNumber} of {totalPages}
                </span>

                <button
                    className="pagination-button"
                    disabled={pageNumber >= totalPages}
                    onClick={() =>
                        setPageNumber(
                            (previous) => previous + 1
                        )
                    }
                >
                    Next
                </button>

            </div>

            <ConfirmDialog
                isOpen={showConfirm}
                title="Archive Product Variant Branch"
                message="Are you sure you want to archive this product variant branch?"
                onConfirm={handleArchiveConfirm}
                onCancel={() => {
                    setShowConfirm(false);
                    setSelectedId(null);
                }}
            />

        </div>
    );
};

export default ProductVariantBranchList;