import React from "react";
import { Link } from "react-router-dom";
import { AiOutlineShoppingCart } from "react-icons/ai";
import { BiSearch, BiBell } from "react-icons/bi";
import logo from "../../../assets/Logo.png";
import useHover from "../../../hooks/userHover"; // Adjust path as necessary
import NotificationDropdown from "../../../components/NotificationDropdown";
import UserDropdown from "../../../components/Shared/Header/UserDropdown";

const AdminNavbar = () => {
    const [userRef, isUserHovered] = useHover();
    const [notificationRef, isNotificationHovered] = useHover();

    return (
        <header className="flex items-center justify-between py-4 px-6 bg-gray-800 text-white sticky top-0 z-50">
            <div className="flex items-center gap-4">
                <Link
                    to="/"
                    className="flex items-center gap-1"
                    onClick={() => window.scrollTo({ top: 0, behavior: "smooth" })}
                >
                    <img className="h-8" src={logo} alt="Logo" />
                    <h1 className="text-2xl font-bold">Admin Panel</h1>
                </Link>
            </div>

            <div className="flex items-center gap-4">
                <div className="relative" ref={notificationRef}>
                    <BiBell className="text-2xl cursor-pointer" />
                    {isNotificationHovered && <NotificationDropdown />}
                </div>

                <div className="relative" ref={userRef}>
                    <div className="flex items-center gap-2 p-2 bg-white text-black rounded-3xl">
                        <span className="text-sm font-semibold">Ly Ly</span>
                        <img
                            src="https://www.einfosoft.com/templates/admin/axen/source/light/assets/images/user.jpg"
                            alt="User"
                            className="w-8 h-8 rounded-full object-cover"
                        />
                    </div>
                    {isUserHovered && <UserDropdown />}
                </div>
            </div>
        </header>
    );
};

export default AdminNavbar;
