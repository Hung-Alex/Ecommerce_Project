import { useState, useEffect, useCallback } from "react";
import axios from "../utils/axios.js";

const useFetch = (endpoint, method = "GET", queryParams = {}, body = null) => {
  const [data, setData] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  const fetchData = useCallback(async () => {
    try {
      setLoading(true);
      let res;
      if (method === "GET") {
        res = await axios.get(endpoint, { params: queryParams });
      } else if (method === "POST") {
        res = await axios.post(endpoint, body);
      } else if (method === "PUT") {
        res = await axios.put(endpoint, body);
      } else if (method === "DELETE") {
        res = await axios.delete(endpoint, { params: queryParams });
      }
      setData(res.data.data);
      setLoading(false);
    } catch (err) {
      setLoading(false);
      setError(true);
    }
  }, [endpoint, method, body]);

  useEffect(() => {
    fetchData();
  }, [fetchData]);

  return { data, loading, error, refetch: fetchData };
};

export default useFetch;
