import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import {
    getPagedProductBranchPermissions,
    deactivateProductBranchPermission
} from "../../api/product/productBranchPermissionApi";
import "./productBranchPermission.css";

const ProductBranchPermissionList = () => {
    const [permissions, setPermissions] = useState([]);
    const [pageNumber, setPageNumber] = useState(1);
    const [pageSize] = useState(10);
    const [totalPages, setTotalPages] = useState(1);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");

    const loadPermissions = async () => {
        try {
            setLoading(true);
            setError("");

            const response =
                await getPagedProductBranchPermissions(
                    pageNumber,
                    pageSize
                );

            const data = response.messageData;

            setPermissions(data?.items || []);
            setTotalPages(data?.totalPages || 1);
        } catch (error) {
            setError(
                error.response?.data?.message ||
                "Failed to load product branch permissions."
            );
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        loadPermissions();
    }, [pageNumber]);

    const handleDeactivate = async (id) => {
        const confirmed = window.confirm(
            "Are you sure you want to deactivate this product branch permission?"
        );

        if (!confirmed) return;

        try {
            await deactivateProductBranchPermission(id);
            loadPermissions();
        } catch (error) {
            setError(
                error.response?.data?.message ||
                "Failed to deactivate product branch permission."
            );
        }
    };

    return (
        <div className="product-branch-permission-page">
            <div className="page-header">
                <div>
                    <h2>Product Branch Permissions</h2>
                    <p>Manage product branch permissions.</p>
                </div>

                <Link
                    to="/product-branch-permissions/new"
                    className="btn btn-primary"
                >
                    Add Permission
                </Link>
            </div>

            {error && (
                <div className="error-message">
                    {error}
                </div>
            )}

            {loading ? (
                <div className="loading">Loading...</div>
            ) : (
                <>
                    <div className="table-container">
                        <table>
                            <thead>
                                <tr>
                                    <th>ID</th>
                                    <th>Product ID</th>
                                    <th>Branch ID</th>
                                    <th>Active</th>
                                    <th>Online</th>
                                    <th>Featured</th>
                                    <th>Included</th>
                                    <th>Actions</th>
                                </tr>
                            </thead>

                            <tbody>
                                {permissions.length === 0 ? (
                                    <tr>
                                        <td
                                            colSpan="8"
                                            className="empty-message"
                                        >
                                            No product branch permissions found.
                                        </td>
                                    </tr>
                                ) : (
                                    permissions.map((permission) => (
                                        <tr
                                            key={
                                                permission.productBranchPermissionId
                                            }
                                        >
                                            <td>
                                                {
                                                    permission.productBranchPermissionId
                                                }
                                            </td>

                                            <td>
                                                {permission.productId}
                                            </td>

                                            <td>
                                                {permission.branchId}
                                            </td>

                                            <td>
                                                {permission.isActive
                                                    ? "Yes"
                                                    : "No"}
                                            </td>

                                            <td>
                                                {permission.isOnline
                                                    ? "Yes"
                                                    : "No"}
                                            </td>

                                            <td>
                                                {permission.isFeatured
                                                    ? "Yes"
                                                    : "No"}
                                            </td>

                                            <td>
                                                {permission.isIncluded
                                                    ? "Yes"
                                                    : "No"}
                                            </td>

                                            <td>
                                                <div className="action-buttons">
                                                    <Link
                                                        to={`/product-branch-permissions/${permission.productBranchPermissionId}`}
                                                        className="btn btn-view"
                                                    >
                                                        View
                                                    </Link>

                                                    <Link
                                                        to={`/product-branch-permissions/${permission.productBranchPermissionId}/edit`}
                                                        className="btn btn-edit"
                                                    >
                                                        Edit
                                                    </Link>

                                                    <button
                                                        type="button"
                                                        className="btn btn-delete"
                                                        onClick={() =>
                                                            handleDeactivate(
                                                                permission.productBranchPermissionId
                                                            )
                                                        }
                                                    >
                                                        Deactivate
                                                    </button>
                                                </div>
                                            </td>
                                        </tr>
                                    ))
                                )}
                            </tbody>
                        </table>
                    </div>

                    <div className="pagination">
                        <button
                            type="button"
                            disabled={pageNumber === 1}
                            onClick={() =>
                                setPageNumber((page) => page - 1)
                            }
                        >
                            Previous
                        </button>

                        <span>
                            Page {pageNumber} of {totalPages}
                        </span>

                        <button
                            type="button"
                            disabled={pageNumber >= totalPages}
                            onClick={() =>
                                setPageNumber((page) => page + 1)
                            }
                        >
                            Next
                        </button>
                    </div>
                </>
            )}
        </div>
    );
};

export default ProductBranchPermissionList;