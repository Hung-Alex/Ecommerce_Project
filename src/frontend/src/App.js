// App.js
import './App.css';
import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Layout from './layout/Layout';
import NotFound from './pages/NotFound/NotFound';
import { routes, adminRoutes } from './router/Router';

function App() {
  return (
    <Router>
      <Layout>
        <Routes>
          {/* Routes for regular users */}
          {routes.map((route, index) => (
            <Route key={index} path={route.path} element={route.element} />
          ))}

          {/* Routes for admin users */}
          {adminRoutes.map((route, index) => (
            <Route key={index} path={route.path} element={route.element} />
          ))}

          {/* Handle 404 error */}
          <Route path="*" element={<NotFound />} />
        </Routes>
      </Layout>
    </Router>
  );
}

export default App;
