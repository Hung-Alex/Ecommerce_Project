// api/users.js

import { get_api, post_json, post_form, put_json, put_form, delete_api } from './methods.js';

/**
 * Upload a user avatar.
 * @param {string} userId - The ID of the user.
 * @param {File} file - The avatar file to upload.
 * @returns {Promise<Object>} - A promise that resolves with the response data.
 */
export const UploadUserAvatar = async (userId, file) => {
        const formData = new FormData();
        formData.append('UserId', userId); // If needed, you can send the userId as a field in formData
        formData.append('Image', file);

        return put_form(`/users/avatar/${userId}`, formData)
}
