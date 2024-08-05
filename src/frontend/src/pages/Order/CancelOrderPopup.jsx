// CancelOrderPopup.jsx
import React, { useState } from 'react';
import axios from '../../utils/axios';

const CancelOrderPopup = ({ orderId, onClose, onCancelSuccess }) => {
  const [cancelReason, setCancelReason] = useState('');
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  const handleCancelOrder = () => {
    setLoading(true);
    axios.post('/orders/cancel-order', { orderId, cancelReason })
      .then(() => {
        setLoading(false);
        onCancelSuccess();
        onClose();
      })
      .catch(error => {
        setError(error);
        setLoading(false);
      });
  };

  return (
    <div className="fixed inset-0 bg-gray-800 bg-opacity-50 flex items-center justify-center z-50">
      <div className="bg-white p-6 rounded-lg shadow-lg w-96">
        <h2 className="text-xl font-semibold mb-4">Cancel Order</h2>
        <textarea
          value={cancelReason}
          onChange={(e) => setCancelReason(e.target.value)}
          placeholder="Enter cancel reason"
          className="w-full p-2 border border-gray-300 rounded mb-4"
        />
        {error && <p className="text-red-500 mb-4">Error: {error.message}</p>}
        <div className="flex justify-end">
          <button
            onClick={onClose}
            className="px-4 py-2 bg-gray-200 text-gray-600 rounded-lg mr-2 hover:bg-gray-300"
          >
            Close
          </button>
          <button
            onClick={handleCancelOrder}
            className="px-4 py-2 bg-red-500 text-white rounded-lg hover:bg-red-600"
            disabled={loading}
          >
            {loading ? 'Cancelling...' : 'Cancel Order'}
          </button>
        </div>
      </div>
    </div>
  );
};

export default CancelOrderPopup;
