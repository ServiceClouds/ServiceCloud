import { useEffect, useState } from "react";

import {
    createCurrency,
    updateCurrency
} from "../../api/currency/currencyApi";

const emptyCurrency = {
    currencyName: "",
    currencyCode: ""
};


const CurrencyForm = ({
    currency,
    onSuccess,
    onCancel
}) => {

    const [formData, setFormData] =
        useState(emptyCurrency);

    const [saving, setSaving] =
        useState(false);

    const [error, setError] =
        useState("");


    const isEdit =
        currency !== null &&
        currency !== undefined;


    useEffect(() => {

        if (!currency) {

            setFormData(
                emptyCurrency
            );

            return;

        }


        setFormData({

            currencyName:
                currency.currencyName ?? "",

            currencyCode:
                currency.currencyCode ?? ""

        });

    }, [currency]);


    const handleChange = (event) => {

        const {
            name,
            value
        } = event.target;


        setFormData(
            (previous) => ({
                ...previous,
                [name]: value
            })
        );

    };


    const handleSubmit = async (event) => {

        event.preventDefault();


        if (
            !formData.currencyName.trim()
        ) {

            setError(
                "Currency name is required."
            );

            return;

        }


        if (
            !formData.currencyCode.trim()
        ) {

            setError(
                "Currency code is required."
            );

            return;

        }


        try {

            setSaving(true);

            setError("");


            const payload = {

                currencyName:
                    formData.currencyName.trim(),

                currencyCode:
                    formData.currencyCode.trim()

            };


            if (isEdit) {

                await updateCurrency({

                    currencyId:
                        currency.currencyId,

                    ...payload

                });

            } else {

                await createCurrency(
                    payload
                );

            }


            onSuccess();


        } catch (err) {

            console.error(
                "Failed to save currency:",
                err
            );


            const message =
                err?.response?.data?.message ||
                err?.response?.data?.error ||
                "Failed to save currency.";


            setError(message);


        } finally {

            setSaving(false);

        }

    };


    return (

        <div className="currency-modal-overlay">

            <div className="currency-modal">

                <div className="currency-modal-header">

                    <div>

                        <h2>

                            {
                                isEdit
                                    ? "Edit Currency"
                                    : "Create Currency"
                            }

                        </h2>


                        <p>

                            {
                                isEdit
                                    ? "Update currency information"
                                    : "Enter currency information"
                            }

                        </p>

                    </div>


                    <button
                        type="button"
                        className="currency-close-button"
                        onClick={onCancel}
                        disabled={saving}
                    >
                        ×
                    </button>

                </div>


                {error && (

                    <div className="currency-error">

                        {error}

                    </div>

                )}


                <form
                    onSubmit={handleSubmit}
                >

                    <div className="currency-form-grid">

                        {/* Currency Name */}

                        <div className="currency-field">

                            <label htmlFor="currencyName">
                                Currency Name
                            </label>


                            <input
                                id="currencyName"
                                name="currencyName"
                                value={
                                    formData.currencyName
                                }
                                onChange={
                                    handleChange
                                }
                                maxLength={100}
                                required
                                disabled={saving}
                            />

                        </div>


                        {/* Currency Code */}

                        <div className="currency-field">

                            <label htmlFor="currencyCode">
                                Currency Code
                            </label>


                            <input
                                id="currencyCode"
                                name="currencyCode"
                                value={
                                    formData.currencyCode
                                }
                                onChange={
                                    handleChange
                                }
                                maxLength={10}
                                required
                                disabled={saving}
                            />

                        </div>

                    </div>


                    <div className="currency-form-actions">

                        <button
                            type="button"
                            onClick={onCancel}
                            disabled={saving}
                        >
                            Cancel
                        </button>


                        <button
                            type="submit"
                            disabled={saving}
                            className="currency-primary-button"
                        >

                            {
                                saving
                                    ? "Saving..."
                                    : isEdit
                                        ? "Update Currency"
                                        : "Create Currency"
                            }

                        </button>

                    </div>

                </form>

            </div>

        </div>

    );

};


export default CurrencyForm;