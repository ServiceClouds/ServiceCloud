import { useEffect, useState } from "react";
import { Link, useNavigate, useParams } from "react-router-dom";
import { getProductBranchPermissionById } from "../../api/product/productBranchPermissionApi";
import "./productBranchPermission.css";

const ProductBranchPermissionDetails = () => {
    const { id } = useParams();
    const navigate = useNavigate();

    const [permission, setPermission] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");

    useEffect(() => {
        const loadPermission = async () => {
            try {
                const response =
                    await getProductBranchPermissionById(id);

                setPermission(response.messageData);
            } catch (error) {
                setError(
                    error.response?.data?.message ||
                    "Failed to load product branch permission."
                );
            } finally {
                setLoading(false);
            }
        };

        loadPermission();
    }, [id]);

    if (loading) {
        return (
            <div className="loading">
                Loading...
            </div>
        );
    }

    if (error) {
        return (
            <div className="product-branch-permission-page">
                <div className="error-message">
                    {error}
                </div>
            </div>
        );
    }

    if (!permission) {
        return (
            <div className="product-branch-permission-page">
                <div className="error-message">
                    Product branch permission not found.
                </div>
            </div>
        );
    }

    const booleanValue = (value) =>
        value ? "Yes" : "No";

    return (
        <div className="product-branch-permission-page">
            <div className="page-header">
                <div>
                    <h2>Product Branch Permission Details</h2>
                    <p>View product branch permission information.</p>
                </div>

                <div className="action-buttons">
                    <Link
                        to={`/product-branch-permissions/${id}/edit`}
                        className="btn btn-edit"
                    >
                        Edit
                    </Link>

                    <button
                        type="button"
                        className="btn btn-secondary"
                        onClick={() =>
                            navigate(
                                "/product-branch-permissions"
                            )
                        }
                    >
                        Back
                    </button>
                </div>
            </div>

            <div className="details-card">
                <div className="detail-item">
                    <span>ID</span>
                    <strong>
                        {permission.productBranchPermissionId}
                    </strong>
                </div>

                <div className="detail-item">
                    <span>Product ID</span>
                    <strong>
                        {permission.productId}
                    </strong>
                </div>

                <div className="detail-item">
                    <span>Branch ID</span>
                    <strong>
                        {permission.branchId}
                    </strong>
                </div>

                <div className="detail-item">
                    <span>Active</span>
                    <strong>
                        {booleanValue(permission.isActive)}
                    </strong>
                </div>

                <div className="detail-item">
                    <span>Online</span>
                    <strong>
                        {booleanValue(permission.isOnline)}
                    </strong>
                </div>

                <div className="detail-item">
                    <span>Hide Price Online</span>
                    <strong>
                        {booleanValue(
                            permission.isHidePriceOnline
                        )}
                    </strong>
                </div>

                <div className="detail-item">
                    <span>Featured</span>
                    <strong>
                        {booleanValue(permission.isFeatured)}
                    </strong>
                </div>

                <div className="detail-item">
                    <span>Tracking Inventory</span>
                    <strong>
                        {booleanValue(
                            permission.hasTrackingventory
                        )}
                    </strong>
                </div>

                <div className="detail-item">
                    <span>Shipping</span>
                    <strong>
                        {booleanValue(permission.hasShipping)}
                    </strong>
                </div>

                <div className="detail-item">
                    <span>Included</span>
                    <strong>
                        {booleanValue(permission.isIncluded)}
                    </strong>
                </div>

                <div className="detail-item">
                    <span>Business Use Only</span>
                    <strong>
                        {booleanValue(
                            permission.businessUseOnly
                        )}
                    </strong>
                </div>

                <div className="detail-item">
                    <span>Variant Generated</span>
                    <strong>
                        {booleanValue(
                            permission.isVariantGenerated
                        )}
                    </strong>
                </div>

                <div className="detail-item">
                    <span>Shared Privately</span>
                    <strong>
                        {booleanValue(
                            permission.isSharedPrivately
                        )}
                    </strong>
                </div>
            </div>
        </div>
    );
};

export default ProductBranchPermissionDetails;