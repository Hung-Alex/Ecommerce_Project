// src/components/TokenChecker.js
import React, { useEffect } from 'react';
import { getCookie, getExpirationTime } from './utils/cookieUtils';

const TokenChecker = () => {
  // useEffect(() => {
  //   const accessToken = getCookie('access-token');
  //   const refreshToken = getCookie('refresh-token');
  //   const userInfo = getCookie('info-user');

  //   // console.log('access-token:', accessToken);
  //   // console.log('refresh-token:', refreshToken);
  //   // console.log('info-user:', userInfo);

  //   // Kiểm tra và xuất thời gian hết hạn của access-token
  //   if (accessToken) {
  //     console.log('Thời gian hết hạn access-token:', getExpirationTime('access-token'));
  //   } else {
  //     console.log('Không có access-token trong cookies.');
  //   }

  //   // Kiểm tra và xuất thời gian hết hạn của refresh-token
  //   if (refreshToken) {
  //     console.log('Thời gian hết hạn refresh-token:', getExpirationTime('refresh-token'));
  //   } else {
  //     console.log('Không có refresh-token trong cookies.');
  //   }

  //   // Xuất thông tin người dùng (nếu cần)
  //   if (userInfo) {
  //     try {
  //       const parsedUserInfo = JSON.parse(userInfo);
  //       console.log('Thông tin người dùng:', parsedUserInfo);
  //     } catch (error) {
  //       console.error('Error parsing userInfo:', error);
  //     }
  //   } else {
  //     console.log('Không có thông tin người dùng trong cookies.');
  //   }
  // }, []);

  // return <div>Kiểm tra token</div>;
};

export default TokenChecker;
