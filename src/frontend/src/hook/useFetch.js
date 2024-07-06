import { useState, useEffect } from 'react';
import axios from 'axios';

const useAxios = (url, method = 'GET', body = null, headers = {}) => {
  const [data, setData] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchData = async () => {
      setLoading(true);
      try {
        const response = await axios({
          url,
          method,
          data: body,
          headers
        });
        setData(response.data);
      } catch (error) {
        setError(error);
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, [url, method, body, headers]);

  return { data, loading, error };
};

export default useAxios;
