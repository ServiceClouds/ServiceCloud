import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";

import {
    createProductAttribute,
    getProductAttributeById,
    updateProductAttribute
} from "../../api/product/productAttributeApi";

import "./productAttribute.css";

const ProductAttributeForm = () => {
    const { id } = useParams();
    const navigate = useNavigate();

    const isEditMode = Boolean(id);

    const [formData, setFormData] = useState({
        productAttributeId: "",
        productId: "",
        eAttributeId: "",
        sortOrder: ""
    });

    const [loading, setLoading] = useState(false);
    const [error, setError] = useState("");
    const [isSubmitting, setIsSubmitting] = useState(false);

    useEffect(() => {
        let isMounted = true;

        if (isEditMode) {
            const loadData = async () => {
                try {
                    setLoading(true);
                    setError("");

                    const result = await getProductAttributeById(id);

                    if (isMounted) {
                        setFormData({
                            productAttributeId: result.productAttributeId ?? "",
                            productId: result.productId ?? "",
                            eAttributeId: result.eAttributeId ?? "",
                            sortOrder: result.sortOrder ?? ""
                        });
                    }

                } catch (err) {
                    console.error(err);

                    if (isMounted) {
                        setError(
                            err.response?.data?.message ||
                            "Failed to load product attribute."
                        );
                    }
                } finally {
                    if (isMounted) {
                        setLoading(false);
                    }
                }
            };

            loadData();
        }

        return () => {
            isMounted = false;
        };
    }, [id]);

    const handleChange = (event) => {
        const { name, value } = event.target;

        setFormData({
            ...formData,
            [name]: value
        });
    };

    const handleSubmit = async (event) => {
        event.preventDefault();

        try {
            setError("");
            setIsSubmitting(true);

            // Validate fields
            const productAttributeId = Number(formData.productAttributeId);
            const productId = Number(formData.productId);
            const eAttributeId = Number(formData.eAttributeId);
            const sortOrder = Number(formData.sortOrder);

            if (isNaN(productAttributeId) || isNaN(productId) || 
                isNaN(eAttributeId) || isNaN(sortOrder)) {
                setError('All fields must be valid numbers');
                return;
            }

            if (productAttributeId < 0 || productId < 0 || 
                eAttributeId < 0 || sortOrder < 0) {
                setError('All fields must be positive numbers');
                return;
            }

            if (isEditMode) {
                await updateProductAttribute(id, {
                    productAttributeId,
                    productId,
                    eAttributeId,
                    sortOrder
                });
            } else {
                await createProductAttribute({
                    productAttributeId,
                    productId,
                    eAttributeId,
                    sortOrder
                });
            }

            navigate("/dashboard/product-attributes");

        } catch (err) {
            console.error(err);

            setError(
                err.response?.data?.message ||
                "Failed to save product attribute."
            );
        } finally {
            setIsSubmitting(false);
        }
    };

    if (loading) {
        return (
            <div className="crud-page">
                <div className="loading-message">
                    Loading product attribute...
                </div>
            </div>
        );
    }

    if (error && isEditMode && !loading) {
        return (
            <div className="crud-page">
                <div className="error-message">
                    {error}
                </div>
                <button
                    className="secondary-button"
                    onClick={() => navigate("/dashboard/product-attributes")}
                >
                    Back to List
                </button>
            </div>
        );
    }

    return (
        <div className="crud-page">

            <div className="crud-header">
                <div>
                    <h1>
                        {isEditMode
                            ? "Edit Product Attribute"
                            : "Create Product Attribute"}
                    </h1>

                    <p>
                        {isEditMode
                            ? "Update product attribute information."
                            : "Add an attribute to a product."}
                    </p>
                </div>
            </div>

            {error && (
                <div className="error-message">
                    {error}
                </div>
            )}

            <form
                className="crud-form"
                onSubmit={handleSubmit}
            >

                <div className="form-grid">

                    <div className="form-group">
                        <label>
                            Product Attribute ID
                        </label>

                        <input
                            type="number"
                            name="productAttributeId"
                            value={formData.productAttributeId}
                            onChange={handleChange}
                            disabled={isEditMode}
                            required
                            min="0"
                        />
                    </div>

                    <div className="form-group">
                        <label>
                            Product ID
                        </label>

                        <input
                            type="number"
                            name="productId"
                            value={formData.productId}
                            onChange={handleChange}
                            disabled={isEditMode}
                            required
                            min="0"
                        />
                    </div>

                    <div className="form-group">
                        <label>
                            E Attribute ID
                        </label>

                        <input
                            type="number"
                            name="eAttributeId"
                            value={formData.eAttributeId}
                            onChange={handleChange}
                            required
                            min="0"
                        />
                    </div>

                    <div className="form-group">
                        <label>
                            Sort Order
                        </label>

                        <input
                            type="number"
                            name="sortOrder"
                            value={formData.sortOrder}
                            onChange={handleChange}
                            required
                            min="0"
                        />
                    </div>

                </div>

                <div className="form-actions">

                    <button
                        type="button"
                        className="secondary-button"
                        onClick={() =>
                            navigate(
                                "/dashboard/product-attributes"
                            )
                        }
                        disabled={isSubmitting}
                    >
                        Cancel
                    </button>

                    <button
                        type="submit"
                        className="primary-button"
                        disabled={isSubmitting}
                    >
                        {isSubmitting ? 'Saving...' : (isEditMode ? 'Update' : 'Create')}
                    </button>

                </div>

            </form>

        </div>
    );
};

export default ProductAttributeForm;