import React, { createContext, useContext, useState, useEffect } from "react";
import axios from "../utils/axios";  // Make sure you have axios setup correctly
import useFetch from "../hooks/useFetch";
// Tạo BrandContext
export const BrandContext = createContext({
  brands: [],
  loading: false,
  error: false,
  addBrand: () => {},
  updateBrand: () => {}
});

// Provider cho BrandContext
const BrandProvider = ({ children }) => {
  const { data: brands, loading, error } = useFetch("/brands");
  const [brandList, setBrandList] = useState(brands || []);
  
  useEffect(() => {
    if (brands) {
      setBrandList(brands);
    }
  }, [brands]);

  const addBrand = async (brand) => {
    try {
      const response = await axios.post("/brands", brand);
      setBrandList([...brandList, response.data]);
    } catch (error) {
      console.error("Error adding brand:", error);
    }
  };

  const updateBrand = async (id, updatedBrand) => {
    try {
      const response = await axios.put(`/brands/${id}`, updatedBrand);
      setBrandList(
        brandList.map((brand) => (brand.id === id ? response.data : brand))
      );
    } catch (error) {
      console.error("Error updating brand:", error);
    }
  };

  return (
    <BrandContext.Provider
      value={{
        brands: brandList,
        loading,
        error,
        addBrand,
        updateBrand
      }}
    >
      {children}
    </BrandContext.Provider>
  );
};

// Hook để sử dụng BrandContext dễ dàng hơn
export const useBrandContext = () => useContext(BrandContext);

export default BrandProvider;
