import { useContext } from "react";
import { Link } from "react-router-dom";
import { UserContext } from "../../../context/UserContext";

const UserDropdown = () => {
  const { logout, user } = useContext(UserContext);

  const handleLogout = () => {
    logout();
  };

  return (
    <div className="absolute top-full right-0 w-56 bg-white text-black border border-gray-300 rounded-lg shadow-lg ring-1 ring-black ring-opacity-5">
      <div className="py-1">
        <Link
          to="/user/profile"
          className="block px-4 py-2 text-gray-700 hover:bg-gray-100 transition-colors duration-200"
        >
          My Account
        </Link>
        <Link
          to="/user/orders"
          className="block px-4 py-2 text-gray-700 hover:bg-gray-100 transition-colors duration-200"
        >
          My Orders
        </Link>
        <Link
          to="/user/wishlist"
          className="block px-4 py-2 text-gray-700 hover:bg-gray-100 transition-colors duration-200"
        >
          My Wishlist
        </Link>
        <Link
          to="/user/points"
          className="block px-4 py-2 text-gray-700 hover:bg-gray-100 transition-colors duration-200"
        >
          My Points
        </Link>

        {user?.role === "admin" && (
          <>
            <Link
              to="/admin/products"
              className="block px-4 py-2 text-gray-700 hover:bg-gray-100 transition-colors duration-200"
            >
              My Products
            </Link>
            <Link
              to="/admin/add-product"
              className="block px-4 py-2 text-gray-700 hover:bg-gray-100 transition-colors duration-200"
            >
              Add Product
            </Link>
          </>
        )}
        <button
          onClick={handleLogout}
          className="w-full text-left block px-4 py-2 text-gray-700 hover:bg-gray-100 transition-colors duration-200"
        >
          Logout
        </button>
      </div>
    </div>
  );
};

export default UserDropdown;
