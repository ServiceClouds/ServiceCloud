import { useEffect, useState } from "react";

import {
    getCompanies,
    getCompanyById,
    archiveCompany
} from "../../api/company/companyApi";

import Loading from "../../components/common/Loading";
import EmptyState from "../../components/common/EmptyState";

import CompanyForm from "./CompanyForm";
import CompanyDetails from "./CompanyDetails";

import "./company.css";

const CompanyList = () => {

    const [companies, setCompanies] = useState([]);

    const [loading, setLoading] = useState(false);

    const [error, setError] = useState("");

    const [pageNumber, setPageNumber] = useState(1);

    const [pageSize] = useState(10);

    const [totalCount, setTotalCount] = useState(0);

    const [showForm, setShowForm] = useState(false);

    const [editingCompany, setEditingCompany] =
        useState(null);

    const [selectedCompany, setSelectedCompany] =
        useState(null);

    const [showDetails, setShowDetails] =
        useState(false);

    const loadCompanies = async () => {

        try {

            setLoading(true);
            setError("");

            const response = await getCompanies({
                PageNumber: pageNumber,
                PageSize: pageSize
            });

            /*
             * Your backend returns:
             *
             * Result<PagedResponse<Company>>
             *
             * through:
             *
             * result.ToApiResponse()
             *
             * The exact JSON wrapper was not included,
             * so this section is isolated here.
             */

            const pagedResponse =
                response?.data ?? response;

            const items =
                pagedResponse?.items ??
                pagedResponse?.data ??
                [];

            setCompanies(
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
                "Failed to load companies:",
                err
            );

            setError(
                "Failed to load companies."
            );

        } finally {

            setLoading(false);

        }
    };

    useEffect(() => {

        loadCompanies();

    }, [pageNumber]);

    const handleAdd = () => {

        setEditingCompany(null);

        setShowForm(true);

    };

    const handleEdit = async (companyId) => {

        try {

            setLoading(true);
            setError("");

            const response =
                await getCompanyById(companyId);

            const company =
                response?.data ?? response;

            setEditingCompany(company);

            setShowForm(true);

        } catch (err) {

            console.error(
                "Failed to load company:",
                err
            );

            setError(
                "Failed to load company."
            );

        } finally {

            setLoading(false);

        }
    };

    const handleDetails = async (companyId) => {

        try {

            setLoading(true);
            setError("");

            const response =
                await getCompanyById(companyId);

            const company =
                response?.data ?? response;

            setSelectedCompany(company);

            setShowDetails(true);

        } catch (err) {

            console.error(
                "Failed to load company:",
                err
            );

            setError(
                "Failed to load company."
            );

        } finally {

            setLoading(false);

        }
    };

    const handleArchive = async (company) => {

        const confirmed = window.confirm(
            `Are you sure you want to archive "${company.companyName}"?`
        );

        if (!confirmed) {
            return;
        }

        try {

            setLoading(true);
            setError("");

            await archiveCompany(
                company.companyId
            );

            await loadCompanies();

        } catch (err) {

            console.error(
                "Failed to archive company:",
                err
            );

            setError(
                "Failed to archive company."
            );

        } finally {

            setLoading(false);

        }
    };

    const handleFormSuccess = async () => {

        setShowForm(false);

        setEditingCompany(null);

        await loadCompanies();

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
        companies.length === 0
    ) {
        return <Loading />;
    }

    return (

        <div className="company-page">

            {/* HEADER */}

            <div className="company-header">

                <div>

                    <h1>
                        Companies
                    </h1>

                    <p>
                        Manage companies
                    </p>

                </div>

                <button
                    className="company-primary-button"
                    onClick={handleAdd}
                >
                    + Add Company
                </button>

            </div>


            {/* ERROR */}

            {error && (

                <div className="company-error">
                    {error}
                </div>

            )}


            {/* TABLE */}

            {companies.length === 0 ? (

                <EmptyState />

            ) : (

                <div className="company-table-container">

                    <table className="company-table">

                        <thead>

                            <tr>

                                <th>
                                    Company Name
                                </th>

                                <th>
                                    Code
                                </th>

                                <th>
                                    Email
                                </th>

                                <th>
                                    Phone
                                </th>

                                <th>
                                    City
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

                            {companies.map(
                                (company) => (

                                    <tr
                                        key={
                                            company.companyId
                                        }
                                    >

                                        <td>
                                            {
                                                company.companyName ||
                                                "-"
                                            }
                                        </td>

                                        <td>
                                            {
                                                company.companyCode
                                            }
                                        </td>

                                        <td>
                                            {
                                                company.email ||
                                                "-"
                                            }
                                        </td>

                                        <td>
                                            {
                                                company.phone ||
                                                "-"
                                            }
                                        </td>

                                        <td>
                                            {
                                                company.cityName ||
                                                "-"
                                            }
                                        </td>

                                        <td>

                                            <span
                                                className={
                                                    company.isActive
                                                        ? "company-status active"
                                                        : "company-status inactive"
                                                }
                                            >
                                                {
                                                    company.isActive
                                                        ? "Active"
                                                        : "Inactive"
                                                }
                                            </span>

                                        </td>

                                        <td>

                                            <div className="company-actions">

                                                <button
                                                    onClick={() =>
                                                        handleDetails(
                                                            company.companyId
                                                        )
                                                    }
                                                >
                                                    View
                                                </button>

                                                <button
                                                    onClick={() =>
                                                        handleEdit(
                                                            company.companyId
                                                        )
                                                    }
                                                >
                                                    Edit
                                                </button>

                                                {company.isActive && (

                                                    <button
                                                        className="danger"
                                                        onClick={() =>
                                                            handleArchive(
                                                                company
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

            <div className="company-pagination">

                <button
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

                <CompanyForm
                    company={editingCompany}
                    onSuccess={
                        handleFormSuccess
                    }
                    onCancel={() => {

                        setShowForm(false);

                        setEditingCompany(
                            null
                        );

                    }}
                />

            )}


            {/* DETAILS */}

            {showDetails && (

                <CompanyDetails
                    company={selectedCompany}
                    onClose={() => {

                        setShowDetails(false);

                        setSelectedCompany(
                            null
                        );

                    }}
                />

            )}

        </div>

    );
};

export default CompanyList;