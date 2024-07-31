import PrivateRoute from "./PrivateRoute";
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

// Import các trang của bạn
import AdminProducts from "../pages/Admin/AdminProducts/AdminProducts";
import AdminBrands from "../pages/Admin/AdminBrands/AdminBrands";
import AdminCategories from "../pages/Admin/AdminCategory/AdminCategory";
import AdminBanners from "../pages/Admin/AdminBanner/AdminBanners";
import AdminRoles from "../pages/Admin/AdminRoles/AdminRoles";
import AdminSliders from "../pages/Admin/AdminSlider/AdminSliders";
import AdminNews from "../pages/Admin/AdminNews/AdminNews";
import AdminUsers from "../pages/Admin/AdminUser/AdminUser";

// Định nghĩa các routes cho người dùng bình thường
export const routes = [
  { path: '/', element: <HomePage /> },
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
  { path: '/login', element: <Login /> },
  { path: '/signup', element: <SignUp /> },
  { path: '/user/profile', element: <PrivateRoute><Profile /></PrivateRoute> },
  { path: '/user/profile/update', element: <PrivateRoute><UpdateProfile /></PrivateRoute> },
];


// Định nghĩa các routes cho quản trị viên
export const adminRoutes = [
  { path: '/admin', element: <AdminProducts /> },
  { path: '/admin/products', element: <AdminProducts /> },
  { path: '/admin/brands', element: <AdminBrands /> },
  { path: '/admin/banner', element: <AdminBanners /> },
  { path: '/admin/category', element: <AdminCategories /> },
  { path: '/admin/roles', element: <AdminRoles /> },
  { path: '/admin/users', element: <AdminUsers /> },
  { path: '/admin/sliders', element: <AdminSliders /> },
  { path: '/admin/news', element: <AdminNews /> },
];
