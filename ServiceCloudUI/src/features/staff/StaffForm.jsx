import { useEffect, useState } from "react";
import {
    useNavigate,
    useParams
} from "react-router-dom";

import {
    getStaffById,
    createStaff,
    updateStaff
} from "../../api/staff/staffApi";

import Loading from "../../components/common/Loading";

import "./staff.css";


function StaffForm() {

    const navigate = useNavigate();

    const { id } = useParams();

    const isEditMode = Boolean(id);


    // ============================================================
    // FORM STATE
    // ============================================================

    const [formData, setFormData] = useState({

        staffName: "",
        email: "",
        phoneNumber: ""

    });


    const [loading, setLoading] =
        useState(isEditMode);

    const [saving, setSaving] =
        useState(false);

    const [error, setError] =
        useState("");

    const [validationErrors, setValidationErrors] =
        useState({});


    // ============================================================
    // LOAD STAFF FOR EDIT
    // ============================================================

    useEffect(() => {

        if (!isEditMode) {
            return;
        }


        const loadStaff = async () => {

            try {

                setLoading(true);

                setError("");


                const response =
                    await getStaffById(id);


                const staff =
                    response?.data ??
                    response;


                setFormData({

                    staffName:
                        staff?.staffName ??
                        staff?.StaffName ??
                        "",

                    email:
                        staff?.email ??
                        staff?.Email ??
                        "",

                    phoneNumber:
                        staff?.phoneNumber ??
                        staff?.PhoneNumber ??
                        ""

                });

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

    }, [
        id,
        isEditMode
    ]);


    // ============================================================
    // INPUT CHANGE
    // ============================================================

    const handleChange = (event) => {

        const {
            name,
            value
        } = event.target;


        setFormData(
            previous => ({
                ...previous,
                [name]: value
            })
        );


        setValidationErrors(
            previous => ({
                ...previous,
                [name]: ""
            })
        );

    };


    // ============================================================
    // VALIDATION
    // ============================================================

    const validate = () => {

        const errors = {};


        // Staff Name

        if (
            !formData.staffName.trim()
        ) {

            errors.staffName =
                "Staff name is required.";

        }
        else if (
            formData.staffName.trim().length > 100
        ) {

            errors.staffName =
                "Staff name cannot exceed 100 characters.";

        }


        // Email

        if (
            !formData.email.trim()
        ) {

            errors.email =
                "Email is required.";

        }
        else if (
            !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(
                formData.email.trim()
            )
        ) {

            errors.email =
                "Please enter a valid email address.";

        }
        else if (
            formData.email.trim().length > 150
        ) {

            errors.email =
                "Email cannot exceed 150 characters.";

        }


        // Phone

        if (
            formData.phoneNumber &&
            formData.phoneNumber.length > 20
        ) {

            errors.phoneNumber =
                "Phone number cannot exceed 20 characters.";

        }


        setValidationErrors(errors);

        return (
            Object.keys(errors).length === 0
        );

    };


    // ============================================================
    // SUBMIT
    // ============================================================

    const handleSubmit = async (event) => {

        event.preventDefault();

        setError("");


        if (!validate()) {
            return;
        }


        try {

            setSaving(true);


            const payload = {

                StaffName:
                    formData.staffName.trim(),

                Email:
                    formData.email.trim(),

                PhoneNumber:
                    formData.phoneNumber.trim() ||
                    null

            };


            if (isEditMode) {

                await updateStaff(
                    Number(id),
                    payload
                );

            }
            else {

                await createStaff(
                    payload
                );

            }


            navigate(
                "/dashboard/staff"
            );

        }
        catch (err) {

            console.error(
                "Failed to save staff:",
                err
            );


            const responseData =
                err?.response?.data;


            if (
                responseData?.errors
            ) {

                setValidationErrors(
                    responseData.errors
                );

            }


            setError(
                responseData?.message ||
                responseData?.Message ||
                "Failed to save staff."
            );

        }
        finally {

            setSaving(false);

        }

    };


    // ============================================================
    // CANCEL
    // ============================================================

    const handleCancel = () => {

        if (saving) {
            return;
        }


        navigate(
            "/dashboard/staff"
        );

    };


    // ============================================================
    // LOADING
    // ============================================================

    if (loading) {

        return (
            <Loading
                message="Loading staff..."
            />
        );

    }


    // ============================================================
    // LOAD ERROR
    // ============================================================

    if (
        isEditMode &&
        error &&
        !formData.staffName
    ) {

        return (

            <div className="staff-form-page">

                <div className="staff-form-header">

                    <div>

                        <h2>
                            Edit Staff
                        </h2>

                        <p>
                            Unable to load the staff member.
                        </p>

                    </div>

                </div>


                <div className="staff-error">
                    {error}
                </div>


                <button
                    type="button"
                    className="btn-secondary"
                    onClick={handleCancel}
                >
                    Back to Staff
                </button>

            </div>

        );

    }


    // ============================================================
    // RENDER
    // ============================================================

    return (

        <div className="staff-form-page">


            {/* ================================================== */}
            {/* HEADER */}
            {/* ================================================== */}

            <div className="staff-form-header">

                <div>

                    <h2>

                        {isEditMode
                            ? "Edit Staff"
                            : "Add Staff"}

                    </h2>


                    <p>

                        {isEditMode
                            ? "Update staff information."
                            : "Create a new staff member."}

                    </p>

                </div>

            </div>


            {/* ================================================== */}
            {/* ERROR */}
            {/* ================================================== */}

            {error && (

                <div className="staff-error">
                    {error}
                </div>

            )}


            {/* ================================================== */}
            {/* FORM */}
            {/* ================================================== */}

            <form
                className="staff-form"
                onSubmit={handleSubmit}
            >


                {/* ================================================== */}
                {/* STAFF NAME */}
                {/* ================================================== */}

                <div className="form-group">

                    <label htmlFor="staffName">

                        Staff Name

                        <span className="required">
                            *
                        </span>

                    </label>


                    <input
                        id="staffName"
                        name="staffName"
                        type="text"
                        value={formData.staffName}
                        onChange={handleChange}
                        placeholder="Enter staff name"
                        maxLength={100}
                        disabled={saving}
                    />


                    {validationErrors.staffName && (

                        <span className="field-error">

                            {validationErrors.staffName}

                        </span>

                    )}

                </div>


                {/* ================================================== */}
                {/* EMAIL */}
                {/* ================================================== */}

                <div className="form-group">

                    <label htmlFor="email">

                        Email

                        <span className="required">
                            *
                        </span>

                    </label>


                    <input
                        id="email"
                        name="email"
                        type="email"
                        value={formData.email}
                        onChange={handleChange}
                        placeholder="Enter email address"
                        maxLength={150}
                        disabled={saving}
                    />


                    {validationErrors.email && (

                        <span className="field-error">

                            {validationErrors.email}

                        </span>

                    )}

                </div>


                {/* ================================================== */}
                {/* PHONE */}
                {/* ================================================== */}

                <div className="form-group">

                    <label htmlFor="phoneNumber">
                        Phone Number
                    </label>


                    <input
                        id="phoneNumber"
                        name="phoneNumber"
                        type="text"
                        value={formData.phoneNumber}
                        onChange={handleChange}
                        placeholder="Enter phone number"
                        maxLength={20}
                        disabled={saving}
                    />


                    {validationErrors.phoneNumber && (

                        <span className="field-error">

                            {validationErrors.phoneNumber}

                        </span>

                    )}

                </div>


                {/* ================================================== */}
                {/* ACTIONS */}
                {/* ================================================== */}

                <div className="staff-form-actions">

                    <button
                        type="button"
                        className="btn-secondary"
                        onClick={handleCancel}
                        disabled={saving}
                    >
                        Cancel
                    </button>


                    <button
                        type="submit"
                        className="btn-primary"
                        disabled={saving}
                    >

                        {saving
                            ? "Saving..."
                            : isEditMode
                                ? "Update Staff"
                                : "Create Staff"}

                    </button>

                </div>

            </form>

        </div>

    );

}


export default StaffForm;