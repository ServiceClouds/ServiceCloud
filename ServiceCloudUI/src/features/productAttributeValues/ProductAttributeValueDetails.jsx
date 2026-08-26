import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";

import {
    getProductAttributeValueById
} from "../../api/product/productAttributeValueApi";

import "./productAttributeValue.css";

const ProductAttributeValueDetails = () => {
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
                await getProductAttributeValueById(id);

            setData(result);
        } catch (err) {
            console.error(err);

            setError(
                err.response?.data?.message ||
                "Failed to load attribute value."
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
                    {error || "Attribute value not found."}
                </div>
            </div>
        );
    }

    return (
        <div className="crud-page">

            <div className="crud-header">
                <div>
                    <h1>
                        Product Attribute Value Details
                    </h1>

                    <p>
                        View attribute value information.
                    </p>
                </div>

                <button
                    className="edit-button"
                    onClick={() =>
                        navigate(
                            `/product-attribute-values/${data.productAttributeValueId}/edit`
                        )
                    }
                >
                    Edit
                </button>
            </div>

            <div className="details-card">

                <div className="details-section">
                    <h2>Attribute Value Information</h2>

                    <div className="details-grid">

                        <div className="detail-item">
                            <span>
                                Product Attribute Value ID
                            </span>

                            <strong>
                                {
                                    data.productAttributeValueId
                                }
                            </strong>
                        </div>

                        <div className="detail-item">
                            <span>
                                Product Attribute ID
                            </span>

                            <strong>
                                {data.productAttributeId}
                            </strong>
                        </div>

                        <div className="detail-item">
                            <span>
                                Attribute Value ID
                            </span>

                            <strong>
                                {data.attributeValueId}
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
                            "/product-attribute-values"
                        )
                    }
                >
                    Back
                </button>
            </div>

        </div>
    );
};

export default ProductAttributeValueDetails;