import "./Dashboard.css";

function Dashboard() {

    return (

        <>

            {/* WELCOME */}

            <section className="welcome-card">

                <div>

                    <span>
                        GOOD MORNING
                    </span>

                    <h2>
                        Welcome back, Administrator 👋
                    </h2>

                    <p>
                        Here's what's happening across your
                        organization today.
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

                        <small>
                            +8.2% this month
                        </small>

                    </div>

                </div>


                <div className="stat-card">

                    <div className="stat-icon purple">
                        ⌂
                    </div>

                    <div>

                        <span>Branches</span>

                        <strong>12</strong>

                        <small>
                            +2 this month
                        </small>

                    </div>

                </div>


                <div className="stat-card">

                    <div className="stat-icon green">
                        ◈
                    </div>

                    <div>

                        <span>Services</span>

                        <strong>46</strong>

                        <small>
                            4 added recently
                        </small>

                    </div>

                </div>


                <div className="stat-card">

                    <div className="stat-icon orange">
                        ◉
                    </div>

                    <div>

                        <span>Active Users</span>

                        <strong>94</strong>

                        <small>
                            Currently online
                        </small>

                    </div>

                </div>

            </section>


            {/* CONTENT */}

            <section className="dashboard-content-grid">

                <div className="activity-card">

                    <div className="card-header">

                        <div>

                            <h3>
                                Recent Activity
                            </h3>

                            <p>
                                Latest activity across your
                                workspace.
                            </p>

                        </div>

                        <button type="button">
                            View all
                        </button>

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
                                    Sarah Ahmed was added to
                                    the organization.
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
                                    Lahore Main Branch was
                                    successfully created.
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
                                    Customer Support service
                                    was updated.
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

                            <h3>
                                Quick Actions
                            </h3>

                            <p>
                                Common management tasks.
                            </p>

                        </div>

                    </div>


                    <button
                        className="quick-action"
                        type="button"
                    >

                        <span>+</span>

                        <div>

                            <strong>
                                Add Staff
                            </strong>

                            <small>
                                Create a new staff account
                            </small>

                        </div>

                        →

                    </button>


                    <button
                        className="quick-action"
                        type="button"
                    >

                        <span>+</span>

                        <div>

                            <strong>
                                Add Branch
                            </strong>

                            <small>
                                Create a new branch
                            </small>

                        </div>

                        →

                    </button>


                    <button
                        className="quick-action"
                        type="button"
                    >

                        <span>+</span>

                        <div>

                            <strong>
                                Add Service
                            </strong>

                            <small>
                                Create a new service
                            </small>

                        </div>

                        →

                    </button>

                </div>

            </section>

        </>

    );

}

export default Dashboard;