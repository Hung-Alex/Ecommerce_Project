import React, { useState } from "react";
import { BsChevronDown } from "react-icons/bs";
import { Link } from "react-router-dom";

const DropDown = () => {
  const [isDropdownOpen, setDropdownOpen] = useState(false);

  const handleMouseEnter = () => {
    setDropdownOpen(true);
  };

  const handleMouseLeave = () => {
    setDropdownOpen(false);
  };

  const categories = [
    { name: "Honey", slug: "honey" },
    { name: "Ghee", slug: "ghee" },
    { name: "Oil", slug: "oil" },
    { name: "Fruits", slug: "fruit" },
    { name: "Nuts & Seeds", slug: "nuts" },
    { name: "Tea & Snacks", slug: "tea" },
    { name: "Spices", slug: "spices" },
  ];

  return (
    <div
      className="relative inline-block text-left"
      onMouseEnter={handleMouseEnter}
      onMouseLeave={handleMouseLeave}
    >
      <button
        className="inline-flex items-center justify-center p-2 rounded-md text-gray-600 hover:text-gray-800 focus:outline-none"
      >
        Category
        <BsChevronDown className="ml-1 text-lg" />
      </button>

      {isDropdownOpen && (
        <div className="origin-top-right absolute right-0 mt-2 w-48 shadow-lg bg-white ring-1 ring-black ring-opacity-5 text-base font-normal">
          <div className="">
            {categories.map((category, index) => (
              <li key={index} className="border-b-2 py-1 pl-3">
                <Link to={`/category/${category.slug}`}>{category.name}</Link>
              </li>
            ))}
          </div>
        </div>
      )}
    </div>
  );
};

export default DropDown;
