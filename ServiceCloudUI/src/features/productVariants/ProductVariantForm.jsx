import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";

import {
    createProductVariant,
    getProductVariantById,
    updateProductVariant
} from "../../api/product/productVariantApi";

import "./productVariant.css";


const initialForm = {
    productVariantId: "",

    productId: "",
    productVariantName: "",
    attributeValueIds: "",
    isStandard: false,
    sortedAttributeIds: "",
    sortedAttributeValueIds: ""
};


function ProductVariantForm() {

    const navigate = useNavigate();

    const { id } = useParams();

    const isEditMode = Boolean(id);

    const [form, setForm] = useState(initialForm);

    const [loading, setLoading] = useState(false);

    const [saving, setSaving] = useState(false);

    const [error, setError] = useState("");


    // ============================================================
    // LOAD VARIANT
    // ============================================================

    useEffect(() => {

        if (!isEditMode) {
            return;
        }

        const loadVariant = async () => {

            try {

                setLoading(true);
                setError("");

                const response =
                    await getProductVariantById(id);

                const variant =
                    response?.messageData ??
                    response?.data ??
                    response;

                setForm({

                    productVariantId:
                        variant?.productVariantId ?? id,

                    productId:
                        variant?.productId ?? "",

                    productVariantName:
                        variant?.productVariantName ?? "",

                    attributeValueIds:
                        variant?.attributeValueIds ?? "",

                    isStandard:
                        variant?.isStandard ?? false,

                    sortedAttributeIds:
                        variant?.sortedAttributeIds ?? "",

                    sortedAttributeValueIds:
                        variant?.sortedAttributeValueIds ?? ""

                });

            }
            catch (err) {

                console.error(err);

                setError(
                    err?.response?.data?.message ||
                    "Unable to load product variant."
                );

            }
            finally {

                setLoading(false);

            }

        };

        loadVariant();

    }, [id, isEditMode]);


    // ============================================================
    // CHANGE HANDLER
    // ============================================================

    const handleChange = (event) => {

        const {
            name,
            value,
            type,
            checked
        } = event.target;

        setForm((previous) => ({
            ...previous,

            [name]:
                type === "checkbox"
                    ? checked
                    : value
        }));

    };


    // ============================================================
    // SUBMIT
    // ============================================================

    const handleSubmit = async (event) => {

        event.preventDefault();

        try {

            setSaving(true);
            setError("");

            const payload = {
 productVariantId: Number(form.productVariantId),
                productId:
                    Number(form.productId),

                productVariantName:
                    form.productVariantName,

                attributeValueIds:
                    form.attributeValueIds || null,

                isStandard:
                    form.isStandard,

                sortedAttributeIds:
                    form.sortedAttributeIds || null,

                sortedAttributeValueIds:
                    form.sortedAttributeValueIds || null

            };


            if (isEditMode) {

                await updateProductVariant(
                    Number(id),
                    {
                        productVariantId:
                            Number(id),

                        ...payload
                    }
                );

            }
            else {

                await createProductVariant(
                    payload
                );

            }

            navigate("/dashboard/product-variants");

        }
        catch (err) {

            console.error(err);

            setError(
                err?.response?.data?.message ||
                err?.response?.data?.messageData ||
                "Unable to save product variant."
            );

        }
        finally {

            setSaving(false);

        }

    };


    // ============================================================
    // LOADING
    // ============================================================

    if (loading) {

        return (
            <div className="product-variant-form-page">
                <p>Loading product variant...</p>
            </div>
        );

    }


    return (

        <div className="product-variant-form-page">

            {/* ================================================== */}
            {/* HEADER */}
            {/* ================================================== */}

            <div className="product-page-header">

                <div>

                    <h1>
                        {isEditMode
                            ? "Edit Product Variant"
                            : "Create Product Variant"}
                    </h1>

                    <p>
                        {isEditMode
                            ? "Update product variant information."
                            : "Add a new variant to your product."}
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
            {/* ERROR */}
            {/* ================================================== */}

            {error && (

                <div className="product-error">
                    {error}
                </div>

            )}


            {/* ================================================== */}
            {/* FORM */}
            {/* ================================================== */}

            <form
                className="product-form-card"
                onSubmit={handleSubmit}
            >

                <div className="form-section">

                    <h2>Variant Information</h2>

                    <div className="form-grid">

                        <div className="form-group">
                            <div className="form-group">

                                <label>
                                    Product Variant ID
                                    <span>*</span>
                                </label>

                                <input
                                    type="number"
                                    name="productVariantId"
                                    value={form.productVariantId}
                                    onChange={handleChange}
                                    required
                                    min="1"
                                    placeholder="Enter Variant ID"
                                />

                            </div>

                            <label>
                                Product ID
                                <span>*</span>
                            </label>

                            <input
                                type="number"
                                name="productId"
                                value={form.productId}
                                onChange={handleChange}
                                required
                                min="1"
                                placeholder="Enter Product ID"
                            />

                        </div>


                        <div className="form-group">

                            <label>
                                Variant Name
                                <span>*</span>
                            </label>

                            <input
                                type="text"
                                name="productVariantName"
                                value={
                                    form.productVariantName
                                }
                                onChange={handleChange}
                                required
                                maxLength={200}
                                placeholder="Enter variant name"
                            />

                        </div>

                    </div>


                    <div className="form-group">

                        <label>
                            Attribute Value IDs
                        </label>

                        <input
                            type="text"
                            name="attributeValueIds"
                            value={
                                form.attributeValueIds
                            }
                            onChange={handleChange}
                            placeholder="Example: 1,2,3"
                        />

                    </div>


                    <div className="form-group">

                        <label>
                            Sorted Attribute IDs
                        </label>

                        <input
                            type="text"
                            name="sortedAttributeIds"
                            value={
                                form.sortedAttributeIds
                            }
                            onChange={handleChange}
                            placeholder="Example: 1,2,3"
                        />

                    </div>


                    <div className="form-group">

                        <label>
                            Sorted Attribute Value IDs
                        </label>

                        <input
                            type="text"
                            name="sortedAttributeValueIds"
                            value={
                                form.sortedAttributeValueIds
                            }
                            onChange={handleChange}
                            placeholder="Example: 10,20,30"
                        />

                    </div>


                    <div className="checkbox-grid">

                        <label className="checkbox-option">

                            <input
                                type="checkbox"
                                name="isStandard"
                                checked={
                                    form.isStandard
                                }
                                onChange={handleChange}
                            />

                            <span>
                                Standard Variant
                            </span>

                        </label>

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
                        disabled={saving}
                    >
                        Cancel
                    </button>

                    <button
                        type="submit"
                        className="btn-primary"
                        disabled={saving}
                    >
                        {saving
                            ? "Saving..."
                            : isEditMode
                                ? "Update Variant"
                                : "Create Variant"}
                    </button>

                </div>

            </form>

        </div>

    );

}


export default ProductVariantForm;