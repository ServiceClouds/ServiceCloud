import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

import {
    getPagedProducts,
    archiveProduct
} from "../../api/product/productApi";

import Loading from "../../components/common/Loading";
import EmptyState from "../../components/common/EmptyState";
import ConfirmDialog from "../../components/common/ConfirmDialog";

import "./product.css";

import "./product.css";

function ProductList() {

    const navigate = useNavigate();

    const [products, setProducts] = useState([]);

    const [loading, setLoading] = useState(true);

    const [error, setError] = useState("");

    const [pageNumber, setPageNumber] = useState(1);
    const [pageSize] = useState(10);

    const [totalPages, setTotalPages] = useState(1);

    const [selectedProduct, setSelectedProduct] = useState(null);

    const [archiving, setArchiving] = useState(false);

    // ============================================================
    // LOAD PRODUCTS
    // ============================================================

    const loadProducts = async () => {

        try {

            setLoading(true);
            setError("");

            const response = await getPagedProducts(
                pageNumber,
                pageSize
            );

            /*
             * Your Result<T> API normally returns messageData.
             * We keep the extraction here so the UI stays clean.
             */

            const data =
                response?.messageData ??
                response?.data ??
                response;

            if (Array.isArray(data)) {

                setProducts(data);
                setTotalPages(1);

            } else {

                setProducts(
                    data?.items ??
                    data?.data ??
                    data?.results ??
                    []
                );

                setTotalPages(
                    data?.totalPages ??
                    data?.pagination?.totalPages ??
                    1
                );
            }

        }
        catch (err) {

            console.error(
                "Failed to load products:",
                err
            );

            setError(
                err?.response?.data?.message ||
                err?.response?.data?.messageData ||
                "Unable to load products."
            );

        }
        finally {

            setLoading(false);

        }
    };

    useEffect(() => {

        loadProducts();

    }, [pageNumber]);

    // ============================================================
    // ARCHIVE
    // ============================================================

    const handleArchive = async () => {

        if (!selectedProduct) {
            return;
        }

        try {

            setArchiving(true);

            await archiveProduct(
                selectedProduct.productId
            );

            setSelectedProduct(null);

            await loadProducts();

        }
        catch (err) {

            console.error(
                "Failed to archive product:",
                err
            );

            setError(
                err?.response?.data?.message ||
                "Unable to archive product."
            );

        }
        finally {

            setArchiving(false);

        }
    };

    // ============================================================
    // LOADING
    // ============================================================

    if (loading) {

        return (
            <Loading message="Loading products..." />
        );

    }

    return (

        <div className="product-page">

            {/* ================================================== */}
            {/* HEADER */}
            {/* ================================================== */}

            <div className="product-page-header">

                <div>
                    <h1>Products</h1>

                    <p>
                        Manage your products and inventory settings.
                    </p>
                </div>

                <button
                    type="button"
                    className="btn-primary"
                    onClick={() =>
                        navigate("/products/new")
                    }
                >
                    + Add Product
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
            {/* TABLE */}
            {/* ================================================== */}

            {products.length === 0 ? (

                <EmptyState
                    title="No products found"
                    message="Create your first product to get started."
                />

            ) : (

                <div className="product-table-card">

                    <div className="product-table-wrapper">

                        <table className="product-table">

                            <thead>

                                <tr>

                                    <th>ID</th>

                                    <th>Product Name</th>

                                    <th>Category</th>

                                    <th>Status</th>

                                    <th>Branch Permission</th>

                                    <th>Actions</th>

                                </tr>

                            </thead>

                            <tbody>

                                {products.map((product) => (

                                    <tr
                                        key={product.productId}
                                    >

                                        <td>
                                            #{product.productId}
                                        </td>

                                        <td>

                                            <div className="product-name">

                                                {product.productName ||
                                                    "Unnamed Product"}

                                            </div>

                                        </td>

                                        <td>
                                            {product.productCategoryName ||
                                                product.productCategoryId ||
                                                "-"}
                                        </td>

                                        <td>

                                            <span
                                                className={
                                                    product.isActive
                                                        ? "status-badge active"
                                                        : "status-badge inactive"
                                                }
                                            >
                                                {product.isActive
                                                    ? "Active"
                                                    : "Inactive"}
                                            </span>

                                        </td>

                                        <td>

                                            {product.hasBranchPermission
                                                ? "Enabled"
                                                : "Disabled"}

                                        </td>

                                        <td>

                                            <div className="product-actions">

                                                <button
                                                    type="button"
                                                    className="action-button"
                                                    onClick={() =>
                                                        navigate(
                                                            `/products/${product.productId}`
                                                        )
                                                    }
                                                >
                                                    View
                                                </button>

                                                <button
                                                    type="button"
                                                    className="action-button"
                                                    onClick={() =>
                                                        navigate(
                                                            `/products/${product.productId}/edit`
                                                        )
                                                    }
                                                >
                                                    Edit
                                                </button>

                                                <button
                                                    type="button"
                                                    className="action-button danger"
                                                    onClick={() =>
                                                        setSelectedProduct(product)
                                                    }
                                                >
                                                    Archive
                                                </button>

                                            </div>

                                        </td>

                                    </tr>

                                ))}

                            </tbody>

                        </table>

                    </div>

                    {/* ================================================== */}
                    {/* PAGINATION */}
                    {/* ================================================== */}

                    <div className="pagination">

                        <button
                            type="button"
                            disabled={pageNumber <= 1}
                            onClick={() =>
                                setPageNumber(
                                    (previous) =>
                                        previous - 1
                                )
                            }
                        >
                            ← Previous
                        </button>

                        <span>
                            Page {pageNumber} of {totalPages}
                        </span>

                        <button
                            type="button"
                            disabled={
                                pageNumber >= totalPages
                            }
                            onClick={() =>
                                setPageNumber(
                                    (previous) =>
                                        previous + 1
                                )
                            }
                        >
                            Next →
                        </button>

                    </div>

                </div>

            )}

            {/* ================================================== */}
            {/* ARCHIVE CONFIRMATION */}
            {/* ================================================== */}

            <ConfirmDialog
                open={selectedProduct !== null}
                title="Archive Product"
                message={
                    `Are you sure you want to archive "${selectedProduct?.productName || "this product"}"?`
                }
                confirmText="Archive"
                cancelText="Cancel"
                loading={archiving}
                onConfirm={handleArchive}
                onCancel={() =>
                    setSelectedProduct(null)
                }
            />

        </div>

    );
}

export default ProductList;