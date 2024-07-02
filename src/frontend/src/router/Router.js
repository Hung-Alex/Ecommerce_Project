import Home from "../pages/Home/Home";
import About from "../pages/About/About";

/**
 * Routes for regular users of the application.
 */
export const routes = [
  { path: "/", element: <Home /> },
  { path: "/home", element: <Home /> },
  { path: "/about", element: <About /> }, // Thêm route cho trang About
];

/**
 * Routes for admin users of the application.
 */
export const adminRoutes = [
  { path: "/admin", element: <Home /> },
];
