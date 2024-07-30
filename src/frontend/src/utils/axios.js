// axiosInstance.js
import axios from "axios";
import useAuthService from '../service/authService.js';

const axiosInstance = axios.create({
  baseURL: "https://localhost:7113/api",
  headers: {
    "Content-Type": "application/json",
  },
  withCredentials: true,
});

const setupInterceptors = (getAuthStatus) => {
  axiosInstance.interceptors.request.use(
    async (config) => {
      await getAuthStatus();
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
      return Promise.reject(error);
    }
  );
};

export const initializeAxiosInstance = () => {
  const { getAuthStatus } = useAuthService();
  setupInterceptors(getAuthStatus);
};

export default axiosInstance;
