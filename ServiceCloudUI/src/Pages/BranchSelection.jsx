import { useState } from "react";
import { useNavigate, useLocation } from "react-router-dom";
import { login } from "../api/authApi";
import "./Auth.css";

function BranchSelection() {

    const navigate = useNavigate();
    const location = useLocation();

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

            // Save JWT
            localStorage.setItem("accessToken", response.accessToken);

            // Navigate to Dashboard
            navigate("/dashboard");

        }
        catch (err) {

            console.error(err);
            setError("Unable to login.");

        }
        finally {

            setLoading(false);

        }

    };

    return (

        <div className="login-container">

            <div className="login-card">

                <h2>Select Branch</h2>

                <p>{company.companyName}</p>

                <p>{email}</p>

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
                    disabled={loading}
                >
                    {loading ? "Please wait..." : "Login"}
                </button>

            </div>

        </div>

    );

}

export default BranchSelection;