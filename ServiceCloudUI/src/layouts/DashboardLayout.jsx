import { Outlet } from "react-router-dom";

import Sidebar from "../components/layout/Sidebar";
import Header from "../components/layout/Header";

import "./DashboardLayout.css";

function DashboardLayout() {

    return (

        <div className="dashboard-layout">

            <Sidebar />

            <main className="dashboard-main">

                <Header />

                <div className="dashboard-content">
                    <Outlet />
                </div>

            </main>

        </div>

    );

}

export default DashboardLayout;