import React, { useContext, useState, useEffect } from "react";
import { BsCheck2 } from "react-icons/bs";
import { CartContext } from "../../context/CartContext";
import { Link } from "react-router-dom";

const ShoppingCart = () => {
  const { cart, addToCart, removeFromCart, deleteFromCart, totalPrice } = useContext(CartContext);
  const items = cart.items || [];
  const [localCart, setLocalCart] = useState(items);

  useEffect(() => {
    setLocalCart(items);
  }, [items]);

  const handleQuantityChange = async (itemId, newQuantity) => {
    if (newQuantity < 1) return; // Prevent negative quantities

    // Optimistic UI update
    setLocalCart(prevCart =>
      prevCart.map(item =>
        item.id === itemId ? { ...item, quantity: newQuantity } : item
      )
    );

    try {
      // Use addToCart to handle updating quantity
      await addToCart({ productId: itemId, quantity: newQuantity });
    } catch (error) {
      console.error("Error updating item quantity:", error);
      // Optionally, revert the optimistic update if needed
    }
  };

  const calculateTotalPrice = () => {
    return cart.total || 0; // Ensure total is defined and default to 0
  };

  return (
    <>
      {localCart.length > 0 ? (
        <section className="mx-auto max-w-2xl px-4 py-8 sm:px-6 sm:py-24 lg:max-w-7xl lg:px-8">
          <h1 className="text-4xl font-bold tracking-tight text-gray-900">Cart</h1>
          {/* products */}
          <div className="grid grid-cols-1 lg:grid-cols-5 gap-8 my-8">
            <div className="col-span-3 border-b">
              {localCart.map((item) => (
                <div key={item.id} className="flex gap-6 py-8 border-t">
                  <figure>
                    <img
                      src={item.image || "/path/to/default-image.jpg"} // Use default image if item.image is null
                      alt={item.productName}
                      className="h-[96px] w-[96px] md:h-[192px] md:w-[192px] object-cover object-center"
                    />
                  </figure>
                  <div className="flex-1 flex flex-col justify-between">
                    <div className="grid grid-cols-2 items-start justify-between">
                      {/* Product details */}
                      <div className="flex flex-col gap-1">
                        <h3>{item.productName}</h3>
                        <p>${item.price.toFixed(2)}</p>
                      </div>
                      {/* Quantity adjustment */}
                      <div className="flex gap-4 items-center justify-between">
                        <div className="flex gap-1">
                          <button
                            className="px-3 border"
                            onClick={() => handleQuantityChange(item.id, item.quantity - 1)}
                            disabled={item.quantity <= 1} // Disable decrement if quantity is 1
                          >
                            -
                          </button>
                          <input
                            className="p-1 border outline-orange-400 w-16"
                            value={item.quantity}
                            readOnly
                          />
                          <button
                            className="px-3 border"
                            onClick={() => handleQuantityChange(item.id, item.quantity + 1)}
                          >
                            +
                          </button>
                        </div>
                        {/* Remove button */}
                        <button onClick={() => deleteFromCart(item.id)}>
                          <svg
                            xmlns="http://www.w3.org/2000/svg"
                            fill="none"
                            viewBox="0 0 24 24"
                            strokeWidth={1.5}
                            stroke="currentColor"
                            className="w-6 h-6 text-gray-600"
                          >
                            <path
                              strokeLinecap="round"
                              strokeLinejoin="round"
                              d="M6 18L18 6M6 6l12 12"
                            />
                          </svg>
                        </button>
                      </div>
                    </div>
                    <div className="flex justify-between items-center gap-2">
                      <div className="flex items-center gap-2">
                        <BsCheck2 className="text-green-500" />
                        <p>In stock</p>
                      </div>
                      <p>
                        <span className="font-medium">Subtotal Price:</span>$
                        {(item.price * item.quantity).toFixed(2)}
                      </p>
                    </div>
                  </div>
                </div>
              ))}
            </div>
            {/* Checkout */}
            <div className="bg-[#F9FAFB] h-96 col-span-2 p-8 flex flex-col justify-between rounded">
              <h1 className="text-xl font-medium">Order summary</h1>
              <div className="flex flex-col gap-3">
                <div className="flex justify-between">
                  <p>Total Price</p> <p>${calculateTotalPrice().toFixed(2)}</p> {/* Use totalPrice from API */}
                </div>
                <hr />
                <div className="flex justify-between">
                  <p>Shipping estimate</p> <p>$5.00</p>
                </div>
                <hr />
                <div className="flex justify-between">
                  <p className="text-lg font-medium">Order total</p>
                  <p>${(calculateTotalPrice() + 5).toFixed(2)}</p> {/* Include shipping in total */}
                </div>
              </div>
              <Link
                to="/checkout"
                className="bg-[#274C5B] text-white text-lg text-center font-medium py-3 rounded w-full"
              >
                Checkout
              </Link>
            </div>
          </div>
        </section>
      ) : (
        <p className="flex justify-center mt-32 text-xl">No items in cart</p>
      )}
    </>
  );
};

export default ShoppingCart;
