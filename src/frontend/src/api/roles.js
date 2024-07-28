import axiosInstance from "../utils/axios.js";// Import your axios instance

// Fetch roles data
export const fetchRolessData = async () => {
    const response = await axiosInstance.get("/roles");
    return response.data.data;
};
// Fetch roles data by id
export const fetchRolessDataId = async (role) => {
    const response = await axiosInstance.get(`/roles/${role}`);
    return response.data.data;
};
// Fetch permissions data
export const fetchPermissionsData = async (role) => {
    const response = await axiosInstance.get("/permissions");
    return response.data.data;
};

export const createRoles = async (role) => {
    const response = await axiosInstance.post("/roles", role);
    return response;
};

export const updateRoles = async (id, updatedRole) => {
    const response = await axiosInstance.put(`/roles/${id}`, updatedRole);
    return response;
};
export const deleteRoles = async (id) => {
    const response = await axiosInstance.delete(`/roles/${id}`);
    return response;
};

