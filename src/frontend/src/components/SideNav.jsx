import React, { useContext } from "react";
import { Link } from "react-router-dom";
import { UserContext } from "../context/Usercontext";
import logo from '../assets/Logo.png'; // Đảm bảo bạn có đường dẫn đúng đến hình ảnh logo

const navItems = [
  { label: "Products", path: "/admin/products" },
  { label: "Category", path: "/admin/Category" },
  { label: "Brand", path: "/admin/brands" }
];

const adminNavItems = [
  { label: "Products", path: "/admin/products" },
  { label: "Category", path: "/admin/Category" },
  { label: "Brand", path: "/admin/Brand" }
];

const SideNav = () => {
  const { user } = useContext(UserContext);

  return (
    <div className="mt-12 min-h-full bg-white w-52 shadow-md rounded-lg">
      <div className="mt-4">
        {navItems.map((item, index) => (
          <Link
            key={index}
            to={item.path}
            className="block px-4 py-2 text-gray-700 hover:bg-gray-100 border-b border-gray-200"
          >
            {item.label}
          </Link>
        ))}
        {user?.role === "admin" && (
          <>
            {adminNavItems.map((item, index) => (
              <Link
                key={index}
                to={item.path}
                className="block px-4 py-2 text-gray-700 hover:bg-gray-100 border-b border-gray-200"
              >
                {item.label}
              </Link>
            ))}
          </>
        )}
      </div>
    </div>
  );
};

export default SideNav;
