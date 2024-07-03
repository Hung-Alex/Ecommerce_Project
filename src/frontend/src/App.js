import './App.css';
import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Layout from './layout/Layout'; // Đảm bảo đường dẫn đúng
import NotFound from './pages/NotFound/NotFound';
import HomePage from './pages/Home/Home';
import { routes, adminRoutes } from './router/Router';

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Layout />}>
          <Route index element={<HomePage />} />
          {/* Routes for regular users */}
          {routes.map((route, index) => (
            <Route key={`route-${index}`} path={route.path} element={route.element} />
          ))}
          {/* Routes for admin users */}
          {adminRoutes.map((route, index) => (
            <Route key={`adminRoute-${index}`} path={route.path} element={route.element} />
          ))}
          {/* Handle 404 error */}
          <Route path="*" element={<NotFound />} />
        </Route>
      </Routes>
    </Router>
  );
}

export default App;
