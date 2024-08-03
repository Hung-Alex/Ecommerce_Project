import { post_json, post_form, put_json, put_form, get_api, delete_api } from './methods.js';

/**
 * Fetch all roles data.
 * @returns {Promise<Array>} - A promise that resolves with an array of roles data.
 */
export const fetchRolesData = async () => {
    return get_api("/roles");
};

/**
 * Fetch role data by ID.
 * @param {string} roleId - The ID of the role to fetch.
 * @returns {Promise<Object>} - A promise that resolves with the role data.
 */
export const fetchRoleDataById = async (roleId) => {
    const data = await get_api(`/roles/${roleId}`);
    return data.data;
};

/**
 * Fetch all permissions data.
 * @returns {Promise<Array>} - A promise that resolves with an array of permissions data.
 */
export const fetchPermissionsData = async () => {
    const data = await get_api("/permissions");
    return data.data;
};

/**
 * Create a new role.
 * @param {Object} role - The role data to create.
 * @returns {Promise<Object>} - A promise that resolves with the created role data.
 */
export const createRole = async (role) => {
    return post_json("/roles", role);
};

/**
 * Update an existing role.
 * @param {string} id - The ID of the role to update.
 * @param {Object} updatedRole - The updated role data.
 * @returns {Promise<Object>} - A promise that resolves with the updated role data.
 */
export const updateRole = async (id, updatedRole) => {
    return put_json(`/roles/${id}`, updatedRole);
};

/**
 * Delete a role.
 * @param {string} id - The ID of the role to delete.
 * @returns {Promise<Object>} - A promise that resolves with a success message or confirmation.
 */
export const deleteRole = async (id) => {
    return delete_api(`/roles/${id}`);
};
