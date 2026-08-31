import { useState } from "react";
import { useNavigate, useLocation } from "react-router-dom";

import { login } from "../api/authApi";
import { useAuth } from "../context/AuthContext";

import "./Auth.css";

function BranchSelection() {

    const navigate = useNavigate();
    const location = useLocation();

    const { loginSession } = useAuth();

    const email = location.state?.email;
    const company = location.state?.company;
    const branches = location.state?.branches || [];

    const [selectedBranch, setSelectedBranch] = useState(null);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState("");

    const handleLogin = async () => {

        if (!selectedBranch) {

            setError("Please select a branch.");

            return;
        }

        try {

            setLoading(true);
            setError("");

            const response = await login({
                staffId: company.staffId,
                companyId: company.companyId,
                branchId: selectedBranch.branchId
            });

            console.log("Login Response:", response);

            /*
             * Store both access and refresh tokens
             * through AuthContext.
             */
            loginSession(
                response.accessToken,
                response.refreshToken
            );

            navigate("/dashboard", {
                replace: true
            });

        }
        catch (err) {

            console.error(err);

            setError(
                err?.response?.data?.message ||
                "Unable to login."
            );

        }
        finally {

            setLoading(false);

        }

    };

    return (

        <div className="login-container">

            <div className="login-card">

                <h2>Select your branch</h2>

                <p>
                    {company?.companyName}
                    <br />
                    {email}
                </p>

                {branches.map((branch) => (

                    <div
                        key={branch.branchId}
                        className={`company-card ${
                            selectedBranch?.branchId === branch.branchId
                                ? "selected"
                                : ""
                        }`}
                        onClick={() => setSelectedBranch(branch)}
                    >
                        {branch.branchName}
                    </div>

                ))}

                {error && (
                    <p className="error">
                        {error}
                    </p>
                )}

                <button
                    onClick={handleLogin}
                    disabled={loading || !selectedBranch}
                >
                    {loading
                        ? "Signing in..."
                        : "Enter Workspace →"
                    }
                </button>

            </div>

        </div>

    );

}

export default BranchSelection;