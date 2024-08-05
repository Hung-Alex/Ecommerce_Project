import PrivateRoute from "./PrivateRoute";
import { createBrowserRouter, RouterProvider } from 'react-router-dom';
// Import các trang của bạn
import HomePage from "../pages/Home/Home";
import AboutPage from "../pages/About/About";
import ShopPage from "../pages/Shop/Shop";
import Product from "../pages/Shop/Product/Product";
import Team from "../pages/Team/Team";
import Login from "../pages/Auth/Login";
import SignUp from "../pages/Auth/Signup";
import Profile from "../pages/User/Profile";
import ShoppingCart from "../pages/Cart/ShoppingCart";
import UpdateProfile from "../pages/User/UpdateProfile";
import Checkout from "../pages/Order/Checkout";
import News from "../pages/News/News";
import NewsDetail from "../pages/News/NewsDetail";
import OrderSuccess from "../pages/Order/OrderSuccess";

// Import các trang của bạn
import AdminProducts from "../pages/Admin/AdminProducts/AdminProducts";
import AdminBrands from "../pages/Admin/AdminBrands/AdminBrands";
import AdminCategories from "../pages/Admin/AdminCategory/AdminCategory";
import AdminBanners from "../pages/Admin/AdminBanner/AdminBanners";
import AdminRoles from "../pages/Admin/AdminRoles/AdminRoles";
import AdminSliders from "../pages/Admin/AdminSlider/AdminSliders";
import AdminNews from "../pages/Admin/AdminNews/AdminNews";
import AdminUsers from "../pages/Admin/AdminUser/AdminUser";

import StandardLayout from "../layout/Layout";
import DashboardLayout from "../layout/DashboardLayout";

const routes = createBrowserRouter([
  {
    path: '',
    element: <StandardLayout />,
    errorElement: <Error />,  // Add errorElement for the StandardLayout
    children: [
      { path: '', element: <HomePage /> },
      { path: '/about', element: <AboutPage /> },
      { path: '/category/:name', element: <ShopPage /> },
      { path: '/news', element: <News /> },
      { path: '/news/:slug', element: <NewsDetail /> },
      { path: '/products/:slug', element: <Product /> },
      { path: '/our-team', element: <Team /> },
      {
        path: '/checkout',
        element: (
          <PrivateRoute>
            <Checkout />
          </PrivateRoute>
        ),
      },
      { path: '/cart', element: <ShoppingCart /> },
      { path: '/order-success', element: <OrderSuccess /> },
      { path: '/login', element: <Login /> },
      { path: '/signup', element: <SignUp /> },
      { path: '/user/profile', element: <PrivateRoute><Profile /></PrivateRoute> },
      { path: '/user/profile/update', element: <PrivateRoute><UpdateProfile /></PrivateRoute> },
    ],
  },
  {
    path: '/admin',
    element: <DashboardLayout />,
    errorElement: <Error />,  // Add errorElement for the AdminLayout
    children: [
      { path: '', element: <AdminProducts /> },
      { path: 'products', element: <AdminProducts /> },
      { path: 'brands', element: <AdminBrands /> },
      { path: 'banner', element: <AdminBanners /> },
      { path: 'category', element: <AdminCategories /> },
      { path: 'roles', element: <AdminRoles /> },
      { path: 'users', element: <AdminUsers /> },
      { path: 'sliders', element: <AdminSliders /> },
      { path: 'news', element: <AdminNews /> },
    ],
  },
  { path: '*', element: <Error /> },  // Catch-all route for undefined paths
]);

export default routes;