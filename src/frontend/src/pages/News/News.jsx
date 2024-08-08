import React, { useState, useEffect } from 'react';
import NewsSection from './NewsSection';
import NewsBanner from './NewsBanner';

import { fetchNewsPublished } from '../../api';
import Banner from './Banner';

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
      const response = await fetchNewsPublished(payload);
      setNewsData(response.data);
    };

    fetchData();
  }, []);

  return (
    <div>
      <Banner />
      <NewsBanner newsData={newsData} />
      <NewsSection newsData={newsData} />
    </div>
  );
};

export default News;
