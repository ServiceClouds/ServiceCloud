import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";

import {
    getProductVariantPackagingById
} from "../../api/product/productVariantPackagingApi";

import "./productVariantPackaging.css";

const ProductVariantPackagingDetails = () => {
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
                await getProductVariantPackagingById(id);

            setData(result);
        } catch (err) {
            console.error(err);

            setError(
                err.response?.data?.message ||
                "Failed to load packaging."
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

    if (error || !data) {
        return (
            <div className="crud-page">
                <div className="error-message">
                    {error || "Packaging not found."}
                </div>
            </div>
        );
    }

    return (
        <div className="crud-page">

            <div className="crud-header">

                <div>
                    <h1>
                        Product Variant Packaging Details
                    </h1>

                    <p>
                        View packaging information.
                    </p>
                </div>

                <button
                    className="edit-button"
                    onClick={() =>
                        navigate(
                            `/product-variant-packagings/${data.productVariantPackagingId}/edit`
                        )
                    }
                >
                    Edit
                </button>

            </div>

            <div className="details-card">

                <div className="details-section">

                    <h2>Basic Information</h2>

                    <div className="details-grid">

                        <div className="detail-item">
                            <span>Packaging ID</span>
                            <strong>
                                {data.productVariantPackagingId}
                            </strong>
                        </div>

                        <div className="detail-item">
                            <span>Product Variant ID</span>
                            <strong>
                                {data.productVariantId}
                            </strong>
                        </div>

                    </div>

                </div>

                <div className="details-section">

                    <h2>Weight & Dimensions</h2>

                    <div className="details-grid">

                        <div className="detail-item">
                            <span>Weight</span>
                            <strong>
                                {data.weight ?? "-"}
                            </strong>
                        </div>

                        <div className="detail-item">
                            <span>Weight Unit ID</span>
                            <strong>
                                {data.weightUnitId ?? "-"}
                            </strong>
                        </div>

                        <div className="detail-item">
                            <span>Dimension Unit ID</span>
                            <strong>
                                {data.dimensionUnitId ?? "-"}
                            </strong>
                        </div>

                        <div className="detail-item">
                            <span>Length</span>
                            <strong>
                                {data.length ?? "-"}
                            </strong>
                        </div>

                        <div className="detail-item">
                            <span>Width</span>
                            <strong>
                                {data.width ?? "-"}
                            </strong>
                        </div>

                        <div className="detail-item">
                            <span>Height</span>
                            <strong>
                                {data.height ?? "-"}
                            </strong>
                        </div>

                    </div>

                </div>

                <div className="details-section">

                    <h2>Volume</h2>

                    <div className="details-grid">

                        <div className="detail-item">
                            <span>Size Volume</span>
                            <strong>
                                {data.sizeVolume ?? "-"}
                            </strong>
                        </div>

                        <div className="detail-item">
                            <span>Size Volume Unit ID</span>
                            <strong>
                                {data.sizeVolumeUnitId ?? "-"}
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
                            "/product-variant-packagings"
                        )
                    }
                >
                    Back
                </button>

            </div>

        </div>
    );
};

export default ProductVariantPackagingDetails;