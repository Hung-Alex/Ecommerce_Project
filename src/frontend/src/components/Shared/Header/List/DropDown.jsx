import { BsChevronDown } from "react-icons/bs";
import { Link } from "react-router-dom";
import React, { useState} from "react";
import { useCategoryContext } from "../../../../context/CategoryContext"

const DropDown = () => {
  const [isDropdownOpen, setDropdownOpen] = useState(false);
  const { categories, loading, error } = useCategoryContext();

  if (loading) return <p>Loading...</p>;
  if (error) return <p>Error loading categories</p>;

  const handleMouseEnter = () => {
    setDropdownOpen(true);
  };

  const handleMouseLeave = () => {
    setDropdownOpen(false);
  };

  return (
    <div
      className="relative inline-block text-left"
      onMouseEnter={handleMouseEnter}
      onMouseLeave={handleMouseLeave}
    >
      <Link to="/category/category">
        <button
          className="inline-flex items-center justify-center p-2 rounded-md text-gray-600 hover:text-gray-800 focus:outline-none"
        >
          Category
          <BsChevronDown className="ml-1 text-lg" />
        </button>
      </Link>
      {isDropdownOpen && (
        <div className="origin-top-right absolute right-0 w-48 shadow-lg bg-white ring-1 ring-black ring-opacity-5 text-base font-normal">
          <div className="">
            {categories.map((category, index) => (
              <li key={index} className="border-b-2 py-1 pl-3">
                <Link to={`/category/${category.id}`}>{category.name}</Link>
              </li>
            ))}
          </div>
        </div>
      )}
    </div>
  );
};

export default DropDown;
