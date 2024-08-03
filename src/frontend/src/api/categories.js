import { post_json, post_form, put_json, put_form, get_api, delete_api } from './methods.js';

/**
 * Fetch all categories.
 * @returns {Promise<Object>} - A promise that resolves with the categories data.
 */
export const fetchCategories = async () => {
    return get_api(`/categories`);
}

/**
 * Fetch category data by ID.
 * @param {string} categoryId - The ID of the category to fetch.
 * @returns {Promise<Object>} - A promise that resolves with the category data.
 */
export const fetchCategoryDataById = async (categoryId) => {
    const data = await get_api(`/categories/${categoryId}`);
    return data.data;
};

/**
 * Create a new category.
 * @param {Object} formData - The category data to create.
 * @returns {Promise<Object>} - A promise that resolves with the created category data.
 */
export const createCategory = async (formData) => {
    return post_form("/categories", formData);
};

/**
 * Update an existing category.
 * @param {string} id - The ID of the category to update.
 * @param {Object} formData - The updated category data.
 * @returns {Promise<Object>} - A promise that resolves with the updated category data.
 */
export const updateCategory = async (id, formData) => {
    return put_form(`/categories/${id}`, formData);
};

/**
 * Delete a category.
 * @param {string} id - The ID of the category to delete.
 * @returns {Promise<Object>} - A promise that resolves with a success message or confirmation.
 */
export const deleteCategory = async (id) => {
    return delete_api(`/categories/${id}`);
};
