import { BsChevronDown } from "react-icons/bs";
import { Link } from "react-router-dom";
import React, { useState, useEffect } from "react";
import axios from "../../../../utils/axios";

const DropDown = () => {
  const [isDropdownOpen, setDropdownOpen] = useState(false);
  const [categories, setCategories] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(false);

  useEffect(() => {
    (async () => {
      try {
        setLoading(true);
        const res = await axios.get(`/categories?PageSize=8`);
        setCategories(res.data.data);
        setLoading(false);
      } catch (error) {
        setLoading(false);
        setError(true);
      }
    })();
  }, []);
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
      <Link to="/category">
        <button
          className="inline-flex items-center justify-center p-2 rounded-md text-gray-600 hover:text-gray-800 focus:outline-none"
        >
          Category
          <BsChevronDown className="ml-1 text-lg" />
        </button>
      </Link>
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