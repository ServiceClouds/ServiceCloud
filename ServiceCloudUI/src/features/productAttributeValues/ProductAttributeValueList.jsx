import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

import {
    getPagedProductAttributeValues
} from "../../api/product/productAttributeValueApi";

import "./productAttributeValue.css";

const ProductAttributeValueList = () => {
    const navigate = useNavigate();

    const [items, setItems] = useState([]);
    const [pageNumber, setPageNumber] = useState(1);
    const [totalPages, setTotalPages] = useState(1);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");

    const pageSize = 10;

    const loadData = async () => {
        try {
            setLoading(true);
            setError("");

            const result =
                await getPagedProductAttributeValues(
                    pageNumber,
                    pageSize
                );

            setItems(result.items || []);
            setTotalPages(result.totalPages || 1);
        } catch (err) {
            console.error(err);

            setError(
                err.response?.data?.message ||
                "Failed to load product attribute values."
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
                    Loading product attribute values...
                </div>
            </div>
        );
    }

    return (
        <div className="crud-page">

            <div className="crud-header">
                <div>
                    <h1>Product Attribute Values</h1>
                    <p>
                        Manage product attribute values.
                    </p>
                </div>

                <button
                    className="primary-button"
                    onClick={() =>
                        navigate(
                            "/product-attribute-values/new"
                        )
                    }
                >
                    + Add Attribute Value
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
                            <th>Product Attribute ID</th>
                            <th>Attribute Value ID</th>
                            <th>Actions</th>
                        </tr>
                    </thead>

                    <tbody>
                        {items.length === 0 ? (
                            <tr>
                                <td
                                    colSpan="4"
                                    className="empty-row"
                                >
                                    No attribute values found.
                                </td>
                            </tr>
                        ) : (
                            items.map((item) => (
                                <tr
                                    key={
                                        item.productAttributeValueId
                                    }
                                >
                                    <td>
                                        {
                                            item.productAttributeValueId
                                        }
                                    </td>

                                    <td>
                                        {item.productAttributeId}
                                    </td>

                                    <td>
                                        {item.attributeValueId}
                                    </td>

                                    <td>
                                        <div className="action-buttons">
                                            <button
                                                className="view-button"
                                                onClick={() =>
                                                    navigate(
                                                        `/product-attribute-values/${item.productAttributeValueId}`
                                                    )
                                                }
                                            >
                                                View
                                            </button>

                                            <button
                                                className="edit-button"
                                                onClick={() =>
                                                    navigate(
                                                        `/product-attribute-values/${item.productAttributeValueId}/edit`
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
                        setPageNumber(pageNumber - 1)
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
                        setPageNumber(pageNumber + 1)
                    }
                >
                    Next
                </button>
            </div>

        </div>
    );
};

export default ProductAttributeValueList;