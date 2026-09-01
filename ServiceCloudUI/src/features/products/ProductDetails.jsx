import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";

import { getProductById } from "../../api/product/productApi";

import Loading from "../../components/common/Loading";

import "./product.css";

function ProductDetails() {

    const navigate = useNavigate();

    const { id } = useParams();

    const [product, setProduct] = useState(null);

    const [loading, setLoading] = useState(true);

    const [error, setError] = useState("");

    useEffect(() => {

        const loadProduct = async () => {

            try {

                setLoading(true);

                const response =
                    await getProductById(id);

                const data =
                    response?.messageData ??
                    response?.data ??
                    response;

                setProduct(data);

            }
            catch (err) {

                console.error(err);

                setError(
                    err?.response?.data?.message ||
                    "Unable to load product."
                );

            }
            finally {

                setLoading(false);

            }
        };

        loadProduct();

    }, [id]);

    if (loading) {

        return (
            <Loading message="Loading product..." />
        );

    }

    if (error) {

        return (
            <div className="product-error">
                {error}
            </div>
        );

    }

    if (!product) {

        return (
            <div className="product-error">
                Product not found.
            </div>
        );

    }

    return (

        <div className="product-details-page">

            <div className="product-page-header">

                <div>

                    <h1>
                        {product.productName ||
                            "Unnamed Product"}
                    </h1>

                    <p>
                        Product #{product.productId}
                    </p>

                </div>

                <div className="product-header-actions">

                    <button
                        type="button"
                        className="btn-secondary"
                        onClick={() =>
                            navigate("/dashboard/products")
                        }
                    >
                        ← Back
                    </button>

                    <button
                        type="button"
                        className="btn-primary"
                        onClick={() =>
                            navigate(
                                `/products/${product.productId}/edit`
                            )
                        }
                    >
                        Edit Product
                    </button>

                </div>

            </div>

            <div className="details-grid">

                <div className="details-card">

                    <h2>Basic Information</h2>

                    <div className="detail-row">

                        <span>Product ID</span>

                        <strong>
                            {product.productId}
                        </strong>

                    </div>

                    <div className="detail-row">

                        <span>Product Name</span>

                        <strong>
                            {product.productName || "-"}
                        </strong>

                    </div>

                    <div className="detail-row">

                        <span>Category</span>

                        <strong>
                            {product.productCategoryName ||
                                product.productCategoryId ||
                                "-"}
                        </strong>

                    </div>

                    <div className="detail-row">

                        <span>Description</span>

                        <strong>
                            {product.description || "-"}
                        </strong>

                    </div>

                </div>

                <div className="details-card">

                    <h2>Product Settings</h2>

                    <div className="detail-row">

                        <span>Status</span>

                        <strong>
                            {product.isActive
                                ? "Active"
                                : "Inactive"}
                        </strong>

                    </div>

                    <div className="detail-row">

                        <span>Branch Inventory Tracking</span>

                        <strong>
                            {product.allowBranchTrackInventory
                                ? "Enabled"
                                : "Disabled"}
                        </strong>

                    </div>

                    <div className="detail-row">

                        <span>Branch Permission</span>

                        <strong>
                            {product.hasBranchPermission
                                ? "Enabled"
                                : "Disabled"}
                        </strong>

                    </div>

                    <div className="detail-row">

                        <span>Branch Price Editing</span>

                        <strong>
                            {product.allowBranchEditPrice
                                ? "Enabled"
                                : "Disabled"}
                        </strong>

                    </div>

                </div>

            </div>

        </div>

    );
}

export default ProductDetails;