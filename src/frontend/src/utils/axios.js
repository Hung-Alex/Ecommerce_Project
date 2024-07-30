import axios from "axios";
import useAuthService from "../service/authService.js"; // Adjust the path according to your project structure

const axiosInstance = axios.create({
  baseURL: "https://localhost:7113/api",
  headers: {
    "Content-Type": "application/json",
  },
  withCredentials: true,
});

axiosInstance.interceptors.request.use(
  (config) => {
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

axiosInstance.interceptors.response.use(
  (response) => {
    return response;
  },
  (error) => {
    if (error.response) {
      if (error.response.status === 401) {
        useAuthService().getAuthStatus(); // Call to check auth status and handle 401 error
      } else if (error.response.status >= 500) {
        if (!sessionStorage.getItem('hasReloaded')) {
          sessionStorage.setItem('hasReloaded', 'true'); // Mark as reloaded
          window.location.reload(); // Reload the page
        }
      }
    } else {
      console.error("An unexpected error occurred.");
    }
    return Promise.reject(error);
  }
);

export default axiosInstance;
