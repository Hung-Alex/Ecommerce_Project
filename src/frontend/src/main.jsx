import React from "react";
import ReactDOM from "react-dom/client";
import { RouterProvider } from "react-router-dom";
import { Toaster } from "react-hot-toast";
import "./index.css";
import { router } from "./routes/Routes.jsx";
import ProductProvider from "./context/ProductContext";
import CategoryProvider from "./context/CategoryContext.jsx";
import CartProvider from "./context/CartContext";
import UserProvider from "./context/Usercontext";

ReactDOM.createRoot(document.getElementById("root")).render(
  <React.StrictMode>
      <ProductProvider>
    <CategoryProvider>
        <CartProvider>
          <UserProvider>
            <RouterProvider router={router} />
            <Toaster />
          </UserProvider>
        </CartProvider>
    </CategoryProvider>
      </ProductProvider>
  </React.StrictMode>
);
