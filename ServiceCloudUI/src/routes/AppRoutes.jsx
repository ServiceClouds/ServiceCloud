import { Routes, Route, Navigate } from "react-router-dom";

// AUTH PAGES
import Login from "../Pages/Login";
import CompanySelection from "../Pages/CompanySelection";
import Password from "../Pages/Password";
import BranchSelection from "../Pages/BranchSelection";

// SYSTEM LAYOUTS & ACCESSIBILITY
import DashboardLayout from "../layouts/DashboardLayout";
import ProtectedRoute from "./ProtectedRoute";
import Dashboard from "../Pages/Dashboard/Dashboard";

// COUNTRIES & CURRENCIES FEATURES
import CountryList from "../features/countries/CountryList";
import CountryForm from "../features/countries/CountryForm";

// PRODUCTS MODULE

// ============================================================
// COMPANIES
// ============================================================
import CompanyList from "../features/companies/CompanyList";

// ============================================================
// STAFF
// ============================================================
import StaffList from "../features/staff/StaffList";
import StaffForm from "../features/staff/StaffForm";
import StaffDetails from "../features/staff/StaffDetails";

// ============================================================
// COUNTRIES
// ============================================================
import CountryList from "../features/countries/CountryList";
import CountryForm from "../features/countries/CountryForm";
import CountryDetails from "../features/countries/CountryDetails";

// ============================================================
// CURRENCIES
// ============================================================
import CurrencyList from "../features/currencies/CurrencyList";

// ============================================================
// ROLES
// ============================================================
import RoleList from "../features/roles/RoleList";

// ============================================================
// PRODUCTS
// ============================================================
import ProductList from "../features/products/ProductList";
import ProductForm from "../features/products/ProductForm";
import ProductDetails from "../features/products/ProductDetails";

// PRODUCT CATEGORIES MODULE
import ProductCategoryList from "../features/productCategories/ProductCategoryList";
import ProductCategoryForm from "../features/productCategories/ProductCategoryForm";
import ProductCategoryDetails from "../features/productCategories/ProductCategoryDetails";
// ============================================================
// PRODUCT CATEGORIES
// ============================================================
import ProductCategoryList from "../features/productCategories/ProductCategoryList";
import ProductCategoryForm from "../features/productCategories/ProductCategoryForm";
import ProductCategoryDetails from "../features/productCategories/ProductCategoryDetails";

// PRODUCT VARIANTS MODULE
import ProductVariantList from "../features/productVariants/ProductVariantList";
import ProductVariantForm from "../features/productVariants/ProductVariantForm";
import ProductVariantDetails from "../features/productVariants/ProductVariantDetails";
// ============================================================
// PRODUCT VARIANTS
// ============================================================
import ProductVariantList from "../features/productVariants/ProductVariantList";
import ProductVariantForm from "../features/productVariants/ProductVariantForm";
import ProductVariantDetails from "../features/productVariants/ProductVariantDetails";

// PRODUCT VARIANT BRANCHES MODULE
import ProductVariantBranchList from "../features/productVariantBranches/ProductVariantBranchList";
import ProductVariantBranchForm from "../features/productVariantBranches/ProductVariantBranchForm";
import ProductVariantBranchDetails from "../features/productVariantBranches/ProductVariantBranchDetails";
// ============================================================
// PRODUCT VARIANT BRANCHES
// ============================================================
import ProductVariantBranchList from "../features/productVariantBranches/ProductVariantBranchList";
import ProductVariantBranchForm from "../features/productVariantBranches/ProductVariantBranchForm";
import ProductVariantBranchDetails from "../features/productVariantBranches/ProductVariantBranchDetails";

// PRODUCT VARIANT PACKAGINGS MODULE
import ProductVariantPackagingList from "../features/productVariantPackagings/ProductVariantPackagingList";
import ProductVariantPackagingForm from "../features/productVariantPackagings/ProductVariantPackagingForm";
import ProductVariantPackagingDetails from "../features/productVariantPackagings/ProductVariantPackagingDetails";

