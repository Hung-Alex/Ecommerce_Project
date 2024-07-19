import React from "react";
import Card from "../../../../components/UI/Card/Card";
import useFetch from "../../../../hooks/useFetch";

const RelatedProducts = ({ slug }) => {
  const fetchUrl = slug ? `searchs?UrlSlugCategory=${slug}` : 'searchs?UrlSlugCategory=spinach';
  const { data: product } = useFetch(fetchUrl);

  if (!product) return null;

  return (
    <div className="my-24 mx-auto max-w-2xl px-4 py-16 sm:px-6 sm:py-24 lg:max-w-7xl lg:px-8">
      <h3 className="text-2xl text-gray-600">Related Products</h3>
      <div className="mt-6 grid grid-cols-1 gap-x-6 gap-y-10 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 xl:gap-x-8">
        {product.map((item) => (
          <Card key={item?.id} item={item} />
        ))}
      </div>
    </div>
  );
};

export default RelatedProducts;
