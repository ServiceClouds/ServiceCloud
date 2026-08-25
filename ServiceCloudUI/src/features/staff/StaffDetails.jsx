import { useEffect, useState } from "react";
import {
    useNavigate,
    useParams
} from "react-router-dom";

import {
    getStaffById
} from "../../api/staff/staffApi";

import Loading from "../../components/common/Loading";

import "./staff.css";


function StaffDetails() {

    const {
        id
    } = useParams();

    const navigate = useNavigate();


    // ============================================================
    // STATE
    // ============================================================

    const [staff, setStaff] =
        useState(null);

    const [loading, setLoading] =
        useState(true);

    const [error, setError] =
        useState("");


    // ============================================================
    // LOAD STAFF
    // ============================================================

    useEffect(() => {

        const loadStaff = async () => {

            try {

                setLoading(true);

                setError("");


                const response =
                    await getStaffById(id);


                const data =
                    response?.data ??
                    response;


                setStaff(data);

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

            }
            finally {

                setLoading(false);

            }

        };


        loadStaff();

    }, [id]);


    // ============================================================
    // LOADING
    // ============================================================

    if (loading) {

        return (
            <Loading
                message="Loading staff details..."
            />
        );

    }


    // ============================================================
    // ERROR
    // ============================================================

    if (error) {

        return (

            <div className="staff-details-page">

                <div className="staff-error">
                    {error}
                </div>


                <button
                    type="button"
                    className="btn-secondary"
                    onClick={() =>
                        navigate(
                            "/dashboard/staff"
                        )
                    }
                >
                    Back to Staff
                </button>

            </div>

        );

    }


    // ============================================================
    // NOT FOUND
    // ============================================================

    if (!staff) {

        return (

            <div className="staff-details-page">

                <div className="staff-error">
                    Staff member was not found.
                </div>


                <button
                    type="button"
                    className="btn-secondary"
                    onClick={() =>
                        navigate(
                            "/dashboard/staff"
                        )
                    }
                >
                    Back to Staff
                </button>

            </div>

        );

    }


    // ============================================================
    // NORMALIZE PROPERTY NAMES
    // ============================================================

    const staffId =
        staff.staffId ??
        staff.StaffId;


    const staffName =
        staff.staffName ??
        staff.StaffName;


    const email =
        staff.email ??
        staff.Email;


    const phoneNumber =
        staff.phoneNumber ??
        staff.PhoneNumber;


    const isActive =
        staff.isActive ??
        staff.IsActive;


    const createdOn =
        staff.createdOn ??
        staff.CreatedOn;


    const createdBy =
        staff.createdBy ??
        staff.CreatedBy;


    const modifiedOn =
        staff.modifiedOn ??
        staff.ModifiedOn;


    const modifiedBy =
        staff.modifiedBy ??
        staff.ModifiedBy;


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

        <div className="staff-details-page">


            {/* ================================================== */}
            {/* HEADER */}
            {/* ================================================== */}

            <div className="staff-details-header">

                <div>

                    <h2>
                        Staff Details
                    </h2>

                    <p>
                        View staff member information.
                    </p>

                </div>


                <div className="staff-details-actions">

                    <button
                        type="button"
                        className="btn-secondary"
                        onClick={() =>
                            navigate(
                                "/dashboard/staff"
                            )
                        }
                    >
                        Back
                    </button>


                    <button
                        type="button"
                        className="btn-primary"
                        onClick={() =>
                            navigate(
                                `/dashboard/staff/${staffId}/edit`
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

            <div className="staff-details-card">

                <div className="details-card-header">

                    <h3>
                        Basic Information
                    </h3>

                </div>


                <div className="details-grid">


                    <div className="detail-item">

                        <span className="detail-label">
                            Staff ID
                        </span>

                        <strong>
                            {staffId}
                        </strong>

                    </div>


                    <div className="detail-item">

                        <span className="detail-label">
                            Staff Name
                        </span>

                        <strong>
                            {staffName || "-"}
                        </strong>

                    </div>


                    <div className="detail-item">

                        <span className="detail-label">
                            Email
                        </span>

                        <strong>
                            {email || "-"}
                        </strong>

                    </div>


                    <div className="detail-item">

                        <span className="detail-label">
                            Phone Number
                        </span>

                        <strong>
                            {phoneNumber || "-"}
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

            <div className="staff-details-card">

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


export default StaffDetails;