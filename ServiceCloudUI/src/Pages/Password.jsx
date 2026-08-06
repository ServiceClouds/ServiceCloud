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

                <h2>{company.companyName}</h2>

                <p>{email}</p>

                <input
                    type="password"
                    placeholder="Enter Password"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                />

                {error && (
                    <p className="error">
                        {error}
                    </p>
                )}

                <button
                    onClick={login}
                    disabled={loading}
                >
                    {loading ? "Please wait..." : "Continue"}
                </button>

            </div>

        </div>

    );

}

export default Password;