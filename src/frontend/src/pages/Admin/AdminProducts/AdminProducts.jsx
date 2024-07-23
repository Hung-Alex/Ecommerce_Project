import React, { useState, useEffect } from 'react';
import DashboardLayout from '../../../layout/DashboardLayout.jsx';
import Table from '../comp/Table';
import useFetch from '../../../hooks/useFetch';

const AdminProducts = () => {
  const { data: products, loading, error } = useFetch('/products');
  const [data, setData] = useState([]);

  useEffect(() => {
    if (products) {
      // Giả sử mỗi sản phẩm có các thuộc tính như name, price, category, brand
      const filteredData = products.map(product => ({
        id: product.id,
        image: product.images,
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
    { header: 'image', accessor: 'image' },
    { header: 'Name', accessor: 'name' },
    { header: 'Price', accessor: 'price' },
    { header: 'Category', accessor: 'category' },
    { header: 'Brand', accessor: 'brand' },
  ];

  const handleEdit = (row) => {
    console.log('Edit', row.id);
    // Thực hiện chỉnh sửa
  };

  const handleDelete = (row) => {
    console.log('Delete', row.id);
    // Thực hiện xóa
    setData(data.filter(item => item.id !== row.id));
  };

  const handleAddProduct = () => {
    console.log('Add product button clicked');
    // Implement the logic for adding a product here
  };

  if (loading) return <p>Loading...</p>;
  if (error) return <p>Error loading products: {error.message}</p>;

  return (
    <DashboardLayout>
      <div className='p-6'>
        <Table
          columns={columns}
          data={data}
          onEdit={handleEdit}
          onDelete={handleDelete}
          onAdd={handleAddProduct}
        />
      </div>
    </DashboardLayout>
  );
};

export default AdminProducts;
