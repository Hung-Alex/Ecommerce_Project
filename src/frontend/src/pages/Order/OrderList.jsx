// OrderList.jsx
import React, { useState, useEffect } from 'react';
import axios from '../../utils/axios';
import CancelOrderPopup from './CancelOrderPopup';

const OrderList = ({ statusId }) => {
  const [orders, setOrders] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(10);
  const [totalPages, setTotalPages] = useState(1);
  const [showCancelPopup, setShowCancelPopup] = useState(false);
  const [selectedOrderId, setSelectedOrderId] = useState(null);

  useEffect(() => {
    setLoading(true);
    axios.get('/orders/user', {
      params: {
        Status: statusId,
        PageSize: pageSize,
        PageNumber: pageNumber,
        SortColumn: 'createdAt',
        SortBy: 'ASC'
      }
    })
      .then(response => {
        setOrders(response.data.data);
        setTotalPages(response.data.totalPages);
        setLoading(false);
      })
      .catch(error => {
        setError(error);
        setLoading(false);
      });
  }, [statusId, pageNumber, pageSize]);

  const handlePreviousPage = () => {
    if (pageNumber > 1) {
      setPageNumber(pageNumber - 1);
    }
  };

  const handleNextPage = () => {
    if (pageNumber < totalPages) {
      setPageNumber(pageNumber + 1);
    }
  };

  const openCancelPopup = (orderId) => {
    setSelectedOrderId(orderId);
    setShowCancelPopup(true);
  };

  const closeCancelPopup = () => {
    setShowCancelPopup(false);
    setSelectedOrderId(null);
  };

  const handleCancelSuccess = () => {
    setOrders(orders.filter(order => order.id !== selectedOrderId));
  };

  if (loading) return <p className="text-center text-gray-500">Loading...</p>;
  if (error) return <p className="text-center text-red-500">Error: {error.message}</p>;

  return (
    <div className="container mx-auto p-4">
      {orders.length === 0 ? (
        <p className="text-center text-gray-500 h-[50vh] flex justify-center items-center text-2xl text-black-500">No orders found.</p>
      ) : (
        <ul className="space-y-4">
          {orders.map(order => (
            <li key={order.id} className="bg-white p-4 rounded-lg shadow-md">
              <div className="flex justify-between items-center mb-4">
                <div>
                  <p className="font-semibold text-lg">Order ID: {order.id}</p>
                  <p><strong>Ship Address:</strong> {order.shipAddress.address}</p>
                  <p><strong>Name:</strong> {order.shipAddress.name}</p>
                  <p><strong>Email:</strong> {order.shipAddress.email}</p>
                  <p><strong>Phone:</strong> {order.shipAddress.phone}</p>
                  <p><strong>Payment Method:</strong> {order.paymentMethod}</p>
                  <p><strong>Status:</strong> {order.status}</p>
                </div>
                <div className="text-right">
                  <p className="text-lg font-semibold">Total Amount: {order.totalAmount.toLocaleString()} {order.orderItems[0].unitPrice}</p>
                </div>
              </div>
              <div className="mt-2">
                <h3 className="font-semibold mb-2">Order Items:</h3>
                <ul className="list-disc list-inside pl-4">
                  {order.orderItems.map(item => (
                    <li key={item.id} className="flex justify-between">
                      <span>{item.productName} - {item.quantity} x {item.price.toLocaleString()} {item.unitPrice}</span>
                      <span className="ml-4">{(item.quantity * item.price).toLocaleString()} {item.unitPrice}</span>
                    </li>
                  ))}
                </ul>
              </div>
              {order.status !== 'Cancelled' && (
                <button 
                  onClick={() => openCancelPopup(order.id)} 
                  className="mt-4 px-4 py-2 bg-red-500 text-white rounded-lg hover:bg-red-600"
                >
                  Cancel Order
                </button>
              )}
            </li>
          ))}
        </ul>
      )}
      <div className="mt-4 flex justify-between items-center">
        <button 
          onClick={handlePreviousPage} 
          className="px-4 py-2 bg-gray-200 text-gray-600 rounded-lg hover:bg-gray-300"
          disabled={pageNumber === 1}
        >
          Previous
        </button>
        <span className="text-gray-600">Page {pageNumber} of {totalPages}</span>
        <button 
          onClick={handleNextPage} 
          className="px-4 py-2 bg-gray-200 text-gray-600 rounded-lg hover:bg-gray-300"
          disabled={pageNumber === totalPages}
        >
          Next
        </button>
      </div>
      {showCancelPopup && (
        <CancelOrderPopup 
          orderId={selectedOrderId} 
          onClose={closeCancelPopup} 
          onCancelSuccess={handleCancelSuccess} 
        />
      )}
    </div>
  );
};

export default OrderList;
