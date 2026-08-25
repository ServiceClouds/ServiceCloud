import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import {
    getProductCategoryById
} from "../../api/product/productCategoryApi";

import Loading from "../../components/common/Loading";

import "./productCategory.css";

function ProductCategoryDetails() {

    const navigate = useNavigate();

    const { id } = useParams();

    const [category, setCategory] = useState(null);

    const [loading, setLoading] = useState(true);

    const [error, setError] = useState("");

    useEffect(() => {

        const loadCategory = async () => {

            try {

                setLoading(true);
                setError("");

                const response =
                    await getProductCategoryById(id);

                const data =
                    response?.messageData ??
                    response?.data ??
                    response;

                setCategory(data);

            }
            catch (err) {

                console.error(err);

                setError(
                    err?.response?.data?.message ||
                    "Unable to load product category."
                );

            }
            finally {

                setLoading(false);

            }
        };

        loadCategory();

    }, [id]);

    if (loading) {

        return (
            <Loading
                message="Loading product category..."
            />
        );

    }

    if (error) {

        return (
            <div className="product-category-page">

                <div className="product-category-error">
                    {error}
                </div>

                <button
                    type="button"
                    className="btn-secondary"
                    onClick={() =>
                        navigate("/product-categories")
                    }
                >
                    ← Back
                </button>

            </div>
        );

    }

    if (!category) {
        return null;
    }

    return (

        <div className="product-category-details-page">

            <div className="product-category-page-header">

                <div>

                    <h1>
                        Product Category Details
                    </h1>

                    <p>
                        View product category information.
                    </p>

                </div>

                <button
                    type="button"
                    className="btn-secondary"
                    onClick={() =>
                        navigate("/product-categories")
                    }
                >
                    ← Back
                </button>

            </div>

            <div className="product-category-details-card">

                <div className="detail-item">

                    <span className="detail-label">
                        Category ID
                    </span>

                    <span className="detail-value">
                        #{category.productCategoryId}
                    </span>

                </div>

                <div className="detail-item">

                    <span className="detail-label">
                        Category Name
                    </span>

                    <span className="detail-value">
                        {
                            category.productCategoryName ||
                            "-"
                        }
                    </span>

                </div>

                <div className="detail-item">

                    <span className="detail-label">
                        Description
                    </span>

                    <span className="detail-value">
                        {
                            category.description ||
                            "-"
                        }
                    </span>

                </div>

                <div className="detail-item">

                    <span className="detail-label">
                        Image Path
                    </span>

                    <span className="detail-value">
                        {
                            category.imagePath ||
                            "-"
                        }
                    </span>

                </div>

                <div className="detail-item">

                    <span className="detail-label">
                        Branch Permission
                    </span>

                    <span className="detail-value">

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

                    </span>

                </div>

                <div className="detail-item">

                    <span className="detail-label">
                        App Source Type
                    </span>

                    <span className="detail-value">
                        {
                            category.appSourceTypeId ??
                            "-"
                        }
                    </span>

                </div>

            </div>

            <div className="form-actions">

                <button
                    type="button"
                    className="btn-secondary"
                    onClick={() =>
                        navigate(
                            `/product-categories/${id}/edit`
                        )
                    }
                >
                    Edit Category
                </button>

            </div>

        </div>

    );
}

export default ProductCategoryDetails;