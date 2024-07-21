import React from "react";
import { Menu } from "@headlessui/react";
import { UserIcon, CogIcon, LogoutIcon } from "@heroicons/react/outline";
import { Link } from "react-router-dom";

const UserMenu = ({ onLogout }) => {
  return (
    <Menu as="div" className="relative inline-block text-left">
      <div>
        <Menu.Button className="flex items-center gap-2 text-gray-700 hover:text-gray-900">
          <UserIcon className="h-6 w-6" />
          <span>Admin</span>
        </Menu.Button>
      </div>

      <Menu.Items className="absolute right-0 mt-2 w-48 origin-top-right bg-white border border-gray-200 rounded-lg shadow-lg ring-1 ring-black ring-opacity-5 focus:outline-none">
        <div className="p-1">
          <Menu.Item>
            {({ active }) => (
              <Link
                to="/admin/profile"
                className={`${
                  active ? "bg-gray-100 text-gray-900" : "text-gray-700"
                } group flex items-center px-2 py-2 text-sm`}
              >
                <CogIcon className="h-5 w-5 mr-2" />
                Profile
              </Link>
            )}
          </Menu.Item>
          <Menu.Item>
            {({ active }) => (
              <button
                onClick={onLogout}
                className={`${
                  active ? "bg-gray-100 text-gray-900" : "text-gray-700"
                } group flex items-center px-2 py-2 text-sm w-full text-left`}
              >
                <LogoutIcon className="h-5 w-5 mr-2" />
                Logout
              </button>
            )}
          </Menu.Item>
        </div>
      </Menu.Items>
    </Menu>
  );
};

export default UserMenu;
