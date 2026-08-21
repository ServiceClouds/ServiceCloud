import { createContext, useContext, useState } from "react";

import { authStorage } from "../services/authStorage";

const AuthContext = createContext(null);

export function AuthProvider({ children }) {

    const [accessToken, setAccessToken] = useState(
        authStorage.getAccessToken()
    );

    const isAuthenticated = !!accessToken;

    const loginSession = (token) => {

        authStorage.setAccessToken(token);

        setAccessToken(token);
    };

    const logout = () => {

        authStorage.clear();

        setAccessToken(null);
    };

    const value = {
        accessToken,
        isAuthenticated,
        loginSession,
        logout
    };

    return (
        <AuthContext.Provider value={value}>
            {children}
        </AuthContext.Provider>
    );
}

export function useAuth() {

    const context = useContext(AuthContext);

    if (!context) {
        throw new Error(
            "useAuth must be used inside AuthProvider"
        );
    }

    return context;
}