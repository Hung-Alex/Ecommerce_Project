import axios from "../utils/axios.js"; // Import your axios instance
import { toast } from 'react-hot-toast';

/**
 * Performs a GET request to the specified API.
 *
 * @param {string} your_api The URL of the API to send a GET request to.
 * @returns {Promise<any|null>} The result from the API if successful, null if there's an error.
 */
export async function get_api(your_api) {
  try {
    const response = await axios.get(your_api);
    const data = response.data;
    if (data.isSuccess) {
    //   toast.success('Retrieved successfully!');
      return data;
    } else {
      toast.warn('Retrieval was not successful.');
      return null;
    }
  } catch (error) {
    toast.error(`Error retrieving data: ${error.message}`);
    return null;
  }
}

/**
 * Performs a DELETE request to the specified API.
 *
 * @param {string} your_api The URL of the API to send a DELETE request to.
 * @returns {Promise<any|null>} The result from the API if successful, null if there's an error.
 */
export async function delete_api(your_api) {
  try {
    const response = await axios.delete(your_api);
    const data = response.data;
    if (data.isSuccess) {
      toast.success('Deleted successfully!');
      return data;
    } else {
      toast.warn('Deletion was not successful.');
      return null;
    }
  } catch (error) {
    toast.error(`Error deleting: ${error.message}`);
    return null;
  }
}

/**
 * Performs a POST request to the specified API.
 *
 * @param {string} your_api The URL of the API to send a POST request to.
 * @param {FormData} formData The form data to send in the request.
 * @returns {Promise<any|null>} The result from the API if successful, null if there's an error.
 */
export async function post_api(your_api, formData) {
  try {
    const response = await axios({
      method: "post",
      url: your_api,
      data: formData,
      headers: {
        accept: "multipart/form-data",
        "Content-Type": "multipart/form-data",
      },
    });
    const data = response.data;
    if (data.isSuccess) {
      toast.success('Submitted successfully!');
      return data;
    } else {
      toast.warn('Submission was not successful.');
      return null;
    }
  } catch (error) {
    toast.error(`Error submitting: ${error.message}`);
    return null;
  }
}

/**
 * Performs a PUT request to a specific API.
 *
 * @param {string} your_api - The URL of the API you want to send a PUT request to.
 * @param {FormData} formData - The form data to send in the request.
 * @returns {Promise<any|null>} - The result from the API if successful, null if there's an error.
 */
export async function put_api(your_api, formData) {
  try {
    const response = await axios({
      method: "put",
      url: your_api,
      data: formData,
      headers: {
        accept: "multipart/form-data",
        "Content-Type": "multipart/form-data",
      },
    });

    const data = response.data;
    if (data.isSuccess) {
      toast.success('Updated successfully!');
      return data;
    } else {
      toast.warn('Update was not successful.');
      return null;
    }
  } catch (error) {
    toast.error(`Error updating: ${error.message}`);
    return null;
  }
}
