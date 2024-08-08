import {  get_api } from './methods.js';
import { toQueryString } from '../utils/queryStringUtils.js';


/**
 * Fetch published news data with query parameters.
 * @param {Object} params - The query parameters to filter the published news.
 * @returns {Promise<Object>} - A promise that resolves with the news data.
 */
export const fetchData = async ( params ) => {
    const queryParams = toQueryString(params);
    return get_api(`/searchs${queryParams}`);
};
