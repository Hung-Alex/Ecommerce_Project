import React, { createContext, useContext } from "react";
import useFetch from "../hooks/useFetch";

// Tạo ProductContext
export const ProductContext = createContext({
  products: [],
  loading: false,
  error: false,
});

// Provider cho ProductContext
const ProductProvider = ({ children }) => {
  const { data: products, loading, error } = useFetch("/products");
  return (
    <ProductContext.Provider
      value={{
        products,
        loading,
        error,
      }}
    >
      {children}
    </ProductContext.Provider>
  );
};

// Hook để sử dụng ProductContext dễ dàng hơn
export const useProductContext = () => useContext(ProductContext);

export default ProductProvider;
