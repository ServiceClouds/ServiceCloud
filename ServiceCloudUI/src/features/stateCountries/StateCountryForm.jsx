import { useEffect, useState } from "react";

import {
    createStateCountry,
    updateStateCountry
} from "../../api/stateCountry/stateCountryApi";


const StateCountryForm = ({
    stateCountry,
    onSuccess,
    onCancel
}) => {

    const isEdit =
        Boolean(stateCountry);


    const [formData, setFormData] = useState({

        stateCountryId:
            stateCountry?.stateCountryId ?? 0,

        stateCountryName:
            stateCountry?.stateCountryName ?? "",

        countryId:
            stateCountry?.countryId ?? ""

    });


    const [loading, setLoading] =
        useState(false);


    const [error, setError] =
        useState("");


    /*
    ============================================================
        UPDATE FORM WHEN EDITING ENTITY CHANGES
    ============================================================
    */

    useEffect(() => {

        setFormData({

            stateCountryId:
                stateCountry?.stateCountryId ?? 0,

            stateCountryName:
                stateCountry?.stateCountryName ?? "",

            countryId:
                stateCountry?.countryId ?? ""

        });

    }, [stateCountry]);


    /*
    ============================================================
        INPUT CHANGE
    ============================================================
    */

    const handleChange = (event) => {

        const {
            name,
            value
        } = event.target;


        setFormData(
            (current) => ({
                ...current,
                [name]: value
            })
        );

    };


    /*
    ============================================================
        SUBMIT
    ============================================================
    */

    const handleSubmit = async (event) => {

        event.preventDefault();

        setError("");


        const stateCountryName =
            formData.stateCountryName.trim();


        if (!stateCountryName) {

            setError(
                "State/Country name is required."
            );

            return;

        }


        if (!formData.countryId) {

            setError(
                "Country ID is required."
            );

            return;

        }


        const countryId =
            Number(formData.countryId);


        if (
            !Number.isInteger(countryId) ||
            countryId <= 0
        ) {

            setError(
                "Country ID must be a valid positive number."
            );

            return;

        }


        try {

            setLoading(true);


            if (isEdit) {

                await updateStateCountry({

                    stateCountryId:
                        formData.stateCountryId,

                    stateCountryName:
                        stateCountryName,

                    countryId:
                        countryId

                });

            }
            else {

                await createStateCountry({

                    stateCountryName:
                        stateCountryName,

                    countryId:
                        countryId

                });

            }


            onSuccess();

        }
        catch (err) {

            console.error(
                "Failed to save state/country:",
                err
            );


            /*
             * Try to display a backend validation/error
             * message when available.
             */

            const message =
                err?.response?.data?.message ??
                err?.response?.data?.error ??
                err?.response?.data?.title ??
                "Failed to save state/country.";


            setError(message);

        }
        finally {

            setLoading(false);

        }

    };


    return (

        <div className="state-country-modal-overlay">

            <div className="state-country-modal">

                {/* =================================================
                    HEADER
                ================================================== */}

                <div className="state-country-modal-header">

                    <div>

                        <h2>

                            {isEdit
                                ? "Edit State / Country"
                                : "Add State / Country"}

                        </h2>


                        <p>

                            {isEdit
                                ? "Update state or province information."
                                : "Create a new state or province."}

                        </p>

                    </div>


                    <button
                        type="button"
                        className="state-country-close-button"
                        onClick={onCancel}
                        disabled={loading}
                    >
                        ×
                    </button>

                </div>


                {/* =================================================
                    ERROR
                ================================================== */}

                {error && (

                    <div className="state-country-form-error">

                        {error}

                    </div>

                )}


                {/* =================================================
                    FORM
                ================================================== */}

                <form
                    onSubmit={handleSubmit}
                >

                    <div className="state-country-form-grid">


                        {/* =================================================
                            STATE / COUNTRY NAME
                        ================================================== */}

                        <div className="state-country-field">

                            <label htmlFor="stateCountryName">

                                State / Country Name

                            </label>


                            <input
                                id="stateCountryName"
                                name="stateCountryName"
                                type="text"
                                value={
                                    formData.stateCountryName
                                }
                                onChange={
                                    handleChange
                                }
                                placeholder="Enter state or province name"
                                maxLength={100}
                                disabled={loading}
                            />

                        </div>


                        {/* =================================================
                            COUNTRY ID
                        ================================================== */}

                        <div className="state-country-field">

                            <label htmlFor="countryId">

                                Country ID

                            </label>


                            <input
                                id="countryId"
                                name="countryId"
                                type="number"
                                min="1"
                                value={
                                    formData.countryId
                                }
                                onChange={
                                    handleChange
                                }
                                placeholder="Enter country ID"
                                disabled={loading}
                            />


                            <small>

                                Enter the ID of the active Country.

                            </small>

                        </div>

                    </div>


                    {/* =================================================
                        ACTIONS
                    ================================================== */}

                    <div className="state-country-form-actions">

                        <button
                            type="button"
                            onClick={onCancel}
                            disabled={loading}
                        >
                            Cancel
                        </button>


                        <button
                            type="submit"
                            disabled={loading}
                        >

                            {loading
                                ? "Saving..."
                                : isEdit
                                    ? "Update"
                                    : "Create"}

                        </button>

                    </div>

                </form>

            </div>

        </div>

    );

};


export default StateCountryForm;