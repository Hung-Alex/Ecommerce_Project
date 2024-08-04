import React from 'react';
import { useNavigate } from 'react-router-dom';

const OrderSuccess = () => {
    const navigate = useNavigate();

    const handleBackToHome = () => {
        navigate('/'); // Navigate to the home page
    };

    return (
        <div className="min-h-screen bg-gray-100 p-6 flex items-center justify-center">
            <div className="max-w-md bg-white shadow-md rounded-lg p-6 text-center">
                <h1 className="text-2xl font-bold mb-4">Order Successful!</h1>
                <p className="text-gray-700 mb-4">
                    Thank you for your order. Your order has been confirmed and is being processed.
                </p>
                <button
                    onClick={handleBackToHome}
                    className="mt-4 w-full py-2 px-4 text-black font-semibold rounded-lg "
                >
                    Back to Home
                </button>
            </div>
        </div>
    );
};

export default OrderSuccess;
