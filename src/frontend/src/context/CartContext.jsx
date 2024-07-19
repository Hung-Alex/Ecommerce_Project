// CartContext.js
import React, { createContext, useState, useEffect } from "react";

export const CartContext = createContext();

const CartContextProvider = (props) => {
  const initialCart = JSON.parse(localStorage.getItem("cart")) || [];
  const [cart, setCart] = useState(initialCart);

  useEffect(() => {
    localStorage.setItem("cart", JSON.stringify(cart));
  }, [cart]);

  const addToCart = (product) => {
    console.log(product);
    const existingItemIndex = cart.findIndex((item) => {
      return item.id === product.id;
    });

    if (existingItemIndex !== -1) {
      // If item already exists in cart, increase quantity
      const updatedCart = [...cart];
      updatedCart[existingItemIndex].quantity++;
      setCart(updatedCart);
    } else {
      // Otherwise, add new item to cart
      setCart([...cart, { ...product, quantity: 1 }]);
    }
  };

  const removeFromCart = (productId) => {
    const updatedCart = cart.map((item) =>
      item.id === productId && item.quantity > 1
        ? { ...item, quantity: item.quantity - 1 }
        : item
    );
    setCart(updatedCart.filter((item) => item.quantity > 0));
  };

  const deleteFromCart = (productId) => {
    setCart(cart.filter((item) => item.id !== productId));
  };

  const totalPrice = () => {
    return cart.reduce((total, item) => total + item.price * item.quantity, 0);
  };

  return (
    <CartContext.Provider
      value={{ cart, addToCart, removeFromCart, deleteFromCart, totalPrice }}
    >
      {props.children}
    </CartContext.Provider>
  );
};

export default CartContextProvider;
