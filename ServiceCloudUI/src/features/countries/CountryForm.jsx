import { useEffect, useState } from "react";
import {
    useNavigate,
    useParams
} from "react-router-dom";

import {
    getCountryById,
    createCountry,
    updateCountry
} from "../../api/country/countryApi";

import Loading from "../../components/common/Loading";

import "./country.css";


function CountryForm() {

    const navigate = useNavigate();

    const { id } = useParams();

    const isEditMode = Boolean(id);


    // ============================================================
    // FORM STATE
    // ============================================================

    const [formData, setFormData] = useState({
        countryName: "",
        countryCode: ""
    });


    const [loading, setLoading] = useState(
        isEditMode
    );

    const [saving, setSaving] = useState(false);

    const [error, setError] = useState("");

    const [validationErrors, setValidationErrors] =
        useState({});


    // ============================================================
    // LOAD COUNTRY FOR EDIT
    // ============================================================

    useEffect(() => {

        if (!isEditMode) {
            return;
        }

        const loadCountry = async () => {

            try {

                setLoading(true);
                setError("");

                const response =
                    await getCountryById(id);

                /*
                 * Supports common API response shapes:
                 *
                 * response.data
                 * response
                 */

                const country =
                    response?.data ?? response;

                setFormData({
                    countryName:
                        country?.countryName ??
                        country?.CountryName ??
                        "",

                    countryCode:
                        country?.countryCode ??
                        country?.CountryCode ??
                        ""
                });

            }
            catch (err) {

                console.error(
                    "Failed to load country:",
                    err
                );

                setError(
                    err?.response?.data?.message ||
                    err?.response?.data?.Message ||
                    "Failed to load country."
                );

            }
            finally {

                setLoading(false);

            }

        };

        loadCountry();

    }, [id, isEditMode]);


    // ============================================================
    // INPUT CHANGE
    // ============================================================

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


        // Remove validation error
        // when user starts correcting field.

        setValidationErrors(
            (previous) => ({
                ...previous,
                [name]: ""
            })
        );

    };


    // ============================================================
    // VALIDATION
    // ============================================================

    const validate = () => {

        const errors = {};


        if (
            !formData.countryName.trim()
        ) {

            errors.countryName =
                "Country name is required.";

        }
        else if (
            formData.countryName.trim().length > 100
        ) {

            errors.countryName =
                "Country name cannot exceed 100 characters.";

        }


        if (
            formData.countryCode &&
            formData.countryCode.length > 10
        ) {

            errors.countryCode =
                "Country code cannot exceed 10 characters.";

        }


        setValidationErrors(errors);

        return Object.keys(errors).length === 0;

    };


    // ============================================================
    // SUBMIT
    // ============================================================

    const handleSubmit = async (event) => {

        event.preventDefault();

        setError("");


        if (!validate()) {
            return;
        }


        try {

            setSaving(true);


            const payload = {
                CountryName:
                    formData.countryName.trim(),

                CountryCode:
                    formData.countryCode.trim() || null
            };


            if (isEditMode) {

                await updateCountry(
                    Number(id),
                    payload
                );

            }
            else {

                await createCountry(
                    payload
                );

            }


            // Return to Country list
            navigate("/countries");

        }
        catch (err) {

            console.error(
                "Failed to save country:",
                err
            );


            const responseData =
                err?.response?.data;


            /*
             * Handle common API validation formats.
             */

            if (
                responseData?.errors
            ) {

                setValidationErrors(
                    responseData.errors
                );

            }


            setError(
                responseData?.message ||
                responseData?.Message ||
                "Failed to save country."
            );

        }
        finally {

            setSaving(false);

        }

    };


    // ============================================================
    // CANCEL
    // ============================================================

    const handleCancel = () => {

        if (saving) {
            return;
        }

        navigate("/countries");

    };


    // ============================================================
    // LOADING
    // ============================================================

    if (loading) {

        return (
            <Loading
                message="Loading country..."
            />
        );

    }


    // ============================================================
    // ERROR WHILE LOADING
    // ============================================================

    if (
        isEditMode &&
        error &&
        !formData.countryName
    ) {

        return (

            <div className="country-form-page">

                <div className="country-form-header">

                    <div>

                        <h2>
                            Edit Country
                        </h2>

                        <p>
                            Unable to load the country.
                        </p>

                    </div>

                </div>


                <div className="country-error">

                    {error}

                </div>


                <button
                    type="button"
                    className="btn-secondary"
                    onClick={handleCancel}
                >
                    Back to Countries
                </button>

            </div>

        );

    }


    // ============================================================
    // RENDER
    // ============================================================

    return (

        <div className="country-form-page">


            {/* ================================================== */}
            {/* HEADER */}
            {/* ================================================== */}

            <div className="country-form-header">

                <div>

                    <h2>
                        {isEditMode
                            ? "Edit Country"
                            : "Add Country"}
                    </h2>

                    <p>
                        {isEditMode
                            ? "Update country information."
                            : "Create a new country."}
                    </p>

                </div>

            </div>


            {/* ================================================== */}
            {/* ERROR */}
            {/* ================================================== */}

            {error && (
                <div className="country-error">
                    {error}
                </div>
            )}


            {/* ================================================== */}
            {/* FORM */}
            {/* ================================================== */}

            <form
                className="country-form"
                onSubmit={handleSubmit}
            >


                {/* ============================================== */}
                {/* COUNTRY NAME */}
                {/* ============================================== */}

                <div className="form-group">

                    <label htmlFor="countryName">
                        Country Name
                        <span className="required">
                            *
                        </span>
                    </label>

                    <input
                        id="countryName"
                        name="countryName"
                        type="text"
                        value={
                            formData.countryName
                        }
                        onChange={handleChange}
                        placeholder="Enter country name"
                        maxLength={100}
                        disabled={saving}
                    />

                    {validationErrors.countryName && (
                        <span className="field-error">
                            {
                                validationErrors.countryName
                            }
                        </span>
                    )}

                </div>


                {/* ============================================== */}
                {/* COUNTRY CODE */}
                {/* ============================================== */}

                <div className="form-group">

                    <label htmlFor="countryCode">
                        Country Code
                    </label>

                    <input
                        id="countryCode"
                        name="countryCode"
                        type="text"
                        value={
                            formData.countryCode
                        }
                        onChange={handleChange}
                        placeholder="e.g. PK"
                        maxLength={10}
                        disabled={saving}
                    />

                    {validationErrors.countryCode && (
                        <span className="field-error">
                            {
                                validationErrors.countryCode
                            }
                        </span>
                    )}

                </div>


                {/* ============================================== */}
                {/* ACTIONS */}
                {/* ============================================== */}

                <div className="country-form-actions">

                    <button
                        type="button"
                        className="btn-secondary"
                        onClick={handleCancel}
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
                                ? "Update Country"
                                : "Create Country"}

                    </button>

                </div>

            </form>

        </div>

    );

}


export default CountryForm;