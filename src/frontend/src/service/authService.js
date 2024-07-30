// authService.js
import { useContext } from 'react';
import { toast } from 'react-hot-toast'; // Thư viện thông báo
import { UserContext } from '../context/UserContext.jsx';

const useAuthService = () => {
  const { checkAuthStatus } = useContext(UserContext);

  const getAuthStatus = async () => {
      try {
          await checkAuthStatus();
          toast.success('gửi thành công'); // Hiển thị thông báo lỗi
    } catch (error) {
      toast.error('Đã xảy ra lỗi khi kiểm tra trạng thái xác thực.'); // Hiển thị thông báo lỗi
    }
  };

  return { getAuthStatus };
};

export default useAuthService;
