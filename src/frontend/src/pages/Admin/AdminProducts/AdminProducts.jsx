import React, { useState, useEffect } from 'react';
import DashboardLayout from '../../../layout/DashboardLayout.jsx';
import Table from '../comp/Table';
import useFetch from '../../../hooks/useFetch';
import AddProductForm from './AddProduct.jsx';

const AdminProducts = () => {
  const { data: products, loading, error } = useFetch('/products?SortColoumn=name&SortBy=ASC');
  const [data, setData] = useState([]);
  const [showAddProductForm, setShowAddProductForm] = useState(false);
  const [currentProduct, setCurrentProduct] = useState(null);

  useEffect(() => {
    if (products) {
      // Map the products to match the table data structure
      const filteredData = products.map(product => ({
        id: product.id,
        image: product.images[0] || '', // Assuming the first image is to be shown
        name: product.name,
        price: product.price,
        category: product.category.name,
        brand: product.brand.name
      }));
      setData(filteredData);
    }
  }, [products]);

  const columns = [
    { header: 'ID', accessor: 'id' },
    { header: 'Image', accessor: 'image' },
    { header: 'Name', accessor: 'name' },
    { header: 'Price', accessor: 'price' },
    { header: 'Category', accessor: 'category' },
    { header: 'Brand', accessor: 'brand' },
    { header: 'Actions', accessor: 'actions' }
  ];

  const handleEdit = (row) => {
    setCurrentProduct(row);
    setShowAddProductForm(true);
  };

  const handleDelete = async (id) => {
    try {
      await fetch(`/products/${id}`, { method: 'DELETE' });
      // Refresh the product list
      const response = await fetch('/products');
      const updatedProducts = await response.json();
      setData(updatedProducts);
    } catch (error) {
      console.error('Error deleting product:', error);
    }
  };

  const handleAddProduct = () => {
    setCurrentProduct(null);
    setShowAddProductForm(true);
  };

  const handleCloseForm = () => {
    setShowAddProductForm(false);
    setCurrentProduct(null);
  };

  const handleSaveProduct = async (product) => {
    try {
      const formData = new FormData();
      formData.append('name', product.name);
      formData.append('description', product.description);
      formData.append('urlSlug', product.urlSlug);
      formData.append('price', product.price);
      formData.append('discount', product.discount);
      formData.append('brandId', product.brandId);
      formData.append('categoryId', product.categoryId);
      formData.append('variant', JSON.stringify(product.variant)); // Assuming variant is an array of objects
      formData.append('images', product.images); // Assuming images is an array of strings or URLs

      const method = product.id ? 'PUT' : 'POST';
      const endpoint = product.id ? `/products/${product.id}` : '/products';

      await fetch(endpoint, {
        method,
        body: formData
      });

      // Refresh the product list
      const response = await fetch('/products');
      const updatedProducts = await response.json();
      setData(updatedProducts);
    } catch (error) {
      console.error('Error saving product:', error);
    }
    handleCloseForm();
  };

  if (loading) return <p>Loading...</p>;
  if (error) return <p>Error loading products: {error.message}</p>;

  return (
    <DashboardLayout>
      <div className="p-6">
        <button
          onClick={handleAddProduct}
          className="mb-4 px-4 py-2 bg-blue-600 text-white rounded-md hover:bg-blue-700"
        >
          Add Product
        </button>

        <Table
          columns={columns}
          data={data.map(item => ({
            ...item,
            actions: (
              <div className="flex space-x-2">
                <button
                  onClick={() => handleEdit(item)}
                  className="text-blue-600 hover:text-blue-800"
                >
                  Edit
                </button>
                <button
                  onClick={() => handleDelete(item.id)}
                  className="text-red-600 hover:text-red-800"
                >
                  Delete
                </button>
              </div>
            )
          }))}
        />

        {showAddProductForm && (
          <AddProductForm
            product={currentProduct}
            onClose={handleCloseForm}
            saveProduct={handleSaveProduct}
          />
        )}
      </div>
    </DashboardLayout>
  );
};

export default AdminProducts;
