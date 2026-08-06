import { useNavigate, useLocation } from "react-router-dom";
import { useState } from "react";

function BranchSelection() {

    const navigate = useNavigate();
    const location = useLocation();

    const branches = location.state?.branches || [];

    const [selectedBranch, setSelectedBranch] = useState(null);

    const login = async () => {

        /*
        Verify Login

        Returns JWT
        */

        navigate("/dashboard");

    };

    return (

        <div className="login-container">

            <div className="login-card">

                <h2>Select Branch</h2>

                {branches.map(branch => (

                    <div
                        key={branch.branchId}
                        className={`company-card ${
                            selectedBranch?.branchId === branch.branchId
                                ? "selected"
                                : ""
                        }`}
                        onClick={()=>setSelectedBranch(branch)}
                    >

                        {branch.branchName}

                    </div>

                ))}

                <button onClick={login}>
                    Continue
                </button>

            </div>

        </div>

    );

}

export default BranchSelection;