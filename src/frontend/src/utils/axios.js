import axios from "axios";
import { handleUnauthorized } from "../service/authService.js"; // Adjust the path

const axiosInstance = axios.create({
  baseURL: "https://localhost:7113/api",
  headers: {
    'ngrok-skip-browser-warning': true,
    "Content-Type": "application/json",
  },
  withCredentials: true,
});

axiosInstance.interceptors.request.use(
  (config) => {
    // You can modify request config here if needed
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
  async (error) => {
    if (error.response) {
      if (error.response.status === 401) {
        await handleUnauthorized(); // Handle 401 error
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
