import React from "react";
import { Link } from "react-router-dom";
import { useCategoryContext } from "../../../context/CategoryContext";

const CategoryList = () => {
  const { categories, loading, error } = useCategoryContext();

  if (loading) return <p>Loading...</p>;
  if (error) return <p>Error loading categories</p>;

  return (
    <div className="mt-10 px-3">
      <h3 className="text-2xl mb-3">Categories</h3>
      <ul className="text-sm">
        {categories.map((category) => (
          <li key={category.slug} className="border-b-2 py-1 pl-1">
            <Link to={`/category/${category.name}`}>{category.name}</Link>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default CategoryList;
