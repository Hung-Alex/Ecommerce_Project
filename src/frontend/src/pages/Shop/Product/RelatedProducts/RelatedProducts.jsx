import React, { useContext } from "react";
import Card from "../../../../components/UI/Card/Card";
import { useParams } from "react-router-dom";
import useFetch from "../../../../hooks/useFetch";
import { ProductContext } from "../../../../context/ProductContext";

const RelatedProducts = () => {
  const { category } = useParams();
  useFetch(`/products?PageSize=4`);
  const { products } = useContext(ProductContext);
  return (
    <div className="my-24 mx-auto max-w-2xl px-4 py-16 sm:px-6 sm:py-24 lg:max-w-7xl lg:px-8">
      <h3 className="text-2xl text-gray-600">Related Products</h3>
      <div className="mt-6 grid grid-cols-1 gap-x-6 gap-y-10 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 xl:gap-x-8">
        {products.map((item,index) => (
          <Card key={index} item={item} />
        ))}
      </div>
    </div>
  );
};

export default RelatedProducts;
