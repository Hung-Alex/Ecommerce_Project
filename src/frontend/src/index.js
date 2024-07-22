import React from "react";
import ReactDOM from "react-dom/client";
import { RouterProvider } from "react-router-dom";
import { Toaster } from "react-hot-toast";

// Import global styles
import "./index.css";

// Import application routes and context providers
import { router } from "./routes/Routes.jsx";
import CategoryProvider from "./context/CategoryContext.jsx";
import CartProvider from "./context/CartContext.jsx";
import UserProvider from "./context/Usercontext.jsx";
import BrandProvider from "./context/BrandContext.jsx";

// AppProviders component to wrap all context providers
const AppProviders = ({ children }) => (
  <CategoryProvider>
    <BrandProvider>
      <UserProvider>
        <CartProvider>
          {children}
        </CartProvider>
      </UserProvider>
    </BrandProvider>
  </CategoryProvider>
);

// Render the root of the React application
ReactDOM.createRoot(document.getElementById("root")).render(
  <React.StrictMode>
    <AppProviders>
      {/* Provide the router configuration */}
      <RouterProvider router={router} />
      {/* Display toast notifications */}
      <Toaster />
    </AppProviders>
  </React.StrictMode>
);
