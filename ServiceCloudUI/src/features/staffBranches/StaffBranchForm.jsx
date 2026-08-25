import React, { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { getStaffBranchById, createStaffBranch, updateStaffBranch } from "../../api/staffBranch/staffBranchApi";
import Loading from "../../components/common/Loading";
import "./staffBranch.css";

const StaffBranchForm = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const isEditMode = Boolean(id);

    const [formData, setFormData] = useState({
        staffId: "",
        branchId: "",
        roleId: "",
        dialerStatusTypeId: "",
        onlineDisplayName: "",
        showOnScheduler: false,
        canDoClass: false,
        canDoService: false,
        canDoServiceOnline: false,
        canDoCourse: false,
        firstAidAllowed: false,
        allowTip: false,
        doorAccessAllowed: false
    });

    const [loading, setLoading] = useState(isEditMode);
    const [saving, setSaving] = useState(false);
    const [error, setError] = useState(null);

    useEffect(() => {
        if (isEditMode) {
            const fetchDetails = async () => {
                try {
                    const response = await getStaffBranchById(id);
                    const data = response.data || response.Data;
                    setFormData({
                        staffId: data.staffId ?? data.StaffId ?? "",
                        branchId: data.branchId ?? data.BranchId ?? "",
                        roleId: data.roleId ?? data.RoleId ?? "",
                        dialerStatusTypeId: data.dialerStatusTypeId ?? data.DialerStatusTypeId ?? "",
                        onlineDisplayName: data.onlineDisplayName ?? data.OnlineDisplayName ?? "",
                        showOnScheduler: data.showOnScheduler ?? data.ShowOnScheduler ?? false,
                        canDoClass: data.canDoClass ?? data.CanDoClass ?? false,
                        canDoService: data.canDoService ?? data.CanDoService ?? false,
                        canDoServiceOnline: data.canDoServiceOnline ?? data.CanDoServiceOnline ?? false,
                        canDoCourse: data.canDoCourse ?? data.CanDoCourse ?? false,
                        firstAidAllowed: data.firstAidAllowed ?? data.FirstAidAllowed ?? false,
                        allowTip: data.allowTip ?? data.AllowTip ?? false,
                        doorAccessAllowed: data.doorAccessAllowed ?? data.DoorAccessAllowed ?? false
                    });
                } catch (err) {
                    setError("Failed to load staff branch details.");
                } finally {
                    setLoading(false);
                }
            };
            fetchDetails();
        }
    }, [id, isEditMode]);

    const handleChange = (e) => {
        const { name, value, type, checked } = e.target;
        setFormData(prev => ({
            ...prev,
            [name]: type === "checkbox" ? checked : value
        }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError(null);
        setSaving(true);

        if (!formData.staffId || !formData.branchId) {
            setError("Staff ID and Branch ID are required.");
            setSaving(false);
            return;
        }

        try {
            const payload = {
                ...formData,
                staffId: Number(formData.staffId),
                branchId: Number(formData.branchId),
                roleId: formData.roleId ? Number(formData.roleId) : null,
                dialerStatusTypeId: formData.dialerStatusTypeId ? Number(formData.dialerStatusTypeId) : null
            };

            if (isEditMode) {
                await updateStaffBranch(Number(id), payload);
            } else {
                await createStaffBranch(payload);
            }
            navigate("/dashboard/staff-branches");
        } catch (err) {
            setError(err.response?.data?.message || "Failed to save staff branch.");
        } finally {
            setSaving(false);
        }
    };

    if (loading) return <Loading />;
    if (error && isEditMode) return <div className="entity-error">{error}</div>;

    return (
        <div className="entity-form-page">
            <h2>{isEditMode ? "Edit Staff Branch" : "Create Staff Branch"}</h2>
            {error && <div className="entity-error">{error}</div>}
            
            <form onSubmit={handleSubmit} className="entity-form">
                <div className="entity-details-grid">
                    {/* NOTE: If you have existing staffApi/branchApi/roleApi, replace these number inputs with <select> dropdowns */}
                    <div className="form-group">
                        <label>Staff ID *</label>
                        <input type="number" name="staffId" value={formData.staffId} onChange={handleChange} required />
                    </div>
                    <div className="form-group">
                        <label>Branch ID *</label>
                        <input type="number" name="branchId" value={formData.branchId} onChange={handleChange} required />
                    </div>
                    <div className="form-group">
                        <label>Role ID</label>
                        <input type="number" name="roleId" value={formData.roleId} onChange={handleChange} />
                    </div>
                    <div className="form-group">
                        <label>Dialer Status Type ID</label>
                        <input type="number" name="dialerStatusTypeId" value={formData.dialerStatusTypeId} onChange={handleChange} />
                    </div>
                    <div className="form-group full-width">
                        <label>Online Display Name (Max 40)</label>
                        <input type="text" name="onlineDisplayName" value={formData.onlineDisplayName} onChange={handleChange} maxLength={40} />
                    </div>
                </div>

                <div className="form-group-checkboxes">
                    <label><input type="checkbox" name="showOnScheduler" checked={formData.showOnScheduler} onChange={handleChange} /> Show on Scheduler</label>
                    <label><input type="checkbox" name="canDoClass" checked={formData.canDoClass} onChange={handleChange} /> Can Do Class</label>
                    <label><input type="checkbox" name="canDoService" checked={formData.canDoService} onChange={handleChange} /> Can Do Service</label>
                    <label><input type="checkbox" name="canDoServiceOnline" checked={formData.canDoServiceOnline} onChange={handleChange} /> Can Do Service Online</label>
                    <label><input type="checkbox" name="canDoCourse" checked={formData.canDoCourse} onChange={handleChange} /> Can Do Course</label>
                    <label><input type="checkbox" name="firstAidAllowed" checked={formData.firstAidAllowed} onChange={handleChange} /> First Aid Allowed</label>
                    <label><input type="checkbox" name="allowTip" checked={formData.allowTip} onChange={handleChange} /> Allow Tip</label>
                    <label><input type="checkbox" name="doorAccessAllowed" checked={formData.doorAccessAllowed} onChange={handleChange} /> Door Access Allowed</label>
                </div>

                <div className="form-actions">
                    <button type="button" className="secondary" onClick={() => navigate("/dashboard/staff-branches")}>Cancel</button>
                    <button type="submit" disabled={saving}>
                        {saving ? "Saving..." : (isEditMode ? "Update" : "Create")}
                    </button>
                </div>
            </form>
        </div>
    );
};

export default StaffBranchForm;
