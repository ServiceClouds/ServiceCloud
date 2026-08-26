import { useEffect, useState } from "react";

import {
    getBranches,
    getBranchById,
    archiveBranch
} from "../../api/branch/branchApi";

import Loading from "../../components/common/Loading";
import EmptyState from "../../components/common/EmptyState";

import BranchForm from "./BranchForm";
import BranchDetails from "./BranchDetails";

import "./branch.css";


const BranchList = () => {

    const [branches, setBranches] = useState([]);

    const [loading, setLoading] = useState(false);

    const [error, setError] = useState("");

    const [pageNumber, setPageNumber] = useState(1);

    const [pageSize] = useState(10);

    const [totalRecords, setTotalRecords] = useState(0);

    const [showForm, setShowForm] = useState(false);

    const [editingBranch, setEditingBranch] = useState(null);

    const [selectedBranch, setSelectedBranch] = useState(null);

    const [showDetails, setShowDetails] = useState(false);


    /*
    ============================================================
        LOAD BRANCHES
    ============================================================
    */

    const loadBranches = async () => {

        try {

            setLoading(true);
            setError("");

            const response =
                await getBranches({
                    PageNumber: pageNumber,
                    PageSize: pageSize
                });


            /*
             * Depending on your ApiResponse wrapper,
             * the paged response may be inside response.data.
             */

            const pagedResponse =
                response?.data ?? response;


            const items =
                pagedResponse?.items ??
                pagedResponse?.data ??
                [];


            setBranches(
                Array.isArray(items)
                    ? items
                    : []
            );


            setTotalRecords(
                pagedResponse?.totalRecords ??
                pagedResponse?.totalCount ??
                pagedResponse?.total ??
                0
            );

        }
        catch (err) {

            console.error(
                "Failed to load branches:",
                err
            );

            setError(
                "Failed to load branches."
            );

        }
        finally {

            setLoading(false);

        }

    };


    useEffect(() => {

        loadBranches();

    }, [pageNumber]);


    /*
    ============================================================
        ADD
    ============================================================
    */

    const handleAdd = () => {

        setEditingBranch(null);

        setShowForm(true);

    };


    /*
    ============================================================
        EDIT
    ============================================================
    */

    const handleEdit = async (branchId) => {

        try {

            setLoading(true);
            setError("");

            const response =
                await getBranchById(
                    branchId
                );


            const branch =
                response?.data ?? response;


            setEditingBranch(branch);

            setShowForm(true);

        }
        catch (err) {

            console.error(
                "Failed to load branch:",
                err
            );

            setError(
                "Failed to load branch."
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

    const handleDetails = async (branchId) => {

        try {

            setLoading(true);
            setError("");

            const response =
                await getBranchById(
                    branchId
                );


            const branch =
                response?.data ?? response;


            setSelectedBranch(branch);

            setShowDetails(true);

        }
        catch (err) {

            console.error(
                "Failed to load branch:",
                err
            );

            setError(
                "Failed to load branch."
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

    const handleArchive = async (branch) => {

        const confirmed =
            window.confirm(
                `Are you sure you want to archive "${branch.branchName || branch.branchCode}"?`
            );


        if (!confirmed) {

            return;

        }


        try {

            setLoading(true);
            setError("");

            await archiveBranch(
                branch.branchId
            );


            await loadBranches();

        }
        catch (err) {

            console.error(
                "Failed to archive branch:",
                err
            );

            setError(
                "Failed to archive branch."
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

        setEditingBranch(null);

        await loadBranches();

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
                totalRecords / pageSize
            )
        );


    /*
    ============================================================
        INITIAL LOADING
    ============================================================
    */

    if (
        loading &&
        branches.length === 0
    ) {

        return <Loading />;

    }


    return (

        <div className="branch-page">

            {/* =================================================
                HEADER
            ================================================== */}

            <div className="branch-header">

                <div>

                    <h1>
                        Branches
                    </h1>

                    <p>
                        Manage company branches
                    </p>

                </div>


                <button
                    type="button"
                    className="branch-primary-button"
                    onClick={handleAdd}
                >
                    + Add Branch
                </button>

            </div>


            {/* =================================================
                ERROR
            ================================================== */}

            {error && (

                <div className="branch-error">

                    {error}

                </div>

            )}


            {/* =================================================
                TABLE
            ================================================== */}

            {branches.length === 0 ? (

                <EmptyState />

            ) : (

                <div className="branch-table-container">

                    <table className="branch-table">

                        <thead>

                            <tr>

                                <th>
                                    ID
                                </th>

                                <th>
                                    Branch
                                </th>

                                <th>
                                    Code
                                </th>

                                <th>
                                    City
                                </th>

                                <th>
                                    Country
                                </th>

                                <th>
                                    Contact
                                </th>

                                <th>
                                    Online
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

                            {branches.map(
                                (branch) => (

                                    <tr
                                        key={
                                            branch.branchId
                                        }
                                    >

                                        <td>
                                            {
                                                branch.branchId
                                            }
                                        </td>


                                        <td>

                                            <strong>

                                                {
                                                    branch.branchName ||
                                                    "-"
                                                }

                                            </strong>

                                        </td>


                                        <td>
                                            {
                                                branch.branchCode
                                            }
                                        </td>


                                        <td>
                                            {
                                                branch.cityName ||
                                                "-"
                                            }
                                        </td>


                                        <td>
                                            {
                                                branch.countryId ??
                                                "-"
                                            }
                                        </td>


                                        <td>

                                            {
                                                branch.email ||
                                                branch.phone ||
                                                branch.mobile ||
                                                "-"
                                            }

                                        </td>


                                        <td>

                                            <span
                                                className={
                                                    branch.isOnline
                                                        ? "branch-online online"
                                                        : "branch-online offline"
                                                }
                                            >

                                                {
                                                    branch.isOnline
                                                        ? "Online"
                                                        : "Offline"
                                                }

                                            </span>

                                        </td>


                                        <td>

                                            <span
                                                className={
                                                    branch.isActive
                                                        ? "branch-status active"
                                                        : "branch-status inactive"
                                                }
                                            >

                                                {
                                                    branch.isActive
                                                        ? "Active"
                                                        : "Inactive"
                                                }

                                            </span>

                                        </td>


                                        <td>

                                            <div className="branch-actions">

                                                <button
                                                    type="button"
                                                    onClick={() =>
                                                        handleDetails(
                                                            branch.branchId
                                                        )
                                                    }
                                                >
                                                    View
                                                </button>


                                                <button
                                                    type="button"
                                                    onClick={() =>
                                                        handleEdit(
                                                            branch.branchId
                                                        )
                                                    }
                                                >
                                                    Edit
                                                </button>


                                                {branch.isActive && (

                                                    <button
                                                        type="button"
                                                        className="danger"
                                                        onClick={() =>
                                                            handleArchive(
                                                                branch
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

            <div className="branch-pagination">

                <button
                    type="button"
                    disabled={
                        pageNumber <= 1 ||
                        loading
                    }
                    onClick={() =>
                        setPageNumber(
                            current =>
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
                            current =>
                                current + 1
                        )
                    }
                >
                    Next
                </button>

            </div>


            {/* =================================================
                FORM MODAL
            ================================================== */}

            {showForm && (

                <BranchForm
                    branch={editingBranch}

                    onSuccess={
                        handleFormSuccess
                    }

                    onCancel={() => {

                        setShowForm(false);

                        setEditingBranch(null);

                    }}
                />

            )}


            {/* =================================================
                DETAILS MODAL
            ================================================== */}

            {showDetails && (

                <BranchDetails
                    branch={selectedBranch}

                    onClose={() => {

                        setShowDetails(false);

                        setSelectedBranch(null);

                    }}
                />

            )}

        </div>

    );

};


export default BranchList;