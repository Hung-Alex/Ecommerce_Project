import React, { useState, useCallback } from "react";
import Table from "../comp/Table.jsx";
import UpdateOrderForm from "./UpdateOrderForm.jsx";

const AdminOrder = () => {
  const [showForm, setShowForm] = useState(false);
  const [editingOrder, setEditingOrder] = useState(null);
  const [refresh, setRefresh] = useState(""); // State to trigger refresh

  // Handle editing of an order
  const handleEdit = useCallback((row) => {
    setEditingOrder(row);
    setShowForm(true);
  }, []);

  // Close the form and reset editing state
  const handleCloseForm = useCallback(() => {
    setShowForm(false);
    setEditingOrder(null);
    setRefresh(prev => !prev); // Trigger refresh
  }, []);

  return (
    <div className='p-6'>
      <Table
        apiUrl="/orders" // Specify the API URL for orders
        columns={[
          { header: 'ID', accessor: 'id' },
          { header: 'Status', accessor: 'status' },
          { header: 'Cancel Reason', accessor: 'cancelReason' },
          { header: 'Total Amount', accessor: 'totalAmount' },
          { header: 'Payment Status', accessor: 'paymentStatus' },
          { header: 'Payment Method', accessor: 'paymentMethod' }
        ]}
        onEdit={handleEdit}
        searchParam="ShipAddress" // Adjust search parameter if needed
        refresh={refresh} // Pass refresh state to Table component if needed
      />
      {showForm && (
        <UpdateOrderForm
          order={editingOrder}
          onClose={handleCloseForm}
        />
      )}
    </div>
  );
};

export default AdminOrder;
