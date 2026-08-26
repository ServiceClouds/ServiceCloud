import { NavLink } from "react-router-dom";
import { useAuth } from "../../context/AuthContext";

function Sidebar() {
    const { logout } = useAuth();

    const handleLogout = () => {
        logout();
    };

    return (
        <aside className="dashboard-sidebar">
            {/* =====================================================
                BRAND
            ====================================================== */}
            <div className="dashboard-brand">
                <div className="dashboard-logo">S</div>
                <div>
                    <h2>ServiceCloud</h2>
                    <span>Enterprise Platform</span>
                </div>
            </div>

            {/* =====================================================
                NAVIGATION
            ====================================================== */}
            <nav className="dashboard-nav">
                {/* =================================================
                    MAIN
                ================================================== */}
                <span className="nav-section">MAIN</span>

                <NavLink
                    to="/dashboard"
                    end
                    className={({ isActive }) => `nav-item ${isActive ? "active" : ""}`}
                >
                    <span>▦</span>
                    Dashboard
                </NavLink>

                {/* =================================================
                    ORGANIZATION
                ================================================== */}
                <span className="nav-section">ORGANIZATION</span>

                <NavLink
                    to="/dashboard/companies"
                    className={({ isActive }) => `nav-item ${isActive ? "active" : ""}`}
                >
                    <span>▣</span>
                    Companies
                </NavLink>

                <NavLink
                    to="/dashboard/branches"
                    className={({ isActive }) => `nav-item ${isActive ? "active" : ""}`}
                >
                    <span>⌂</span>
                    Branches
                </NavLink>

                

                <NavLink
                    to="/dashboard/staff"
                    className={({ isActive }) => `nav-item ${isActive ? "active" : ""}`}
                >
                    <span>♙</span>
                    Staff
                </NavLink>


                <NavLink
                    to="/dashboard/staff-branches"
                    className={({ isActive }) =>
                        `nav-item ${isActive ? "active" : ""}`
                    }
                >
                    <span>♧</span>
                    Staff Branches
                </NavLink>

                {/* =================================================
                    BUSINESS
                ================================================== */}
                <span className="nav-section">BUSINESS</span>

                <NavLink
                    to="/dashboard/products"
                    className={({ isActive }) => `nav-item ${isActive ? "active" : ""}`}
                >
                    <span>◈</span>
                    Products
                </NavLink>

                <NavLink
                    to="/dashboard/product-variants"
                    className={({ isActive }) => `nav-item ${isActive ? "active" : ""}`}
                >
                    <span>◇</span>
                    Product Variants
                </NavLink>

                    <NavLink to="/product-attributes" className={({ isActive }) =>
                        `nav-item ${isActive ? "active" : ""}`
                    }
                        >
                <span>◇</span>
                Product Attributes
                </NavLink>

                <NavLink
                    to="/product-attribute-values"
                    className={({ isActive }) =>
                        `nav-item ${isActive ? "active" : ""}`
                    }
                >
                    <span>◇</span>
                    Product Attribute Values
                </NavLink>

                <NavLink
                    to="/product-branch-permissions"
                    className={({ isActive }) =>
                        `nav-item ${isActive ? "active" : ""}`
                    }
                >
                    <span>◇</span>
                    Product Branch Permissions
                </NavLink>

                <NavLink
                    to="/dashboard/product-variant-branches"
                    className={({ isActive }) => `nav-item ${isActive ? "active" : ""}`}
                >
                    <span>◇</span>
                    Product Variant Branches
                </NavLink>

                <NavLink
                    to="/dashboard/product-variant-packagings"
                    className={({ isActive }) => `nav-item ${isActive ? "active" : ""}`}
                >
                    <span>◇</span>
                    Product Variant Packagings
                </NavLink>

                <NavLink
                    to="/dashboard/product-categories"
                    className={({ isActive }) => `nav-item ${isActive ? "active" : ""}`}
                >
                    <span>◇</span>
                    Product Categories
                </NavLink>

               

                <NavLink
                    to="/dashboard/services"
                    className={({ isActive }) => `nav-item ${isActive ? "active" : ""}`}
                >
                    <span>◇</span>
                    Services
                </NavLink>

                <NavLink to="/dashboard/service-categories"
                className={({ isActive }) => `nav-item ${isActive ? "active" : ""}`}
                >
                    Service Categories
                </NavLink>

                <NavLink
                to="/service-category-branches"
                className={({ isActive }) =>
                    `nav-item ${isActive ? "active" : ""}`
                }
            >
                <span>◇</span>
                Service Category Branches
            </NavLink>

                {/* =================================================
                    SYSTEM
                ================================================== */}
                <span className="nav-section settings-section">SYSTEM</span>

                <NavLink
                    to="/dashboard/countries"
                    className={({ isActive }) => `nav-item ${isActive ? "active" : ""}`}
                >
                    <span>◎</span>
                    Countries
                </NavLink>

                <NavLink
                    to="/dashboard/state-countries"
                    className={({ isActive }) =>
                        `nav-item ${isActive ? "active" : ""}`
                    }
                >
                    <span>◎</span>
                    States / Provinces
                </NavLink>

                <NavLink
                    to="/dashboard/currencies"
                    className={({ isActive }) => `nav-item ${isActive ? "active" : ""}`}
                >
                    <span>¤</span>
                    Currencies
                </NavLink>

                <NavLink
                    to="/dashboard/roles"
                    className={({ isActive }) => `nav-item ${isActive ? "active" : ""}`}
                >
                    <span>♙</span>
                    Roles
                </NavLink>

                <NavLink
                    to="/dashboard/settings"
                    className={({ isActive }) => `nav-item ${isActive ? "active" : ""}`}
                >
                    <span>⚙</span>
                    Settings
                </NavLink>
            </nav>

            {/* =====================================================
                BOTTOM USER AREA
            ====================================================== */}
            <div className="sidebar-bottom">
                <div className="sidebar-user">
                    <div className="user-avatar">A</div>
                    <div>
                        <strong>Administrator</strong>
                        <span>Admin</span>
                    </div>
                </div>

                <button
                    type="button"
                    className="logout-button"
                    onClick={handleLogout}
                >
                    Logout
                </button>
            </div>
        </aside>
    );
}

export default Sidebar;