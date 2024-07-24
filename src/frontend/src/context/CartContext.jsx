import React, { createContext, useState, useEffect, useContext, useCallback } from "react";
import axios from "../utils/axios"; // Import your axios instance
import { UserContext } from "./UserContext";

export const CartContext = createContext();

const CartContextProvider = (props) => {
  const [cart, setCart] = useState({ items: [], total: 0 });
  const { user } = useContext(UserContext);

  const fetchCart = useCallback( async () => {
    if (!user) {
      setCart({ items: [], total: 0 }); // Set default cart when there's no user
      return;
    }

    try {
      const response = await axios.get("/carts");
      setCart(response.data.data || { items: [], total: 0 });
    } catch (error) {
      console.error("Error fetching cart:", error);
      setCart({ items: [], total: 0 });
    }
  });

  useEffect(() => {
    fetchCart();
  }, [user]); // Re-fetch cart when user changes

  const addToCart = async ({ productId, quantity }) => {
    if (!user) return; // Exit if there is no user

    try {
      await axios.post("/carts", { productId, quantity });
      fetchCart();
    } catch (error) {
      console.error("Error adding to cart:", error);
    }
  };

  const updateCart = async ({ cartItemId, quantity }) => {
    if (!user) return; // Exit if there is no user

    try {
      await axios.put("/carts", { cartItemId, quantity });
      fetchCart();
    } catch (error) {
      console.error("Error updating cart:", error);
    }
  };

  const deleteFromCart = async (cartItemId) => {
    if (!user) return; // Exit if there is no user

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
