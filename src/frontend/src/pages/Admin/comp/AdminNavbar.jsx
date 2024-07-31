import React, { useContext, useState } from 'react';
import { Link } from 'react-router-dom';
import { AiOutlineShoppingCart } from 'react-icons/ai';
import { BiSearch } from 'react-icons/bi';
import logo from '../../../assets/Logo.png';
import AdminNavList from './AdminNavList'; // Assuming a separate component for admin nav items
import { CartContext } from '../../../context/CartContext';
import { UserContext } from '../../../context/UserContext';
import UserDropdown from '../../../components/Shared/Header/UserDropdown';

const AdminNavbar = () => {
  const { cart } = useContext(CartContext);
  const { user } = useContext(UserContext);
  const [isDropdownOpen, setDropdownOpen] = useState(false);
  const [isMobileMenuOpen, setMobileMenuOpen] = useState(false);

  const toggleDropdown = () => setDropdownOpen(!isDropdownOpen);
  const toggleMobileMenu = () => setMobileMenuOpen(!isMobileMenuOpen);

  return (
    <>
    <header className="flex flex-wrap items-center justify-between py-4 px-6 sm:px-12 sticky w-full top-0 bg-black drop-shadow z-50">
      <div className="flex items-center justify-between w-full sm:w-auto">
        <button onClick={toggleMobileMenu} className="text-gray-300 sm:hidden">
          ☰
        </button>
        <Link to="/admin" onClick={() => window.scrollTo({ top: 0, behavior: 'smooth' })} className="flex items-center gap-1">
          <img className="h-8" src={logo} alt="admin-logo" />
          <h1 className="text-2xl font-bold">Admin Panel</h1>
        </Link>
        <div className="sm:hidden flex items-center gap-2">
          <BiSearch className="text-2xl text-gray-600" />
          {user ? (
            <div className="relative">
              <button onClick={toggleDropdown} className="border-4 shadow-lg rounded-full">
                <img
                  className="w-10 h-10 rounded-full"
                  src={user?.ImageUrl || "https://cdn-icons-png.flaticon.com/512/149/149071.png"}
                  alt="Admin avatar"
                />
              </button>
              {isDropdownOpen && <UserDropdown />}
            </div>
          ) : (
            <Link className="text-lg drop-shadow-xl py-2 px-3 rounded-full hover:bg-[#274C5B] hover:text-white transition-all duration-500 ease-in-out" to="/login">
              Login
            </Link>
          )}
        </div>
      </div>

      <div className="hidden sm:flex items-center gap-4">
        <div className="relative">
          <BiSearch className="absolute bg-dark text-white text-4xl top-1 right-1 p-[8px] rounded-full" />
          <input
            className="bg-[#ffffff93] text-gray-600 py-2 pl-4 rounded-full border-2 outline-none w-full"
            type="search"
            placeholder="Search"
          />
        </div>
        {/* <Link to="/cart" className="flex items-center gap-2 border-2 rounded-full px-3 py-2 font-semibold">
          <AiOutlineShoppingCart className="text-3xl text-white font-extrabold bg-[#274C5B] rounded-full p-1" />
          <span>Cart {cart?.items?.length}</span>
        </Link> */}
        {user ? (
          <div className="relative">
            <button onClick={toggleDropdown} className="border-4 shadow-lg rounded-full">
              <img
                className="w-10 h-10 rounded-full"
                src={user?.ImageUrl || "https://cdn-icons-png.flaticon.com/512/149/149071.png"}
                alt="Admin avatar"
              />
            </button>
            {isDropdownOpen && <UserDropdown />}
          </div>
        ) : (
          <Link className="text-lg drop-shadow-xl py-2 px-3 rounded-full hover:bg-[#274C5B] hover:text-white transition-all duration-500 ease-in-out" to="/login">
            Login
          </Link>
        )}
      </div>
    </header>

      {/* Mobile Dropdown Menu */}
      {isMobileMenuOpen && (
        <div className=" absolute bg-white sm:hidden w-full max-w-[100vw] overflow-auto">
          <AdminNavList />
        </div>
      )}
    </>
  );
};

export default AdminNavbar;
