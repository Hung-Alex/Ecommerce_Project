import React from "react";
import { useParams } from "react-router-dom";
import useFetch from "../../../hooks/useFetch";
import ProductBanner from "./Banner/ProductBanner";
import ProductInfo from "./ProductInfo";
import RelatedProducts from "./RelatedProducts/RelatedProducts";
import Header from "../../../components/Shared/Header/Header";
import Footer from "../../../components/Shared/Footer/Footer";

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
