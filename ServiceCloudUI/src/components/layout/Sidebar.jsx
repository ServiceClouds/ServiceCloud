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

                <div className="dashboard-logo">
                    S
                </div>

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
                    className={({ isActive }) =>
                        `nav-item ${isActive ? "active" : ""}`
                    }
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


                <span className="nav-section">
                    BUSINESS
                </span>

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

                    <div className="user-avatar">
                        A
                    </div>

                    <div>

                        <strong>
                            Administrator
                        </strong>

                        <span>
                            Admin
                        </span>

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