import { useEffect, useState } from "react";
import {
    useNavigate,
    useParams
} from "react-router-dom";

import {
    getCountryById
} from "../../api/country/countryApi";

import Loading from "../../components/common/Loading";

import "./country.css";


function CountryDetails() {

    const {
        id
    } = useParams();

    const navigate = useNavigate();


    const [country, setCountry] =
        useState(null);

    const [loading, setLoading] =
        useState(true);

    const [error, setError] =
        useState("");


    // ============================================================
    // LOAD COUNTRY
    // ============================================================

    useEffect(() => {

        const loadCountry = async () => {

            try {

                setLoading(true);
                setError("");

                const response =
                    await getCountryById(id);

                const data =
                    response?.data ?? response;

                setCountry(data);

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

    }, [id]);


    // ============================================================
    // LOADING
    // ============================================================

    if (loading) {

        return (
            <Loading
                message="Loading country details..."
            />
        );

    }


    // ============================================================
    // ERROR
    // ============================================================

    if (error) {

        return (

            <div className="country-details-page">

                <div className="country-error">
                    {error}
                </div>

                <button
                    type="button"
                    className="btn-secondary"
                    onClick={() =>
                        navigate("/countries")
                    }
                >
                    Back to Countries
                </button>

            </div>

        );

    }


    if (!country) {

        return (

            <div className="country-details-page">

                <div className="country-error">
                    Country was not found.
                </div>

                <button
                    type="button"
                    className="btn-secondary"
                    onClick={() =>
                        navigate("/countries")
                    }
                >
                    Back to Countries
                </button>

            </div>

        );

    }


    // ============================================================
    // NORMALIZE PROPERTY NAMES
    // ============================================================

    const countryId =
        country.countryId ??
        country.CountryId;

    const countryName =
        country.countryName ??
        country.CountryName;

    const countryCode =
        country.countryCode ??
        country.CountryCode;

    const isActive =
        country.isActive ??
        country.IsActive;

    const createdOn =
        country.createdOn ??
        country.CreatedOn;

    const createdBy =
        country.createdBy ??
        country.CreatedBy;

    const modifiedOn =
        country.modifiedOn ??
        country.ModifiedOn;

    const modifiedBy =
        country.modifiedBy ??
        country.ModifiedBy;


    // ============================================================
    // DATE FORMAT
    // ============================================================

    const formatDate = (value) => {

        if (!value) {
            return "-";
        }

        const date =
            new Date(value);

        if (
            Number.isNaN(
                date.getTime()
            )
        ) {
            return value;
        }

        return date.toLocaleString();

    };


    // ============================================================
    // RENDER
    // ============================================================

    return (

        <div className="country-details-page">


            {/* ================================================== */}
            {/* HEADER */}
            {/* ================================================== */}

            <div className="country-details-header">

                <div>

                    <h2>
                        Country Details
                    </h2>

                    <p>
                        View country information.
                    </p>

                </div>


                <div className="country-details-actions">

                    <button
                        type="button"
                        className="btn-secondary"
                        onClick={() =>
                            navigate("/countries")
                        }
                    >
                        Back
                    </button>


                    <button
                        type="button"
                        className="btn-primary"
                        onClick={() =>
                            navigate(
                                `/countries/${countryId}/edit`
                            )
                        }
                    >
                        Edit
                    </button>

                </div>

            </div>


            {/* ================================================== */}
            {/* BASIC INFORMATION */}
            {/* ================================================== */}

            <div className="country-details-card">

                <div className="details-card-header">

                    <h3>
                        Basic Information
                    </h3>

                </div>


                <div className="details-grid">


                    <div className="detail-item">

                        <span className="detail-label">
                            Country ID
                        </span>

                        <strong>
                            {countryId}
                        </strong>

                    </div>


                    <div className="detail-item">

                        <span className="detail-label">
                            Country Name
                        </span>

                        <strong>
                            {countryName || "-"}
                        </strong>

                    </div>


                    <div className="detail-item">

                        <span className="detail-label">
                            Country Code
                        </span>

                        <strong>
                            {countryCode || "-"}
                        </strong>

                    </div>


                    <div className="detail-item">

                        <span className="detail-label">
                            Status
                        </span>

                        <strong>

                            <span
                                className={
                                    isActive
                                        ? "status-badge active"
                                        : "status-badge inactive"
                                }
                            >
                                {isActive
                                    ? "Active"
                                    : "Inactive"}
                            </span>

                        </strong>

                    </div>

                </div>

            </div>


            {/* ================================================== */}
            {/* AUDIT INFORMATION */}
            {/* ================================================== */}

            <div className="country-details-card">

                <div className="details-card-header">

                    <h3>
                        Audit Information
                    </h3>

                </div>


                <div className="details-grid">


                    <div className="detail-item">

                        <span className="detail-label">
                            Created On
                        </span>

                        <strong>
                            {formatDate(createdOn)}
                        </strong>

                    </div>


                    <div className="detail-item">

                        <span className="detail-label">
                            Created By
                        </span>

                        <strong>
                            {createdBy ?? "-"}
                        </strong>

                    </div>


                    <div className="detail-item">

                        <span className="detail-label">
                            Modified On
                        </span>

                        <strong>
                            {formatDate(modifiedOn)}
                        </strong>

                    </div>


                    <div className="detail-item">

                        <span className="detail-label">
                            Modified By
                        </span>

                        <strong>
                            {modifiedBy ?? "-"}
                        </strong>

                    </div>

                </div>

            </div>

        </div>

    );

}


export default CountryDetails;