// src/utils/tokenUtils.js
import { decodeToken } from './jwtUtils.js';

/**
 * Kiểm tra thời hạn của token.
 * @param {string} token - JWT token.
 * @returns {string} - Thời hạn còn lại của token.
 */
export const checkTokenExpiration = (token) => {
  const decoded = decodeToken(token);
  if (decoded && decoded.exp) {
    const expirationDate = new Date(decoded.exp * 1000);
    const now = new Date();
    const timeLeft = expirationDate - now;

    if (timeLeft > 0) {
      return `Token còn hiệu lực trong ${Math.floor(timeLeft / (1000 * 60))} phút`;
    } else {
      return 'Token đã hết hạn';
    }
  }
  return 'Không thể kiểm tra thời hạn token';
};
