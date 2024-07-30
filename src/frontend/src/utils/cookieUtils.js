// src/utils/cookieUtils.js
import { Cookies } from 'react-cookie';

const cookies = new Cookies();

/**
 * Thiết lập cookie với thời gian hết hạn.
 * @param {string} name - Tên cookie.
 * @param {string} value - Giá trị cookie.
 * @param {Date} expires - Thời gian hết hạn của cookie.
 */
export const setCookie = (name, value, expires) => {
  cookies.set(name, value, { expires });

  // Lưu trữ thời gian hết hạn trong localStorage
  localStorage.setItem(`${name}-expiration`, expires.toISOString());
};

/**
 * Lấy giá trị của cookie.
 * @param {string} name - Tên cookie.
 * @returns {string|null} - Giá trị cookie hoặc null nếu không tồn tại.
 */
export const getCookie = (name) => {
  const res = cookies.get(name, true) || null;
  console.log(res);
  return res;
};

/**
 * Lấy thời gian hết hạn của cookie từ localStorage.
 * @param {string} name - Tên cookie.
 * @returns {string|null} - Thời gian hết hạn của cookie hoặc null nếu không có.
 */
export const getExpirationTime = (name) => {
  const expirationTime = localStorage.getItem(`${name}-expiration`);
  return expirationTime ? new Date(expirationTime).toLocaleString() : 'Không có thông tin hết hạn';
};
