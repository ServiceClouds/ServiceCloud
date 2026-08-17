import "./Dashboard.css";

function Dashboard() {

    return (

        <div className="dashboard-layout">

            {/* SIDEBAR */}

            <aside className="dashboard-sidebar">

                <div className="dashboard-brand">

                    <div className="dashboard-logo">
                        S
                    </div>

                    <div>
                        <h2>ServiceCloud</h2>
                        <span>Enterprise Platform</span>
                    </div>

                </div>


                <nav className="dashboard-nav">

                    <span className="nav-section">
                        MAIN
                    </span>

                    <a className="nav-item active">
                        <span>▦</span>
                        Dashboard
                    </a>

                    <a className="nav-item">
                        <span>▣</span>
                        Companies
                    </a>

                    <a className="nav-item">
                        <span>⌂</span>
                        Branches
                    </a>

                    <a className="nav-item">
                        <span>♙</span>
                        Staff
                    </a>

                    <a className="nav-item">
                        <span>◈</span>
                        Services
                    </a>


                    <span className="nav-section settings-section">
                        SYSTEM
                    </span>

                    <a className="nav-item">
                        <span>⚙</span>
                        Settings
                    </a>

                </nav>


                <div className="sidebar-bottom">

                    <div className="sidebar-user">

                        <div className="user-avatar">
                            A
                        </div>

                        <div>
                            <strong>Administrator</strong>
                            <span>Admin</span>
                        </div>

                    </div>

                    <button className="logout-button">
                        Logout
                    </button>

                </div>

            </aside>


            {/* MAIN */}

            <main className="dashboard-main">

                {/* HEADER */}

                <header className="dashboard-header">

                    <div>

                        <span className="header-label">
                            OVERVIEW
                        </span>

                        <h1>Dashboard</h1>

                    </div>

                    <div className="header-actions">

                        <button className="notification-button">
                            🔔
                            <span></span>
                        </button>

                        <div className="header-profile">

                            <div className="header-avatar">
                                A
                            </div>

                            <div>
                                <strong>Administrator</strong>
                                <span>Admin</span>
                            </div>

                        </div>

                    </div>

                </header>


                {/* WELCOME */}

                <section className="welcome-card">

                    <div>

                        <span>GOOD MORNING</span>

                        <h2>
                            Welcome back, Administrator 👋
                        </h2>

                        <p>
                            Here's what's happening across your organization today.
                        </p>

                    </div>

                    <div className="welcome-decoration">
                        S
                    </div>

                </section>


                {/* STATISTICS */}

                <section className="stats-grid">

                    <div className="stat-card">

                        <div className="stat-icon blue">
                            ♙
                        </div>

                        <div>
                            <span>Total Staff</span>
                            <strong>128</strong>
                            <small>+8.2% this month</small>
                        </div>

                    </div>


                    <div className="stat-card">

                        <div className="stat-icon purple">
                            ⌂
                        </div>

                        <div>
                            <span>Branches</span>
                            <strong>12</strong>
                            <small>+2 this month</small>
                        </div>

                    </div>


                    <div className="stat-card">

                        <div className="stat-icon green">
                            ◈
                        </div>

                        <div>
                            <span>Services</span>
                            <strong>46</strong>
                            <small>4 added recently</small>
                        </div>

                    </div>


                    <div className="stat-card">

                        <div className="stat-icon orange">
                            ◉
                        </div>

                        <div>
                            <span>Active Users</span>
                            <strong>94</strong>
                            <small>Currently online</small>
                        </div>

                    </div>

                </section>


                {/* CONTENT */}

                <section className="dashboard-content-grid">

                    <div className="activity-card">

                        <div className="card-header">

                            <div>
                                <h3>Recent Activity</h3>
                                <p>Latest activity across your workspace.</p>
                            </div>

                            <button>View all</button>

                        </div>


                        <div className="activity-list">

                            <div className="activity-item">

                                <div className="activity-avatar blue">
                                    S
                                </div>

                                <div>
                                    <strong>
                                        New staff member added
                                    </strong>

                                    <span>
                                        Sarah Ahmed was added to the organization.
                                    </span>
                                </div>

                                <time>
                                    10m ago
                                </time>

                            </div>


                            <div className="activity-item">

                                <div className="activity-avatar purple">
                                    B
                                </div>

                                <div>
                                    <strong>
                                        New branch created
                                    </strong>

                                    <span>
                                        Lahore Main Branch was successfully created.
                                    </span>
                                </div>

                                <time>
                                    1h ago
                                </time>

                            </div>


                            <div className="activity-item">

                                <div className="activity-avatar green">
                                    S
                                </div>

                                <div>
                                    <strong>
                                        Service updated
                                    </strong>

                                    <span>
                                        Customer Support service was updated.
                                    </span>
                                </div>

                                <time>
                                    3h ago
                                </time>

                            </div>

                        </div>

                    </div>


                    <div className="quick-actions-card">

                        <div className="card-header">

                            <div>
                                <h3>Quick Actions</h3>
                                <p>Common management tasks.</p>
                            </div>

                        </div>


                        <button className="quick-action">
                            <span>+</span>
                            <div>
                                <strong>Add Staff</strong>
                                <small>Create a new staff account</small>
                            </div>
                            →
                        </button>


                        <button className="quick-action">
                            <span>+</span>
                            <div>
                                <strong>Add Branch</strong>
                                <small>Create a new branch</small>
                            </div>
                            →
                        </button>


                        <button className="quick-action">
                            <span>+</span>
                            <div>
                                <strong>Add Service</strong>
                                <small>Create a new service</small>
                            </div>
                            →
                        </button>

                    </div>

                </section>

            </main>

        </div>

    );

}

export default Dashboard;