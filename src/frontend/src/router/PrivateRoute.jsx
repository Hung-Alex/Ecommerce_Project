import React from 'react';
import { Navigate } from 'react-router-dom';
import { useContext } from 'react';
import { UserContext } from '../context/UserContext'; // Assuming you have a UserContext

const PrivateRoute = ({ children }) => {
  const { user } = useContext(UserContext); // Access the user state from the context
console.log(user);
  return user ? children : <Navigate to="/login" />;
};

export default PrivateRoute;
