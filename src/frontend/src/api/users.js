// api/users.js

import { get_api, post_json, post_form, put_json, put_form, delete_api } from './methods.js';

/**
 * Fetch users data.
 * @returns {Promise<Array>} - A promise that resolves with an array of users data.
 */
export const fetchUsersData = async () => {
  return get_api("/users");
};

/**
 * Fetch user data by ID.
 * @param {number} id - The ID of the user.
 * @returns {Promise<Object>} - A promise that resolves with the user data.
 */
export const fetchUsersId = async (id) => {
  return get_api(`/users/${id}`);
};

/**
 * Update an existing user.
 * @param {number} userId - The ID of the user to update.
 * @param {Object} formData - The updated user data including images.
 * @returns {Promise} - A promise that resolves with the response.
 */
export const updateUser = async (userId, formData) => {
  return put_form(`/users/${userId}`, formData);
};

/**
 * Create a new user.
 * @param {Object} formData - The data for the new user including images.
 * @returns {Promise} - A promise that resolves with the response.
 */
export const createUser = async (formData) => {
  return post_form("/users", formData);
};

/**
 * Delete a user by ID.
 * @param {number} id - The ID of the user to delete.
 * @returns {Promise} - A promise that resolves with the response.
 */
export const deleteUser = async (id) => {
  return delete_api(`/users/${id}`);
};
