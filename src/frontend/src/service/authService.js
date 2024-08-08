// authService.js

import axios from 'axios';
import { toast } from 'react-hot-toast';
import { useContext } from 'react';
import { UserContext } from '../context/UserContext.jsx'; // Ensure the import path is correct

/**
 * Handles unauthorized access by checking the authentication status.
 * If the user is not authenticated, an error message is displayed,
 * and the user is redirected to the login page.
 */
const handleUnauthorized = async () => {
  // Access the UserContext to get the authentication status
  const { checkAuthStatus } = useContext(UserContext);

  // Check if the user is authenticated
  if (!checkAuthStatus) {
    // Display an error message indicating authentication failure
    toast.error('Authentication failed. Please log in.');

    // Redirect the user to the login page
    window.location.href = '/login';
  }
};

export { handleUnauthorized };
