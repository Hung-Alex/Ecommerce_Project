import React, { useState, useCallback } from "react";
import Table from "../comp/Table";
import AddBrandForm from "./AddBrands";
import { deleteBrand } from "../../../api";

const AdminBrands = () => {
  const [showForm, setShowForm] = useState(false);
  const [editingBrand, setEditingBrand] = useState(null);
  const [refresh, setRefresh] = useState(""); // State to trigger refresh

  const handleAddBrand = useCallback(() => {
    setEditingBrand(null);
    setShowForm(true);
  }, []);

  const handleEdit = useCallback((row) => {
    setEditingBrand(row.id);
    setShowForm(true);
  }, []);
  const handleDelete = useCallback(async (row) => {
    await deleteBrand(row.id);
    setRefresh(prev => !prev); // Trigger refresh
  }, [deleteBrand]);
  const handleCloseForm = useCallback(() => {
    setRefresh(prev => !prev); // Trigger refresh
    setShowForm(false);
    setEditingBrand(null);
  }, []);

  return (
      <div className='p-6'>
        <Table
          apiUrl="/brands"
          columns={[
            { header: 'ID', accessor: 'id' },
            { header: 'Image', accessor: 'image' },
            { header: 'Name', accessor: 'name' },
            { header: 'URL Slug', accessor: 'urlSlug' },
            { header: 'Description', accessor: 'description' },
          ]}
          onEdit={handleEdit}
          onDelete={handleDelete}
          onAdd={handleAddBrand}
          refresh={refresh} // Pass refresh state to Table component if needed
          searchParam="Name"
        />
        {showForm && (
          <AddBrandForm
            brandId={editingBrand}
            onClose={handleCloseForm}
          />
        )}
      </div>
  );
};

export default AdminBrands;
