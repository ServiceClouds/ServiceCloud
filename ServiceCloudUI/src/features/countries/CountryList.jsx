import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

import {
    getCountries,
    archiveCountry
} from "../../api/country/countryApi";

import Loading from "../../components/common/Loading";
import EmptyState from "../../components/common/EmptyState";
import ConfirmDialog from "../../components/common/ConfirmDialog";

import CrudToolbar from "../../components/crud/CrudToolbar";
import DataTable from "../../components/crud/DataTable";
import Pagination from "../../components/crud/Pagination";

import "./country.css";


function CountryList() {

    const navigate = useNavigate();


    // ============================================================
    // STATE
    // ============================================================

    const [countries, setCountries] = useState([]);

    const [loading, setLoading] = useState(true);

    const [error, setError] = useState("");

    const [search, setSearch] = useState("");

    const [currentPage, setCurrentPage] = useState(1);

    const [pageSize] = useState(10);

    const [totalPages, setTotalPages] = useState(1);

    const [totalRecords, setTotalRecords] = useState(0);


    // ============================================================
    // ARCHIVE DIALOG
    // ============================================================

    const [showArchiveDialog, setShowArchiveDialog] =
        useState(false);

    const [selectedCountry, setSelectedCountry] =
        useState(null);

    const [archiveLoading, setArchiveLoading] =
        useState(false);


    // ============================================================
    // LOAD COUNTRIES
    // ============================================================

    const loadCountries = async () => {

        try {

            setLoading(true);
            setError("");

            const response = await getCountries({
                PageNumber: currentPage,
                PageSize: pageSize,
                Search: search
            });


            /*
             * Your backend returns:
             *
             * PagedResponse<Country>
             *
             * containing:
             *
             * Items
             * PageNumber
             * PageSize
             * TotalRecords
             * TotalPages
             *
             * The API response is currently kept flexible
             * because your ToApiResponse() wrapper has not
             * been shown yet.
             */

            const pagedResponse =
                response?.data ?? response;


            setCountries(
                pagedResponse?.items ??
                pagedResponse?.Items ??
                []
            );

            setTotalPages(
                pagedResponse?.totalPages ??
                pagedResponse?.TotalPages ??
                1
            );

            setTotalRecords(
                pagedResponse?.totalRecords ??
                pagedResponse?.TotalRecords ??
                0
            );


        }
        catch (err) {

            console.error(
                "Failed to load countries:",
                err
            );

            setError(
                err?.response?.data?.message ||
                err?.response?.data?.Message ||
                "Failed to load countries."
            );

            setCountries([]);

        }
        finally {

            setLoading(false);

        }

    };


    // ============================================================
    // LOAD WHEN PAGE / SEARCH CHANGES
    // ============================================================

    useEffect(() => {

        loadCountries();

    }, [currentPage, pageSize, search]);


    // ============================================================
    // SEARCH
    // ============================================================

    const handleSearch = (value) => {

        setSearch(value);

        setCurrentPage(1);

    };


    // ============================================================
    // CREATE
    // ============================================================

    const handleAdd = () => {

        navigate("/dashboard/countries/new");

    };


    // ============================================================
    // VIEW
    // ============================================================

    const handleView = (country) => {

        navigate(
            `/dashboard/countries/${country.countryId ?? country.CountryId}`
        );

    };


    // ============================================================
    // EDIT
    // ============================================================

    const handleEdit = (country) => {

        navigate(
            `/dashboard/countries/${country.countryId ?? country.CountryId}/edit`
        );

    };


    // ============================================================
    // OPEN ARCHIVE DIALOG
    // ============================================================

    const handleArchive = (country) => {

        setSelectedCountry(country);

        setShowArchiveDialog(true);

    };


    // ============================================================
    // CANCEL ARCHIVE
    // ============================================================

    const handleCancelArchive = () => {

        if (archiveLoading) {
            return;
        }

        setShowArchiveDialog(false);

        setSelectedCountry(null);

    };


    // ============================================================
    // CONFIRM ARCHIVE
    // ============================================================

    const handleConfirmArchive = async () => {

        if (!selectedCountry) {
            return;
        }

        try {

            setArchiveLoading(true);

            const countryId =
                selectedCountry.countryId ??
                selectedCountry.CountryId;

            await archiveCountry(countryId);


            setShowArchiveDialog(false);

            setSelectedCountry(null);


            /*
             * If the current page becomes empty after
             * archiving its last record, move to the
             * previous page.
             */

            if (
                countries.length === 1 &&
                currentPage > 1
            ) {

                setCurrentPage(
                    currentPage - 1
                );

            }
            else {

                await loadCountries();

            }


        }
        catch (err) {

            console.error(
                "Failed to archive country:",
                err
            );

            setError(
                err?.response?.data?.message ||
                err?.response?.data?.Message ||
                "Failed to archive country."
            );

        }
        finally {

            setArchiveLoading(false);

        }

    };


    // ============================================================
    // TABLE COLUMNS
    // ============================================================

    const columns = [

        {
            key: "countryName",
            label: "Country",
            render: (country) =>
                country.countryName ??
                country.CountryName ??
                "-"
        },

        {
            key: "countryCode",
            label: "Code",
            render: (country) =>
                country.countryCode ??
                country.CountryCode ??
                "-"
        },

        {
            key: "isActive",
            label: "Status",
            render: (country) => {

                const isActive =
                    country.isActive ??
                    country.IsActive;

                return (
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
                );

            }
        }

    ];


    // ============================================================
    // LOADING
    // ============================================================

    if (loading && countries.length === 0) {

        return (
            <Loading
                message="Loading countries..."
            />
        );

    }


    // ============================================================
    // ERROR
    // ============================================================

    if (
        error &&
        countries.length === 0
    ) {

        return (
            <div className="country-page">

                <CrudToolbar
                    title="Countries"
                    description="Manage countries available in ServiceCloud."
                    onAdd={handleAdd}
                    addButtonText="Add Country"
                />

                <div className="country-error">

                    <h3>
                        Unable to load countries
                    </h3>

                    <p>
                        {error}
                    </p>

                    <button
                        type="button"
                        onClick={loadCountries}
                    >
                        Try Again
                    </button>

                </div>

            </div>
        );

    }


    // ============================================================
    // RENDER
    // ============================================================

    return (

        <div className="country-page">


            {/* ================================================== */}
            {/* HEADER / TOOLBAR */}
            {/* ================================================== */}

            <CrudToolbar
                title="Countries"
                description="Manage countries available in ServiceCloud."
                searchValue={search}
                onSearch={handleSearch}
                searchPlaceholder="Search countries..."
                onAdd={handleAdd}
                addButtonText="Add Country"
            />


            {/* ================================================== */}
            {/* ERROR */}
            {/* ================================================== */}

            {error && (
                <div className="country-error-message">
                    {error}
                </div>
            )}


            {/* ================================================== */}
            {/* TABLE */}
            {/* ================================================== */}

            {countries.length === 0 && !loading ? (

                <EmptyState
                    title="No countries found"
                    message={
                        search
                            ? "No countries match your search."
                            : "There are no countries available."
                    }
                />

            ) : (

                <>

                    <DataTable
                        columns={columns}
                        data={countries}
                        loading={loading}
                        getRowKey={(country) =>
                            country.countryId ??
                            country.CountryId
                        }
                        onView={handleView}
                        onEdit={handleEdit}
                        onArchive={handleArchive}
                        emptyMessage="No countries found."
                    />


                    {/* ========================================== */}
                    {/* TABLE FOOTER */}
                    {/* ========================================== */}

                    <div className="country-list-footer">

                        <span>
                            Total records: {totalRecords}
                        </span>

                        <Pagination
                            currentPage={currentPage}
                            totalPages={totalPages}
                            onPageChange={setCurrentPage}
                            disabled={loading}
                        />

                    </div>

                </>

            )}


            {/* ================================================== */}
            {/* ARCHIVE CONFIRMATION */}
            {/* ================================================== */}

            <ConfirmDialog
                open={showArchiveDialog}
                title="Archive Country"
                message={
                    selectedCountry
                        ? `Are you sure you want to archive "${selectedCountry.countryName ?? selectedCountry.CountryName}"?`
                        : "Are you sure you want to archive this country?"
                }
                confirmText="Archive"
                cancelText="Cancel"
                loading={archiveLoading}
                onConfirm={handleConfirmArchive}
                onCancel={handleCancelArchive}
            />

        </div>

    );

}


export default CountryList;