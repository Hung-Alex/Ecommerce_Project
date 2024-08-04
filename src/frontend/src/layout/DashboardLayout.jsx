import React from "react";
import SideNav from "../components/SideNav";
import AdminNavbar from "../pages/Admin/comp/AdminNavbar";
import { Outlet } from "react-router-dom";

const DashboardLayout = () => {
  return (
    <>
    <div className=" min-h-screen  bg-slate-100">
      <AdminNavbar />
      <div className="flex justify-center items-start w-full">
      <SideNav />
      <Outlet />
      </div>
    </div>
    </>
  );
};

export default DashboardLayout;
