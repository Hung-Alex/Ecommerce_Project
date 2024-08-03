import React, { useState, useEffect } from 'react';
import NewsSection from './NewsSection';
import NewsBanner from './NewsBanner';

import { fetchNewsPublished } from '../../api';
import Header from '../../components/Shared/Header/Header';
import Footer from '../../components/Shared/Footer/Footer';

const News = () => {
  const [newsData, setNewsData] = useState([]);

  const payload = {
    Title: '',
    PageSize: 10,
    PageNumber: 1,
    SortColumn: "createdAt",
    SortBy: "ASC",
  };

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await fetchNewsPublished(payload);
        setNewsData(response);
      } catch (error) {
        console.error('Error fetching news:', error);
      }
    };

    fetchData();
  }, []);

  return (
    <div>
      <Header />
      <NewsBanner newsData={newsData} />
      <NewsSection newsData={newsData} />
      <Footer />
    </div>
  );
};

export default News;
