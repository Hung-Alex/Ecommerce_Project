import { post_json, post_form, put_json, put_form, get_api, delete_api } from './methods.js';

/**
 * Fetch brand data by ID.
 * @param {string} brandId - The ID of the brand to fetch.
 * @returns {Promise<Object>} - A promise that resolves with the brand data.
 */
export const fetchBrandData = async () => {
    return get_api(`/brands`);
}


/**
 * Fetch brand data by ID.
 * @param {string} brandId - The ID of the brand to fetch.
 * @returns {Promise<Object>} - A promise that resolves with the brand data.
 */
export const fetchBrandDataById = async (brandId) => {
    const data = await get_api(`/brands/${brandId}`);
    return data.data;
};

/**
 * Create a new brand.
 * @param {Object} formData - The brand data to create.
 * @returns {Promise<Object>} - A promise that resolves with the created brand data.
 */
export const createBrand = async (formData) => {
    return post_form("/brands", formData);
};

/**
 * Update an existing brand.
 * @param {string} id - The ID of the brand to update.
 * @param {Object} formData - The updated brand data.
 * @returns {Promise<Object>} - A promise that resolves with the updated brand data.
 */
export const updateBrand = async (id, formData) => {
    return put_form(`/brands/${id}`, formData);
};

/**
 * Delete a brand.
 * @param {string} id - The ID of the brand to delete.
 * @returns {Promise<Object>} - A promise that resolves with a success message or confirmation.
 */
export const deleteBrand = async (id) => {
    return delete_api(`/brands/${id}`);
};
