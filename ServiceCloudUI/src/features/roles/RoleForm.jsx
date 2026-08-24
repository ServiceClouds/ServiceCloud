import { useEffect, useState } from "react";

import {
    createRole,
    updateRole
} from "../../api/role/roleApi";


const emptyRole = {
    roleName: ""
};


const RoleForm = ({
    role,
    onSuccess,
    onCancel
}) => {

    const [formData, setFormData] =
        useState(emptyRole);

    const [saving, setSaving] =
        useState(false);

    const [error, setError] =
        useState("");


    const isEdit =
        role !== null &&
        role !== undefined;


    /*
     * ============================================================
     * LOAD ROLE INTO FORM
     * ============================================================
     */

    useEffect(() => {

        if (!role) {

            setFormData(
                emptyRole
            );

            return;

        }


        setFormData({

            roleName:
                role.roleName ?? ""

        });

    }, [role]);


    /*
     * ============================================================
     * CHANGE
     * ============================================================
     */

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


    /*
     * ============================================================
     * SUBMIT
     * ============================================================
     */

    const handleSubmit = async (event) => {

        event.preventDefault();


        if (
            !formData.roleName.trim()
        ) {

            setError(
                "Role name is required."
            );

            return;

        }


        try {

            setSaving(true);

            setError("");


            const payload = {

                roleName:
                    formData.roleName.trim()

            };


            if (isEdit) {

                await updateRole({

                    roleId:
                        role.roleId,

                    ...payload

                });

            } else {

                await createRole(
                    payload
                );

            }


            onSuccess();


        } catch (err) {

            console.error(
                "Failed to save role:",
                err
            );


            setError(
                err?.response?.data?.message ||
                err?.response?.data?.error ||
                "Failed to save role."
            );

        } finally {

            setSaving(false);

        }

    };


    return (

        <div className="role-modal-overlay">

            <div className="role-modal">

                <div className="role-modal-header">

                    <div>

                        <h2>

                            {
                                isEdit
                                    ? "Edit Role"
                                    : "Create Role"
                            }

                        </h2>


                        <p>

                            {
                                isEdit
                                    ? "Update role information"
                                    : "Enter role information"
                            }

                        </p>

                    </div>


                    <button
                        type="button"
                        className="role-close-button"
                        onClick={onCancel}
                        disabled={saving}
                    >
                        ×
                    </button>

                </div>


                {error && (

                    <div className="role-error">

                        {error}

                    </div>

                )}


                <form
                    onSubmit={handleSubmit}
                >

                    <div className="role-form-grid">

                        <div className="role-field">

                            <label htmlFor="roleName">
                                Role Name
                            </label>


                            <input
                                id="roleName"
                                name="roleName"
                                value={
                                    formData.roleName
                                }
                                onChange={
                                    handleChange
                                }
                                maxLength={100}
                                required
                                disabled={saving}
                                placeholder="Enter role name"
                            />

                        </div>

                    </div>


                    <div className="role-form-actions">

                        <button
                            type="button"
                            onClick={onCancel}
                            disabled={saving}
                        >
                            Cancel
                        </button>


                        <button
                            type="submit"
                            className="role-primary-button"
                            disabled={saving}
                        >

                            {
                                saving
                                    ? "Saving..."
                                    : isEdit
                                        ? "Update Role"
                                        : "Create Role"
                            }

                        </button>

                    </div>

                </form>

            </div>

        </div>

    );

};


export default RoleForm;