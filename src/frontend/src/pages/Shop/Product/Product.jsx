import React from "react";
import { useParams } from "react-router-dom";
import useFetch from "../../../hooks/useFetch";
import ProductBanner from "./Banner/ProductBanner";
import ProductInfo from "./ProductInfo";
import RelatedProducts from "./RelatedProducts/RelatedProducts";

const Product = () => {

  const { slug } = useParams();
  const { data } = useFetch(`/products/${slug}`);

  return (
    <div>
      <ProductBanner />
      <div className="flex flex-col md:flex-row justify-center items-center gap-4 bg-[#f9f8f8] pt-24 pb-12">
        <ProductInfo productData={data} />
      </div>
      <RelatedProducts slug={data?.category?.urlSlug} />
    </div>
  );
};

export default Product;
