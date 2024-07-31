import React from "react";
import ReactDOM from "react-dom/client";
import { Toaster } from "react-hot-toast";

// Import global styles
import "./index.css";

// Import application routes and context providers
import CategoryProvider from "./context/CategoryContext.jsx";
import CartProvider from "./context/CartContext.jsx";
import UserProvider from "./context/UserContext.jsx";
import BrandProvider from "./context/BrandContext.jsx";
import App from "./App.jsx";

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
      <App />
      <Toaster />
    </AppProviders>
  </React.StrictMode>
);
