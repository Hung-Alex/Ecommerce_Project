import React from "react";
import SideNav from "../components/SideNav";
import AdminNavbar from "../pages/Admin/comp/AdminNavbar";

const DashboardLayout = ({ children }) => {
  return (
    <>
      <AdminNavbar />
    <div className="flex justify-center items-start bg-slate-100">
      <SideNav />
      {children}
    </div>
    </>
  );
};

export default DashboardLayout;
