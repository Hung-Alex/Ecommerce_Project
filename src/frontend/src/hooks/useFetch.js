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
      switch (method) {
        case "GET":
          res = await axios.get(endpoint, { params: queryParams });
          break;
        case "POST":
          res = await axios.post(endpoint, body);
          break;
        case "PUT":
          res = await axios.put(endpoint, body);
          break;
        case "DELETE":
          res = await axios.delete(endpoint, { params: queryParams });
          break;
        default:
          throw new Error(`Unsupported request method: ${method}`);
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
