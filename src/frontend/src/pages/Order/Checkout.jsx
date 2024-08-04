import React, { useContext, useState } from 'react';
import { formatCurrency } from '../../utils/formatCurrency';
import { CartContext } from '../../context/CartContext';
import { useNavigate } from 'react-router-dom';
import axios from '../../utils/axios';
import { toast } from 'react-hot-toast';

const Checkout = () => {
  const { cart } = useContext(CartContext);
  const [userInfo, setUserInfo] = useState({
    name: '',
    number: '',
    email: '',
    address: '',
    note: '',
    paymentMethod: 0,
  });
  const navigate = useNavigate();

  const grandTotal = cart.total;

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setUserInfo(prevState => ({
      ...prevState,
      [name]: name === 'paymentMethod' ? Number(value) : value
    }));
  };

  const handlePaymentMethod = (paymentMethod, paymentUrl) => {
    switch (paymentMethod) {
      case 0: // COD
        navigate('/order-success');
        break;
      case 1: // VNPAY
        if (paymentUrl) {
          window.location.href = paymentUrl;
        } else {
          toast.error('An error occurred with this payment method. Please try again.');
        }
        break;
      default:
        toast.error('Invalid payment method.');
    }
  };

  const handleSubmit = async (event) => {
    event.preventDefault();
    try {
      const response = await axios.post('/orders', {
        name: userInfo.name,
        email: userInfo.email,
        phone: userInfo.number,
        address: userInfo.address,
        note: userInfo.note,
        paymentMethod: Number(userInfo.paymentMethod),
      });
      if (response.data.isSuccess) {
        handlePaymentMethod(userInfo.paymentMethod, response.data.data.paymentUrl);
      }
    } catch (error) {
      console.error('Error placing order:', error);
      toast.error('An error occurred while placing the order. Please try again later.');
    }
  };

  return (
    <div className="min-h-screen bg-gray-100 p-6">
      <div className="max-w-4xl mx-auto bg-white shadow-md rounded-lg overflow-hidden">
        <header className="bg-gray-800 text-white py-4 px-6">
          <h1 className="text-2xl font-semibold">Checkout</h1>
          <p className="mt-2">
            <a href="/" className="underline">Home</a> <span> / Checkout</span>
          </p>
        </header>

        <section className="p-6">
          <h2 className="text-xl font-bold mb-4">Order Summary</h2>

          <form onSubmit={handleSubmit} className="space-y-6">
            <div className="bg-gray-50 p-4 rounded-lg shadow-sm">
              <h3 className="text-lg font-semibold mb-2">Items in Your Cart</h3>
              {cart.items.length > 0 ? (
                cart.items.map((item, index) => (
                  <div key={index} className="flex justify-between items-center border-b py-2">
                    <span className="font-medium">{item.productName}</span>
                    <span className="text-gray-700">{formatCurrency(item.price)} x {item.quantity}</span>
                  </div>
                ))
              ) : (
                <p className="text-gray-500">Your cart is empty!</p>
              )}
              <div className="flex justify-between items-center mt-4 font-semibold">
                <span>Total:</span>
                <span>{formatCurrency(grandTotal)}</span>
              </div>
              <a href="/cart" className="block mt-4 text-blue-500 underline">View Cart</a>
            </div>

            <div className="bg-gray-50 p-4 rounded-lg shadow-sm mt-6">
              <h3 className="text-lg font-semibold mb-2">Your Information</h3>
              <div className="mb-4">
                <label className="block mb-2">
                  Name:
                  <input
                    type="text"
                    name="name"
                    value={userInfo.name}
                    onChange={handleInputChange}
                    className="block w-full mt-1 p-2 border rounded-lg"
                    required
                  />
                </label>
                <label className="block mb-2">
                  Phone Number:
                  <input
                    type="text"
                    name="number"
                    value={userInfo.number}
                    onChange={handleInputChange}
                    className="block w-full mt-1 p-2 border rounded-lg"
                    required
                  />
                </label>
                <label className="block mb-2">
                  Email:
                  <input
                    type="email"
                    name="email"
                    value={userInfo.email}
                    onChange={handleInputChange}
                    className="block w-full mt-1 p-2 border rounded-lg"
                    required
                  />
                </label>
                <label className="block mb-2">
                  Address:
                  <input
                    type="text"
                    name="address"
                    value={userInfo.address}
                    onChange={handleInputChange}
                    className="block w-full mt-1 p-2 border rounded-lg"
                    required
                  />
                </label>
                <label className="block mb-2">
                  Note:
                  <textarea
                    name="note"
                    value={userInfo.note}
                    onChange={handleInputChange}
                    className="block w-full mt-1 p-2 border rounded-lg"
                  />
                </label>
              </div>

              <h3 className="text-lg font-semibold mb-2">Shipping Address</h3>
              <p className="text-gray-700 mb-4">
                <i className="fas fa-map-marker-alt"></i> {userInfo.address || 'Please enter your address!'}
              </p>

              <label className="block mb-4">
                Payment Method:
                <select
                  name="paymentMethod"
                  value={userInfo.paymentMethod}
                  onChange={handleInputChange}
                  className="block w-full mt-1 p-2 border rounded-lg"
                  required
                >
                  <option value="" disabled>Choose a payment method --</option>
                  <option value={0}>COD</option>
                  <option value={1}>VNPAY</option>
                </select>
              </label>

              <button
                type="submit"
                className={`mt-4 w-full py-2 px-4 text-white font-semibold rounded-lg ${userInfo.address ? 'bg-red-500 hover:bg-red-600' : 'bg-gray-500 cursor-not-allowed'}`}
                disabled={!userInfo.address}
              >
                Place Order
              </button>
            </div>
          </form>
        </section>
      </div>
    </div>
  );
};

export default Checkout;
