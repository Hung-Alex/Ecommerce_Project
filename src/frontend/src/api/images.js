import axiosInstance from "../utils/axios.js";// Import your axios instance
import { toast } from 'react-hot-toast'; // Import toast from react-toastify

/**
 * Upload a user avatar.
 * @param {string} userId - The ID of the user.
 * @param {File} file - The avatar file to upload.
 * @returns {Promise<Object>} - A promise that resolves with the response data.
 */
export const UploadUserAvatar = async (userId, file) => {
    try {
        const formData = new FormData();
        formData.append('UserId', userId); // If needed, you can send the userId as a field in formData
        formData.append('Image', file);

        const response = await axiosInstance.put(`/users/avatar/${userId}`, formData, {
            headers: {
                'Content-Type': 'multipart/form-data', // Set header for form data including files
            },
        });

        toast.success('Upload Img successfully.');
        return response.data; // Assuming response.data contains the response data
    } catch (error) {
        console.error('Error uploading avatar:', error);
        toast.error('Error uploading avatar. Please try again.');
        throw error;
    }
};
