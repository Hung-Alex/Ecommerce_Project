import React, { createContext, useContext, useState, useEffect } from "react";
import axios from "../utils/axios";  // Make sure you have axios setup correctly
import useFetch from "../hooks/useFetch";

// Create BrandContext
export const BrandContext = createContext({
  brands: [],
  loading: false,
  error: false,
  addBrand: () => {},
  updateBrand: () => {},
  deleteBrand: () => {}
});

// Provider for BrandContext
const BrandContextProvider = ({ children }) => {
  const { data: brands, loading, error } = useFetch("/brands");
  const [brandList, setBrandList] = useState(brands || []);

  useEffect(() => {
    if (brands) {
      setBrandList(brands);
    }
  }, [brands]);

  const addBrand = async (brand) => {
    try {
      const formData = new FormData();
      formData.append("name", brand.name);
      formData.append("urlSlug", brand.urlSlug);
      formData.append("description", brand.description);
      formData.append("FormFile", brand.image);

      const response = await axios.post("/brands", formData, {
        headers: {
          "Content-Type": "multipart/form-data"
        }
      });
      setBrandList([...brandList, response.data]);
    } catch (error) {
      console.error("Error adding brand:", error);
    }
  };

  const updateBrand = async (id, updatedBrand) => {
    try {
      const formData = new FormData();
      formData.append("id", id);
      formData.append("name", updatedBrand.name);
      formData.append("urlSlug", updatedBrand.urlSlug);
      formData.append("description", updatedBrand.description);
      formData.append("FormFile", updatedBrand.image);

      const response = await axios.put(`/brands/${id}`, formData, {
        headers: {
          "Content-Type": "multipart/form-data"
        }
      });
      setBrandList(
        brandList.map((brand) => (brand.id === id? response.data : brand))
      );
    } catch (error) {
      console.error("Error updating brand:", error);
    }
  };

  const deleteBrand = async (id) => {
    try {
      await axios.delete(`/brands/${id}`);
      setBrandList(brandList.filter((brand) => brand.id!== id));
    } catch (error) {
      console.error("Error deleting brand:", error);
    }
  };

  return (
    <BrandContext.Provider
      value={{
        brands: brandList,
        loading,
        error,
        addBrand,
        updateBrand,
        deleteBrand
      }}
    >
      {children}
    </BrandContext.Provider>
  );
};

// Hook to use BrandContext easily
export const useBrandContext = () => useContext(BrandContext);

export default BrandContextProvider;