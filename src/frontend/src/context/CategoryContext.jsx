import React, { createContext, useContext } from "react";
import useFetch from "../hooks/useFetch";

// Tạo CategoryContext
export const CategoryContext = createContext({
  categories: [],
  loading: false,
  error: false,
});

// Provider cho CategoryContext
const CategoryProvider = ({ children }) => {
  const { data: categories, loading, error } = useFetch("/categories");

  return (
    <CategoryContext.Provider
      value={{
        categories,
        loading,
        error,
      }}
    >
      {children}
    </CategoryContext.Provider>
  );
};

// Hook để sử dụng CategoryContext dễ dàng hơn
export const useCategoryContext = () => useContext(CategoryContext);

export default CategoryProvider;
