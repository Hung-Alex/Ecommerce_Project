import { createContext, useState, useEffect } from "react";
import axios from "../utils/axios";
import Cookies from "js-cookie";
import img from "../assets/Home/img/user.png"

export const UserContext = createContext({
  user: null,
  setUser: () => {},
  login: (data) => {},
  logout: () => {},
});

const UserProvider = ({ children }) => {
  const [user, setUser] = useState(null);

  useEffect(() => {
    const token = Cookies.get("accessToken");
    if (token) {
      axios.defaults.headers.common["Authorization"] = `Bearer ${token}`;
      getUserInfo(); // Fetch user info on startup if token exists
    }
  }, []);

  useEffect(() => {
    const refreshToken = async () => {
      try {
        const response = await axios.post("/authentications/refresh", {
          refreshToken: Cookies.get("refreshToken"),
        });
        if (response.data.accessToken) {
          Cookies.set("accessToken", response.data.accessToken);
          axios.defaults.headers.common["Authorization"] = `Bearer ${response.data.accessToken}`;
        }
      } catch (error) {
        console.log("Failed to refresh token: ", error);
        logout(); // Clear user and tokens on refresh failure
      }
    };

    const tokenTimer = setInterval(refreshToken, 120000);

    return () => clearInterval(tokenTimer);
  }, []);
  const getUserInfo = async () => {
    try {
      const userName = Cookies.get('userName');
      if (userName) {
        setUser({ name: userName, image: img });
      } else {
      }
    } catch (error) {
      console.log('Failed to fetch user info: ', error);
    }
  };

  const login = (data) => {
    // setUser(data);
    setUser({ image: img });
    Cookies.set("accessToken", data.accessToken);
    Cookies.set("refreshToken", data.refreshToken);
    Cookies.set("userName", data.user.name);
    axios.defaults.headers.common["Authorization"] = `Bearer ${data.accessToken}`;
  };

  const logout = async () => {
    try {
      await axios.get("/authentications/logout");
    } catch (error) {
      console.log("Logout failed: ", error);
    } finally {
      Cookies.remove("accessToken");
      Cookies.remove("refreshToken");
      Cookies.remove("X-Access-Token");
      Cookies.remove("X-Refresh-Token");
      delete axios.defaults.headers.common["Authorization"];
      setUser(null);
    }
  };

  const userInfo = {
    user,
    setUser,
    login,
    logout,
  };

  return (
    <UserContext.Provider value={userInfo}>{children}</UserContext.Provider>
  );
};

export default UserProvider;