import { useState } from "react";
import { getCompanies } from "../api/authApi";
import LoginView from "./LoginView";

function Login() {

    const [email, setEmail] = useState("");
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState("");

    const handleContinue = async () => {

        try {

            setLoading(true);
            setError("");

            const companies = await getCompanies(email);

            console.log(companies);

        }
        catch (err) {

            setError("Unable to find companies.");

        }
        finally {

            setLoading(false);

        }

    };

    return (

        <LoginView
            email={email}
            setEmail={setEmail}
            loading={loading}
            error={error}
            handleContinue={handleContinue}
        />

    );

}

export default Login;