import { Routes, Route, Navigate } from "react-router-dom";

import Login from "../Pages/Login";
import CompanySelection from "../Pages/CompanySelection";
import Password from "../Pages/Password";
import BranchSelection from "../Pages/BranchSelection";

import DashboardLayout from "../layouts/DashboardLayout";
import ProtectedRoute from "./ProtectedRoute";

import Dashboard from "../Pages/Dashboard/Dashboard";
import ProductList from "../features/products/ProductList";
import ProductForm from "../features/products/ProductForm";
import ProductDetails from "../features/products/ProductDetails";

function AppRoutes() {

    return (

        <Routes>

            {/* Authentication */}
            <Route
                path="/"
                element={
                    <Navigate
                        to="/login"
                        replace
                    />
                }
            />


            {/* =====================================================
                AUTHENTICATION
            ====================================================== */}

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

                <Route
                    path="/dashboard/countries/new"
                    element={<CountryForm />}
                />

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
            <Route path="/products" element={<ProductList />} />

<Route
    path="/products/new"
    element={<ProductForm />}
/>

<Route
    path="/products/:id"
    element={<ProductDetails />}
/>

<Route
    path="/products/:id/edit"
    element={<ProductForm />}
/>

        </Routes>

    );

}

export default AppRoutes;