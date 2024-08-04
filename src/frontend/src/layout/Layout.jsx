// StandardLayout.js
import React from 'react';
import { Outlet } from 'react-router-dom';
import Header from '../components/Shared/Header/Header';
import Footer from '../components/Shared/Footer/Footer';

const StandardLayout = () => {
  return (
    <>
      <Header />
      <main>
        <Outlet />
      </main>
      <Footer />
    </>
  );
};

export default StandardLayout;
