// src/utils/jwtUtils.js
import { jwtDecode } from 'jwt-decode';

/**
 * Giải mã JWT token.
 * @param {string} token - JWT token.
 * @returns {object} - Payload của token.
 */
export const decodeToken = (token) => {
  try {
    return jwtDecode(token);
  } catch (error) {
    console.error('Error decoding token:', error);
    return null;
  }
};
