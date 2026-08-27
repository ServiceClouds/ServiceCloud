import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

import {
    getPagedProductCategories,
    archiveProductCategory
} from "../../api/product/productCategoryApi";

import Loading from "../../components/common/Loading";
import EmptyState from "../../components/common/EmptyState";
import ConfirmDialog from "../../components/common/ConfirmDialog";

import "./productCategory.css";

function ProductCategoryList() {
    const navigate = useNavigate();

    const [categories, setCategories] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");

    const [pageNumber, setPageNumber] = useState(1);
    const [pageSize] = useState(10);
    const [totalPages, setTotalPages] = useState(1);
    const [totalRecords, setTotalRecords] = useState(0);

    const [search, setSearch] = useState("");
    const [searchInput, setSearchInput] = useState("");

    const [selectedCategory, setSelectedCategory] = useState(null);
    const [archiving, setArchiving] = useState(false);

    const loadCategories = async () => {
        try {
            setLoading(true);
            setError("");

            const response = await getPagedProductCategories(
                pageNumber,
                pageSize,
                search
            );

            const data =
                response?.messageData ??
                response?.data ??
                response;

            const items =
                data?.items ??
                data?.data ??
                data?.results ??
                [];

            setCategories(
                Array.isArray(items) ? items : []
            );

            setTotalPages(data?.totalPages ?? 1);
            setTotalRecords(data?.totalRecords ?? 0);

        } catch (err) {
            console.error(
                "Failed to load product categories:",
                err
            );

            setError(
                err?.response?.data?.message ||
                err?.response?.data?.messageData ||
                "Unable to load product categories."
            );
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        loadCategories();
    }, [pageNumber, search]);

    const handleSearch = (event) => {
        event.preventDefault();

        setPageNumber(1);
        setSearch(searchInput.trim());
    };

    const handleArchive = async () => {
        if (!selectedCategory) {
            return;
        }

        try {
            setArchiving(true);

            await archiveProductCategory(
                selectedCategory.productCategoryId
            );

            setSelectedCategory(null);

            if (
                categories.length === 1 &&
                pageNumber > 1
            ) {
                setPageNumber(
                    (previous) => previous - 1
                );
            } else {
                await loadCategories();
            }

        } catch (err) {
            console.error(
                "Failed to archive product category:",
                err
            );

            setError(
                err?.response?.data?.message ||
                "Unable to archive product category."
            );
        } finally {
            setArchiving(false);
        }
    };

    if (loading) {
        return (
            <Loading
                message="Loading product categories..."
            />
        );
    }

    return (
        <div className="product-category-page">

            <div className="product-category-page-header">

                <div>
                    <h1>Product Categories</h1>

                    <p>
                        Manage your product categories.
                    </p>
                </div>

                <button
                    type="button"
                    className="btn-primary"
                    onClick={() =>
                        navigate(
                            "/dashboard/product-categories/new"
                        )
                    }
                >
                    + Add Category
                </button>

            </div>

            {error && (
                <div className="product-category-error">
                    {error}
                </div>
            )}

            <form
                className="category-search"
                onSubmit={handleSearch}
            >

                <input
                    type="text"
                    value={searchInput}
                    onChange={(event) =>
                        setSearchInput(event.target.value)
                    }
                    placeholder="Search categories..."
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
                        onClick={() => {
                            setSearchInput("");
                            setSearch("");
                            setPageNumber(1);
                        }}
                    >
                        Clear
                    </button>
                )}

            </form>

            {categories.length === 0 ? (

                <EmptyState
                    title={
                        search
                            ? "No categories found"
                            : "No product categories found"
                    }
                    message={
                        search
                            ? "Try a different search."
                            : "Create your first product category to get started."
                    }
                />

            ) : (

                <div className="product-category-table-card">

                    <div className="category-summary">
                        <span>
                            Total Categories: {totalRecords}
                        </span>
                    </div>

                    <div className="product-category-table-wrapper">

                        <table className="product-category-table">

                            <thead>
                                <tr>
                                    <th>ID</th>
                                    <th>Category Name</th>
                                    <th>Description</th>
                                    <th>Branch Permission</th>
                                    <th>App Source Type</th>
                                    <th>Actions</th>
                                </tr>
                            </thead>

                            <tbody>

                                {categories.map((category) => (
                                    <tr
                                        key={
                                            category.productCategoryId
                                        }
                                    >

                                        <td>
                                            #
                                            {
                                                category.productCategoryId
                                            }
                                        </td>

                                        <td>
                                            <div className="category-name">
                                                {
                                                    category.productCategoryName ||
                                                    "Unnamed Category"
                                                }
                                            </div>
                                        </td>

                                        <td>
                                            {
                                                category.description ||
                                                "-"
                                            }
                                        </td>

                                        <td>
                                            <span
                                                className={
                                                    category.hasBranchPermission
                                                        ? "status-badge active"
                                                        : "status-badge inactive"
                                                }
                                            >
                                                {
                                                    category.hasBranchPermission
                                                        ? "Enabled"
                                                        : "Disabled"
                                                }
                                            </span>
                                        </td>

                                        <td>
                                            {
                                                category.appSourceTypeId ??
                                                "-"
                                            }
                                        </td>

                                        <td>

                                            <div className="category-actions">

                                                <button
                                                    type="button"
                                                    className="action-button"
                                                    onClick={() =>
                                                        navigate(
                                                            `/dashboard/product-categories/${category.productCategoryId}`
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
                                                            `/dashboard/product-categories/${category.productCategoryId}/edit`
                                                        )
                                                    }
                                                >
                                                    Edit
                                                </button>

                                                <button
                                                    type="button"
                                                    className="action-button danger"
                                                    onClick={() =>
                                                        setSelectedCategory(
                                                            category
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

            <ConfirmDialog
                open={selectedCategory !== null}
                title="Archive Product Category"
                message={
                    `Are you sure you want to archive "${selectedCategory?.productCategoryName || "this category"}"?`
                }
                confirmText="Archive"
                cancelText="Cancel"
                loading={archiving}
                onConfirm={handleArchive}
                onCancel={() =>
                    setSelectedCategory(null)
                }
            />

        </div>
    );
}

export default ProductCategoryList;