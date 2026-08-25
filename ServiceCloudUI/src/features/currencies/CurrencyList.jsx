import { useEffect, useState } from "react";

import {
    getCurrencies,
    getCurrencyById,
    archiveCurrency
} from "../../api/currency/currencyApi";

import Loading from "../../components/common/Loading";
import EmptyState from "../../components/common/EmptyState";

import CurrencyForm from "./CurrencyForm";
import CurrencyDetails from "./CurrencyDetails";

import "./currency.css";

const CurrencyList = () => {

    const [currencies, setCurrencies] = useState([]);

    const [loading, setLoading] = useState(false);

    const [error, setError] = useState("");

    const [pageNumber, setPageNumber] = useState(1);

    const [pageSize] = useState(10);

    const [totalCount, setTotalCount] = useState(0);

    const [showForm, setShowForm] = useState(false);

    const [editingCurrency, setEditingCurrency] =
        useState(null);

    const [selectedCurrency, setSelectedCurrency] =
        useState(null);

    const [showDetails, setShowDetails] =
        useState(false);


    const loadCurrencies = async () => {

        try {

            setLoading(true);

            setError("");

            const response =
                await getCurrencies({
                    PageNumber: pageNumber,
                    PageSize: pageSize
                });


            /*
             * Backend returns:
             *
             * Result<PagedResponse<Currency>>
             *
             * through ToApiResponse().
             *
             * Handle both possible wrapper levels,
             * following the existing Company pattern.
             */

            const pagedResponse =
                response?.data ?? response;


            const items =
                pagedResponse?.items ??
                pagedResponse?.data ??
                [];


            setCurrencies(
                Array.isArray(items)
                    ? items
                    : []
            );


            setTotalCount(
                pagedResponse?.totalCount ??
                pagedResponse?.total ??
                0
            );


        } catch (err) {

            console.error(
                "Failed to load currencies:",
                err
            );

            setError(
                err?.response?.data?.message ||
                err?.response?.data?.error ||
                "Failed to load currencies."
            );

        } finally {

            setLoading(false);

        }

    };


    useEffect(() => {

        loadCurrencies();

    }, [pageNumber]);


    const handleAdd = () => {

        setEditingCurrency(null);

        setShowForm(true);

    };


    const handleEdit = async (currencyId) => {

        try {

            setLoading(true);

            setError("");


            const response =
                await getCurrencyById(currencyId);


            const currency =
                response?.data ?? response;


            setEditingCurrency(currency);

            setShowForm(true);


        } catch (err) {

            console.error(
                "Failed to load currency:",
                err
            );

            setError(
                err?.response?.data?.message ||
                err?.response?.data?.error ||
                "Failed to load currency."
            );

        } finally {

            setLoading(false);

        }

    };


    const handleDetails = async (currencyId) => {

        try {

            setLoading(true);

            setError("");


            const response =
                await getCurrencyById(currencyId);


            const currency =
                response?.data ?? response;


            setSelectedCurrency(currency);

            setShowDetails(true);


        } catch (err) {

            console.error(
                "Failed to load currency:",
                err
            );

            setError(
                err?.response?.data?.message ||
                err?.response?.data?.error ||
                "Failed to load currency."
            );

        } finally {

            setLoading(false);

        }

    };


    const handleArchive = async (currency) => {

        const confirmed =
            window.confirm(
                `Are you sure you want to archive "${currency.currencyName}"?`
            );


        if (!confirmed) {

            return;

        }


        try {

            setLoading(true);

            setError("");


            await archiveCurrency(
                currency.currencyId
            );


            await loadCurrencies();


        } catch (err) {

            console.error(
                "Failed to archive currency:",
                err
            );

            setError(
                err?.response?.data?.message ||
                err?.response?.data?.error ||
                "Failed to archive currency."
            );

        } finally {

            setLoading(false);

        }

    };


    const handleFormSuccess = async () => {

        setShowForm(false);

        setEditingCurrency(null);

        await loadCurrencies();

    };


    const totalPages =
        Math.max(
            1,
            Math.ceil(
                totalCount / pageSize
            )
        );


    if (
        loading &&
        currencies.length === 0
    ) {

        return <Loading />;

    }


    return (

        <div className="currency-page">

            {/* HEADER */}

            <div className="currency-header">

                <div>

                    <h1>
                        Currencies
                    </h1>

                    <p>
                        Manage currencies
                    </p>

                </div>


                <button
                    type="button"
                    className="currency-primary-button"
                    onClick={handleAdd}
                >
                    + Add Currency
                </button>

            </div>


            {/* ERROR */}

            {error && (

                <div className="currency-error">

                    {error}

                </div>

            )}


            {/* TABLE */}

            {currencies.length === 0 ? (

                <EmptyState
                    title="No currencies found"
                    message="There are no active currencies to display."
                />

            ) : (

                <div className="currency-table-container">

                    <table className="currency-table">

                        <thead>

                            <tr>

                                <th>
                                    Currency Name
                                </th>

                                <th>
                                    Currency Code
                                </th>

                                <th>
                                    Status
                                </th>

                                <th>
                                    Created On
                                </th>

                                <th>
                                    Actions
                                </th>

                            </tr>

                        </thead>


                        <tbody>

                            {currencies.map(
                                (currency) => (

                                    <tr
                                        key={
                                            currency.currencyId
                                        }
                                    >

                                        <td>
                                            {
                                                currency.currencyName ||
                                                "-"
                                            }
                                        </td>


                                        <td>
                                            {
                                                currency.currencyCode ||
                                                "-"
                                            }
                                        </td>


                                        <td>

                                            <span
                                                className={
                                                    currency.isActive
                                                        ? "currency-status active"
                                                        : "currency-status inactive"
                                                }
                                            >

                                                {
                                                    currency.isActive
                                                        ? "Active"
                                                        : "Inactive"
                                                }

                                            </span>

                                        </td>


                                        <td>

                                            {
                                                currency.createdOn
                                                    ? new Date(
                                                        currency.createdOn
                                                    ).toLocaleString()
                                                    : "-"
                                            }

                                        </td>


                                        <td>

                                            <div className="currency-actions">

                                                <button
                                                    type="button"
                                                    onClick={() =>
                                                        handleDetails(
                                                            currency.currencyId
                                                        )
                                                    }
                                                >
                                                    View
                                                </button>


                                                <button
                                                    type="button"
                                                    onClick={() =>
                                                        handleEdit(
                                                            currency.currencyId
                                                        )
                                                    }
                                                >
                                                    Edit
                                                </button>


                                                {currency.isActive && (

                                                    <button
                                                        type="button"
                                                        className="danger"
                                                        onClick={() =>
                                                            handleArchive(
                                                                currency
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


            {/* PAGINATION */}

            <div className="currency-pagination">

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


            {/* FORM */}

            {showForm && (

                <CurrencyForm
                    currency={editingCurrency}

                    onSuccess={
                        handleFormSuccess
                    }

                    onCancel={() => {

                        setShowForm(false);

                        setEditingCurrency(
                            null
                        );

                    }}
                />

            )}


            {/* DETAILS */}

            {showDetails && (

                <CurrencyDetails
                    currency={selectedCurrency}

                    onClose={() => {

                        setShowDetails(false);

                        setSelectedCurrency(
                            null
                        );

                    }}
                />

            )}

        </div>

    );

};


export default CurrencyList;