import { AiOutlineShoppingCart } from "react-icons/ai";
import { BiSearch } from "react-icons/bi";
import logo from "../../../assets/Logo.png";
import NavList from "./List/NavList";
import { Link, useNavigate } from "react-router-dom";
import { useContext, useState, useEffect } from "react";
import { CartContext } from "../../../context/CartContext";
import { UserContext } from "../../../context/UserContext";
import UserDropdown from "./UserDropdown";
import { DEFAULT_IMAGE_URLS } from "../../../constants/imageUrls";

const Header = () => {
  const { cart } = useContext(CartContext);
  const { user } = useContext(UserContext);
  const [isDropdownOpen, setDropdownOpen] = useState(false);
  const [isMobileMenuOpen, setMobileMenuOpen] = useState(false);
  const [isScrolled, setIsScrolled] = useState(false);
  const [searchTerm, setSearchTerm] = useState(''); // State for search term
  const navigate = useNavigate(); // Hook for navigation

  useEffect(() => {
    const handleScroll = () => {
      setIsScrolled(window.scrollY > 0);
    };

    window.addEventListener('scroll', handleScroll);
    return () => window.removeEventListener('scroll', handleScroll);
  }, []);

  const toggleDropdown = () => {
    setDropdownOpen(!isDropdownOpen);
  };

  const toggleMobileMenu = () => {
    setMobileMenuOpen(!isMobileMenuOpen);
  };

  const handleSearchChange = (e) => {
    setSearchTerm(e.target.value);
  };

  const handleSearchSubmit = (e) => {
    e.preventDefault();
    if (searchTerm.trim()) {
      navigate(`/category/category/${encodeURIComponent(searchTerm.trim())}`);
    }
  };

  return (
    <header className={`flex flex-wrap items-center justify-between py-4 px-6 sm:px-12 fixed w-full top-0 bg-[#EFF6F1] drop-shadow z-50 header ${isScrolled ? 'scrolled' : ''}`}>
      <div className="flex items-center justify-between w-full sm:w-auto">
        <button onClick={toggleMobileMenu} className="text-gray-600 md:hidden p-4">
          ☰
        </button>
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
            {cart?.items?.length > 0 && (
              <span className="absolute top-0 right-0 bg-red-500 text-white text-xs rounded-full px-1">
                {cart?.items?.length}
              </span>
            )}
          </Link>
          {user ? (
            <div className="relative">
              <button onClick={toggleDropdown} className="border-4 shadow-lg rounded-full">
                <img
                  className="w-10 h-10 rounded-full"
                  src={user?.ImageUrl || DEFAULT_IMAGE_URLS.avatar} 
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
      </div>

      <nav className={`hidden md:flex items-center gap-12`}>
        <NavList />
      </nav>

      <div className="hidden sm:flex items-center gap-4">
        <form onSubmit={handleSearchSubmit} className="relative flex items-center">
          <BiSearch className="absolute bg-[#7EB693] text-white text-4xl top-1 right-1 p-[8px] rounded-full" />
          <input
            className="bg-[#ffffff93] text-gray-600 py-2 pl-4 rounded-full border-2 outline-none w-full"
            type="search"
            placeholder="Search"
            value={searchTerm}
            onChange={handleSearchChange}
          />
        </form>
        <Link to="/cart" className="flex items-center gap-2 border-2 rounded-full px-3 py-2 font-semibold">
          <AiOutlineShoppingCart className="text-3xl text-white font-extrabold bg-[#274C5B] rounded-full p-1" />
          <span>Cart {cart?.items?.length}</span>
        </Link>
        {user ? (
          <div className="relative">
            <button onClick={toggleDropdown} className="border-4 shadow-lg rounded-full">
              <img
                className="w-10 h-10 rounded-full"
                src={user?.ImageUrl || DEFAULT_IMAGE_URLS.avatar}
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
        <div className="md:hidden w-full mt-4">
          <NavList />
        </div>
      )}
    </header>
  );
};

export default Header;
