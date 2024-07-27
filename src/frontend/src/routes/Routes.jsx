import { createBrowserRouter } from "react-router-dom";
import Root from "../layout/Root";
import HomePage from "../pages/Home/Home";
import AboutPage from "../pages/About/About";
import ShopPage from "../pages/Shop/Shop";
import Product from "../pages/Shop/Product/Product";
import Team from "../pages/Team/Team";
import Login from "../pages/Auth/Login";
import SignUp from "../pages/Auth/Signup";
import Profile from "../pages/User/Profile";
import PrivateRoute from "./PrivateRoute";
import ShoppingCart from "../pages/Cart/ShoppingCart";
import UpdateProfile from "../pages/User/UpdateProfile";
import Checkout from "../pages/Order/Checkout";
import Error from "../pages/Error/Error";
import AdminProducts from "../pages/Admin/AdminProducts/AdminProducts";
import AdminBrands from "../pages/Admin/AdminBrands/AdminBrands";
import AdminCategories from "../pages/Admin/AdminCategory/AdminCategory";
import AdminBanners from "../pages/Admin/AdminBanner/AdminBanners";
import AdminRoles from "../pages/Admin/AdminRoles/AdminRoles";
import AdminSliders from "../pages/Admin/AdminSlider/AdminSliders";
import News from "../pages/News/News";
import NewsDetail from "../pages/News/NewsDetail";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <Root />,
    errorElement: <Error />,
    children: [
      {
        path: "/",
        element: <HomePage />,
      },
      {
        path: "/about",
        element: <AboutPage />,
      },
      {
        path: "/category/:name",
        element: <ShopPage />,
      },
      {
        path: "/news",
        element: <News />,
      },
      {
        path: "/news/:slug",
        element: <NewsDetail />,
      },
      {
        path: "/products/:slug",
        element: <Product />,
      },
      {
        path: "/our-team",
        element: <Team />,
      },
      {
        path: "/checkout",
        element: (
          <PrivateRoute>
            <Checkout />
          </PrivateRoute>
        ),
      },
      {
        path: "/cart",
        element: <ShoppingCart />,
      },
      {
        path: "/login",
        element: <Login />,
      },
      {
        path: "/signup",
        element: <SignUp />,
      },
    ],
  },
  {
    path: "/user",
    element: <PrivateRoute />, // Ensure all user routes are private
    children: [
      {
        path: "profile",
        element: <Profile />,
      },
      {
        path: "profile/update",
        element: <UpdateProfile />,
      },
    ],
  },
  {
    path: "/admin",
    // element: <PrivateRoute />, // Ensure all admin routes are private
    children: [
      {
        path: "",
        element: <AdminProducts />,
      },
      {
        path: "admin",
        element: <AdminProducts />,
      },
      {
        path: "products",
        element: <AdminProducts />,
      },
      {
        path: "brands",
        element: <AdminBrands />,
      },
      {
        path: "banner",
        element: <AdminBanners />,
      },
      {
        path: "category",
        element: <AdminCategories />,
      },
      {
        path: "Roles",
        element: <AdminRoles />,
      },
      {
        path: "Sliders",
        element: <AdminSliders />,
      },
    ],
  },
]);
