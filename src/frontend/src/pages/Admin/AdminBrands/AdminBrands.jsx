import React, { useState, useEffect } from "react";
import DashboardLayout from "../../../layout/DashboardLayout.jsx";
import Table from "../comp/Table";
import useFetch from "../../../hooks/useFetch";
import AddBrandForm from "./AddBrands"; // Import AddBrandForm
import { useBrandContext } from "../../../context/BrandContext.jsx";

const AdminBrands = () => {
  const { brands, loading, error, addBrand, updateBrand, deleteBrand } = useBrandContext();
  const [showForm, setShowForm] = useState(false);
  const [editingBrand, setEditingBrand] = useState(null);

  const handleEdit = (row) => {
    setEditingBrand(row);
    setShowForm(true);
  };

  const handleDelete = async (row) => {
    try {
      await deleteBrand(row.id);
    } catch (error) {
      console.error('Error deleting brand:', error);
    }
  };

  const handleAddBrand = () => {
    setEditingBrand(null);
    setShowForm(true);
  };

  const handleCloseForm = () => {
    setShowForm(false);
    setEditingBrand(null);
  };

  if (loading) return <p>Loading...</p>;
  if (error) return <p>Error loading brands: {error.message}</p>;

  return (
    <DashboardLayout>
      <div className='p-6'>
        <Table
          columns={[
            { header: 'ID', accessor: 'id' },
            { header: 'Name', accessor: 'name' },
            { header: 'URL Slug', accessor: 'urlSlug' },
            { header: 'Description', accessor: 'description' },
            { header: 'image', accessor: 'image' }
          ]}
          data={brands}
          onEdit={handleEdit}
          onDelete={handleDelete}
          onAdd={handleAddBrand}
        />
        {showForm && (
          <AddBrandForm
            brand={editingBrand}
            onClose={handleCloseForm}
            addBrand={addBrand}
            updateBrand={updateBrand}
          />
        )}
      </div>
    </DashboardLayout>
  );
};

export default AdminBrands;