import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";

import {
    getProductAttributeById
} from "../../api/product/productAttributeApi";

import "./productAttribute.css";

const ProductAttributeDetails = () => {
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
            const result =
                await getProductAttributeById(id);

            setData(result);
        } catch (err) {
            console.error(err);

            setError(
                err.response?.data?.message ||
                "Failed to load product attribute."
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

    if (!data) {
        return (
            <div className="crud-page">
                <div className="error-message">
                    {error || "Product attribute not found."}
                </div>
            </div>
        );
    }

    return (
        <div className="crud-page">

            <div className="crud-header">

                <div>
                    <h1>
                        Product Attribute Details
                    </h1>

                    <p>
                        View product attribute information.
                    </p>
                </div>

                <button
                    className="edit-button"
                    onClick={() =>
                        navigate(
                            `/dashboard/product-attributes/${data.productAttributeId}/edit`
                        )
                    }
                >
                    Edit
                </button>

            </div>

            <div className="details-card">

                <div className="details-section">

                    <h2>Attribute Information</h2>

                    <div className="details-grid">

                        <div className="detail-item">
                            <span>
                                Product Attribute ID
                            </span>

                            <strong>
                                {data.productAttributeId}
                            </strong>
                        </div>

                        <div className="detail-item">
                            <span>Product ID</span>

                            <strong>
                                {data.productId}
                            </strong>
                        </div>

                        <div className="detail-item">
                            <span>E Attribute ID</span>

                            <strong>
                                {data.eAttributeId}
                            </strong>
                        </div>

                        <div className="detail-item">
                            <span>Sort Order</span>

                            <strong>
                                {data.sortOrder}
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
                            "/dashboard/product-attributes"
                        )
                    }
                >
                    Back
                </button>

            </div>

        </div>
    );
};

export default ProductAttributeDetails;