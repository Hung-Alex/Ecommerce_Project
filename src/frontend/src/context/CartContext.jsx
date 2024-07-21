// CartContext.js
import React, { createContext, useState, useEffect } from "react";
import axios from "../utils/axios"; // Import your axios instance

export const CartContext = createContext();

const CartContextProvider = (props) => {
  const [cart, setCart] = useState({ items: [], total: 0 });

  const fetchCart = async () => {
    try {
      const response = await axios.get("/carts");
      setCart(response.data.data || { items: [], total: 0 });
    } catch (error) {
      console.error("Error fetching cart:", error);
      setCart({ items: [], total: 0 });
    }
  };

  useEffect(() => {
    fetchCart();
  }, []);

  const addToCart = async ({ productId, quantity }) => {
    try {
      await axios.post("/carts", { productId, quantity });
      fetchCart();
    } catch (error) {
      console.error("Error adding to cart:", error);
    }
  };

  const updateCart = async ({ cartItemId, quantity }) => {
    try {
      await axios.put("/carts", { cartItemId, quantity });
      fetchCart();
    } catch (error) {
      console.error("Error updating cart:", error);
    }
  };

  const deleteFromCart = async (cartItemId) => {
    try {
      await axios.delete(`/carts/${cartItemId}`);
      fetchCart();
    } catch (error) {
      console.error("Error deleting from cart:", error);
    }
  };

  return (
    <CartContext.Provider
      value={{ cart, addToCart, updateCart, deleteFromCart }}
    >
      {props.children}
    </CartContext.Provider>
  );
};

export default CartContextProvider;
