import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";

import {
    createProductAttributeValue,
    getProductAttributeValueById,
    updateProductAttributeValue
} from "../../api/product/productAttributeValueApi";

import "./productAttributeValue.css";

const ProductAttributeValueForm = () => {
    const { id } = useParams();
    const navigate = useNavigate();

    const isEditMode = Boolean(id);

    const [formData, setFormData] = useState({
        productAttributeValueId: "",
        productAttributeId: "",
        attributeValueId: ""
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
                await getProductAttributeValueById(id);

            setFormData({
                productAttributeValueId:
                    result.productAttributeValueId ?? "",

                productAttributeId:
                    result.productAttributeId ?? "",

                attributeValueId:
                    result.attributeValueId ?? ""
            });
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
                await updateProductAttributeValue(id, {
                    productAttributeValueId:
                        Number(
                            formData.productAttributeValueId
                        ),

                    attributeValueId:
                        Number(
                            formData.attributeValueId
                        )
                });
            } else {
                await createProductAttributeValue({
                    productAttributeValueId:
                        Number(
                            formData.productAttributeValueId
                        ),

                    productAttributeId:
                        Number(
                            formData.productAttributeId
                        ),

                    attributeValueId:
                        Number(
                            formData.attributeValueId
                        )
                });
            }

            navigate("/product-attribute-values");
        } catch (err) {
            console.error(err);

            setError(
                err.response?.data?.message ||
                "Failed to save attribute value."
            );
        }
    };

    if (loading) {
        return (
            <div className="crud-page">
                <div className="loading-message">
                    Loading...
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
                            ? "Edit Product Attribute Value"
                            : "Create Product Attribute Value"}
                    </h1>

                    <p>
                        {isEditMode
                            ? "Update attribute value information."
                            : "Add an attribute value to a product attribute."}
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
                            Product Attribute Value ID
                        </label>

                        <input
                            type="number"
                            name="productAttributeValueId"
                            value={
                                formData.productAttributeValueId
                            }
                            onChange={handleChange}
                            disabled={isEditMode}
                            required
                        />
                    </div>

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
                            Attribute Value ID
                        </label>

                        <input
                            type="number"
                            name="attributeValueId"
                            value={
                                formData.attributeValueId
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
                                "/product-attribute-values"
                            )
                        }
                    >
                        Cancel
                    </button>

                    <button
                        type="submit"
                        className="primary-button"
                    >
                        {isEditMode ? "Update" : "Create"}
                    </button>
                </div>
            </form>

        </div>
    );
};

export default ProductAttributeValueForm;