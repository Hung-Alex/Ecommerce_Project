import axios from 'axios';

const REGISTER_URL = 'https://localhost:7113/api/authentications/register';
const LOGIN_URL = 'https://localhost:7113/api/authentications/login';

/**
 * Hàm đăng ký tài khoản mới.
 * @param {Object} userData - Dữ liệu người dùng để đăng ký.
 * @param {string} userData.userName - Tên người dùng.
 * @param {string} userData.email - Địa chỉ email.
 * @param {string} userData.password - Mật khẩu.
 * @param {string} userData.confirmPassword - Xác nhận mật khẩu.
 * @returns {Promise<Object>} - Trả về phản hồi từ API.
 */
export const registerUser = async (userData) => {
  try {
    const response = await axios.post(REGISTER_URL, userData, {
      headers: {
        'Content-Type': 'application/json'
      }
    });
    return response.data;
  } catch (error) {
    console.error('Error registering user', error.response.data);
    throw error;
  }
};

/**
 * Hàm đăng nhập người dùng.
 * @param {Object} loginData - Dữ liệu người dùng để đăng nhập.
 * @param {string} loginData.userName - Tên người dùng.
 * @param {string} loginData.password - Mật khẩu.
 * @returns {Promise<Object>} - Trả về phản hồi từ API.
 */
export const loginUser = async (loginData) => {
  try {
    const response = await axios.post(LOGIN_URL, loginData, {
      headers: {
        'Content-Type': 'application/json'
      }
    });
    return response.data;
  } catch (error) {
    console.error('Error logging in user', error.response.data);
    throw error;
  }
};
