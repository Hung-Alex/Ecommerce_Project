import React, { createContext, useContext, useState, useEffect } from "react";
import axios from "../utils/axios";
import useFetch from "../hooks/useFetch";

export const CategoryContext = createContext({
  categories: [],
  loading: false,
  error: false,
  addCategory: () => {},
  updateCategory: () => {},
  deleteCategory: () => {},
  getCategories: () => {}
});

const CategoryContextProvider = ({ children }) => {
  const { data: initialCategories, loading, error } = useFetch("/categories");
  const [categoryList, setCategoryList] = useState(initialCategories || []);

  useEffect(() => {
    if (initialCategories) {
      setCategoryList(initialCategories);
    }
  }, [initialCategories]);

  const getCategories = async () => {
    try {
      const response = await axios.get("/categories");
      setCategoryList(response.data);
    } catch (error) {
      console.error("Error fetching categories:", error);
    }
  };

  const addCategory = async (category) => {
    try {
      const formData = new FormData();
      formData.append("name", category.name);
      formData.append("urlSlug", category.urlSlug);
      formData.append("description", category.description);
      formData.append("FormFile", category.image);

      const response = await axios.post("/categories", formData, {
        headers: {
          "Content-Type": "multipart/form-data"
        }
      });

      // Add the newly created category to the state immediately
      setCategoryList(prevList => [
        ...prevList,
        {
          id: Date.now(), // Temporary ID, replace with real ID if available
          ...category
        }
      ]);
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
