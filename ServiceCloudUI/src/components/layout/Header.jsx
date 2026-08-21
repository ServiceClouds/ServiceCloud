import { useLocation } from "react-router-dom";

function Header() {

    const location = useLocation();

    const pageTitles = {
        "/dashboard": {
            label: "OVERVIEW",
            title: "Dashboard"
        },

        "/companies": {
            label: "ORGANIZATION",
            title: "Companies"
        },

        "/branches": {
            label: "ORGANIZATION",
            title: "Branches"
        },

        "/staff": {
            label: "ORGANIZATION",
            title: "Staff"
        },

        "/products": {
            label: "BUSINESS",
            title: "Products"
        },

        "/services": {
            label: "BUSINESS",
            title: "Services"
        },

        "/settings": {
            label: "SYSTEM",
            title: "Settings"
        }
    };

    const currentPage =
        pageTitles[location.pathname] || {
            label: "SERVICECLOUD",
            title: "Workspace"
        };

    return (

        <header className="dashboard-header">

            <div>

                <span className="header-label">
                    {currentPage.label}
                </span>

                <h1>
                    {currentPage.title}
                </h1>

            </div>


            <div className="header-actions">

                <button
                    className="notification-button"
                    type="button"
                >
                    🔔

                    <span></span>

                </button>


                <div className="header-profile">

                    <div className="header-avatar">
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

            </div>

        </header>

    );

}

export default Header;