import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";

import {
    createProductVariantPackaging,
    getProductVariantPackagingById,
    updateProductVariantPackaging
} from "../../api/product/productVariantPackagingApi";

import "./productVariantPackaging.css";

const ProductVariantPackagingForm = () => {
    const { id } = useParams();
    const navigate = useNavigate();

    const isEditMode = Boolean(id);

    const [formData, setFormData] = useState({
        productVariantPackagingId: "",
        productVariantId: "",
        weight: "",
        weightUnitId: "",
        dimensionUnitId: "",
        length: "",
        width: "",
        height: "",
        sizeVolume: "",
        sizeVolumeUnitId: ""
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
                await getProductVariantPackagingById(id);

            setFormData({
                productVariantPackagingId:
                    result.productVariantPackagingId ?? "",

                productVariantId:
                    result.productVariantId ?? "",

                weight: result.weight ?? "",
                weightUnitId: result.weightUnitId ?? "",
                dimensionUnitId: result.dimensionUnitId ?? "",
                length: result.length ?? "",
                width: result.width ?? "",
                height: result.height ?? "",
                sizeVolume: result.sizeVolume ?? "",
                sizeVolumeUnitId:
                    result.sizeVolumeUnitId ?? ""
            });

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

    const handleChange = (event) => {
        const { name, value } = event.target;

        setFormData((previous) => ({
            ...previous,
            [name]: value
        }));
    };

    const numberOrNull = (value) =>
        value === "" ? null : Number(value);

    const handleSubmit = async (event) => {
        event.preventDefault();

        try {
            setError("");

            if (isEditMode) {

                await updateProductVariantPackaging(
                    id,
                    {
                        productVariantPackagingId:
                            Number(
                                formData.productVariantPackagingId
                            ),

                        weight:
                            numberOrNull(formData.weight),

                        weightUnitId:
                            numberOrNull(
                                formData.weightUnitId
                            ),

                        dimensionUnitId:
                            numberOrNull(
                                formData.dimensionUnitId
                            ),

                        length:
                            numberOrNull(formData.length),

                        width:
                            numberOrNull(formData.width),

                        height:
                            numberOrNull(formData.height),

                        sizeVolume:
                            numberOrNull(
                                formData.sizeVolume
                            ),

                        sizeVolumeUnitId:
                            numberOrNull(
                                formData.sizeVolumeUnitId
                            )
                    }
                );

            } else {

                await createProductVariantPackaging({
                    productVariantPackagingId:
                        Number(
                            formData.productVariantPackagingId
                        ),

                    productVariantId:
                        Number(
                            formData.productVariantId
                        ),

                    weight:
                        numberOrNull(formData.weight),

                    weightUnitId:
                        numberOrNull(
                            formData.weightUnitId
                        ),

                    dimensionUnitId:
                        numberOrNull(
                            formData.dimensionUnitId
                        ),

                    length:
                        numberOrNull(formData.length),

                    width:
                        numberOrNull(formData.width),

                    height:
                        numberOrNull(formData.height),

                    sizeVolume:
                        numberOrNull(
                            formData.sizeVolume
                        ),

                    sizeVolumeUnitId:
                        numberOrNull(
                            formData.sizeVolumeUnitId
                        )
                });
            }

            navigate(
                "/dashboard/product-variant-packagings"
            );

        } catch (err) {
            console.error(err);

            setError(
                err.response?.data?.message ||
                "Failed to save packaging."
            );
        }
    };

    if (loading) {
        return (
            <div className="crud-page">
                <div className="loading-message">
                    Loading packaging...
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
                            ? "Edit Product Variant Packaging"
                            : "Create Product Variant Packaging"}
                    </h1>

                    <p>
                        {isEditMode
                            ? "Update packaging information."
                            : "Add packaging information for a product variant."}
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
                            Packaging ID
                        </label>

                        <input
                            type="number"
                            name="productVariantPackagingId"
                            value={
                                formData.productVariantPackagingId
                            }
                            onChange={handleChange}
                            disabled={isEditMode}
                            required
                        />
                    </div>

                    <div className="form-group">
                        <label>
                            Product Variant ID
                        </label>

                        <input
                            type="number"
                            name="productVariantId"
                            value={
                                formData.productVariantId
                            }
                            onChange={handleChange}
                            disabled={isEditMode}
                            required
                        />
                    </div>

                    <div className="form-group">
                        <label>Weight</label>
                        <input
                            type="number"
                            step="0.01"
                            name="weight"
                            value={formData.weight}
                            onChange={handleChange}
                        />
                    </div>

                    <div className="form-group">
                        <label>Weight Unit ID</label>
                        <input
                            type="number"
                            name="weightUnitId"
                            value={formData.weightUnitId}
                            onChange={handleChange}
                        />
                    </div>

                    <div className="form-group">
                        <label>Dimension Unit ID</label>
                        <input
                            type="number"
                            name="dimensionUnitId"
                            value={formData.dimensionUnitId}
                            onChange={handleChange}
                        />
                    </div>

                    <div className="form-group">
                        <label>Length</label>
                        <input
                            type="number"
                            step="0.01"
                            name="length"
                            value={formData.length}
                            onChange={handleChange}
                        />
                    </div>

                    <div className="form-group">
                        <label>Width</label>
                        <input
                            type="number"
                            step="0.01"
                            name="width"
                            value={formData.width}
                            onChange={handleChange}
                        />
                    </div>

                    <div className="form-group">
                        <label>Height</label>
                        <input
                            type="number"
                            step="0.01"
                            name="height"
                            value={formData.height}
                            onChange={handleChange}
                        />
                    </div>

                    <div className="form-group">
                        <label>Size Volume</label>
                        <input
                            type="number"
                            step="0.01"
                            name="sizeVolume"
                            value={formData.sizeVolume}
                            onChange={handleChange}
                        />
                    </div>

                    <div className="form-group">
                        <label>Size Volume Unit ID</label>
                        <input
                            type="number"
                            name="sizeVolumeUnitId"
                            value={formData.sizeVolumeUnitId}
                            onChange={handleChange}
                        />
                    </div>

                </div>

                <div className="form-actions">

                    <button
                        type="button"
                        className="secondary-button"
                        onClick={() =>
                            navigate(
                                "/dashboard/product-variant-packagings"
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

export default ProductVariantPackagingForm;