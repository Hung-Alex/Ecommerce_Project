import { post_json, post_form, put_json, put_form, get_api, delete_api } from './methods.js';

/**
 * Fetch banner data by ID.
 * @returns {Promise<Object>} - A promise that resolves with the banner data.
 */
export const fetchBannerData = async () => {
    const data = await get_api(`/banners/is-visable`);
    return data.data;
};
/**
 * Fetch banner data by ID.
 * @param {string} bannerId - The ID of the banner to fetch.
 * @returns {Promise<Object>} - A promise that resolves with the banner data.
 */
export const fetchBannerDataById = async (bannerId) => {
    const data = await get_api(`/banners/${bannerId}`);
    return data.data;
};

/**
 * Create a new banner.
 * @param {Object} formData - The banner data to create.
 * @returns {Promise<Object>} - A promise that resolves with the created banner data.
 */
export const createBanner = async (formData) => {
    return post_form("/banners", formData);
};

/**
 * Update an existing banner.
 * @param {string} id - The ID of the banner to update.
 * @param {Object} formData - The updated banner data.
 * @returns {Promise<Object>} - A promise that resolves with the updated banner data.
 */
export const updateBanner = async (id, formData) => {
    return put_form(`/banners/${id}`, formData);
};

/**
 * Delete a banner.
 * @param {string} id - The ID of the banner to delete.
 * @returns {Promise<Object>} - A promise that resolves with a success message or confirmation.
 */
export const deleteBanner = async (id) => {
    return delete_api(`/banners/${id}`);
};
