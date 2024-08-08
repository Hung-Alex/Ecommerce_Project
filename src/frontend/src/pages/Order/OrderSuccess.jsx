import React from 'react';
import { useNavigate, useLocation } from 'react-router-dom';
import errorMessages from '../../config/errorMessages';

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
    const responseCode = paymentDetails.responseCode;

    // Determine success and error message based on responseCode
    const getErrorMessage = (code) => {
        return errorMessages[code] || 'Unable to determine the transaction result.';
    };

    const isSuccess = responseCode === '00'; // '00' indicates success

    const handleBackToHome = () => {
        navigate('/'); // Navigate to the home page
    };

    return (
        <div className="min-h-screen bg-gray-100 p-6 flex items-center justify-center">
            <div className="max-w-md bg-white shadow-md rounded-lg p-6 text-center">
                <h1 className="text-2xl font-bold mb-4">
                    {isSuccess ? 'Payment Successful!' : 'Payment Failed'}
                </h1>
                <p className="text-gray-700 mb-4">
                    {isSuccess ?
                        'Thank you for your payment. Here are your transaction details:' :
                        `Unfortunately, your payment could not be processed. ${getErrorMessage(responseCode)}`}
                </p>
                {isSuccess && (
                    <ul className="text-left">
                        <li><strong>Amount:</strong> {paymentDetails.amount} VND</li>
                        <li><strong>Bank Code:</strong> {paymentDetails.bankCode}</li>
                        <li><strong>Bank Transaction No:</strong> {paymentDetails.bankTranNo}</li>
                        <li><strong>Card Type:</strong> {paymentDetails.cardType}</li>
                        <li><strong>Order Info:</strong> {paymentDetails.orderInfo}</li>
                        <li><strong>Payment Date:</strong> {paymentDetails.payDate}</li>
                        <li><strong>Transaction No:</strong> {paymentDetails.transactionNo}</li>
                    </ul>
                )}
                <button
                    onClick={handleBackToHome}
                    className="mt-4 w-full py-2 px-4 text-black font-semibold rounded-lg border border-gray-300 bg-gray-200 hover:bg-gray-300"
                >
                    Back to Home
                </button>
            </div>
        </div>
    );
};

export default OrderSuccess;
