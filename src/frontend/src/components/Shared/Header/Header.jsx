import { AiOutlineShoppingCart } from "react-icons/ai";
import { BiSearch } from "react-icons/bi";
import logo from "../../../assets/Logo.png";
import NavList from "./List/NavList";
import { Link } from "react-router-dom";
import { useContext, useState } from "react";
import { CartContext } from "../../../context/CartContext";
import { UserContext } from "../../../context/Usercontext";
import UserDropdown from "./UserDropdown";

const Header = () => {
  const { cart } = useContext(CartContext);
  const { user } = useContext(UserContext);
  const [isDropdownOpen, setDropdownOpen] = useState(false);
  const [isMobileMenuOpen, setMobileMenuOpen] = useState(false);

  const toggleDropdown = () => {
    setDropdownOpen(!isDropdownOpen);
  };

  const toggleMobileMenu = () => {
    setMobileMenuOpen(!isMobileMenuOpen);
  };

  return (
    <header className="flex flex-wrap items-center justify-between py-4 px-6 sm:px-12 sticky top-0 bg-[#EFF6F1] drop-shadow z-50">
      <div className="flex items-center justify-between w-full sm:w-auto">
        <Link
          to="/"
          onClick={() => window.scrollTo({ top: 0, behavior: "smooth" })}
          className="flex items-center gap-1"
        >
          <img className="h-8" src={logo} alt="organick" />
          <h1 className="text-2xl font-bold">Organick</h1>
        </Link>
        <div className="sm:hidden flex items-center gap-2">
          <BiSearch className="text-2xl text-gray-600" />
          <Link to="/cart" className="relative">
            <AiOutlineShoppingCart className="text-2xl text-gray-600" />
            {cart.length > 0 && (
              <span className="absolute top-0 right-0 bg-red-500 text-white text-xs rounded-full px-1">
                {cart.length}
              </span>
            )}
          </Link>
          {user ? (
            <div className="relative">
              <button onClick={toggleDropdown} className="border-4 shadow-lg rounded-full">
                <img
                  className="w-8 h-8 rounded-full"
                  src={user?.image || "https://cdn-icons-png.flaticon.com/512/149/149071.png"}
                  alt="User avatar"
                />
              </button>
              {isDropdownOpen && <UserDropdown />}
            </div>
          ) : (
            <Link className="text-lg drop-shadow-xl py-2 px-3 rounded-full hover:bg-[#274C5B] hover:text-white transition-all duration-500 ease-in-out" to="/login">
              Login
            </Link>
          )}
          <button onClick={toggleMobileMenu} className="text-gray-600">
            ☰
          </button>
        </div>
      </div>

      <nav className={`hidden sm:flex items-center gap-12`}>
        <NavList />
      </nav>

      <div className="hidden sm:flex items-center gap-4">
        <div className="relative">
          <BiSearch className="absolute bg-[#7EB693] text-white text-4xl top-1 right-1 p-[8px] rounded-full" />
          <input
            className="bg-[#ffffff93] text-gray-600 py-2 pl-4 rounded-full border-2 outline-none w-full"
            type="search"
            placeholder="Search"
          />
        </div>
        <Link to="/cart" className="flex items-center gap-2 border-2 rounded-full px-3 py-2 font-semibold">
          <AiOutlineShoppingCart className="text-3xl text-white font-extrabold bg-[#274C5B] rounded-full p-1" />
          <span>Cart {cart.length}</span>
        </Link>
        {user ? (
          <div className="relative">
            <button onClick={toggleDropdown} className="border-4 shadow-lg rounded-full">
              <img
                className="w-10 h-10 rounded-full"
                src={user?.image || "https://cdn-icons-png.flaticon.com/512/149/149071.png"}
                alt="User avatar"
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

      {/* Mobile Dropdown Menu */}
      {isMobileMenuOpen && (
        <div className="sm:hidden w-full mt-4">
          <NavList />
        </div>
      )}
    </header>
  );
};

export default Header;
