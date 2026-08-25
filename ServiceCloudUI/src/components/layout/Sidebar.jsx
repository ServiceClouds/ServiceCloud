import { NavLink } from "react-router-dom";
import { useAuth } from "../../context/AuthContext";

function Sidebar() {
    const { logout } = useAuth();

    const handleLogout = () => {

        logout();

    };

    return (
        <aside className="dashboard-sidebar">

            {/* BRAND */}

            <div className="dashboard-brand">
                <div className="dashboard-logo">S</div>
                <div>

                    <h2>ServiceCloud</h2>

                    <span>
                        Enterprise Platform
                    </span>

                </div>
            </div>


            {/* NAVIGATION */}

            <nav className="dashboard-nav">

                <span className="nav-section">
                    MAIN
                </span>

                <NavLink
                    to="/dashboard"
                    end
                    className={({ isActive }) => `nav-item ${isActive ? "active" : ""}`}
                >
                    <span>▦</span>
                    Dashboard
                </NavLink>


                <span className="nav-section">
                    ORGANIZATION
                </span>

                <NavLink
                    to="/companies"
                    className={({ isActive }) =>
                        `nav-item ${isActive ? "active" : ""}`
                    }
                >
                    <span>▣</span>
                    Companies
                </NavLink>

                <NavLink
                    to="/branches"
                    className={({ isActive }) =>
                        `nav-item ${isActive ? "active" : ""}`
                    }
                >
                    <span>⌂</span>
                    Branches
                </NavLink>

                <NavLink
                    to="/staff"
                    className={({ isActive }) =>
                        `nav-item ${isActive ? "active" : ""}`
                    }
                >
                    <span>♙</span>
                    Staff
                </NavLink>


                <NavLink
                    to="/products"
                    className={({ isActive }) =>
                        `nav-item ${isActive ? "active" : ""}`
                    }
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
                    to="/dashboard/product-attributes"
                    className={({ isActive }) => `nav-item ${isActive ? "active" : ""}`}
                >
                    <span>◇</span>
                    Product Attributes
                </NavLink>

                <NavLink
                    to="/services"
                    className={({ isActive }) =>
                        `nav-item ${isActive ? "active" : ""}`
                    }
                >
                    <span>◇</span>
                    Services
                </NavLink>


                <span className="nav-section settings-section">
                    SYSTEM
                </span>

                <NavLink
                    to="/settings"
                    className={({ isActive }) =>
                        `nav-item ${isActive ? "active" : ""}`
                    }
                >
                    <span>⚙</span>
                    Settings
                </NavLink>
            </nav>


            {/* BOTTOM */}

            <div className="sidebar-bottom">
                <div className="sidebar-user">
                    <div className="user-avatar">A</div>
                    <div>
                        <strong>Administrator</strong>
                        <span>Admin</span>
                    </div>
                </div>

                <button
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