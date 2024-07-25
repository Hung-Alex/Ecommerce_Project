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
      <ProductInfo />
      <RelatedProducts slug={data?.category?.urlSlug} />
    </div>
  );
};

export default Product;
