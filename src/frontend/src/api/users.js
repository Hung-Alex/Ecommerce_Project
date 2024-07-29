// api/users.js

import { get_api, post_api, delete_api, put_api } from './method.js';

/**
 * Fetch users data.
 * @returns {Promise<Array>} - A promise that resolves with an array of users data.
 */
export const fetchUsersData = async () => {
  const data = await get_api("/users");
  return data;
};

/**
 * Fetch user data by ID.
 * @param {number} id - The ID of the user.
 * @returns {Promise<Object>} - A promise that resolves with the user data.
 */
export const fetchUsersId = async (id) => {
  const data = await get_api(`/users/${id}`);
  return data;
};

/**
 * Update an existing user.
 * @param {number} userId - The ID of the user to update.
 * @param {Object} formData - The updated user data including images.
 * @returns {Promise} - A promise that resolves with the response.
 */
export const updateUser = async (userId, formData) => {
  const data = await put_api(`/users/${userId}`, formData);
  return data;
};

/**
 * Create a new user.
 * @param {Object} formData - The data for the new user including images.
 * @returns {Promise} - A promise that resolves with the response.
 */
export const createUser = async (formData) => {
  const data = await post_api("/users", formData);
  return data;
};

/**
 * Delete a user by ID.
 * @param {number} id - The ID of the user to delete.
 * @returns {Promise} - A promise that resolves with the response.
 */
export const deleteUser = async (id) => {
  const data = await delete_api(`/users/${id}`);
  return data;
};
