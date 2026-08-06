import { Routes, Route, Navigate } from "react-router-dom";

import Login from "../Pages/Login";
import CompanySelection from "../Pages/CompanySelection";
import Password from "../Pages/Password";
import BranchSelection from "../Pages/BranchSelection";
import Dashboard from "../Pages/Dashboard";

function AppRoutes() {

    return (

        <Routes>

            <Route path="/" element={<Navigate to="/login" />} />

            <Route path="/login" element={<Login />} />

            <Route path="/companies" element={<CompanySelection />} />

            <Route path="/password" element={<Password />} />

            <Route path="/branches" element={<BranchSelection />} />

            <Route path="/dashboard" element={<Dashboard />} />

        </Routes>

    );

}

export default AppRoutes;