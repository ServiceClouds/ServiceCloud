import { useState } from "react";
import { useNavigate, useLocation } from "react-router-dom";

function Password() {

    const navigate = useNavigate();
    const location = useLocation();

    const email = location.state.email;
    const company = location.state.company;

    const [password, setPassword] = useState("");

    const login = async () => {

        /*
        Verify Login API

        Returns

        Branches
        */

        navigate("/branches", {
            state: {
                email,
                company,
                password
            }
        });

    };

    return (

        <div className="login-container">

            <div className="login-card">

                <h2>{company.companyName}</h2>

                <input
                    type="password"
                    placeholder="Password"
                    value={password}
                    onChange={(e)=>setPassword(e.target.value)}
                />

                <button onClick={login}>
                    Login
                </button>

            </div>

        </div>

    );

}

export default Password;