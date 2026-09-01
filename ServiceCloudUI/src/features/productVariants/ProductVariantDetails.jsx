import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";

import {
    getProductVariantById
} from "../../api/product/productVariantApi";

import Loading from "../../components/common/Loading";

import "./productVariant.css";


function ProductVariantDetails() {

    const navigate = useNavigate();

    const { id } = useParams();

    const [variant, setVariant] = useState(null);

    const [loading, setLoading] = useState(true);

    const [error, setError] = useState("");


    // ============================================================
    // LOAD VARIANT
    // ============================================================

    useEffect(() => {

        const loadVariant = async () => {

            try {

                setLoading(true);
                setError("");

                const response =
                    await getProductVariantById(id);

                const data =
                    response?.messageData ??
                    response?.data ??
                    response;

                setVariant(data);

            }
            catch (err) {

                console.error(err);

                setError(
                    err?.response?.data?.message ||
                    err?.response?.data?.messageData ||
                    "Unable to load product variant."
                );

            }
            finally {

                setLoading(false);

            }

        };

        loadVariant();

    }, [id]);


    // ============================================================
    // LOADING
    // ============================================================

    if (loading) {

        return (
            <Loading
                message="Loading product variant..."
            />
        );

    }


    // ============================================================
    // ERROR
    // ============================================================

    if (error) {

        return (

            <div className="product-variant-details-page">

                <div className="product-error">
                    {error}
                </div>

                <button
                    type="button"
                    className="btn-secondary"
                    onClick={() =>
                        navigate("/dashboard/product-variants")
                    }
                >
                    ← Back to Product Variants
                </button>

            </div>

        );

    }


    if (!variant) {

        return (

            <div className="product-variant-details-page">

                <div className="product-error">
                    Product variant not found.
                </div>

                <button
                    type="button"
                    className="btn-secondary"
                    onClick={() =>
                        navigate("/dashboard/product-variants")
                    }
                >
                    ← Back
                </button>

            </div>

        );

    }


    return (

        <div className="product-variant-details-page">

            {/* ================================================== */}
            {/* HEADER */}
            {/* ================================================== */}

            <div className="product-page-header">

                <div>

                    <h1>
                        Product Variant Details
                    </h1>

                    <p>
                        View product variant information.
                    </p>

                </div>

                <button
                    type="button"
                    className="btn-secondary"
                    onClick={() =>
                        navigate("/dashboard/product-variants")
                    }
                >
                    ← Back
                </button>

            </div>


            {/* ================================================== */}
            {/* MAIN CARD */}
            {/* ================================================== */}

            <div className="product-details-card">

                <div className="details-header">

                    <div>

                        <h2>
                            {
                                variant.productVariantName ||
                                "Unnamed Variant"
                            }
                        </h2>

                        <p>
                            Product Variant #
                            {variant.productVariantId}
                        </p>

                    </div>


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

                </div>


                {/* ================================================== */}
                {/* BASIC INFORMATION */}
                {/* ================================================== */}

                <div className="details-section">

                    <h3>
                        Basic Information
                    </h3>


                    <div className="details-grid">

                        <div className="detail-item">

                            <span>
                                Variant ID
                            </span>

                            <strong>
                                {variant.productVariantId}
                            </strong>

                        </div>


                        <div className="detail-item">

                            <span>
                                Product ID
                            </span>

                            <strong>
                                {variant.productId}
                            </strong>

                        </div>


                        <div className="detail-item">

                            <span>
                                Variant Name
                            </span>

                            <strong>
                                {
                                    variant.productVariantName ||
                                    "-"
                                }
                            </strong>

                        </div>


                        <div className="detail-item">

                            <span>
                                Standard
                            </span>

                            <strong>
                                {
                                    variant.isStandard
                                        ? "Yes"
                                        : "No"
                                }
                            </strong>

                        </div>

                    </div>

                </div>


                {/* ================================================== */}
                {/* ATTRIBUTE INFORMATION */}
                {/* ================================================== */}

                <div className="details-section">

                    <h3>
                        Attribute Information
                    </h3>


                    <div className="details-grid">

                        <div className="detail-item">

                            <span>
                                Attribute Value IDs
                            </span>

                            <strong>
                                {
                                    variant.attributeValueIds ||
                                    "-"
                                }
                            </strong>

                        </div>


                        <div className="detail-item">

                            <span>
                                Sorted Attribute IDs
                            </span>

                            <strong>
                                {
                                    variant.sortedAttributeIds ||
                                    "-"
                                }
                            </strong>

                        </div>


                        <div className="detail-item">

                            <span>
                                Sorted Attribute Value IDs
                            </span>

                            <strong>
                                {
                                    variant.sortedAttributeValueIds ||
                                    "-"
                                }
                            </strong>

                        </div>

                    </div>

                </div>


                {/* ================================================== */}
                {/* AUDIT INFORMATION */}
                {/* ================================================== */}

                <div className="details-section">

                    <h3>
                        Audit Information
                    </h3>


                    <div className="details-grid">

                        <div className="detail-item">

                            <span>
                                Created On
                            </span>

                            <strong>
                                {
                                    variant.createdOn
                                        ? new Date(
                                            variant.createdOn
                                        ).toLocaleString()
                                        : "-"
                                }
                            </strong>

                        </div>


                        <div className="detail-item">

                            <span>
                                Created By
                            </span>

                            <strong>
                                {
                                    variant.createdBy ??
                                    "-"
                                }
                            </strong>

                        </div>


                        <div className="detail-item">

                            <span>
                                Modified On
                            </span>

                            <strong>
                                {
                                    variant.modifiedOn
                                        ? new Date(
                                            variant.modifiedOn
                                        ).toLocaleString()
                                        : "-"
                                }
                            </strong>

                        </div>


                        <div className="detail-item">

                            <span>
                                Modified By
                            </span>

                            <strong>
                                {
                                    variant.modifiedBy ??
                                    "-"
                                }
                            </strong>

                        </div>

                    </div>

                </div>


                {/* ================================================== */}
                {/* ACTIONS */}
                {/* ================================================== */}

                <div className="form-actions">

                    <button
                        type="button"
                        className="btn-secondary"
                        onClick={() =>
                            navigate(
                                "/dashboard/product-variants"
                            )
                        }
                    >
                        Back
                    </button>


                    <button
                        type="button"
                        className="btn-primary"
                        onClick={() =>
                            navigate(
                                `/product-variants/${variant.productVariantId}/edit`
                            )
                        }
                    >
                        Edit Variant
                    </button>

                </div>

            </div>

        </div>

    );

}


export default ProductVariantDetails;