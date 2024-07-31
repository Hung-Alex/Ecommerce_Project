import axios from '../utils/axios.js';
import { toast } from 'react-hot-toast';

/**
 * Performs a POST request with JSON data.
 * @param {string} url - The URL for the POST request.
 * @param {Object} data - The JSON data to send in the request.
 * @returns {Promise<any|null>} - The response data if successful, or null if there's an error.
 */
export async function post_json(url, data) {
  try {
    const response = await axios.post(url, data, {
      headers: {
        'Content-Type': 'application/json',
      },
    });
    return response.data;
  } catch (error) {
    toast.error(`Error creating data: ${error.message}`);
    return null;
  }
}

/**
 * Performs a POST request with FormData.
 * @param {string} url - The URL for the POST request.
 * @param {FormData} data - The FormData to send in the request.
 * @returns {Promise<any|null>} - The response data if successful, or null if there's an error.
 */
export async function post_form(url, data) {
  try {
    const response = await axios.post(url, data, {
      headers: {
        'Content-Type': 'multipart/form-data',
      },
    });
    return response.data;
  } catch (error) {
    toast.error(`Error creating data: ${error.message}`);
    return null;
  }
}

/**
 * Performs a PUT request with JSON data.
 * @param {string} url - The URL for the PUT request.
 * @param {Object} data - The JSON data to send in the request.
 * @returns {Promise<any|null>} - The response data if successful, or null if there's an error.
 */
export async function put_json(url, data) {
  try {
    const response = await axios.put(url, data, {
      headers: {
        'Content-Type': 'application/json',
      },
    });
    const responseData = response.data;
    if (responseData.isSuccess) {
      toast.success('Updated successfully!');
      return responseData;
    } else {
      toast.warn('Update was not successful.');
      return null;
    }
  } catch (error) {
    toast.error(`Error updating: ${error.message}`);
    return null;
  }
}

/**
 * Performs a PUT request with FormData.
 * @param {string} url - The URL for the PUT request.
 * @param {FormData} data - The FormData to send in the request.
 * @returns {Promise<any|null>} - The response data if successful, or null if there's an error.
 */
export async function put_form(url, data) {
  try {
    const response = await axios.put(url, data, {
      headers: {
        'Content-Type': 'multipart/form-data',
      },
    });
    const responseData = response.data;
    if (responseData.isSuccess) {
      toast.success('Updated successfully!');
      return responseData;
    } else {
      toast.warn('Update was not successful.');
      return null;
    }
  } catch (error) {
    toast.error(`Error updating: ${error.message}`);
    return null;
  }
}

/**
 * Performs a GET request.
 * @param {string} url - The URL for the GET request.
 * @returns {Promise<any|null>} - The response data if successful, or null if there's an error.
 */
export async function get_api(url) {
  try {
    const response = await axios.get(url);
    return response.data;
  } catch (error) {
    toast.error(`Error fetching data: ${error.message}`);
    return null;
  }
}

/**
 * Performs a DELETE request.
 * @param {string} url - The URL for the DELETE request.
 * @returns {Promise<any|null>} - The response data if successful, or null if there's an error.
 */
export async function delete_api(url) {
  try {
    const response = await axios.delete(url);
    return response.data;
  } catch (error) {
    toast.error(`Error deleting data: ${error.message}`);
    return null;
  }
}
