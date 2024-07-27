import React, { useState, useEffect, useCallback } from "react";
import DashboardLayout from "../../../layout/DashboardLayout.jsx";
import Table from "../comp/Table.jsx";
import AddProductForm from "./AddProductForm"; // Assuming you have a similar form component for products
import axios from "../../../utils/axios";
import UpdateProductForm from "./UpdateProductForm.jsx";
import ImageUpload from "./UploadImg.jsx";

const AdminProducts = () => {
  const [products, setProducts] = useState([]);
  const [EditProduct, setEditingProduct] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [showForm, setShowForm] = useState(false);
  const [showEditForm, setShowEditForm] = useState(false);

  const fetchProducts = useCallback(async () => {
    try {
      const response = await axios.get("/products?SortColoumn=Name&SortBy=ASC");
      setProducts(response.data.data);
      setLoading(false);
    } catch (error) {
      setError(error);
      setLoading(false);
    }
  }, []);

  useEffect(() => {
    fetchProducts();
  }, [fetchProducts]);

  const deleteProduct = async (id) => {
    try {
      await axios.delete(`/products/${id}`);
      setProducts(prevList => prevList.filter(product => product.id !== id));
    } catch (error) {
      console.error("Error deleting product:", error);
    }
  };

  const handleDelete = useCallback(async (row) => {
    try {
      await deleteProduct(row.id);
    } catch (error) {
      console.error('Error deleting product:', error);
    }
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

  if (loading) return <p>Loading...</p>;
  if (error){console.log(error.message);}

  return (
    <DashboardLayout>
      <div className='p-6'>
        <ImageUpload />
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
