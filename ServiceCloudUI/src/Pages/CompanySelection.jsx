import { useState } from "react";
import { useNavigate, useLocation } from "react-router-dom";

function CompanySelection() {
    const navigate = useNavigate();
    const location = useLocation();

    const companies = location.state?.companies || [];
    const email = location.state?.email;

    const [selectedCompany, setSelectedCompany] = useState(null);

    const handleContinue = () => {
        if (!selectedCompany) return;

        navigate("/password", {
            state: {
                email,
                company: selectedCompany
            }
        });
    };

    return (
        <div className="login-container">

            <div className="login-card">

                <h2>Select Company</h2>

                <p>{email}</p>

                {companies.map(company => (

                    <div
                        key={company.companyId}
                        className={`company-card ${
                            selectedCompany?.companyId === company.companyId
                                ? "selected"
                                : ""
                        }`}
                        onClick={() => setSelectedCompany(company)}
                    >
                        {company.companyName}
                    </div>

                ))}

                <button onClick={handleContinue}>
                    Continue
                </button>

            </div>

        </div>
    );
}

export default CompanySelection;