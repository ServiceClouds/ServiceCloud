import { useEffect, useState } from "react";

import {
    getRoles,
    getRoleById,
    archiveRole
} from "../../api/role/roleApi";

import Loading from "../../components/common/Loading";
import EmptyState from "../../components/common/EmptyState";

import RoleForm from "./RoleForm";
import RoleDetails from "./RoleDetails";

import "./role.css";


const RoleList = () => {

    const [roles, setRoles] = useState([]);

    const [loading, setLoading] =
        useState(false);

    const [error, setError] =
        useState("");

    const [pageNumber, setPageNumber] =
        useState(1);

    const [pageSize] =
        useState(10);

    const [totalCount, setTotalCount] =
        useState(0);

    const [showForm, setShowForm] =
        useState(false);

    const [editingRole, setEditingRole] =
        useState(null);

    const [selectedRole, setSelectedRole] =
        useState(null);

    const [showDetails, setShowDetails] =
        useState(false);


    /*
     * ============================================================
     * LOAD ROLES
     * ============================================================
     */

    const loadRoles = async () => {

        try {

            setLoading(true);

            setError("");


            const response =
                await getRoles({

                    PageNumber:
                        pageNumber,

                    PageSize:
                        pageSize

                });


            const pagedResponse =
                response?.data ??
                response;


            const items =
                pagedResponse?.items ??
                pagedResponse?.data ??
                [];


            setRoles(
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
                "Failed to load roles:",
                err
            );


            setError(
                err?.response?.data?.message ||
                err?.response?.data?.error ||
                "Failed to load roles."
            );

        } finally {

            setLoading(false);

        }

    };


    useEffect(() => {

        loadRoles();

    }, [pageNumber]);


    /*
     * ============================================================
     * ADD
     * ============================================================
     */

    const handleAdd = () => {

        setEditingRole(null);

        setShowForm(true);

    };


    /*
     * ============================================================
     * EDIT
     * ============================================================
     */

    const handleEdit = async (roleId) => {

        try {

            setLoading(true);

            setError("");


            const response =
                await getRoleById(
                    roleId
                );


            const role =
                response?.data ??
                response;


            setEditingRole(role);

            setShowForm(true);


        } catch (err) {

            console.error(
                "Failed to load role:",
                err
            );


            setError(
                err?.response?.data?.message ||
                err?.response?.data?.error ||
                "Failed to load role."
            );

        } finally {

            setLoading(false);

        }

    };


    /*
     * ============================================================
     * DETAILS
     * ============================================================
     */

    const handleDetails = async (roleId) => {

        try {

            setLoading(true);

            setError("");


            const response =
                await getRoleById(
                    roleId
                );


            const role =
                response?.data ??
                response;


            setSelectedRole(role);

            setShowDetails(true);


        } catch (err) {

            console.error(
                "Failed to load role:",
                err
            );


            setError(
                err?.response?.data?.message ||
                err?.response?.data?.error ||
                "Failed to load role."
            );

        } finally {

            setLoading(false);

        }

    };


    /*
     * ============================================================
     * ARCHIVE
     * ============================================================
     */

    const handleArchive = async (role) => {

        const confirmed =
            window.confirm(
                `Are you sure you want to archive "${role.roleName}"?`
            );


        if (!confirmed) {

            return;

        }


        try {

            setLoading(true);

            setError("");


            await archiveRole(
                role.roleId
            );


            await loadRoles();


        } catch (err) {

            console.error(
                "Failed to archive role:",
                err
            );


            setError(
                err?.response?.data?.message ||
                err?.response?.data?.error ||
                "Failed to archive role."
            );

        } finally {

            setLoading(false);

        }

    };


    /*
     * ============================================================
     * FORM SUCCESS
     * ============================================================
     */

    const handleFormSuccess = async () => {

        setShowForm(false);

        setEditingRole(null);

        await loadRoles();

    };


    /*
     * ============================================================
     * PAGINATION
     * ============================================================
     */

    const totalPages =
        Math.max(
            1,
            Math.ceil(
                totalCount /
                pageSize
            )
        );


    /*
     * ============================================================
     * INITIAL LOADING
     * ============================================================
     */

    if (
        loading &&
        roles.length === 0
    ) {

        return <Loading />;

    }


    return (

        <div className="role-page">

            {/* =================================================
                HEADER
            ================================================== */}

            <div className="role-header">

                <div>

                    <h1>
                        Roles
                    </h1>

                    <p>
                        Manage system roles
                    </p>

                </div>


                <button
                    type="button"
                    className="role-primary-button"
                    onClick={handleAdd}
                >
                    + Add Role
                </button>

            </div>


            {/* =================================================
                ERROR
            ================================================== */}

            {error && (

                <div className="role-error">

                    {error}

                </div>

            )}


            {/* =================================================
                TABLE
            ================================================== */}

            {roles.length === 0 ? (

                <EmptyState
                    title="No roles found"
                    message="There are no active roles to display."
                />

            ) : (

                <div className="role-table-container">

                    <table className="role-table">

                        <thead>

                            <tr>

                                <th>
                                    Role Name
                                </th>

                                <th>
                                    Status
                                </th>

                                <th>
                                    Created On
                                </th>

                                <th>
                                    Actions
                                </th>

                            </tr>

                        </thead>


                        <tbody>

                            {roles.map(
                                (role) => (

                                    <tr
                                        key={
                                            role.roleId
                                        }
                                    >

                                        <td>
                                            {
                                                role.roleName ||
                                                "-"
                                            }
                                        </td>


                                        <td>

                                            <span
                                                className={
                                                    role.isActive
                                                        ? "role-status active"
                                                        : "role-status inactive"
                                                }
                                            >

                                                {
                                                    role.isActive
                                                        ? "Active"
                                                        : "Inactive"
                                                }

                                            </span>

                                        </td>


                                        <td>

                                            {
                                                role.createdOn
                                                    ? new Date(
                                                        role.createdOn
                                                    ).toLocaleString()
                                                    : "-"
                                            }

                                        </td>


                                        <td>

                                            <div className="role-actions">

                                                <button
                                                    type="button"
                                                    onClick={() =>
                                                        handleDetails(
                                                            role.roleId
                                                        )
                                                    }
                                                >
                                                    View
                                                </button>


                                                <button
                                                    type="button"
                                                    onClick={() =>
                                                        handleEdit(
                                                            role.roleId
                                                        )
                                                    }
                                                >
                                                    Edit
                                                </button>


                                                {role.isActive && (

                                                    <button
                                                        type="button"
                                                        className="danger"
                                                        onClick={() =>
                                                            handleArchive(
                                                                role
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


            {/* =================================================
                PAGINATION
            ================================================== */}

            <div className="role-pagination">

                <button
                    type="button"
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
                    type="button"
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


            {/* =================================================
                FORM
            ================================================== */}

            {showForm && (

                <RoleForm
                    role={editingRole}

                    onSuccess={
                        handleFormSuccess
                    }

                    onCancel={() => {

                        setShowForm(false);

                        setEditingRole(null);

                    }}
                />

            )}


            {/* =================================================
                DETAILS
            ================================================== */}

            {showDetails && (

                <RoleDetails
                    role={selectedRole}

                    onClose={() => {

                        setShowDetails(false);

                        setSelectedRole(null);

                    }}
                />

            )}

        </div>

    );

};


export default RoleList;