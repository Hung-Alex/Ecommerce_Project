// OrderStatus.jsx
import React, { useState, useEffect } from 'react';
import axios from '../../utils/axios';
import OrderList from './OrderList';
import OrderBanner from './Banner/OrderBanner';
import { fetchOrdersStatus } from '../../api';

const OrderStatus = () => {
  const [statuses, setStatuses] = useState([]);
  const [selectedStatus, setSelectedStatus] = useState(null);

  useEffect(() => {
    fetchOrdersStatus()
      .then(response => {
        setStatuses(response.data);
      })
  }, []);

  const handleStatusClick = (statusId) => {
    setSelectedStatus(statusId);
  };

  return (
    <div>
      <OrderBanner />
      <div className="container mx-auto p-4">
        <div className="flex justify-around mt-4">
          <button
            key={null}
            onClick={() => handleStatusClick(null)}
            className={`px-4 py-2 rounded-lg ${selectedStatus === null ? 'bg-green-500 text-white' : 'bg-gray-200 text-gray-600 hover:bg-gray-300'}`}
          >
            ALL
          </button>
          {statuses.map(status => (
            <button
              key={status.id}
              onClick={() => handleStatusClick(status.id)}
              className={`px-4 py-2 rounded-lg ${selectedStatus === status.id ? 'bg-green-500 text-white' : 'bg-gray-200 text-gray-600 hover:bg-gray-300'}`}
            >
              {status.display}
            </button>
          ))}
        </div>
        {<OrderList statusId={selectedStatus} />}
      </div>
    </div>
  );
};

export default OrderStatus;
