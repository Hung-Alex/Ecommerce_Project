import CatCard from "../../../components/UI/Card/catCard";
import React from "react";
import { useCategoryContext } from "../../../context/CategoryContext.jsx"

const Category = () => {
  const { categories, loading, error } = useCategoryContext();

  if (loading) return <p>Loading...</p>;
  if (error) return <p>Error loading categories</p>;

  console.log(categories);

  return (
    <div className="mt-12">
      <div className="text-center">
        <p className="text-[#7EB693] font-[Yellowtail] text-3xl ">Categories</p>
        <h3 className="text-[#274C5B] text-4xl font-bold my-3 mb-8">
          Our Products
        </h3>
      </div>
      <div className="grid grid-cols-4 lg:grid-cols-6 gap-6 mt-16">
        {categories.map((item, index) => (
          <CatCard key={index} item={item} />
        ))}
      </div>
    </div>
  );
};

export default Category;
