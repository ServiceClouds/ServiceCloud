import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import {
    getAllServiceCategoryBranches,
    deleteServiceCategoryBranch
} from "../../api/serviceCategoryBranch/serviceCategoryBranchApi";
import "./serviceCategoryBranch.css";

const ServiceCategoryBranchList = () => {
    const navigate = useNavigate();

    const [branches, setBranches] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");

    const loadBranches = async () => {
        try {
            setLoading(true);
            setError("");

            const response =
                await getAllServiceCategoryBranches();

            setBranches(
                response.messageData?.items || []
            );
        } catch (error) {
            console.error(
                "Failed to load service category branches:",
                error
            );

            setError(
                error.response?.data?.message ||
                "Failed to load service category branches."
            );
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        loadBranches();
    }, []);

    const handleDelete = async (id) => {
        if (
            !window.confirm(
                "Are you sure you want to deactivate this service category branch?"
            )
        ) {
            return;
        }

        try {
            await deleteServiceCategoryBranch(id);

            loadBranches();
        } catch (error) {
            console.error(
                "Failed to deactivate service category branch:",
                error
            );

            setError(
                error.response?.data?.message ||
                "Failed to deactivate service category branch."
            );
        }
    };

    if (loading) {
        return (
            <div className="crud-page">
                <div className="crud-header">
                    <h2>Service Category Branches</h2>
                </div>

                <p>Loading...</p>
            </div>
        );
    }

    return (
        <div className="crud-page">

            <div className="crud-header">
                <div>
                    <h2>Service Category Branches</h2>
                    <p>
                        Manage service category branch permissions.
                    </p>
                </div>

                <button
                    className="btn-primary"
                    onClick={() =>
                        navigate(
                            "/service-category-branches/new"
                        )
                    }
                >
                    + Add Service Category Branch
                </button>
            </div>

            {error && (
                <div className="error-message">
                    {error}
                </div>
            )}

            <div className="table-container">
                <table className="crud-table">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Service Category ID</th>
                            <th>Branch ID</th>
                            <th>Active</th>
                            <th>Included</th>
                            <th>Actions</th>
                        </tr>
                    </thead>

                    <tbody>
                        {branches.length === 0 ? (
                            <tr>
                                <td
                                    colSpan="6"
                                    className="empty-state"
                                >
                                    No service category branches found.
                                </td>
                            </tr>
                        ) : (
                            branches.map((branch) => (
                                <tr
                                    key={
                                        branch.serviceCategoryBranchId
                                    }
                                >
                                    <td>
                                        {
                                            branch.serviceCategoryBranchId
                                        }
                                    </td>

                                    <td>
                                        {
                                            branch.serviceCategoryId
                                        }
                                    </td>

                                    <td>
                                        {branch.branchId}
                                    </td>

                                    <td>
                                        {branch.isActive
                                            ? "Yes"
                                            : "No"}
                                    </td>

                                    <td>
                                        {branch.isIncluded
                                            ? "Yes"
                                            : "No"}
                                    </td>

                                    <td className="action-buttons">
                                        <button
                                            className="btn-view"
                                            onClick={() =>
                                                navigate(
                                                    `/service-category-branches/${branch.serviceCategoryBranchId}`
                                                )
                                            }
                                        >
                                            View
                                        </button>

                                        <button
                                            className="btn-edit"
                                            onClick={() =>
                                                navigate(
                                                    `/service-category-branches/${branch.serviceCategoryBranchId}/edit`
                                                )
                                            }
                                        >
                                            Edit
                                        </button>

                                        {branch.isActive && (
                                            <button
                                                className="btn-delete"
                                                onClick={() =>
                                                    handleDelete(
                                                        branch.serviceCategoryBranchId
                                                    )
                                                }
                                            >
                                                Deactivate
                                            </button>
                                        )}
                                    </td>
                                </tr>
                            ))
                        )}
                    </tbody>
                </table>
            </div>
        </div>
    );
};

export default ServiceCategoryBranchList;