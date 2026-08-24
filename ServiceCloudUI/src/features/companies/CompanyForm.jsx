import { useEffect, useState } from "react";

import {
    createCompany,
    updateCompany
} from "../../api/company/companyApi";

const emptyCompany = {
    countryId: "",
    currencyId: "",
    companyName: "",
    companyCode: "",
    ntn: "",
    registrationNumber: "",
    email: "",
    website: "",
    phone: "",
    fax: "",
    addressLine1: "",
    addressLine2: "",
    cityName: "",
    stateCountryName: "",
    postalCode: "",
    imagePath: "",
    appleStoreUrl: "",
    googlePlayStoreUrl: ""
};

const CompanyForm = ({
    company,
    onSuccess,
    onCancel
}) => {

    const [formData, setFormData] =
        useState(emptyCompany);

    const [saving, setSaving] =
        useState(false);

    const [error, setError] =
        useState("");

    const isEdit =
        company !== null &&
        company !== undefined;

    useEffect(() => {

        if (!company) {

            setFormData(emptyCompany);

            return;
        }

        setFormData({

            countryId:
                company.countryId ?? "",

            currencyId:
                company.currencyId ?? "",

            companyName:
                company.companyName ?? "",

            companyCode:
                company.companyCode ?? "",

            ntn:
                company.ntn ?? "",

            registrationNumber:
                company.registrationNumber ?? "",

            email:
                company.email ?? "",

            website:
                company.website ?? "",

            phone:
                company.phone ?? "",

            fax:
                company.fax ?? "",

            addressLine1:
                company.addressLine1 ?? "",

            addressLine2:
                company.addressLine2 ?? "",

            cityName:
                company.cityName ?? "",

            stateCountryName:
                company.stateCountryName ?? "",

            postalCode:
                company.postalCode ?? "",

            imagePath:
                company.imagePath ?? "",

            appleStoreUrl:
                company.appleStoreUrl ?? "",

            googlePlayStoreUrl:
                company.googlePlayStoreUrl ?? ""

        });

    }, [company]);


    const handleChange = (event) => {

        const {
            name,
            value
        } = event.target;

        setFormData(
            (previous) => ({
                ...previous,
                [name]: value
            })
        );

    };


    const handleSubmit = async (event) => {

        event.preventDefault();

        try {

            setSaving(true);

            setError("");

            const payload = {

                countryId:
                    Number(formData.countryId),

                currencyId:
                    Number(formData.currencyId),

                companyName:
                    formData.companyName || null,

                companyCode:
                    formData.companyCode,

                ntn:
                    formData.ntn || null,

                registrationNumber:
                    formData.registrationNumber ||
                    null,

                email:
                    formData.email || null,

                website:
                    formData.website || null,

                phone:
                    formData.phone || null,

                fax:
                    formData.fax || null,

                addressLine1:
                    formData.addressLine1 ||
                    null,

                addressLine2:
                    formData.addressLine2 ||
                    null,

                cityName:
                    formData.cityName || null,

                stateCountryName:
                    formData.stateCountryName ||
                    null,

                postalCode:
                    formData.postalCode || null,

                imagePath:
                    formData.imagePath || null,

                appleStoreUrl:
                    formData.appleStoreUrl ||
                    null,

                googlePlayStoreUrl:
                    formData.googlePlayStoreUrl ||
                    null

            };


            if (isEdit) {

                await updateCompany({

                    companyId:
                        company.companyId,

                    ...payload

                });

            } else {

                await createCompany(
                    payload
                );

            }

            onSuccess();

        } catch (err) {

            console.error(
                "Failed to save company:",
                err
            );

            const message =
                err?.response?.data?.message ||
                err?.response?.data?.error ||
                "Failed to save company.";

            setError(message);

        } finally {

            setSaving(false);

        }
    };


    return (

        <div className="company-modal-overlay">

            <div className="company-modal">

                <div className="company-modal-header">

                    <div>

                        <h2>
                            {
                                isEdit
                                    ? "Edit Company"
                                    : "Create Company"
                            }
                        </h2>

                        <p>
                            {
                                isEdit
                                    ? "Update company information"
                                    : "Enter company information"
                            }
                        </p>

                    </div>

                    <button
                        type="button"
                        className="company-close-button"
                        onClick={onCancel}
                    >
                        ×
                    </button>

                </div>


                {error && (

                    <div className="company-error">
                        {error}
                    </div>

                )}


                <form
                    onSubmit={handleSubmit}
                >

                    <div className="company-form-grid">

                        <div className="company-field">

                            <label>
                                Country ID
                            </label>

                            <input
                                type="number"
                                name="countryId"
                                value={
                                    formData.countryId
                                }
                                onChange={
                                    handleChange
                                }
                                required
                            />

                        </div>


                        <div className="company-field">

                            <label>
                                Currency ID
                            </label>

                            <input
                                type="number"
                                name="currencyId"
                                value={
                                    formData.currencyId
                                }
                                onChange={
                                    handleChange
                                }
                                required
                            />

                        </div>


                        <div className="company-field">

                            <label>
                                Company Name
                            </label>

                            <input
                                name="companyName"
                                value={
                                    formData.companyName
                                }
                                onChange={
                                    handleChange
                                }
                                maxLength={200}
                            />

                        </div>


                        <div className="company-field">

                            <label>
                                Company Code
                            </label>

                            <input
                                name="companyCode"
                                value={
                                    formData.companyCode
                                }
                                onChange={
                                    handleChange
                                }
                                maxLength={50}
                                required
                            />

                        </div>


                        <div className="company-field">

                            <label>
                                NTN
                            </label>

                            <input
                                name="ntn"
                                value={
                                    formData.ntn
                                }
                                onChange={
                                    handleChange
                                }
                                maxLength={100}
                            />

                        </div>


                        <div className="company-field">

                            <label>
                                Registration Number
                            </label>

                            <input
                                name="registrationNumber"
                                value={
                                    formData.registrationNumber
                                }
                                onChange={
                                    handleChange
                                }
                                maxLength={100}
                            />

                        </div>


                        <div className="company-field">

                            <label>
                                Email
                            </label>

                            <input
                                type="email"
                                name="email"
                                value={
                                    formData.email
                                }
                                onChange={
                                    handleChange
                                }
                                maxLength={50}
                            />

                        </div>


                        <div className="company-field">

                            <label>
                                Phone
                            </label>

                            <input
                                name="phone"
                                value={
                                    formData.phone
                                }
                                onChange={
                                    handleChange
                                }
                                maxLength={50}
                            />

                        </div>


                        <div className="company-field">

                            <label>
                                Website
                            </label>

                            <input
                                name="website"
                                value={
                                    formData.website
                                }
                                onChange={
                                    handleChange
                                }
                                maxLength={150}
                            />

                        </div>


                        <div className="company-field">

                            <label>
                                Fax
                            </label>

                            <input
                                name="fax"
                                value={
                                    formData.fax
                                }
                                onChange={
                                    handleChange
                                }
                                maxLength={50}
                            />

                        </div>


                        <div className="company-field company-field-full">

                            <label>
                                Address Line 1
                            </label>

                            <input
                                name="addressLine1"
                                value={
                                    formData.addressLine1
                                }
                                onChange={
                                    handleChange
                                }
                                maxLength={500}
                            />

                        </div>


                        <div className="company-field company-field-full">

                            <label>
                                Address Line 2
                            </label>

                            <input
                                name="addressLine2"
                                value={
                                    formData.addressLine2
                                }
                                onChange={
                                    handleChange
                                }
                                maxLength={500}
                            />

                        </div>


                        <div className="company-field">

                            <label>
                                City
                            </label>

                            <input
                                name="cityName"
                                value={
                                    formData.cityName
                                }
                                onChange={
                                    handleChange
                                }
                                maxLength={100}
                            />

                        </div>


                        <div className="company-field">

                            <label>
                                State / Country
                            </label>

                            <input
                                name="stateCountryName"
                                value={
                                    formData.stateCountryName
                                }
                                onChange={
                                    handleChange
                                }
                                maxLength={100}
                            />

                        </div>


                        <div className="company-field">

                            <label>
                                Postal Code
                            </label>

                            <input
                                name="postalCode"
                                value={
                                    formData.postalCode
                                }
                                onChange={
                                    handleChange
                                }
                                maxLength={10}
                            />

                        </div>


                        <div className="company-field">

                            <label>
                                Image Path
                            </label>

                            <input
                                name="imagePath"
                                value={
                                    formData.imagePath
                                }
                                onChange={
                                    handleChange
                                }
                                maxLength={80}
                            />

                        </div>


                        <div className="company-field">

                            <label>
                                Apple Store URL
                            </label>

                            <input
                                name="appleStoreUrl"
                                value={
                                    formData.appleStoreUrl
                                }
                                onChange={
                                    handleChange
                                }
                                maxLength={200}
                            />

                        </div>


                        <div className="company-field">

                            <label>
                                Google Play URL
                            </label>

                            <input
                                name="googlePlayStoreUrl"
                                value={
                                    formData.googlePlayStoreUrl
                                }
                                onChange={
                                    handleChange
                                }
                                maxLength={200}
                            />

                        </div>

                    </div>


                    <div className="company-form-actions">

                        <button
                            type="button"
                            onClick={onCancel}
                            disabled={saving}
                        >
                            Cancel
                        </button>

                        <button
                            type="submit"
                            disabled={saving}
                            className="company-primary-button"
                        >
                            {
                                saving
                                    ? "Saving..."
                                    : isEdit
                                        ? "Update Company"
                                        : "Create Company"
                            }
                        </button>

                    </div>

                </form>

            </div>

        </div>

    );
};

export default CompanyForm;