// CartContext.js
import React, { createContext, useState, useEffect } from "react";
import axios from "../utils/axios"; // Import your axios instance

export const CartContext = createContext();

const CartContextProvider = (props) => {
  const [cart, setCart] = useState([]); // Ensure cart is always an array

  useEffect(() => {
    const fetchCart = async () => {
      try {
        const response = await axios.get("/carts");
        setCart(response.data.data || []);
      } catch (error) {
        console.error("Error fetching cart:", error);
        setCart([]);
      }
    };

    fetchCart();
  }, []);

  const addToCart = async ({ productId, quantity }) => {
    try {
      const response = await axios.post("/carts", { productId, quantity });
      setCart(response.data.data || []);
    } catch (error) {
      console.error("Error adding to cart:", error);
    }
  };

  const removeFromCart = async (productId) => {
    try {
      await axios.delete(`/carts/${productId}`);
      setCart(cart.filter(item => item.id !== productId));
    } catch (error) {
      console.error("Error removing from cart:", error);
    }
  };

  const deleteFromCart = async (productId) => {
    try {
      await axios.delete(`/carts/${productId}`);
      setCart(cart.filter(item => item.id !== productId));
    } catch (error) {
      console.error("Error deleting from cart:", error);
    }
  };

  return (
    <CartContext.Provider
      value={{ cart, addToCart, removeFromCart, deleteFromCart }}
    >
      {props.children}
    </CartContext.Provider>
  );
};

export default CartContextProvider;
