import React, { createContext, useContext, useState, useEffect } from "react";
import axios from "../utils/axios";
import useFetch from "../hooks/useFetch";

export const BrandContext = createContext({
  brands: [],
  loading: false,
  error: false,
  addBrand: () => {},
  updateBrand: () => {},
  deleteBrand: () => {},
  getBrands: () => {}
});

const BrandContextProvider = ({ children }) => {
  const { data: initialBrands, loading, error } = useFetch("/brands");
  const [brandList, setBrandList] = useState(initialBrands || []);

  useEffect(() => {
    if (initialBrands) {
      setBrandList(initialBrands);
    }
  }, [initialBrands]);

  const getBrands = async () => {
    try {
      const response = await axios.get("/brands");
      setBrandList(response.data);
    } catch (error) {
      console.error("Error fetching brands:", error);
    }
  };

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

      // Add the newly created brand to the state immediately
      setBrandList(prevList => [
        ...prevList,
        {
          id: Date.now(), // Temporary ID, replace with real ID if available
          ...brand
        }
      ]);
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
      if (updatedBrand.image) {
        formData.append("FormFile", updatedBrand.image);
      }

      await axios.put(`/brands/${id}`, formData, {
        headers: {
          "Content-Type": "multipart/form-data"
        }
      });

      // Update the brand in the state
      setBrandList(prevList =>
        prevList.map(brand =>
          brand.id === id ? { ...brand, ...updatedBrand } : brand
        )
      );
    } catch (error) {
      console.error("Error updating brand:", error);
    }
  };

  const deleteBrand = async (id) => {
    try {
      await axios.delete(`/brands/${id}`);

      // Remove the deleted brand from the state
      setBrandList(prevList => prevList.filter(brand => brand.id !== id));
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
        deleteBrand,
        getBrands
      }}
    >
      {children}
    </BrandContext.Provider>
  );
};

export const useBrandContext = () => useContext(BrandContext);

export default BrandContextProvider;
