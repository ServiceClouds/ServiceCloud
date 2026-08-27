import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

import {
    getPagedProductVariantPackagings
} from "../../api/product/productVariantPackagingApi";

import "./productVariantPackaging.css";

const ProductVariantPackagingList = () => {
    const navigate = useNavigate();

    const [items, setItems] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");

    const [pageNumber, setPageNumber] = useState(1);
    const pageSize = 10;
    const [totalPages, setTotalPages] = useState(1);

    const loadData = async () => {
        try {
            setLoading(true);
            setError("");

            const result =
                await getPagedProductVariantPackagings(
                    pageNumber,
                    pageSize
                );

            setItems(result.items || []);
            setTotalPages(result.totalPages || 1);

        } catch (err) {
            console.error(err);

            setError(
                err.response?.data?.message ||
                "Failed to load product variant packagings."
            );
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        loadData();
    }, [pageNumber]);

    if (loading) {
        return (
            <div className="crud-page">
                <div className="loading-message">
                    Loading product variant packagings...
                </div>
            </div>
        );
    }

    return (
        <div className="crud-page">

            <div className="crud-header">
                <div>
                    <h1>Product Variant Packagings</h1>
                    <p>
                        Manage product variant packaging information.
                    </p>
                </div>

                <button
                    className="primary-button"
                    onClick={() =>
                        navigate(
                            "/dashboard/product-variant-packagings/new"
                        )
                    }
                >
                    + Add Packaging
                </button>
            </div>

            {error && (
                <div className="error-message">
                    {error}
                </div>
            )}

            <div className="table-container">

                <table className="crud-table">

                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Product Variant ID</th>
                            <th>Weight</th>
                            <th>Weight Unit</th>
                            <th>Dimension Unit</th>
                            <th>Length</th>
                            <th>Width</th>
                            <th>Height</th>
                            <th>Size Volume</th>
                            <th>Volume Unit</th>
                            <th>Actions</th>
                        </tr>
                    </thead>

                    <tbody>

                        {items.length === 0 ? (
                            <tr>
                                <td
                                    colSpan="11"
                                    className="empty-row"
                                >
                                    No product variant packagings found.
                                </td>
                            </tr>
                        ) : (
                            items.map((item) => (
                                <tr
                                    key={
                                        item.productVariantPackagingId
                                    }
                                >
                                    <td>
                                        {
                                            item.productVariantPackagingId
                                        }
                                    </td>

                                    <td>
                                        {item.productVariantId}
                                    </td>

                                    <td>
                                        {item.weight ?? "-"}
                                    </td>

                                    <td>
                                        {item.weightUnitId ?? "-"}
                                    </td>

                                    <td>
                                        {item.dimensionUnitId ?? "-"}
                                    </td>

                                    <td>
                                        {item.length ?? "-"}
                                    </td>

                                    <td>
                                        {item.width ?? "-"}
                                    </td>

                                    <td>
                                        {item.height ?? "-"}
                                    </td>

                                    <td>
                                        {item.sizeVolume ?? "-"}
                                    </td>

                                    <td>
                                        {item.sizeVolumeUnitId ?? "-"}
                                    </td>

                                    <td>
                                        <div className="action-buttons">

                                            <button
                                                className="view-button"
                                                onClick={() =>
                                                    navigate(
                                                        `/dashboard/product-variant-packagings/${item.productVariantPackagingId}`
                                                    )
                                                }
                                            >
                                                View
                                            </button>

                                            <button
                                                className="edit-button"
                                                onClick={() =>
                                                    navigate(
                                                        `/dashboard/product-variant-packagings/${item.productVariantPackagingId}/edit`
                                                    )
                                                }
                                            >
                                                Edit
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
                    disabled={pageNumber === 1}
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

        </div>
    );
};

export default ProductVariantPackagingList;