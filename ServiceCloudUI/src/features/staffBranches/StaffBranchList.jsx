import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { getStaffBranches, archiveStaffBranch } from "../../api/staffBranch/staffBranchApi";
import Loading from "../../components/common/Loading";
import EmptyState from "../../components/common/EmptyState";
import ConfirmDialog from "../../components/common/ConfirmDialog";
import CrudToolbar from "../../components/crud/CrudToolbar";
import DataTable from "../../components/crud/DataTable";
import Pagination from "../../components/crud/Pagination";
import "./staffBranch.css";

const StaffBranchList = () => {
    const navigate = useNavigate();
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [pagination, setPagination] = useState({
        pageNumber: 1,
        pageSize: 10,
        search: ""
    });
    const [confirmDialog, setConfirmDialog] = useState({
        isOpen: false,
        itemId: null,
        itemName: ""
    });

    const fetchData = async () => {
        setLoading(true);
        setError(null);
        try {
            const response = await getStaffBranches(pagination);
            const result = response.data || response.Data;
            setData(result.items || result.Items || []);
            setPagination(prev => ({
                ...prev,
                totalRecords: result.totalRecords || result.TotalRecords || 0,
                totalPages: result.totalPages || result.TotalPages || 1
            }));
        } catch (err) {
            setError("Failed to load staff branches.");
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchData();
    }, [pagination.pageNumber, pagination.pageSize]);

    const handleSearch = (searchTerm) => {
        setPagination(prev => ({ ...prev, search: searchTerm, pageNumber: 1 }));
    };

    const handleArchiveClick = (item) => {
        setConfirmDialog({
            isOpen: true,
            itemId: item.staffBranchId || item.StaffBranchId,
            itemName: `Staff: ${item.staffId || item.StaffId} | Branch: ${item.branchId || item.BranchId}`
        });
    };

    const confirmArchive = async () => {
        try {
            await archiveStaffBranch(confirmDialog.itemId);
            setConfirmDialog({ isOpen: false, itemId: null, itemName: "" });
            fetchData();
        } catch (err) {
            alert("Failed to archive staff branch.");
        }
    };

    if (loading && data.length === 0) return <Loading />;
    if (error) return <div className="entity-error">{error}</div>;

    const columns = [
        { header: "ID", accessor: (row) => row.staffBranchId || row.StaffBranchId },
        { header: "Display Name", accessor: (row) => (row.onlineDisplayName || row.OnlineDisplayName) || "-" },
        { header: "Staff ID", accessor: (row) => row.staffId || row.StaffId },
        { header: "Branch ID", accessor: (row) => row.branchId || row.BranchId },
        { 
            header: "Actions", 
            accessor: (row) => (
                <div className="entity-actions">
                    <button onClick={() => navigate(`/dashboard/staff-branches/${row.staffBranchId || row.StaffBranchId}`)}>View</button>
                    <button onClick={() => navigate(`/dashboard/staff-branches/${row.staffBranchId || row.StaffBranchId}/edit`)}>Edit</button>
                    <button className="danger" onClick={() => handleArchiveClick(row)}>Archive</button>
                </div>
            )
        }
    ];

    return (
        <div className="entity-page">
            <CrudToolbar 
                title="Staff Branches"
                searchPlaceholder="Search by display name..."
                onSearch={handleSearch}
                onAdd={() => navigate("/dashboard/staff-branches/new")}
            />
            
            {data.length === 0 ? (
                <EmptyState message="No staff branches found." />
            ) : (
                <>
                    <DataTable columns={columns} data={data} />
                    <Pagination 
                        currentPage={pagination.pageNumber}
                        totalPages={pagination.totalPages}
                        totalRecords={pagination.totalRecords}
                        pageSize={pagination.pageSize}
                        onPageChange={(page) => setPagination(prev => ({ ...prev, pageNumber: page }))}
                        onPageSizeChange={(size) => setPagination(prev => ({ ...prev, pageSize: size, pageNumber: 1 }))}
                    />
                </>
            )}

            <ConfirmDialog
                isOpen={confirmDialog.isOpen}
                title="Archive Staff Branch"
                message={`Are you sure you want to archive this staff branch assignment?`}
                onConfirm={confirmArchive}
                onCancel={() => setConfirmDialog({ isOpen: false, itemId: null, itemName: "" })}
            />
        </div>
    );
};

export default StaffBranchList;