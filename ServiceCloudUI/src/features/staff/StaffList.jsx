import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

import {
    getStaff,
    archiveStaff
} from "../../api/staff/staffApi";

import Loading from "../../components/common/Loading";
import EmptyState from "../../components/common/EmptyState";
import ConfirmDialog from "../../components/common/ConfirmDialog";

import CrudToolbar from "../../components/crud/CrudToolbar";
import DataTable from "../../components/crud/DataTable";
import Pagination from "../../components/crud/Pagination";

import "./staff.css";


function StaffList() {

    const navigate = useNavigate();


    // ============================================================
    // STATE
    // ============================================================

    const [staff, setStaff] = useState([]);

    const [loading, setLoading] = useState(true);

    const [error, setError] = useState("");

    const [search, setSearch] = useState("");

    const [currentPage, setCurrentPage] = useState(1);

    const [pageSize] = useState(10);

    const [totalPages, setTotalPages] = useState(1);

    const [totalRecords, setTotalRecords] = useState(0);


    // ============================================================
    // ARCHIVE
    // ============================================================

    const [showArchiveDialog, setShowArchiveDialog] =
        useState(false);

    const [selectedStaff, setSelectedStaff] =
        useState(null);

    const [archiveLoading, setArchiveLoading] =
        useState(false);


    // ============================================================
    // LOAD STAFF
    // ============================================================

    const loadStaff = async () => {

        try {

            setLoading(true);
            setError("");

            const response = await getStaff({
                PageNumber: currentPage,
                PageSize: pageSize,
                Search: search
            });


            const pagedResponse =
                response?.data ?? response;


            setStaff(
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
                "Failed to load staff:",
                err
            );

            setError(
                err?.response?.data?.message ||
                err?.response?.data?.Message ||
                "Failed to load staff."
            );

            setStaff([]);

        }
        finally {

            setLoading(false);

        }

    };


    // ============================================================
    // LOAD WHEN PAGE / SEARCH CHANGES
    // ============================================================

    useEffect(() => {

        loadStaff();

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

        navigate("/dashboard/staff/new");

    };


    // ============================================================
    // VIEW
    // ============================================================

    const handleView = (staffMember) => {

        const staffId =
            staffMember.staffId ??
            staffMember.StaffId;

        navigate(
            `/dashboard/staff/${staffId}`
        );

    };


    // ============================================================
    // EDIT
    // ============================================================

    const handleEdit = (staffMember) => {

        const staffId =
            staffMember.staffId ??
            staffMember.StaffId;

        navigate(
            `/dashboard/staff/${staffId}/edit`
        );

    };


    // ============================================================
    // ARCHIVE
    // ============================================================

    const handleArchive = (staffMember) => {

        setSelectedStaff(staffMember);

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

        setSelectedStaff(null);

    };


    // ============================================================
    // CONFIRM ARCHIVE
    // ============================================================

    const handleConfirmArchive = async () => {

        if (!selectedStaff) {
            return;
        }

        try {

            setArchiveLoading(true);

            const staffId =
                selectedStaff.staffId ??
                selectedStaff.StaffId;

            await archiveStaff(staffId);


            setShowArchiveDialog(false);

            setSelectedStaff(null);


            if (
                staff.length === 1 &&
                currentPage > 1
            ) {

                setCurrentPage(
                    currentPage - 1
                );

            }
            else {

                await loadStaff();

            }

        }
        catch (err) {

            console.error(
                "Failed to archive staff:",
                err
            );

            setError(
                err?.response?.data?.message ||
                err?.response?.data?.Message ||
                "Failed to archive staff."
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
            key: "staffName",
            label: "Staff",
            render: (staffMember) =>
                staffMember.staffName ??
                staffMember.StaffName ??
                "-"
        },

        {
            key: "email",
            label: "Email",
            render: (staffMember) =>
                staffMember.email ??
                staffMember.Email ??
                "-"
        },

        {
            key: "phoneNumber",
            label: "Phone",
            render: (staffMember) =>
                staffMember.phoneNumber ??
                staffMember.PhoneNumber ??
                "-"
        },

        {
            key: "isActive",
            label: "Status",
            render: (staffMember) => {

                const isActive =
                    staffMember.isActive ??
                    staffMember.IsActive;

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

    if (
        loading &&
        staff.length === 0
    ) {

        return (
            <Loading
                message="Loading staff..."
            />
        );

    }


    // ============================================================
    // ERROR
    // ============================================================

    if (
        error &&
        staff.length === 0
    ) {

        return (

            <div className="staff-page">

                <CrudToolbar
                    title="Staff"
                    description="Manage staff members available in ServiceCloud."
                    onAdd={handleAdd}
                    addButtonText="Add Staff"
                />

                <div className="staff-error">

                    <h3>
                        Unable to load staff
                    </h3>

                    <p>
                        {error}
                    </p>

                    <button
                        type="button"
                        onClick={loadStaff}
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

        <div className="staff-page">

            <CrudToolbar
                title="Staff"
                description="Manage staff members available in ServiceCloud."
                searchValue={search}
                onSearch={handleSearch}
                searchPlaceholder="Search staff..."
                onAdd={handleAdd}
                addButtonText="Add Staff"
            />


            {error && (

                <div className="staff-error-message">

                    {error}

                </div>

            )}


            {staff.length === 0 && !loading ? (

                <EmptyState
                    title="No staff found"
                    message={
                        search
                            ? "No staff members match your search."
                            : "There are no staff members available."
                    }
                />

            ) : (

                <>

                    <DataTable
                        columns={columns}
                        data={staff}
                        loading={loading}
                        getRowKey={(staffMember) =>
                            staffMember.staffId ??
                            staffMember.StaffId
                        }
                        onView={handleView}
                        onEdit={handleEdit}
                        onArchive={handleArchive}
                        emptyMessage="No staff found."
                    />


                    <div className="staff-list-footer">

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


            <ConfirmDialog
                open={showArchiveDialog}
                title="Archive Staff"
                message={
                    selectedStaff
                        ? `Are you sure you want to archive "${selectedStaff.staffName ?? selectedStaff.StaffName}"?`
                        : "Are you sure you want to archive this staff member?"
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


export default StaffList;