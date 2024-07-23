import React, { useState, useEffect } from "react";
import DashboardLayout from "../../../layout/DashboardLayout.jsx";
import Table from "../comp/Table";
import useFetch from "../../../hooks/useFetch";
import AddBrandForm from "./AddBrands"; // Import AddBrandForm

const AdminBrands = () => {
  const { data: brands = [], loading, error, refetch } = useFetch('/brands');
  const [data, setData] = useState([]);
  const [showForm, setShowForm] = useState(false);
  const [editingBrand, setEditingBrand] = useState(null);

  useEffect(() => {
    if (Array.isArray(brands)) {
      const filteredData = brands.map(brand => ({
        id: brand.id,
        name: brand.name,
        urlSlug: brand.urlSlug,
        description: brand.description
      }));
      setData(filteredData);
    } else {
      console.error('Expected an array of brands but got:', brands);
    }
  }, [brands]);

  const columns = [
    { header: 'ID', accessor: 'id' },
    { header: 'Name', accessor: 'name' },
    { header: 'URL Slug', accessor: 'urlSlug' },
    { header: 'Description', accessor: 'description' }
  ];

  const handleEdit = (row) => {
    setEditingBrand(row);
    setShowForm(true);
  };

  const handleDelete = async (row) => {
    try {
      await useFetch(`/brands/${row.id}`, "DELETE");
      setData(data.filter(item => item.id !== row.id));
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
          columns={columns}
          data={data}
          onEdit={handleEdit}
          onDelete={handleDelete}
          onAdd={handleAddBrand}
        />
        {showForm && (
          <AddBrandForm
            brand={editingBrand}
            onClose={handleCloseForm}
            refetch={refetch} // Refresh data
          />
        )}
      </div>
    </DashboardLayout>
  );
};

export default AdminBrands;
