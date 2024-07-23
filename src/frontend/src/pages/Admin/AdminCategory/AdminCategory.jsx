import React, { useState, useCallback } from "react";
import DashboardLayout from "../../../layout/DashboardLayout.jsx";
import Table from "../comp/Table.jsx";
import AddCategoryForm from "./AddCategoryForm";
import { useCategoryContext } from "../../../context/CategoryContext.jsx";

const AdminCategories = () => {
  const { categories, loading, error, addCategory, updateCategory, deleteCategory } = useCategoryContext();
  const [showForm, setShowForm] = useState(false);
  const [editingCategory, setEditingCategory] = useState(null);

  const handleEdit = useCallback((row) => {
    setEditingCategory(row);
    setShowForm(true);
  }, []);

  const handleDelete = useCallback(async (row) => {
    try {
      await deleteCategory(row.id);
    } catch (error) {
      console.error('Error deleting category:', error);
    }
  }, [deleteCategory]);

  const handleAddCategory = useCallback(() => {
    setEditingCategory(null);
    setShowForm(true);
  }, []);

  const handleCloseForm = useCallback(() => {
    setShowForm(false);
    setEditingCategory(null);
  }, []);

  if (loading) return <p>Loading...</p>;
  if (error) return <p>Error loading categories: {error.message}</p>;

  return (
    <DashboardLayout>
      <div className='p-6'>
        <Table
          columns={[
            { header: 'ID', accessor: 'id' },
            { header: 'image', accessor: 'image' },
            { header: 'Name', accessor: 'name' },
            { header: 'URL Slug', accessor: 'urlSlug' },
            { header: 'Description', accessor: 'description' },
          ]}
          data={categories}
          onEdit={handleEdit}
          onDelete={handleDelete}
          onAdd={handleAddCategory}
        />
        {showForm && (
          <AddCategoryForm
            category={editingCategory}
            onClose={handleCloseForm}
            addCategory={addCategory}
            updateCategory={updateCategory}
          />
        )}
      </div>
    </DashboardLayout>
  );
};

export default AdminCategories;
