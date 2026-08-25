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

import ProductCategoryList
    from "../features/productCategories/ProductCategoryList";
import ProductCategoryForm
    from "../features/productCategories/ProductCategoryForm";
import ProductCategoryDetails
    from "../features/productCategories/ProductCategoryDetails";

import ProductVariantList
    from "../features/productVariants/ProductVariantList";

import ProductVariantForm
    from "../features/productVariants/ProductVariantForm";

import ProductVariantDetails
    from "../features/productVariants/ProductVariantDetails";

    import ProductVariantBranchList
    from "../features/productVariantBranches/ProductVariantBranchList";

import ProductVariantBranchForm
    from "../features/productVariantBranches/ProductVariantBranchForm";

import ProductVariantBranchDetails
    from "../features/productVariantBranches/ProductVariantBranchDetails";

    import ProductVariantPackagingList
    from "../features/productVariantPackagings/ProductVariantPackagingList";

import ProductVariantPackagingForm
    from "../features/productVariantPackagings/ProductVariantPackagingForm";

import ProductVariantPackagingDetails
    from "../features/productVariantPackagings/ProductVariantPackagingDetails";

function AppRoutes() {

    return (

        <Routes>

            {/* ================================================== */}
            {/* AUTHENTICATION */}
            {/* ================================================== */}

            <Route
                path="/"
                element={
                    <Navigate
                        to="/login"
                        replace
                    />
                }
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


            {/* ================================================== */}
            {/* PROTECTED APPLICATION */}
            {/* ================================================== */}

            <Route
                element={
                    <ProtectedRoute>
                        <DashboardLayout />
                    </ProtectedRoute>
                }
            >

                {/* Dashboard */}

                <Route
                    path="/dashboard"
                    element={<Dashboard />}
                />


                {/* ================================================== */}
                {/* PRODUCTS */}
                {/* ================================================== */}

                <Route
                    path="/products"
                    element={<ProductList />}
                />

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


                {/* ================================================== */}
                {/* PRODUCT CATEGORIES */}
                {/* ================================================== */}

                <Route
                    path="/product-categories"
                    element={<ProductCategoryList />}
                />

                <Route
                    path="/product-categories/new"
                    element={<ProductCategoryForm />}
                />

                <Route
                    path="/product-categories/:id"
                    element={<ProductCategoryDetails />}
                />

                <Route
                    path="/product-categories/:id/edit"
                    element={<ProductCategoryForm />}
                />

            </Route>


            {/* ================================================== */}
            {/* UNKNOWN ROUTE */}
            {/* ================================================== */}

            <Route
                path="*"
                element={
                    <Navigate
                        to="/login"
                        replace
                    />
                }
            />

            <Route
    path="/product-variants"
    element={<ProductVariantList />}
/>

<Route
    path="/product-variants/new"
    element={<ProductVariantForm />}
/>

<Route
    path="/product-variants/:id"
    element={<ProductVariantDetails />}
/>

<Route
    path="/product-variants/:id/edit"
    element={<ProductVariantForm />}
/>

<Route
    path="/product-variant-branches"
    element={<ProductVariantBranchList />}
/>

<Route
    path="/product-variant-branches/new"
    element={<ProductVariantBranchForm />}
/>

<Route
    path="/product-variant-branches/:id"
    element={<ProductVariantBranchDetails />}
/>

<Route
    path="/product-variant-branches/:id/edit"
    element={<ProductVariantBranchForm />}
/>

<Route
    path="/product-variant-packagings"
    element={<ProductVariantPackagingList />}
/>

<Route
    path="/product-variant-packagings/new"
    element={<ProductVariantPackagingForm />}
/>

<Route
    path="/product-variant-packagings/:id"
    element={<ProductVariantPackagingDetails />}
/>

<Route
    path="/product-variant-packagings/:id/edit"
    element={<ProductVariantPackagingForm />}
/>
        </Routes>

    );
}

export default AppRoutes;