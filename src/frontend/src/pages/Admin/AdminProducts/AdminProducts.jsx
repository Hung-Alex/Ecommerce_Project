import React, { useState, useCallback } from "react";
import Table from "../comp/Table.jsx";
import AddProductForm from "./AddProductForm"; // Assuming you have a similar form component for products
import UpdateProductForm from "./UpdateProductForm";
import { deleteProductId } from '../../../api/index';

const AdminProducts = () => {
  const [showForm, setShowForm] = useState(false);
  const [showEditForm, setShowEditForm] = useState(false);
  const [refresh, setRefresh] = useState(""); // State to trigger refresh

  const handleDelete = useCallback(async (row) => {
    const res = await deleteProductId(row.id);
      setRefresh((prev) => !prev); // Trigger refresh
  }, []);

  const handleAddProduct = useCallback(() => {
    setShowForm(true);
  }, []);

  const handleEdit = useCallback((row) => {
    setShowEditForm(row.id);
  }, []);

  const handleCloseForm = useCallback(() => {
    setShowForm(false);
    setShowEditForm(false);
    setRefresh((prev) => !prev); // Trigger refresh
  }, []);

  return (
      <div className='md:p-6'>
        <Table
          apiUrl="/products"
          columns={[
            { header: 'ID', accessor: 'id' },
            { header: 'Image', accessor: 'images' },
            { header: 'Name', accessor: 'name' },
            { header: 'URL Slug', accessor: 'urlSlug' },
            { header: 'Description', accessor: 'description' },
          ]}
          onEdit={handleEdit}
          onDelete={handleDelete}
          onAdd={handleAddProduct}
          searchParam="Name" // Specify the search parameter name as per the API requirement
          refresh={refresh} // Pass refresh state to Table component
        />
        {showForm && (
          <AddProductForm onClose={handleCloseForm} />
        )}
        {showEditForm && (
          <UpdateProductForm
            productId={showEditForm}
            onClose={handleCloseForm}
          />
        )}
      </div>
  );
};

export default AdminProducts;