function AppRoutes() {
    return (
        <Routes>
            {/* ================================================== */}
            {/* PUBLIC AUTHENTICATION ROUTES */}
            {/* ================================================== */}
            <Route path="/" element={<Navigate to="/login" replace />} />
            <Route path="/login" element={<Login />} />
            <Route path="/companies" element={<CompanySelection />} />
            <Route path="/password" element={<Password />} />
            <Route path="/auth/branches" element={<BranchSelection />} />

            {/* ================================================== */}
            {/* PROTECTED APPLICATION ZONE (Layout & Auth Bound) */}
            {/* ================================================== */}
            <Route
// ============================================================
// PRODUCT VARIANT PACKAGINGS
// ============================================================
import ProductVariantPackagingList from "../features/productVariantPackagings/ProductVariantPackagingList";
import ProductVariantPackagingForm from "../features/productVariantPackagings/ProductVariantPackagingForm";
import ProductVariantPackagingDetails from "../features/productVariantPackagings/ProductVariantPackagingDetails";

// ============================================================
// PRODUCT ATTRIBUTES
// ============================================================
// import ProductAttributeList from "../features/productAttributes/ProductAttributeList";
// import ProductAttributeForm from "../features/productAttributes/ProductAttributeForm";
// import ProductAttributeDetails from "../features/productAttributes/ProductAttributeDetails";


function AppRoutes() {
    return (
        <Routes>
            {/* =====================================================
                ROOT
            ====================================================== */}
            <Route
                path="/"
                element={<Navigate to="/login" replace />}
            />

            {/* =====================================================
                AUTHENTICATION
            ====================================================== */}
            <Route path="/login" element={<Login />} />
            <Route path="/companies" element={<CompanySelection />} />
            <Route path="/password" element={<Password />} />
            <Route path="/auth/branches" element={<BranchSelection />} />

            {/* =====================================================
                PROTECTED APPLICATION
            ====================================================== */}
            <Route
                element={
                    <ProtectedRoute>
                        <DashboardLayout />
                    </ProtectedRoute>
                }
            >
                {/* Core Dashboard */}
                <Route path="/dashboard" element={<Dashboard />} />
                {/* =================================================
                    DASHBOARD
                ================================================== */}
                <Route path="/dashboard" element={<Dashboard />} />

                {/* =================================================
                    COMPANY CRUD
                ================================================== */}
                <Route path="/dashboard/companies" element={<CompanyList />} />

                {/* =================================================
                    STAFF CRUD
                ================================================== */}
                <Route path="/dashboard/staff" element={<StaffList />} />
                <Route path="/dashboard/staff/new" element={<StaffForm />} />
                <Route path="/dashboard/staff/:id" element={<StaffDetails />} />
                <Route path="/dashboard/staff/:id/edit" element={<StaffForm />} />

                {/* =================================================
                    COUNTRY CRUD
                ================================================== */}
                <Route path="/dashboard/countries" element={<CountryList />} />
                <Route path="/dashboard/countries/new" element={<CountryForm />} />
                <Route path="/dashboard/countries/:id" element={<CountryDetails />} />
                <Route path="/dashboard/countries/:id/edit" element={<CountryForm />} />

                {/* =================================================
                    CURRENCY CRUD
                ================================================== */}
                <Route path="/dashboard/currencies" element={<CurrencyList />} />

                {/* =================================================
                    ROLE CRUD
                ================================================== */}
                <Route path="/dashboard/roles" element={<RoleList />} />

                {/* =================================================
                    PRODUCT CRUD
                ================================================== */}
                <Route path="/dashboard/products" element={<ProductList />} />
                <Route path="/dashboard/products/new" element={<ProductForm />} />
                <Route path="/dashboard/products/:id" element={<ProductDetails />} />
                <Route path="/dashboard/products/:id/edit" element={<ProductForm />} />

                {/* =================================================
                    PRODUCT CATEGORY CRUD
                ================================================== */}
                <Route path="/dashboard/product-categories" element={<ProductCategoryList />} />
                <Route path="/dashboard/product-categories/new" element={<ProductCategoryForm />} />
                <Route path="/dashboard/product-categories/:id" element={<ProductCategoryDetails />} />
                <Route path="/dashboard/product-categories/:id/edit" element={<ProductCategoryForm />} />

                {/* =================================================
                    PRODUCT VARIANT CRUD
                ================================================== */}
                <Route path="/dashboard/product-variants" element={<ProductVariantList />} />
                <Route path="/dashboard/product-variants/new" element={<ProductVariantForm />} />
                <Route path="/dashboard/product-variants/:id" element={<ProductVariantDetails />} />
                <Route path="/dashboard/product-variants/:id/edit" element={<ProductVariantForm />} />

                {/* Countries Management */}
                <Route path="/countries" element={<CountryList />} />
                <Route path="/countries/new" element={<CountryForm />} />
                <Route path="/countries/:id/edit" element={<CountryForm />} />
                {/* =================================================
                    PRODUCT VARIANT BRANCH CRUD
                ================================================== */}
                <Route path="/dashboard/product-variant-branches" element={<ProductVariantBranchList />} />
                <Route path="/dashboard/product-variant-branches/new" element={<ProductVariantBranchForm />} />
                <Route path="/dashboard/product-variant-branches/:id" element={<ProductVariantBranchDetails />} />
                <Route path="/dashboard/product-variant-branches/:id/edit" element={<ProductVariantBranchForm />} />

                {/* Products */}
                <Route path="/products" element={<ProductList />} />
                <Route path="/products/new" element={<ProductForm />} />
                <Route path="/products/:id" element={<ProductDetails />} />
                <Route path="/products/:id/edit" element={<ProductForm />} />
                {/* =================================================
                    PRODUCT VARIANT PACKAGING CRUD
                ================================================== */}
                <Route path="/dashboard/product-variant-packagings" element={<ProductVariantPackagingList />} />
                <Route path="/dashboard/product-variant-packagings/new" element={<ProductVariantPackagingForm />} />
                <Route path="/dashboard/product-variant-packagings/:id" element={<ProductVariantPackagingDetails />} />
                <Route path="/dashboard/product-variant-packagings/:id/edit" element={<ProductVariantPackagingForm />} />

                {/* Product Categories */}
                <Route path="/product-categories" element={<ProductCategoryList />} />
                <Route path="/product-categories/new" element={<ProductCategoryForm />} />
                <Route path="/product-categories/:id" element={<ProductCategoryDetails />} />
                <Route path="/product-categories/:id/edit" element={<ProductCategoryForm />} />
                {/* =================================================
                    PRODUCT ATTRIBUTE CRUD
                ================================================== */}
                {/* <Route path="/dashboard/product-attributes" element={<ProductAttributeList />} />
                <Route path="/dashboard/product-attributes/new" element={<ProductAttributeForm />} />
                <Route path="/dashboard/product-attributes/:id" element={<ProductAttributeDetails />} />
                <Route path="/dashboard/product-attributes/:id/edit" element={<ProductAttributeForm />} /> */}

                {/* Product Variants */}
                <Route path="/product-variants" element={<ProductVariantList />} />
                <Route path="/product-variants/new" element={<ProductVariantForm />} />
                <Route path="/product-variants/:id" element={<ProductVariantDetails />} />
                <Route path="/product-variants/:id/edit" element={<ProductVariantForm />} />

                {/* Product Variant Branches */}
                <Route path="/product-variant-branches" element={<ProductVariantBranchList />} />
                <Route path="/product-variant-branches/new" element={<ProductVariantBranchForm />} />
                <Route path="/product-variant-branches/:id" element={<ProductVariantBranchDetails />} />
                <Route path="/product-variant-branches/:id/edit" element={<ProductVariantBranchForm />} />
            </Route>

                {/* Product Variant Packagings */}
                <Route path="/product-variant-packagings" element={<ProductVariantPackagingList />} />
                <Route path="/product-variant-packagings/new" element={<ProductVariantPackagingForm />} />
                <Route path="/product-variant-packagings/:id" element={<ProductVariantPackagingDetails />} />
                <Route path="/product-variant-packagings/:id/edit" element={<ProductVariantPackagingForm />} />
            </Route>

            {/* ================================================== */}
            {/* FALLBACK CATCH-ALL */}
            {/* ================================================== */}
            <Route path="*" element={<Navigate to="/login" replace />} />
            {/* =====================================================
                UNKNOWN ROUTE
            ====================================================== */}
            <Route
                path="*"
                element={<Navigate to="/login" replace />}
            />
        </Routes>
    );
}

export default AppRoutes;
