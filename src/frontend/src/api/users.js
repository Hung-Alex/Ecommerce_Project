import axiosInstance from "../utils/axios.js"; // Import your axios instance
import { toast } from 'react-hot-toast'; // Import toast from react-toastify

/**
 * Fetch users data.
 * @returns {Promise<Array>} - A promise that resolves with an array of users data.
 */
export const fetchUsersData = async () => {
    try {
        const response = await axiosInstance.get("/users");
        return response.data.data; // Return the users data
    } catch (error) {
        toast.error("Error fetching users data."); // Show error toast
    }
};

/**
 * Fetch users data.
 * @returns {Promise<Array>} - A promise that resolves with an array of users data.
 */
export const fetchUsersId = async (id) => {
    try {
        const response = await axiosInstance.get(`/users/${id}`);
        return response.data.data; // Return the users data
    } catch (error) {
        toast.error("Error fetching users data."); // Show error toast
    }
};


/**
 * Update an existing user.
 * @param {number} userId - The ID of the user to update.
 * @param {Object} formData - The updated user data including images.
 * @returns {Promise} - A promise that resolves with the response.
 */
export const updateUser = async (userId, formData) => {
    try {
      const response = await axiosInstance.put(`/users/${userId}`, formData, {
        headers: {
            'Content-Type': 'multipart/form-data', // Set header for form data including files
        },
    });
      toast.success("User Update successfully.");
      return response.data;
    } catch (error) {
      toast.error('Error updating user:', error);
    }
  };

/**
 * create an existing user.
 * @param {number} userId - The ID of the user to create.
 * @param {Object} formData - The created user data including images.
 * @returns {Promise} - A promise that resolves with the response.
 */
export const createUser = async (formData) => {
    try {
      const response = await axiosInstance.post(`/users`, formData, {
        headers: {
          'Content-Type': 'multipart/form-data',
        },
      });
      toast.success("User created successfully.");
      return response.data;
    } catch (error) {
      toast.error('Error add user:', error);
    }
  };

  // Delete an User by ID
export const deleteUser = async (id) => {
  try {
    const res = await axiosInstance.delete(`/users/${id}`);
    return res.data;
  } catch (error) {
    toast.error('Error delete user:', error);
  }
  };

