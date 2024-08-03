import { post_json, post_form, put_json, put_form, get_api, delete_api } from './methods.js';

/**
 * Fetch all news data.
 * @returns {Promise<Array>} - A promise that resolves with an array of news data.
 */
export const fetchNewsData = async () => {
    return get_api("/posts");
};

/**
 * Fetch news data by id.
 * @param {string} id - The ID of the news item to fetch.
 * @returns {Promise<Object>} - A promise that resolves with the news data.
 */
export const fetchNewsId = async (id) => {
    return get_api(`/posts/${id}`);
};

/**
 * Fetch published news data with query parameters.
 * @param {Object} payload - The query parameters to filter the published news.
 * @returns {Promise<Array>} - A promise that resolves with an array of news data.
 */
export const fetchNewsPublished = async (payload) => {
    const queryParams = toQueryString(payload);
    return get_api(`/posts/published?${queryParams}`);
};

/**
 * Fetch news data by urlSlug.
 * @param {string} slug - The slug of the news item to fetch.
 * @returns {Promise<Object>} - A promise that resolves with the news data.
 */
export const fetchNewsSlug = async (slug) => {
    return get_api(`/posts/${slug}`);
};

/**
 * Update an existing news item.
 * @param {string} id - The ID of the news item to update.
 * @param {FormData} formData - The updated news data including images.
 * @returns {Promise<Object>} - A promise that resolves with the response data.
 */
export const updateNews = async (id, formData) => {
    return put_form(`/posts/${id}`, formData);
};

/**
 * Create a new news item.
 * @param {FormData} formData - The news data including images.
 * @returns {Promise<Object>} - A promise that resolves with the response data.
 */
export const createNews = async (formData) => {
    return post_form('/posts', formData);
};

/**
 * Delete a news item.
 * @param {string} id - The ID of the news item to delete.
 * @returns {Promise<Object>} - A promise that resolves with the response data.
 */
export const deleteNews = async (id) => {
    return delete_api(`/posts/${id}`);
};

/**
 * Convert an object to a query string.
 * @param {Object} params - The object to convert.
 * @returns {string} - The query string.
 */
const toQueryString = (params) => {
    return new URLSearchParams(params).toString();
};
