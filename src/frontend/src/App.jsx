import React from 'react';
import { RouterProvider } from 'react-router-dom';
import routes from './router/Routes';

function App() {
  return <RouterProvider router={routes} />;
}

export default App;