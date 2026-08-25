import React, { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { getStaffBranchById } from "../../api/staffBranch/staffBranchApi";
import Loading from "../../components/common/Loading";
import "./staffBranch.css";

const StaffBranchDetails = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const [data, setData] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchDetails = async () => {
            try {
                const response = await getStaffBranchById(id);
                setData(response.data || response.Data);
            } catch (err) {
                setError("Failed to load staff branch details.");
            } finally {
                setLoading(false);
            }
        };
        fetchDetails();
    }, [id]);

    if (loading) return <Loading />;
    if (error) return <div className="entity-error">{error}</div>;
    if (!data) return <div className="entity-error">Staff branch not found.</div>;

    const formatDate = (dateStr) => dateStr ? new Date(dateStr).toLocaleString() : "N/A";

    return (
        <div className="entity-details-page">
            <div className="entity-actions">
                <button className="secondary" onClick={() => navigate("/dashboard/staff-branches")}>Back to List</button>
                <button onClick={() => navigate(`/dashboard/staff-branches/${id}/edit`)}>Edit</button>
            </div>

            <div className="entity-details-card">
                <h3>Basic Information</h3>
                <div className="entity-details-grid">
                    <div><strong>ID:</strong> {data.staffBranchId ?? data.StaffBranchId}</div>
                    <div><strong>Staff ID:</strong> {data.staffId ?? data.StaffId}</div>
                    <div><strong>Branch ID:</strong> {data.branchId ?? data.BranchId}</div>
                    <div><strong>Role ID:</strong> {data.roleId ?? data.RoleId ?? "N/A"}</div>
                    <div><strong>Dialer Status Type ID:</strong> {data.dialerStatusTypeId ?? data.DialerStatusTypeId ?? "N/A"}</div>
                    <div><strong>Online Display Name:</strong> {data.onlineDisplayName ?? data.OnlineDisplayName ?? "N/A"}</div>
                </div>
            </div>

            <div className="entity-details-card">
                <h3>Permissions & Settings</h3>
                <div className="entity-details-grid">
                    <div><strong>Show on Scheduler:</strong> {data.showOnScheduler ?? data.ShowOnScheduler ? "Yes" : "No"}</div>
                    <div><strong>Can Do Class:</strong> {data.canDoClass ?? data.CanDoClass ? "Yes" : "No"}</div>
                    <div><strong>Can Do Service:</strong> {data.canDoService ?? data.CanDoService ? "Yes" : "No"}</div>
                    <div><strong>Can Do Service Online:</strong> {data.canDoServiceOnline ?? data.CanDoServiceOnline ? "Yes" : "No"}</div>
                    <div><strong>Can Do Course:</strong> {data.canDoCourse ?? data.CanDoCourse ? "Yes" : "No"}</div>
                    <div><strong>First Aid Allowed:</strong> {data.firstAidAllowed ?? data.FirstAidAllowed ? "Yes" : "No"}</div>
                    <div><strong>Allow Tip:</strong> {data.allowTip ?? data.AllowTip ? "Yes" : "No"}</div>
                    <div><strong>Door Access Allowed:</strong> {data.doorAccessAllowed ?? data.DoorAccessAllowed ? "Yes" : "No"}</div>
                </div>
            </div>

            <div className="entity-details-card">
                <h3>Audit Information</h3>
                <div className="entity-details-grid">
                    <div><strong>Created By:</strong> {data.createdBy ?? data.CreatedBy}</div>
                    <div><strong>Created On:</strong> {formatDate(data.createdOn ?? data.CreatedOn)}</div>
                    <div><strong>Modified By:</strong> {data.modifiedBy ?? data.ModifiedBy ?? "N/A"}</div>
                    <div><strong>Modified On:</strong> {formatDate(data.modifiedOn ?? data.ModifiedOn)}</div>
                </div>
            </div>
        </div>
    );
};

export default StaffBranchDetails;