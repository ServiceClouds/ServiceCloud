import { useEffect, useState } from "react";
import { Link, useNavigate, useParams } from "react-router-dom";
import { getServiceCategoryBranchById } from "../../api/serviceCategoryBranch/serviceCategoryBranchApi";
import "./serviceCategoryBranch.css";

const ServiceCategoryBranchDetails = () => {
    const { id } = useParams();
    const navigate = useNavigate();

    const [branch, setBranch] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");

    useEffect(() => {
        const loadBranch = async () => {
            try {
                const response =
                    await getServiceCategoryBranchById(id);

                setBranch(response.messageData);
            } catch (error) {
                setError(
                    error.response?.data?.message ||
                    "Failed to load service category branch."
                );
            } finally {
                setLoading(false);
            }
        };

        loadBranch();
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
            <div className="service-category-branch-page">
                <div className="error-message">
                    {error}
                </div>
            </div>
        );
    }

    if (!branch) {
        return (
            <div className="service-category-branch-page">
                <div className="error-message">
                    Service category branch not found.
                </div>
            </div>
        );
    }

    return (
        <div className="service-category-branch-page">
            <div className="page-header">
                <div>
                    <h2>
                        Service Category Branch Details
                    </h2>

                    <p>
                        View service category branch information.
                    </p>
                </div>

                <div className="action-buttons">
                    <Link
                        to={`/service-category-branches/${id}/edit`}
                        className="btn btn-edit"
                    >
                        Edit
                    </Link>

                    <button
                        type="button"
                        className="btn btn-secondary"
                        onClick={() =>
                            navigate(
                                "/service-category-branches"
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
                        {branch.serviceCategoryBranchId}
                    </strong>
                </div>

                <div className="detail-item">
                    <span>Service Category ID</span>
                    <strong>
                        {branch.serviceCategoryId}
                    </strong>
                </div>

                <div className="detail-item">
                    <span>Branch ID</span>
                    <strong>
                        {branch.branchId}
                    </strong>
                </div>

                <div className="detail-item">
                    <span>Active</span>
                    <strong>
                        {branch.isActive ? "Yes" : "No"}
                    </strong>
                </div>

                <div className="detail-item">
                    <span>Included</span>
                    <strong>
                        {branch.isIncluded ? "Yes" : "No"}
                    </strong>
                </div>
            </div>
        </div>
    );
};

export default ServiceCategoryBranchDetails;