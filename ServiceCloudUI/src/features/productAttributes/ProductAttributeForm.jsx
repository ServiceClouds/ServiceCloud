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

    useEffect(() => {
        if (isEditMode) {
            loadData();
        }
    }, [id]);

    const loadData = async () => {
        try {
            setLoading(true);

            const result =
                await getProductAttributeById(id);

            setFormData({
                productAttributeId:
                    result.productAttributeId ?? "",

                productId:
                    result.productId ?? "",

                eAttributeId:
                    result.eAttributeId ?? "",

                sortOrder:
                    result.sortOrder ?? ""
            });

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

            if (isEditMode) {
                await updateProductAttribute(
                    id,
                    {
                        productAttributeId:
                            Number(
                                formData.productAttributeId
                            ),

                        eAttributeId:
                            Number(
                                formData.eAttributeId
                            ),

                        sortOrder:
                            Number(
                                formData.sortOrder
                            )
                    }
                );
            } else {
                await createProductAttribute({
                    productAttributeId:
                        Number(
                            formData.productAttributeId
                        ),

                    productId:
                        Number(
                            formData.productId
                        ),

                    eAttributeId:
                        Number(
                            formData.eAttributeId
                        ),

                    sortOrder:
                        Number(
                            formData.sortOrder
                        )
                });
            }

            navigate("/dashboard/product-attributes");

        } catch (err) {
            console.error(err);

            setError(
                err.response?.data?.message ||
                "Failed to save product attribute."
            );
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
                            value={
                                formData.productAttributeId
                            }
                            onChange={handleChange}
                            disabled={isEditMode}
                            required
                        />
                    </div>

                    <div className="form-group">
                        <label>
                            Product ID
                        </label>

                        <input
                            type="number"
                            name="productId"
                            value={
                                formData.productId
                            }
                            onChange={handleChange}
                            disabled={isEditMode}
                            required
                        />
                    </div>

                    <div className="form-group">
                        <label>
                            E Attribute ID
                        </label>

                        <input
                            type="number"
                            name="eAttributeId"
                            value={
                                formData.eAttributeId
                            }
                            onChange={handleChange}
                            required
                        />
                    </div>

                    <div className="form-group">
                        <label>
                            Sort Order
                        </label>

                        <input
                            type="number"
                            name="sortOrder"
                            value={
                                formData.sortOrder
                            }
                            onChange={handleChange}
                            required
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
                    >
                        Cancel
                    </button>

                    <button
                        type="submit"
                        className="primary-button"
                    >
                        {isEditMode
                            ? "Update"
                            : "Create"}
                    </button>

                </div>

            </form>

        </div>
    );
};

export default ProductAttributeForm;