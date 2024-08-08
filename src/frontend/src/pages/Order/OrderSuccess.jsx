import React from 'react';
import { useNavigate, useLocation } from 'react-router-dom';

const OrderSuccess = () => {
    const navigate = useNavigate();
    const location = useLocation();

    // Helper function to extract query parameters
    const getQueryParams = () => {
        const searchParams = new URLSearchParams(location.search);
        return {
            amount: searchParams.get('vnp_Amount'),
            bankCode: searchParams.get('vnp_BankCode'),
            bankTranNo: searchParams.get('vnp_BankTranNo'),
            cardType: searchParams.get('vnp_CardType'),
            orderInfo: searchParams.get('vnp_OrderInfo'),
            payDate: searchParams.get('vnp_PayDate'),
            responseCode: searchParams.get('vnp_ResponseCode'),
            transactionNo: searchParams.get('vnp_TransactionNo'),
            transactionStatus: searchParams.get('vnp_TransactionStatus'),
            txnRef: searchParams.get('vnp_TxnRef'),
            secureHash: searchParams.get('vnp_SecureHash'),
        };
    };

    const paymentDetails = getQueryParams();

    // Check if VNPay or other bank-related parameters are present
    const isVNPay = paymentDetails.transactionNo && paymentDetails.bankCode;

    const handleBackToHome = () => {
        navigate('/'); // Navigate to the home page
    };

    return (
        <div className="min-h-screen bg-gray-100 p-6 flex items-center justify-center">
            <div className="max-w-md bg-white shadow-md rounded-lg p-6 text-center">
                {isVNPay ? (
                    <>
                        <h1 className="text-2xl font-bold mb-4">Payment Successful!</h1>
                        <p className="text-gray-700 mb-4">Thank you for your payment. Here are your transaction details:</p>
                        <ul className="text-left">
                            <li><strong>Amount:</strong> {paymentDetails.amount} VND</li>
                            <li><strong>Bank Code:</strong> {paymentDetails.bankCode}</li>
                            <li><strong>Bank Transaction No:</strong> {paymentDetails.bankTranNo}</li>
                            <li><strong>Card Type:</strong> {paymentDetails.cardType}</li>
                            <li><strong>Order Info:</strong> {paymentDetails.orderInfo}</li>
                            <li><strong>Payment Date:</strong> {paymentDetails.payDate}</li>
                            <li><strong>Transaction No:</strong> {paymentDetails.transactionNo}</li>
                        </ul>
                    </>
                ) : (
                    <>
                        <h1 className="text-2xl font-bold mb-4">Order Successful!</h1>
                        <p className="text-gray-700 mb-4">
                            Thank you for your order. Your order has been confirmed and is being processed.
                        </p>
                    </>
                )}
                <button
                    onClick={handleBackToHome}
                    className="mt-4 w-full py-2 px-4 text-black font-semibold rounded-lg"
                >
                    Back to Home
                </button>
            </div>
        </div>
    );
};

export default OrderSuccess;
