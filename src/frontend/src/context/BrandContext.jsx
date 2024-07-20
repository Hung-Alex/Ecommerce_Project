import React, { createContext, useContext } from "react";
import useFetch from "../hooks/useFetch";

// Tạo BrandContext
export const BrandContext = createContext({
  brands: [],
  loading: false,
  error: false,
});

// Provider cho BrandContext
const BrandProvider = ({ children }) => {
  const { data: brands, loading, error } = useFetch("/brands");
  return (
    <BrandContext.Provider
      value={{
        brands,
        loading,
        error,
      }}
    >
      {children}
    </BrandContext.Provider>
  );
};

// Hook để sử dụng BrandContext dễ dàng hơn
export const useBrandContext = () => useContext(BrandContext);

export default BrandProvider;
