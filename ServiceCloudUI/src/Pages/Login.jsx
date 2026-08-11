import { useState } from "react";
import { getCompanies } from "../api/authApi";
import LoginView from "./LoginView";
import { useNavigate } from "react-router-dom";

function Login() {

    const [email, setEmail] = useState("");
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState("");
        const navigate = useNavigate();

    const handleContinue = async () => {
        //test 
        console.log("1. Continue clicked");


        try {

             console.log("2. Calling getCompanies");
            setLoading(true);
            setError("");

            const companies = await getCompanies(email);
            console.log("Companies:", companies);
console.log("Type:", typeof companies);
console.log("Is Array:", Array.isArray(companies));
        

             console.log("3. API Success");

            //sending to next means navigating
            navigate("/companies", {
    state: {
        email,
        companies
    }
});

        }
        catch (err) {
   
              console.log("4. API Failed");
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