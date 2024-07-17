import React from "react";
import Card from "../../../components/UI/Card/Card";

const Products = ({ products }) => {
  return (
    <div className="mb-8 px-4 lg:px-8">
      {products.length >= 0 ? (
        <div className="mt-6 grid grid-cols-1 gap-3 md:grid-cols-2 lg:grid-cols-3 2xl:grid-cols-4 xl:gap-x-8">
          {products.map((item) => (
            <Card key={item.id} item={item} />
          ))}
        </div>
      ) : (
        <h1 className="text-black text-xl text-center mt-32 ">
          No Products Found
        </h1>
      )}
    </div>
  );
};

export default Products;
