import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

import {
    getPagedProductVariants,
    archiveProductVariant
} from "../../api/product/productVariantApi";

import Loading from "../../components/common/Loading";
import EmptyState from "../../components/common/EmptyState";
import ConfirmDialog from "../../components/common/ConfirmDialog";

import "./productVariant.css";


function ProductVariantList() {

    const navigate = useNavigate();

    const [variants, setVariants] = useState([]);

    const [loading, setLoading] = useState(true);

    const [error, setError] = useState("");

    const [pageNumber, setPageNumber] = useState(1);

    const [pageSize] = useState(10);

    const [totalPages, setTotalPages] = useState(1);

    const [totalRecords, setTotalRecords] = useState(0);

    const [searchInput, setSearchInput] = useState("");

    const [search, setSearch] = useState("");

    const [selectedVariant, setSelectedVariant] = useState(null);

    const [archiving, setArchiving] = useState(false);


    // ============================================================
    // LOAD VARIANTS
    // ============================================================

    const loadVariants = async () => {

        try {

            setLoading(true);
            setError("");

            const response =
                await getPagedProductVariants(
                    pageNumber,
                    pageSize,
                    search
                );

            const data =
                response?.messageData ??
                response?.data ??
                response;

            if (Array.isArray(data)) {

                setVariants(data);
                setTotalPages(1);
                setTotalRecords(data.length);

            }
            else {

                setVariants(
                    data?.items ??
                    data?.Items ??
                    []
                );

                setTotalPages(
                    data?.totalPages ??
                    data?.TotalPages ??
                    1
                );

                setTotalRecords(
                    data?.totalRecords ??
                    data?.TotalRecords ??
                    0
                );

            }

        }
        catch (err) {

            console.error(
                "Failed to load product variants:",
                err
            );

            setError(
                err?.response?.data?.message ||
                err?.response?.data?.messageData ||
                "Unable to load product variants."
            );

        }
        finally {

            setLoading(false);

        }

    };


    useEffect(() => {

        loadVariants();

    }, [pageNumber, search]);


    // ============================================================
    // SEARCH
    // ============================================================

    const handleSearch = (event) => {

        event.preventDefault();

        setPageNumber(1);

        setSearch(
            searchInput.trim()
        );

    };


    // ============================================================
    // CLEAR SEARCH
    // ============================================================

    const handleClearSearch = () => {

        setSearchInput("");

        setSearch("");

        setPageNumber(1);

    };


    // ============================================================
    // ARCHIVE
    // ============================================================

    const handleArchive = async () => {

        if (!selectedVariant) {
            return;
        }

        try {

            setArchiving(true);

            await archiveProductVariant(
                selectedVariant.productVariantId
            );

            setSelectedVariant(null);

            await loadVariants();

        }
        catch (err) {

            console.error(
                "Failed to archive product variant:",
                err
            );

            setError(
                err?.response?.data?.message ||
                "Unable to archive product variant."
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
            <Loading
                message="Loading product variants..."
            />
        );

    }


    return (

        <div className="product-variant-page">

            {/* ================================================== */}
            {/* HEADER */}
            {/* ================================================== */}

            <div className="product-page-header">

                <div>

                    <h1>
                        Product Variants
                    </h1>

                    <p>
                        Manage variants for your products.
                    </p>

                </div>

                <button
                    type="button"
                    className="btn-primary"
                    onClick={() =>
                        navigate("/product-variants/new")
                    }
                >
                    + Add Variant
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
            {/* SEARCH */}
            {/* ================================================== */}

            <form
                className="product-search"
                onSubmit={handleSearch}
            >

                <input
                    type="text"
                    value={searchInput}
                    onChange={(event) =>
                        setSearchInput(
                            event.target.value
                        )
                    }
                    placeholder="Search variants..."
                />

                <button
                    type="submit"
                    className="btn-primary"
                >
                    Search
                </button>

                {search && (

                    <button
                        type="button"
                        className="btn-secondary"
                        onClick={handleClearSearch}
                    >
                        Clear
                    </button>

                )}

            </form>


            {/* ================================================== */}
            {/* TABLE */}
            {/* ================================================== */}

            {variants.length === 0 ? (

                <EmptyState
                    title="No product variants found"
                    message={
                        search
                            ? "No variants matched your search."
                            : "Create your first product variant to get started."
                    }
                />

            ) : (

                <div className="product-table-card">

                    <div className="product-table-wrapper">

                        <table className="product-table">

                            <thead>

                                <tr>

                                    <th>ID</th>

                                    <th>Product ID</th>

                                    <th>Variant Name</th>

                                    <th>Standard</th>

                                    <th>Attribute Values</th>

                                    <th>Actions</th>

                                </tr>

                            </thead>

                            <tbody>

                                {variants.map(
                                    (variant) => (

                                    <tr
                                        key={
                                            variant.productVariantId
                                        }
                                    >

                                        <td>
                                            #
                                            {
                                                variant.productVariantId
                                            }
                                        </td>

                                        <td>
                                            #
                                            {
                                                variant.productId
                                            }
                                        </td>

                                        <td>

                                            <div className="product-name">

                                                {
                                                    variant.productVariantName ||
                                                    "Unnamed Variant"
                                                }

                                            </div>

                                        </td>

                                        <td>

                                            <span
                                                className={
                                                    variant.isStandard
                                                        ? "status-badge active"
                                                        : "status-badge inactive"
                                                }
                                            >

                                                {
                                                    variant.isStandard
                                                        ? "Standard"
                                                        : "Custom"
                                                }

                                            </span>

                                        </td>

                                        <td>

                                            {
                                                variant.attributeValueIds ||
                                                "-"
                                            }

                                        </td>

                                        <td>

                                            <div className="product-actions">

                                                <button
                                                    type="button"
                                                    className="action-button"
                                                    onClick={() =>
                                                        navigate(
                                                            `/product-variants/${variant.productVariantId}`
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
                                                            `/product-variants/${variant.productVariantId}/edit`
                                                        )
                                                    }
                                                >
                                                    Edit
                                                </button>

                                                <button
                                                    type="button"
                                                    className="action-button danger"
                                                    onClick={() =>
                                                        setSelectedVariant(
                                                            variant
                                                        )
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
                            disabled={
                                pageNumber <= 1
                            }
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
                            {" "}
                            ({totalRecords} total)
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

                open={
                    selectedVariant !== null
                }

                title="Archive Product Variant"

                message={
                    `Are you sure you want to archive "${selectedVariant?.productVariantName || "this product variant"}"?`
                }

                confirmText="Archive"

                cancelText="Cancel"

                loading={archiving}

                onConfirm={handleArchive}

                onCancel={() =>
                    setSelectedVariant(null)
                }

            />

        </div>

    );

}


export default ProductVariantList;