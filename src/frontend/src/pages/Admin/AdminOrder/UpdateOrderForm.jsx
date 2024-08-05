import React, { useState, useEffect } from 'react';
import { changeOrderStatus, fetchOrdersStatus } from '../../../api'; // Ensure these functions exist

const getStatusColor = (statusCode, statusOptions) => {
  const status = statusOptions.find(s => s.code === statusCode);
  switch (status?.code) {
    case 'PENDING':
      return 'text-blue-500'; // Blue color for Pending
    case 'COMPLETED':
      return 'text-green-500'; // Green color for Completed
    case 'FAILED':
      return 'text-red-500'; // Red color for Failed
    default:
      return 'text-gray-500'; // Default color
  }
};

const UpdateOrderForm = ({ order, onClose }) => {
  const [currentStatusId, setCurrentStatusId] = useState(order.status);
  const [statusOptions, setStatusOptions] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  // Fetch status options on component mount
  useEffect(() => {
    const fetchStatusOptions = async () => {
      try {
        const res = await fetchOrdersStatus(); // Adjust if `data` is wrapped differently
        setStatusOptions(res.data); // Set status options directly from response data
        setLoading(false);
      } catch (err) {
        console.error("Error fetching status options:", err);
        setError('Failed to load status options');
        setLoading(false);
      }
    };

    fetchStatusOptions();
  }, []);

  // Handle status change
  const handleStatusChange = async () => {
    changeOrderStatus({
      orderId: order.id,
      statusId: currentStatusId
    }).then(res => {
      if (res?.isSuccess) {
        onClose();
      }
    })
  };

  if (loading) return <p className="text-center">Loading...</p>;
  if (error) return <p className="text-center text-red-500">{error}</p>;

  return (
    <div
      className="fixed inset-0 bg-gray-900 bg-opacity-50 flex items-center justify-center z-50"
      onClick={onClose}
    >
      <div
        className="bg-white p-6 rounded-lg shadow-lg w-full max-w-3xl"
        onClick={(e) => e.stopPropagation()}
      >
        <div className='flex justify-center font-bold items-center p-4'>
          <h2 className='text-3xl'>Edit Order:</h2>
          <h2 className={`  font-medium text-2xl ${getStatusColor(order.status, statusOptions)}`}>
            ({order.status})
          </h2>
        </div>
        <div className="mb-4">
          <p className="font-medium">Order ID: {order.id}</p>
          <p className="font-medium">Shipping Address: {order.shipAddress.address}</p>
          <p className="font-medium">Customer Name: {order.shipAddress.name}</p>
          <p className="font-medium">Customer Email: {order.shipAddress.email}</p>
          <p className="font-medium">Customer Phone: {order.shipAddress.phone}</p>
          <p className="font-medium text-2xl">Total Amount: {order.totalAmount.toLocaleString()} VND</p>
          {order.note && (
            <p className="font-medium mt-2">Note: {order.note}</p>
          )}
        </div>
        <div className="mb-4">
          <h3 className="text-lg font-semibold mb-2">Order Items</h3>
          <div className="overflow-x-auto">
            <table className="min-w-full divide-y divide-gray-200">
              <thead className="bg-gray-50">
                <tr>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Product Name</th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Quantity</th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Price</th>
                  <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Unit Price</th>
                </tr>
              </thead>
              <tbody className="bg-white divide-y divide-gray-200">
                {order.orderItems.map(item => (
                  <tr key={item.id}>
                    <td className="px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-900">{item.productName}</td>
                    <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">{item.quantity}</td>
                    <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">{item.price.toLocaleString()} VND</td>
                    <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">{item.unitPrice}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </div>
        <label className="block mb-4">
          <span className="block text-sm font-medium text-gray-700">Status:</span>
          <select
            value={currentStatusId}
            onChange={(e) => setCurrentStatus(e.target.value)}
            className="mt-1 block w-full border-gray-300 rounded-md shadow-sm focus:ring-blue-500 focus:border-blue-500 sm:text-sm"
          >
            {statusOptions.map(status => (
              <option key={status.id} value={status.display}>
                {status.display} {/* Use display property for option text */}
              </option>
            ))}
          </select>

        </label>
        <div className="flex justify-end gap-4">
          <button
            onClick={handleStatusChange}
            className="bg-green-500 text-white py-2 px-4 rounded-md shadow-sm hover:bg-green-600"
          >
            Save
          </button>
          <button
            onClick={onClose}
            className="bg-red-500 text-white py-2 px-4 rounded-md shadow-sm hover:bg-red-600"
          >
            Cancel
          </button>
        </div>
      </div>
    </div>
  );
};

export default UpdateOrderForm;
