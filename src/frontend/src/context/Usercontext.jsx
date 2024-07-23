import { createContext, useState, useEffect } from "react";
import axios from "../utils/axios";
import Cookies from "js-cookie";
import img from "../assets/Home/img/user.png";

export const UserContext = createContext({
  user: null,
  setUser: () => {},
  login: (data) => {},
  logout: () => {},
  refreshToken: () => {},
});

const UserProvider = ({ children }) => {
  const [user, setUser] = useState(null);

  useEffect(() => {
    const token = Cookies.get("accessToken");
    if (token) {
      getUserInfo(); // Fetch user info on startup if token exists
    }
  }, []);

  const refreshToken = async () => {
    try {
      const response = await axios.post("/authentications/refresh", {
        refreshToken: Cookies.get("refreshToken"),
      });
      if (response.data.accessToken) {
        Cookies.set("accessToken", response.data.accessToken);
        return response.data.accessToken; // Return new token
      }
    } catch (error) {
      console.error("Failed to refresh token: ", error);
      logout(); // Handle token refresh failure
      throw error; // Re-throw error for further handling
    }
  };

  const getUserInfo = async () => {
    try {
      const userName = Cookies.get("userName");
      if (userName) {
        setUser({ name: userName, image: img });
      } else {
        console.log("User name not found in cookies.");
      }
    } catch (error) {
      console.log("Failed to fetch user info: ", error);
    }
  };

  const login = async (data) => {
    Cookies.set("accessToken", data.accessToken);
    Cookies.set("refreshToken", data.refreshToken);
    Cookies.set("userName", data.user.name);
    await getUserInfo();
  };

  const logout = async () => {
    try {
      await axios.get("/authentications/logout");
    } catch (error) {
      console.log("Logout failed: ", error);
    } finally {
      Cookies.remove("accessToken");
      Cookies.remove("refreshToken");
      Cookies.remove("userName");
      setUser(null);
    }
  };

  const userInfo = {
    user,
    setUser,
    login,
    logout,
    refreshToken,
  };

  return (
    <UserContext.Provider value={userInfo}>{children}</UserContext.Provider>
  );
};

export default UserProvider;
