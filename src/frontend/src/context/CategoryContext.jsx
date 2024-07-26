import React, { createContext, useContext, useState, useEffect, useCallback } from "react";
import axios from "../utils/axios";

export const CategoryContext = createContext({
  categories: [],
  loading: false,
  error: null,
  addCategory: () => {},
  updateCategory: () => {},
  deleteCategory: () => {},
  getCategories: () => {}
});

const CategoryContextProvider = ({ children }) => {
  const [categoryList, setCategoryList] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const getCategories = useCallback(async () => {
    setLoading(true);
    try {
      const response = await axios.get("/categories");
      setCategoryList(response.data.data || []);
      setError(null); // Clear any previous errors
    } catch (error) {
      setError(error);
    } finally {
      setLoading(false);
    }
  }, []);

  useEffect(() => {
    getCategories();
  }, [getCategories]);

  const addCategory = async (category) => {
    try {
      const formData = new FormData();
      formData.append("name", category.name);
      formData.append("urlSlug", category.urlSlug);
      formData.append("description", category.description);
      formData.append("FormFile", category.image);

      await axios.post("/categories", formData, {
        headers: {
          "Content-Type": "multipart/form-data"
        }
      });

      // Fetch the updated category list
      await getCategories();
    } catch (error) {
      console.error("Error adding category:", error);
    }
  };

  const updateCategory = async (id, updatedCategory) => {
    try {
      const formData = new FormData();
      formData.append("id", id);
      formData.append("name", updatedCategory.name);
      formData.append("urlSlug", updatedCategory.urlSlug);
      formData.append("description", updatedCategory.description);
      if (updatedCategory.image) {
        formData.append("FormFile", updatedCategory.image);
      }

      await axios.put(`/categories/${id}`, formData, {
        headers: {
          "Content-Type": "multipart/form-data"
        }
      });

      // Update the category in the state
      setCategoryList(prevList =>
        prevList.map(category =>
          category.id === id ? { ...category, ...updatedCategory } : category
        )
      );
    } catch (error) {
      console.error("Error updating category:", error);
    }
  };

  const deleteCategory = async (id) => {
    try {
      await axios.delete(`/categories/${id}`);

      // Remove the deleted category from the state
      setCategoryList(prevList => prevList.filter(category => category.id !== id));
    } catch (error) {
      console.error("Error deleting category:", error);
    }
  };

  return (
    <CategoryContext.Provider
      value={{
        categories: categoryList,
        loading,
        error,
        addCategory,
        updateCategory,
        deleteCategory,
        getCategories
      }}
    >
      {children}
    </CategoryContext.Provider>
  );
};

export const useCategoryContext = () => useContext(CategoryContext);

export default CategoryContextProvider;
