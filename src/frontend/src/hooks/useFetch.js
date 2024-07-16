// hooks/useFetchData.js
import { useState, useEffect } from "react";
import axios from "../utils/axios";

const useFetch = (endpoint, queryParams = {}) => {
  const [data, setData] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        setLoading(true);
        const res = await axios.get(endpoint, { params: queryParams });
        setData(res.data.data);
        setLoading(false);
      } catch (err) {
        setLoading(false);
        setError(true);
      }
    };

    fetchData();
  }, [endpoint]);

  return { data, loading, error };
};

export default useFetch;
