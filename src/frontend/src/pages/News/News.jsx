import React, { useState, useEffect } from 'react';
import NewsSection from './NewsSection';
import NewsBanner from './NewsBanner';

import { fetchNewsPublished } from '../../api';

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
      <NewsBanner newsData={newsData} />
      <NewsSection newsData={newsData} />
    </div>
  );
};

export default News;
