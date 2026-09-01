import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

import {
    getPagedProductAttributes
} from "../../api/product/productAttributeApi";

import "./productAttribute.css";

const ProductAttributeList = () => {
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
                await getPagedProductAttributes(
                    pageNumber,
                    pageSize
                );

            setItems(result.items || []);
            setTotalPages(result.totalPages || 1);

        } catch (err) {
            console.error(err);

            setError(
                err.response?.data?.message ||
                "Failed to load product attributes."
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
                    Loading product attributes...
                </div>
            </div>
        );
    }

    return (
        <div className="crud-page">

            <div className="crud-header">
                <div>
                    <h1>Product Attributes</h1>
                    <p>
                        Manage product attribute information.
                    </p>
                </div>

                <button
                    className="primary-button"
                    onClick={() =>
                        navigate(
                            "/dashboard/product-attributes/new"
                        )
                    }
                >
                    + Add Attribute
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
                            <th>Attribute ID</th>
                            <th>Product ID</th>
                            <th>Attribute ID</th>
                            <th>Sort Order</th>
                            <th>Actions</th>
                        </tr>
                    </thead>

                    <tbody>

                        {items.length === 0 ? (
                            <tr>
                                <td
                                    colSpan="5"
                                    className="empty-row"
                                >
                                    No product attributes found.
                                </td>
                            </tr>
                        ) : (
                            items.map((item) => (
                                <tr
                                    key={
                                        item.productAttributeId
                                    }
                                >
                                    <td>
                                        {
                                            item.productAttributeId
                                        }
                                    </td>

                                    <td>
                                        {item.productId}
                                    </td>

                                    <td>
                                        {item.eAttributeId}
                                    </td>

                                    <td>
                                        {item.sortOrder}
                                    </td>

                                    <td>
                                        <div className="action-buttons">

                                            <button
                                                className="view-button"
                                                onClick={() =>
                                                    navigate(
                                                        `/dashboard/product-attributes/${item.productAttributeId}`
                                                    )
                                                }
                                            >
                                                View
                                            </button>

                                            <button
                                                className="edit-button"
                                                onClick={() =>
                                                    navigate(
                                                        `/dashboard/product-attributes/${item.productAttributeId}/edit`
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
                            pageNumber - 1
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
                            pageNumber + 1
                        )
                    }
                >
                    Next
                </button>

            </div>

        </div>
    );
};

export default ProductAttributeList;