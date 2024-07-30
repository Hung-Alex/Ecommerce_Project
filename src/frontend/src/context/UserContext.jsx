import { createContext, useState, useEffect } from "react";
import axios from "../utils/axios";
import Cookies from 'js-cookie';
import { checkTokenExpiration } from '../utils/tokenUtils'; // Import hàm kiểm tra thời hạn token
import { toast } from 'react-hot-toast'; // Thư viện thông báo

export const UserContext = createContext({
  user: null,
  setUser: () => { },
  login: (data) => { },
  logout: () => { },
  refreshToken: () => { },
  checkAuthStatus: () => { },
});

const UserProvider = ({ children }) => {
  const [user, setUser] = useState(null);

  // Function to get user info from cookies
  const getUserFromCookies = () => {
    const userInfo = Cookies.get('info-user');
    return userInfo ? JSON.parse(userInfo) : null;
  };

  // Function to check and update the user's authentication status
  const checkAuthStatus = async () => {
    try {
      const accessToken = Cookies.get('access-token');

      if (accessToken) {
        // const expirationStatus = checkTokenExpiration(accessToken);
        // console.log(expirationStatus); // Optional: Log the token expiration status

        // Check authentication status when component mounts
        const userFromCookies = getUserFromCookies();
        if (userFromCookies) {
          setUser(userFromCookies);
        }
      } else {
        const refreshTokens = Cookies.get('refresh-token');

        if (refreshTokens) {
          await refreshToken(); // Try refreshing the token
          await checkAuthStatus(); // Retry checking status
        } else {
          setUser(null); // No token available
        }
      }
    } catch (error) {
      console.error("Failed to check authentication status: ", error);
      setUser(null);
    }

  };

  const refreshToken = async () => {
    try {
      await axios.post("/authentications/refresh");
      await checkAuthStatus();
    } catch (error) {
      console.error("Failed to refresh token: ", error);
      // logout(); // Handle token refresh failure
      // toast.error('login lại đi'); // Hiển thị thông báo lỗi
      throw error; // Re-throw error for further handling
    }
  };
  const login = async (data) => {
    try {
      const response = await axios.post("/authentications/login", data);
      if (response) {
        const userFromCookies = getUserFromCookies();
        if (userFromCookies) {
          setUser(userFromCookies);
        }
      }
    } catch (error) {
      console.error("Login failed: ", error);
    }
  };

  const logout = async () => {
    try {
      await axios.get("/authentications/logout");
    } catch (error) {
      console.log("Logout failed: ", error);
    } finally {
      setUser(null);
    }
  };

  useEffect(() => {
    checkAuthStatus();
  }, []);

  const userInfo = {
    user,
    setUser,
    login,
    logout,
    refreshToken,
    checkAuthStatus,
  };

  return (
    <UserContext.Provider value={userInfo}>{children}</UserContext.Provider>
  );
};

export default UserProvider;
