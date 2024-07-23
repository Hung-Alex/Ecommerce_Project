import { createContext, useState } from "react";
import axios from "../utils/axios";
import img from "../assets/Home/img/user.png";

export const UserContext = createContext({
  user: null,
  setUser: () => {},
  login: (data) => {},
  logout: () => {},
  refreshToken: () => {},
});
//user
const UserProvider = ({ children }) => {
  const [user, setUser] = useState(null);

  const refreshToken = async () => {
    try {
      const response = await axios.post("/authentications/refresh");
      if (response.data.accessToken) {
        return response.data.accessToken; // Return new token
      }
    } catch (error) {
      console.error("Failed to refresh token: ", error);
      logout(); // Handle token refresh failure
      throw error; // Re-throw error for further handling
    }
  };

  const login = async (data) => {
    try {
      // Directly set user info from login response
      setUser({
        name: data.user.name,
        image: img, // Keep this as it is or modify as needed
      });
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
