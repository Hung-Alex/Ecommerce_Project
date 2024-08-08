// src/utils/queryStringUtils.js

/**
 * Convert an object to a query string.
 * @param {Object} params - The object to convert.
 * @returns {string} - The query string.
 */
const toQueryString = (params) => {
    const queryString = new URLSearchParams(params).toString();
    return queryString ? `?${queryString}` : '';
};

export { toQueryString };
