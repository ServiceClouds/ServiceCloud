import { useEffect, useState } from "react";

import {
    createBranch,
    updateBranch
} from "../../api/branch/branchApi";


const BranchForm = ({
    branch,
    onSuccess,
    onCancel
}) => {

    const isEdit =
        Boolean(branch);


    const [formData, setFormData] = useState({

        branchId:
            branch?.branchId ?? 0,

        companyId:
            branch?.companyId ?? "",

        countryId:
            branch?.countryId ?? "",

        branchName:
            branch?.branchName ?? "",

        branchCode:
            branch?.branchCode ?? "",

        cityName:
            branch?.cityName ?? "",

        stateCountryName:
            branch?.stateCountryName ?? "",

        addressLine1:
            branch?.addressLine1 ?? "",

        addressLine2:
            branch?.addressLine2 ?? "",

        postalCode:
            branch?.postalCode ?? "",

        email:
            branch?.email ?? "",

        phone:
            branch?.phone ?? "",

        mobile:
            branch?.mobile ?? "",

        fax:
            branch?.fax ?? "",

        timeZone:
            branch?.timeZone ?? "",

        currency:
            branch?.currency ?? "",

        dateFormatId:
            branch?.dateFormatId ?? "",

        termsOfServiceUrl:
            branch?.termsOfServiceUrl ?? "",

        privacyPolicyUrl:
            branch?.privacyPolicyUrl ?? "",

        isOnline:
            branch?.isOnline ?? false

    });


    const [loading, setLoading] =
        useState(false);


    const [error, setError] =
        useState("");


    /*
    ============================================================
        UPDATE FORM WHEN BRANCH CHANGES
    ============================================================
    */

    useEffect(() => {

        setFormData({

            branchId:
                branch?.branchId ?? 0,

            companyId:
                branch?.companyId ?? "",

            countryId:
                branch?.countryId ?? "",

            branchName:
                branch?.branchName ?? "",

            branchCode:
                branch?.branchCode ?? "",

            cityName:
                branch?.cityName ?? "",

            stateCountryName:
                branch?.stateCountryName ?? "",

            addressLine1:
                branch?.addressLine1 ?? "",

            addressLine2:
                branch?.addressLine2 ?? "",

            postalCode:
                branch?.postalCode ?? "",

            email:
                branch?.email ?? "",

            phone:
                branch?.phone ?? "",

            mobile:
                branch?.mobile ?? "",

            fax:
                branch?.fax ?? "",

            timeZone:
                branch?.timeZone ?? "",

            currency:
                branch?.currency ?? "",

            dateFormatId:
                branch?.dateFormatId ?? "",

            termsOfServiceUrl:
                branch?.termsOfServiceUrl ?? "",

            privacyPolicyUrl:
                branch?.privacyPolicyUrl ?? "",

            isOnline:
                branch?.isOnline ?? false

        });

    }, [branch]);


    /*
    ============================================================
        TEXT INPUT
    ============================================================
    */

    const handleChange = (event) => {

        const {
            name,
            value
        } = event.target;


        setFormData(
            current => ({
                ...current,
                [name]: value
            })
        );

    };


    /*
    ============================================================
        BOOLEAN INPUT
    ============================================================
    */

    const handleCheckboxChange = (event) => {

        const {
            name,
            checked
        } = event.target;


        setFormData(
            current => ({
                ...current,
                [name]: checked
            })
        );

    };


    /*
    ============================================================
        SUBMIT
    ============================================================
    */

    const handleSubmit = async (event) => {

        event.preventDefault();

        setError("");


        /*
        --------------------------------------------------------
            Required fields
        --------------------------------------------------------
        */

        const branchCode =
            formData.branchCode.trim();


        if (!branchCode) {

            setError(
                "Branch code is required."
            );

            return;

        }


        if (!formData.companyId) {

            setError(
                "Company ID is required."
            );

            return;

        }


        if (!formData.countryId) {

            setError(
                "Country ID is required."
            );

            return;

        }


        const companyId =
            Number(formData.companyId);


        const countryId =
            Number(formData.countryId);


        if (
            !Number.isInteger(companyId) ||
            companyId <= 0
        ) {

            setError(
                "Company ID must be a valid positive number."
            );

            return;

        }


        if (
            !Number.isInteger(countryId) ||
            countryId <= 0
        ) {

            setError(
                "Country ID must be a valid positive number."
            );

            return;

        }


        /*
        --------------------------------------------------------
            Prepare payload
        --------------------------------------------------------
        */

        const payload = {

            companyId,

            countryId,

            branchName:
                formData.branchName.trim() ||
                null,

            branchCode,

            cityName:
                formData.cityName.trim() ||
                null,

            stateCountryName:
                formData.stateCountryName.trim() ||
                null,

            addressLine1:
                formData.addressLine1.trim() ||
                null,

            addressLine2:
                formData.addressLine2.trim() ||
                null,

            postalCode:
                formData.postalCode.trim() ||
                null,

            email:
                formData.email.trim() ||
                null,

            phone:
                formData.phone.trim() ||
                null,

            mobile:
                formData.mobile.trim() ||
                null,

            fax:
                formData.fax.trim() ||
                null,

            timeZone:
                formData.timeZone.trim() ||
                null,

            currency:
                formData.currency.trim() ||
                null,

            dateFormatId:
                formData.dateFormatId
                    ? Number(formData.dateFormatId)
                    : null,

            termsOfServiceUrl:
                formData.termsOfServiceUrl.trim() ||
                null,

            privacyPolicyUrl:
                formData.privacyPolicyUrl.trim() ||
                null,

            isOnline:
                formData.isOnline

        };


        /*
        --------------------------------------------------------
            UPDATE
        --------------------------------------------------------
        */

        try {

            setLoading(true);


            if (isEdit) {

                await updateBranch({

                    branchId:
                        formData.branchId,

                    ...payload

                });

            }


            /*
            ----------------------------------------------------
                CREATE
            ----------------------------------------------------
            */

            else {

                await createBranch(
                    payload
                );

            }


            onSuccess();

        }
        catch (err) {

            console.error(
                "Failed to save branch:",
                err
            );


            const message =
                err?.response?.data?.message ??
                err?.response?.data?.error ??
                err?.response?.data?.title ??
                "Failed to save branch.";


            setError(message);

        }
        finally {

            setLoading(false);

        }

    };


    return (

        <div className="branch-modal-overlay">

            <div className="branch-modal branch-form-modal">

                {/* =================================================
                    HEADER
                ================================================== */}

                <div className="branch-modal-header">

                    <div>

                        <h2>

                            {isEdit
                                ? "Edit Branch"
                                : "Add Branch"}

                        </h2>


                        <p>

                            {isEdit
                                ? "Update branch information."
                                : "Create a new company branch."}

                        </p>

                    </div>


                    <button
                        type="button"
                        className="branch-close-button"
                        onClick={onCancel}
                        disabled={loading}
                    >
                        ×
                    </button>

                </div>


                {/* =================================================
                    ERROR
                ================================================== */}

                {error && (

                    <div className="branch-form-error">

                        {error}

                    </div>

                )}


                <form onSubmit={handleSubmit}>

                    <div className="branch-form-grid">


                        {/* =================================================
                            COMPANY ID
                        ================================================== */}

                        <Field
                            label="Company ID"
                            name="companyId"
                            type="number"
                            value={formData.companyId}
                            onChange={handleChange}
                            required
                            disabled={loading}
                        />


                        {/* =================================================
                            COUNTRY ID
                        ================================================== */}

                        <Field
                            label="Country ID"
                            name="countryId"
                            type="number"
                            value={formData.countryId}
                            onChange={handleChange}
                            required
                            disabled={loading}
                        />


                        {/* =================================================
                            BRANCH NAME
                        ================================================== */}

                        <Field
                            label="Branch Name"
                            name="branchName"
                            value={formData.branchName}
                            onChange={handleChange}
                            placeholder="Main Branch"
                            maxLength={160}
                            disabled={loading}
                        />


                        {/* =================================================
                            BRANCH CODE
                        ================================================== */}

                        <Field
                            label="Branch Code"
                            name="branchCode"
                            value={formData.branchCode}
                            onChange={handleChange}
                            placeholder="BR-001"
                            maxLength={50}
                            required
                            disabled={loading}
                        />


                        {/* =================================================
                            CITY
                        ================================================== */}

                        <Field
                            label="City"
                            name="cityName"
                            value={formData.cityName}
                            onChange={handleChange}
                            placeholder="Lahore"
                            maxLength={100}
                            disabled={loading}
                        />


                        {/* =================================================
                            STATE
                        ================================================== */}

                        <Field
                            label="State / Province"
                            name="stateCountryName"
                            value={formData.stateCountryName}
                            onChange={handleChange}
                            placeholder="Punjab"
                            maxLength={100}
                            disabled={loading}
                        />


                        {/* =================================================
                            ADDRESS 1
                        ================================================== */}

                        <Field
                            label="Address Line 1"
                            name="addressLine1"
                            value={formData.addressLine1}
                            onChange={handleChange}
                            placeholder="Street address"
                            maxLength={500}
                            disabled={loading}
                            full
                        />


                        {/* =================================================
                            ADDRESS 2
                        ================================================== */}

                        <Field
                            label="Address Line 2"
                            name="addressLine2"
                            value={formData.addressLine2}
                            onChange={handleChange}
                            placeholder="Apartment / Suite / Building"
                            maxLength={500}
                            disabled={loading}
                            full
                        />


                        {/* =================================================
                            POSTAL CODE
                        ================================================== */}

                        <Field
                            label="Postal Code"
                            name="postalCode"
                            value={formData.postalCode}
                            onChange={handleChange}
                            maxLength={10}
                            disabled={loading}
                        />


                        {/* =================================================
                            EMAIL
                        ================================================== */}

                        <Field
                            label="Email"
                            name="email"
                            type="email"
                            value={formData.email}
                            onChange={handleChange}
                            maxLength={50}
                            disabled={loading}
                        />


                        {/* =================================================
                            PHONE
                        ================================================== */}

                        <Field
                            label="Phone"
                            name="phone"
                            value={formData.phone}
                            onChange={handleChange}
                            maxLength={50}
                            disabled={loading}
                        />


                        {/* =================================================
                            MOBILE
                        ================================================== */}

                        <Field
                            label="Mobile"
                            name="mobile"
                            value={formData.mobile}
                            onChange={handleChange}
                            maxLength={15}
                            disabled={loading}
                        />


                        {/* =================================================
                            FAX
                        ================================================== */}

                        <Field
                            label="Fax"
                            name="fax"
                            value={formData.fax}
                            onChange={handleChange}
                            maxLength={50}
                            disabled={loading}
                        />


                        {/* =================================================
                            TIME ZONE
                        ================================================== */}

                        <Field
                            label="Time Zone"
                            name="timeZone"
                            value={formData.timeZone}
                            onChange={handleChange}
                            placeholder="Asia/Karachi"
                            maxLength={100}
                            disabled={loading}
                        />


                        {/* =================================================
                            CURRENCY
                        ================================================== */}

                        <Field
                            label="Currency"
                            name="currency"
                            value={formData.currency}
                            onChange={handleChange}
                            placeholder="PKR"
                            maxLength={10}
                            disabled={loading}
                        />


                        {/* =================================================
                            DATE FORMAT
                        ================================================== */}

                        <Field
                            label="Date Format ID"
                            name="dateFormatId"
                            type="number"
                            value={formData.dateFormatId}
                            onChange={handleChange}
                            disabled={loading}
                        />


                        {/* =================================================
                            TERMS URL
                        ================================================== */}

                        <Field
                            label="Terms of Service URL"
                            name="termsOfServiceUrl"
                            type="url"
                            value={formData.termsOfServiceUrl}
                            onChange={handleChange}
                            maxLength={250}
                            disabled={loading}
                        />


                        {/* =================================================
                            PRIVACY URL
                        ================================================== */}

                        <Field
                            label="Privacy Policy URL"
                            name="privacyPolicyUrl"
                            type="url"
                            value={formData.privacyPolicyUrl}
                            onChange={handleChange}
                            maxLength={250}
                            disabled={loading}
                        />


                        {/* =================================================
                            ONLINE
                        ================================================== */}

                        <div className="branch-checkbox-field">

                            <label>

                                <input
                                    type="checkbox"
                                    name="isOnline"
                                    checked={
                                        formData.isOnline
                                    }
                                    onChange={
                                        handleCheckboxChange
                                    }
                                    disabled={loading}
                                />

                                <span>
                                    Branch is Online
                                </span>

                            </label>

                        </div>

                    </div>


                    {/* =================================================
                        ACTIONS
                    ================================================== */}

                    <div className="branch-form-actions">

                        <button
                            type="button"
                            onClick={onCancel}
                            disabled={loading}
                        >
                            Cancel
                        </button>


                        <button
                            type="submit"
                            disabled={loading}
                        >

                            {loading
                                ? "Saving..."
                                : isEdit
                                    ? "Update Branch"
                                    : "Create Branch"}

                        </button>

                    </div>

                </form>

            </div>

        </div>

    );

};


/*
================================================================
    REUSABLE FIELD
================================================================
*/

const Field = ({
    label,
    name,
    type = "text",
    value,
    onChange,
    placeholder,
    maxLength,
    required = false,
    disabled = false,
    full = false
}) => {

    return (

        <div
            className={
                `branch-field ${
                    full
                        ? "branch-field-full"
                        : ""
                }`
            }
        >

            <label htmlFor={name}>

                {label}

                {required && (
                    <span className="required-mark">
                        *
                    </span>
                )}

            </label>


            <input
                id={name}
                name={name}
                type={type}
                value={value}
                onChange={onChange}
                placeholder={placeholder}
                maxLength={maxLength}
                required={required}
                disabled={disabled}
            />

        </div>

    );

};


export default BranchForm;