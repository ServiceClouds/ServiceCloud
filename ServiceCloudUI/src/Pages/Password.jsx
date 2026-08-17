import { useState } from "react";
import { useNavigate, useLocation } from "react-router-dom";
import { verifyLogin } from "../api/authApi";
import "./Auth.css";

function Password() {

    const navigate = useNavigate();
    const location = useLocation();

    const email = location.state?.email;
    const company = location.state?.company;

    const [password, setPassword] = useState("");
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState("");

    const login = async () => {

        try {

            setLoading(true);
            setError("");

            const response = await verifyLogin({
                staffId: company.staffId,
                companyId: company.companyId,
                password
            });

            console.log("Verify Login Response:", response);

            navigate("/branches", {
                state: {
                    email,
                    company,
                    branches: response.branches
                }
            });

        }
        catch (err) {

            console.error(err);
            setError("Invalid email or password.");

        }
        finally {

            setLoading(false);

        }

    };

    return (

        <div className="login-container">

            <div className="login-card">

                <h2>Enter your password</h2>

                <p>
                    {company?.companyName}
                    <br />
                    {email}
                </p>

                <input
                    type="password"
                    placeholder="Enter your password"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    onKeyDown={(e) => {
                        if (e.key === "Enter" && password) {
                            login();
                        }
                    }}
                />

                {error && (
                    <p className="error">
                        {error}
                    </p>
                )}

                <button
                    onClick={login}
                    disabled={loading || !password}
                >
                    {loading ? "Verifying..." : "Continue →"}
                </button>

            </div>

        </div>

    );

}

export default Password;