import React, { useState, useEffect, useCallback } from "react";
import DashboardLayout from "../../../layout/DashboardLayout.jsx";
import Table from "../comp/Table.jsx";
import AddProductForm from "./AddProductForm"; // Assuming you have a similar form component for products
import axios from "../../../utils/axios";
import UpdateProductForm from "./UpdateProductForm";
import {
  fetchProductsData,
  deleteProductId
} from '../../../api/index';

const AdminProducts = () => {
  const [products, setProducts] = useState([]);
  const [EditProduct, setEditingProduct] = useState([]);
  const [showForm, setShowForm] = useState(false);
  const [showEditForm, setShowEditForm] = useState(false);

  const fetchProducts = useCallback(async (sortColumn = 'CreatedAt', sortBy = 'DESC') => {
      const response = await fetchProductsData(sortColumn, sortBy);
      setProducts(response.data);
  }, []);

  useEffect(() => {
    fetchProducts(); // Calls with default parameters initially
  }, [fetchProducts]);

  const handleDelete = useCallback(async (row) => {
    deleteProductId(row.id).then(res => {
        if (res?.isSuccess) {
          fetchProducts();
        }
    })
  }, []);

  const handleAddProduct = useCallback(() => {
    setEditingProduct(null);
    setShowForm(true);
  }, []);

  const handleEdit = useCallback((row) => {
    setShowEditForm(row.id);
    setShowForm(true);
  }, []);

  const handleCloseForm = useCallback(() => {
    setShowForm(false);
    setShowEditForm(false);
    setEditingProduct(null);
    fetchProducts();
  }, []);

  return (
    <DashboardLayout>
      <div className='p-6'>
        <Table
          columns={[
            { header: 'ID', accessor: 'id' },
            { header: 'Image', accessor: 'images' },
            { header: 'Name', accessor: 'name' },
            { header: 'URL Slug', accessor: 'urlSlug' },
            { header: 'Description', accessor: 'description' },
          ]}
          data={products}
          onEdit={handleEdit}
          onDelete={handleDelete}
          onAdd={handleAddProduct}
        />
        {showForm && (
          <AddProductForm
            onClose={handleCloseForm}
          />
        )}
        {showEditForm && (
          <UpdateProductForm
            productId={showEditForm}
            onClose={handleCloseForm}
          />
        )}
      </div>
    </DashboardLayout>
  );
};

export default AdminProducts;
