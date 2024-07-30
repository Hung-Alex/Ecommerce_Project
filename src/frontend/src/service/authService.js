// authService.js
import { useContext } from 'react';
import { UserContext } from '../context/UserContext.jsx';

const useAuthService = () => {
  const { checkAuthStatus } = useContext(UserContext);
  const getAuthStatus = async () => {
    await checkAuthStatus();
  };
  return { getAuthStatus };
};

export default useAuthService;
