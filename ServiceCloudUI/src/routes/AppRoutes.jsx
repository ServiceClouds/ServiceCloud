import { Routes, Route, Navigate } from "react-router-dom";

import Login from "../Pages/Login";
import CompanySelection from "../Pages/CompanySelection";
import Password from "../Pages/Password";
import BranchSelection from "../Pages/BranchSelection";

import DashboardLayout from "../layouts/DashboardLayout";
import ProtectedRoute from "./ProtectedRoute";

import Dashboard from "../Pages/Dashboard/Dashboard";

function AppRoutes() {
    return (
        <Routes>

            {/* Authentication */}
            <Route
                path="/"
                element={<Navigate to="/login" replace />}
            />

            <Route
                path="/login"
                element={<Login />}
            />

            <Route
                path="/companies"
                element={<CompanySelection />}
            />

            <Route
                path="/password"
                element={<Password />}
            />

            <Route
                path="/auth/branches"
                element={<BranchSelection />}
            />


            {/* Protected Application */}
            <Route
                element={
                    <ProtectedRoute>
                        <DashboardLayout />
                    </ProtectedRoute>
                }
            >
                <Route
                    path="/dashboard"
                    element={<Dashboard />}
                />
            </Route>


            {/* Unknown route */}
            <Route
                path="*"
                element={
                    <Navigate
                        to="/login"
                        replace
                    />
                }
            />

        </Routes>
    );
}

export default AppRoutes;