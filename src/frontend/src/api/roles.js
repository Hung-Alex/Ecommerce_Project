import axiosInstance from "../utils/axios.js"; // Import your axios instance
import { toast } from 'react-hot-toast'; // Import toast from react-toastify

/**
 * Fetch roles data.
 * @returns {Promise<Array>} - A promise that resolves with an array of roles data.
 */
export const fetchRolesData = async () => {
    try {
        const response = await axiosInstance.get("/roles");
        return response.data.data; // Return the roles data
    } catch (error) {
        toast.error("Error fetching roles data."); // Show error toast
    }
};

/**
 * Fetch role data by ID.
 * @param {string} roleId - The ID of the role to fetch.
 * @returns {Promise<Object>} - A promise that resolves with the role data.
 */
export const fetchRolessDataId = async (roleId) => {
    try {
        const response = await axiosInstance.get(`/roles/${roleId}`);
        return response.data.data; // Return the role data
    } catch (error) {
        toast.error("Error fetching role data."); // Show error toast
    }
};

/**
 * Fetch permissions data.
 * @returns {Promise<Array>} - A promise that resolves with an array of permissions data.
 */
export const fetchPermissionsData = async () => {
    try {
        const response = await axiosInstance.get("/permissions");
        return response.data.data; // Return the permissions data
    } catch (error) {
        toast.error("Error fetching permissions data."); // Show error toast
    }
};

/**
 * Create a new role.
 * @param {Object} role - The role data to create.
 * @returns {Promise<Object>} - A promise that resolves with the created role data.
 */
export const createRoles = async (role) => {
    try {
        const response = await axiosInstance.post("/roles", role);
        toast.success("Role created successfully."); // Show success toast
        return response.data; // Return the created role data
    } catch (error) {
        toast.error("Error creating role."); // Show error toast
    }
};

/**
 * Update an existing role.
 * @param {string} id - The ID of the role to update.
 * @param {Object} updatedRole - The updated role data.
 * @returns {Promise<Object>} - A promise that resolves with the updated role data.
 */
export const updateRoles = async (id, updatedRole) => {
    try {
        const response = await axiosInstance.put(`/roles/${id}`, updatedRole);
        toast.success("Role updated successfully."); // Show success toast
        return response; // Return the updated role data
    } catch (error) {
        toast.error("Error updating role."); // Show error toast
    }
};

/**
 * Delete a role.
 * @param {string} id - The ID of the role to delete.
 * @returns {Promise<Object>} - A promise that resolves with a success message or confirmation.
 */
export const deleteRoles = async (id) => {
    try {
        const response = await axiosInstance.delete(`/roles/${id}`);
        toast.success("Role deleted successfully."); // Show success toast
        return response.data; // Return the success message or confirmation
    } catch (error) {
        toast.error("Error deleting role."); // Show error toast
    }
};
