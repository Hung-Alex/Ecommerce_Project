import React, { useState, useCallback } from "react";
import Table from "../comp/Table.jsx";
import AddCategoryForm from "./AddCategoryForm";
import { deleteCategory } from "../../../api";

const AdminCategories = () => {
  const [showForm, setShowForm] = useState(false);
  const [editingCategory, setEditingCategory] = useState(null);
  const [refresh, setRefresh] = useState(""); // State to trigger refresh

  const handleAddCategory = useCallback(() => {
    setEditingCategory(null);
    setShowForm(true);
  }, []);

  const handleEdit = useCallback((row) => {
    setEditingCategory(row.id);
    setShowForm(true);
  }, []);

  const handleDelete = useCallback(async (row) => {
    await deleteCategory(row.id);
    setRefresh(prev => !prev); // Trigger refresh
  }, [deleteCategory]);

  const handleCloseForm = useCallback(() => {
    setRefresh(prev => !prev); // Trigger refresh
    setShowForm(false);
    setEditingCategory(null);
  }, []);

  return (
      <div className='p-6'>
        <Table
          apiUrl="/categories"
          columns={[
            { header: 'ID', accessor: 'id' },
            { header: 'Image', accessor: 'image' },
            { header: 'Name', accessor: 'name' },
            { header: 'URL Slug', accessor: 'urlSlug' },
            { header: 'Description', accessor: 'description' },
          ]}
          onEdit={handleEdit}
          onDelete={handleDelete}
          onAdd={handleAddCategory}
          refresh={refresh} // Pass refresh state to Table component if needed
          searchParam="Name"
        />
        {showForm && (
          <AddCategoryForm
            categoryId={editingCategory}
            onClose={handleCloseForm}
          />
        )}
      </div>
  );
};

export default AdminCategories;
