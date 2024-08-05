import { post_json, post_form, put_json, put_form, get_api, delete_api } from './methods.js';

/**
 * Fetch orders data with specified parameters.
 * @param {Object} params - Parameters to include in the API request (e.g., filters, pagination).
 * @param {string} StatusId - Status ID to filter orders (if applicable).
 * @returns {Promise<Object>} - A promise that resolves with the response object containing orders data.
 */
export const fetchOrdersData = async (StatusId, params) => {
    const queryParams = toQueryString(params);
    return get_api(`/orders/user?${queryParams}`);
};

/**
 * Change the status of a specific order.
 * @param {Object} param0 - Object containing orderId and statusId.
 * @param {string} param0.orderId - The ID of the order to update.
 * @param {string} param0.statusId - The new status ID to set for the order.
 * @returns {Promise<Object>} - A promise that resolves with the response object from the API.
 */
export const changeOrderStatus = async ({ orderId, statusId }) => {
    return post_json('/orders/change-status-order', { orderId, statusId });
};

/**
 * Fetch data for all possible order statuses.
 * @returns {Promise<Object>} - A promise that resolves with the response object containing order status data.
 */
export const fetchOrdersStatus = async () => {
    return get_api("/states?Type=Order");
};

/**
 * Convert an object to a query string.
 * @param {Object} params - The object to convert.
 * @returns {string} - The query string.
 */
const toQueryString = (params) => {
    return new URLSearchParams(params).toString();
};