import axiosInstance from "../utils/axios.js"; // Import your axios instance for making API requests

/**
 * Fetch news data.
 * @returns {Promise<Array>} - A promise that resolves with an array of news data.
 */
export const fetchNewsData = async () => {
  try {
    const response = await axiosInstance.get("/posts");
    return response.data.data; // Assuming response.data.data contains the news data array
  } catch (error) {
    console.error('Error fetching news data:', error);
  }
};

/**
 * Update an existing news item.
 * @param {string} id - The ID of the news item to update.
 * @param {Object} formData - The updated news data including images.
 * @returns {Promise<Object>} - A promise that resolves with the response data.
 */
export const updateNews = async (id, formData) => {
  try {
    const response = await axiosInstance.put(`/posts/${id}`, formData, {
      headers: {
        'Content-Type': 'multipart/form-data', // Set header for form data including files
      },
    });
    return response.data; // Assuming response.data contains the updated news item
  } catch (error) {
    console.error('Error updating news:', error);
  }
};

/**
 * Create a new news item.
 * @param {Object} formData - The news data including images.
 * @returns {Promise<Object>} - A promise that resolves with the response data.
 */
export const createNews = async (formData) => {
  try {
    const response = await axiosInstance.post('/posts', formData, {
      headers: {
        'Content-Type': 'multipart/form-data', // Set header for form data including files
      },
    });
    return response.data; // Assuming response.data contains the newly created news item
  } catch (error) {
    console.error('Error creating news:', error);
  }
};

/**
 * Delete a news item.
 * @param {string} id - The ID of the news item to delete.
 * @returns {Promise<Object>} - A promise that resolves with the response data.
 */
export const deleteNews = async (id) => {
  try {
    const response = await axiosInstance.delete(`/posts/${id}`);
    return response.data; // Assuming response.data contains a success message or confirmation
  } catch (error) {
    console.error('Error deleting news:', error);
  }
};
