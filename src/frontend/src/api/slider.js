import { post_json, post_form, put_json, put_form, get_api, delete_api } from './methods.js';

/**
 * Fetch Slide data by ID.
 * @param {string} SlideId - The ID of the Slide to fetch.
 * @returns {Promise<Object>} - A promise that resolves with the Slide data.
 */
export const fetchSlideDataById = async (SlideId) => {
    const data = await get_api(`/Slides/${SlideId}`);
    return data.data;
};

/**
 * Fetch active slide data.
 * @returns {Promise<Object>} - A promise that resolves with the active slide data.
 */
export const fetchActiveSlideData = async () => {
    return await get_api('/slides/is-actice');
};


/**
 * Create a new Slide.
 * @param {Object} formData - The Slide data to create.
 * @returns {Promise<Object>} - A promise that resolves with the created Slide data.
 */
export const createSlide = async (formData) => {
    return post_form("/Slides", formData);
};

/**
 * Update an existing Slide.
 * @param {string} id - The ID of the Slide to update.
 * @param {Object} formData - The updated Slide data.
 * @returns {Promise<Object>} - A promise that resolves with the updated Slide data.
 */
export const updateSlide = async (id, formData) => {
    return put_form(`/Slides/${id}`, formData);
};

/**
 * Delete a Slide.
 * @param {string} id - The ID of the Slide to delete.
 * @returns {Promise<Object>} - A promise that resolves with a success message or confirmation.
 */
export const deleteSlide = async (id) => {
    return delete_api(`/Slides/${id}`);
};
