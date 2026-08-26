import { useEffect, useState } from "react";

import {
    getStateCountries,
    getStateCountryById,
    archiveStateCountry
} from "../../api/stateCountry/stateCountryApi";

import Loading from "../../components/common/Loading";
import EmptyState from "../../components/common/EmptyState";

import StateCountryForm from "./StateCountryForm";
import StateCountryDetails from "./StateCountryDetails";

import "./stateCountry.css";


const StateCountryList = () => {

    const [stateCountries, setStateCountries] = useState([]);

    const [loading, setLoading] = useState(false);

    const [error, setError] = useState("");

    const [pageNumber, setPageNumber] = useState(1);

    const [pageSize] = useState(10);

    const [totalCount, setTotalCount] = useState(0);

    const [showForm, setShowForm] = useState(false);

    const [editingStateCountry, setEditingStateCountry] =
        useState(null);

    const [selectedStateCountry, setSelectedStateCountry] =
        useState(null);

    const [showDetails, setShowDetails] =
        useState(false);


    /*
    ============================================================
        LOAD STATE / COUNTRIES
    ============================================================
    */

    const loadStateCountries = async () => {

        try {

            setLoading(true);
            setError("");

            const response =
                await getStateCountries({
                    PageNumber: pageNumber,
                    PageSize: pageSize
                });


            /*
             * Backend:
             *
             * Result<PagedResponse<StateCountry>>
             *
             * The exact ApiResponse wrapper may vary,
             * so keep response extraction defensive.
             */

            const pagedResponse =
                response?.data ?? response;


            const items =
                pagedResponse?.items ??
                pagedResponse?.data ??
                [];


            setStateCountries(
                Array.isArray(items)
                    ? items
                    : []
            );


            setTotalCount(
                pagedResponse?.totalCount ??
                pagedResponse?.totalRecords ??
                pagedResponse?.total ??
                0
            );

        }
        catch (err) {

            console.error(
                "Failed to load state/countries:",
                err
            );

            setError(
                "Failed to load state/countries."
            );

        }
        finally {

            setLoading(false);

        }

    };


    useEffect(() => {

        loadStateCountries();

    }, [pageNumber]);


    /*
    ============================================================
        ADD
    ============================================================
    */

    const handleAdd = () => {

        setEditingStateCountry(null);

        setShowForm(true);

    };


    /*
    ============================================================
        EDIT
    ============================================================
    */

    const handleEdit = async (stateCountryId) => {

        try {

            setLoading(true);
            setError("");

            const response =
                await getStateCountryById(
                    stateCountryId
                );


            const stateCountry =
                response?.data ?? response;


            setEditingStateCountry(
                stateCountry
            );

            setShowForm(true);

        }
        catch (err) {

            console.error(
                "Failed to load state/country:",
                err
            );

            setError(
                "Failed to load state/country."
            );

        }
        finally {

            setLoading(false);

        }

    };


    /*
    ============================================================
        DETAILS
    ============================================================
    */

    const handleDetails = async (stateCountryId) => {

        try {

            setLoading(true);
            setError("");

            const response =
                await getStateCountryById(
                    stateCountryId
                );


            const stateCountry =
                response?.data ?? response;


            setSelectedStateCountry(
                stateCountry
            );

            setShowDetails(true);

        }
        catch (err) {

            console.error(
                "Failed to load state/country:",
                err
            );

            setError(
                "Failed to load state/country."
            );

        }
        finally {

            setLoading(false);

        }

    };


    /*
    ============================================================
        ARCHIVE
    ============================================================
    */

    const handleArchive = async (stateCountry) => {

        const confirmed =
            window.confirm(
                `Are you sure you want to archive "${stateCountry.stateCountryName}"?`
            );


        if (!confirmed) {

            return;

        }


        try {

            setLoading(true);
            setError("");

            await archiveStateCountry(
                stateCountry.stateCountryId
            );


            await loadStateCountries();

        }
        catch (err) {

            console.error(
                "Failed to archive state/country:",
                err
            );

            setError(
                "Failed to archive state/country."
            );

        }
        finally {

            setLoading(false);

        }

    };


    /*
    ============================================================
        FORM SUCCESS
    ============================================================
    */

    const handleFormSuccess = async () => {

        setShowForm(false);

        setEditingStateCountry(null);

        await loadStateCountries();

    };


    /*
    ============================================================
        PAGINATION
    ============================================================
    */

    const totalPages =
        Math.max(
            1,
            Math.ceil(
                totalCount / pageSize
            )
        );


    /*
    ============================================================
        INITIAL LOADING
    ============================================================
    */

    if (
        loading &&
        stateCountries.length === 0
    ) {

        return <Loading />;

    }


    /*
    ============================================================
        UI
    ============================================================
    */

    return (

        <div className="state-country-page">

            {/* =================================================
                HEADER
            ================================================== */}

            <div className="state-country-header">

                <div>

                    <h1>
                        States / Countries
                    </h1>

                    <p>
                        Manage states and provinces
                    </p>

                </div>


                <button
                    type="button"
                    className="state-country-primary-button"
                    onClick={handleAdd}
                >
                    + Add State / Country
                </button>

            </div>


            {/* =================================================
                ERROR
            ================================================== */}

            {error && (

                <div className="state-country-error">

                    {error}

                </div>

            )}


            {/* =================================================
                TABLE
            ================================================== */}

            {stateCountries.length === 0 ? (

                <EmptyState />

            ) : (

                <div className="state-country-table-container">

                    <table className="state-country-table">

                        <thead>

                            <tr>

                                <th>
                                    ID
                                </th>

                                <th>
                                    State / Country Name
                                </th>

                                <th>
                                    Country ID
                                </th>

                                <th>
                                    Status
                                </th>

                                <th>
                                    Actions
                                </th>

                            </tr>

                        </thead>


                        <tbody>

                            {stateCountries.map(
                                (stateCountry) => (

                                    <tr
                                        key={
                                            stateCountry.stateCountryId
                                        }
                                    >

                                        <td>
                                            {
                                                stateCountry.stateCountryId
                                            }
                                        </td>


                                        <td>
                                            {
                                                stateCountry.stateCountryName ||
                                                "-"
                                            }
                                        </td>


                                        <td>
                                            {
                                                stateCountry.countryId ??
                                                "-"
                                            }
                                        </td>


                                        <td>

                                            <span
                                                className={
                                                    stateCountry.isActive
                                                        ? "state-country-status active"
                                                        : "state-country-status inactive"
                                                }
                                            >

                                                {
                                                    stateCountry.isActive
                                                        ? "Active"
                                                        : "Inactive"
                                                }

                                            </span>

                                        </td>


                                        <td>

                                            <div className="state-country-actions">

                                                <button
                                                    type="button"
                                                    onClick={() =>
                                                        handleDetails(
                                                            stateCountry.stateCountryId
                                                        )
                                                    }
                                                >
                                                    View
                                                </button>


                                                <button
                                                    type="button"
                                                    onClick={() =>
                                                        handleEdit(
                                                            stateCountry.stateCountryId
                                                        )
                                                    }
                                                >
                                                    Edit
                                                </button>


                                                {stateCountry.isActive && (

                                                    <button
                                                        type="button"
                                                        className="danger"
                                                        onClick={() =>
                                                            handleArchive(
                                                                stateCountry
                                                            )
                                                        }
                                                    >
                                                        Archive
                                                    </button>

                                                )}

                                            </div>

                                        </td>

                                    </tr>

                                )
                            )}

                        </tbody>

                    </table>

                </div>

            )}


            {/* =================================================
                PAGINATION
            ================================================== */}

            <div className="state-country-pagination">

                <button
                    type="button"
                    disabled={
                        pageNumber <= 1 ||
                        loading
                    }
                    onClick={() =>
                        setPageNumber(
                            (current) =>
                                current - 1
                        )
                    }
                >
                    Previous
                </button>


                <span>

                    Page {pageNumber} of {totalPages}

                </span>


                <button
                    type="button"
                    disabled={
                        pageNumber >= totalPages ||
                        loading
                    }
                    onClick={() =>
                        setPageNumber(
                            (current) =>
                                current + 1
                        )
                    }
                >
                    Next
                </button>

            </div>


            {/* =================================================
                FORM
            ================================================== */}

            {showForm && (

                <StateCountryForm
                    stateCountry={
                        editingStateCountry
                    }

                    onSuccess={
                        handleFormSuccess
                    }

                    onCancel={() => {

                        setShowForm(false);

                        setEditingStateCountry(
                            null
                        );

                    }}
                />

            )}


            {/* =================================================
                DETAILS
            ================================================== */}

            {showDetails && (

                <StateCountryDetails
                    stateCountry={
                        selectedStateCountry
                    }

                    onClose={() => {

                        setShowDetails(false);

                        setSelectedStateCountry(
                            null
                        );

                    }}
                />

            )}

        </div>

    );

};


export default StateCountryList;