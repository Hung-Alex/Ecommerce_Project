import React, { useState, useEffect, useCallback } from "react";
import DashboardLayout from "../../../layout/DashboardLayout.jsx";
import Table from "../comp/Table.jsx";
import AddBannerForm from "./AddBannerForm";
import axios from "../../../utils/axios";

const AdminBanners = () => {
  const [banners, setBanners] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [showForm, setShowForm] = useState(false);
  const [editingBanner, setEditingBanner] = useState(null);

  const fetchBanners = useCallback(async () => {
    try {
      const response = await axios.get("/banners");
      setBanners(response.data.data);
      setLoading(false);
    } catch (error) {
      setError(error);
      setLoading(false);
    }
  }, []);

  useEffect(() => {
    fetchBanners();
  }, [fetchBanners]);

  const addBanner = async (banner) => {
    try {
      const formData = new FormData();
      formData.append("title", banner.title);
      formData.append("description", banner.description);
      formData.append("FormFile", banner.image);
      formData.append("isVisible", banner.visible);

      // POST request to add a new banner
      await axios.post("/banners", formData, {
        headers: {
          "Content-Type": "multipart/form-data"
        }
      });

      // Fetch updated banner list
      await fetchBanners();
      handleCloseForm();
    } catch (error) {
      console.error("Error adding banner:", error);
    }
  };

  const updateBanner = async (id, updatedBanner) => {
    try {
      const formData = new FormData();
      formData.append("id", id);
      formData.append("title", updatedBanner.title);
      formData.append("urlSlug", updatedBanner.urlSlug);
      formData.append("description", updatedBanner.description);
      if (updatedBanner.image) {
        formData.append("FormFile", updatedBanner.image);
      }
      formData.append("visible", updatedBanner.visible);

      const response = await axios.put(`/banners/${id}`, formData, {
        headers: {
          "Content-Type": "multipart/form-data"
        }
      });

      setBanners(prevList =>
        prevList.map(banner =>
          banner.id === id ? { ...banner, ...response.data.data } : banner
        )
      );
    } catch (error) {
      console.error("Error updating banner:", error);
    }
  };

  const deleteBanner = async (id) => {
    try {
      await axios.delete(`/banners/${id}`);
      setBanners(prevList => prevList.filter(banner => banner.id !== id));
    } catch (error) {
      console.error("Error deleting banner:", error);
    }
  };

  const handleEdit = useCallback((row) => {
    setEditingBanner(row);
    setShowForm(true);
  }, []);

  const handleDelete = useCallback(async (row) => {
    try {
      await deleteBanner(row.id);
    } catch (error) {
      console.error('Error deleting banner:', error);
    }
  }, []);

  const handleAddBanner = useCallback(() => {
    setEditingBanner(null);
    setShowForm(true);
  }, []);

  const handleCloseForm = useCallback(() => {
    setShowForm(false);
    setEditingBanner(null);
  }, []);

  if (loading) return <p>Loading...</p>;
  if (error){console.log(error.message);}

  return (
    <DashboardLayout>
      <div className='p-6'>
        <Table
          columns={[
            { header: 'ID', accessor: 'id' },
            { header: 'Image', accessor: 'logoImageUrl' },
            { header: 'Title', accessor: 'title' },
            { header: 'Description', accessor: 'description' },
            { header: 'Visible', accessor: 'isVisible' }
          ]}
          data={banners}
          onEdit={handleEdit}
          onDelete={handleDelete}
          onAdd={handleAddBanner}
        />
        {showForm && (
          <AddBannerForm
            banner={editingBanner}
            onClose={handleCloseForm}
            addBanner={addBanner}
            updateBanner={updateBanner}
          />
        )}
      </div>
    </DashboardLayout>
  );
};

export default AdminBanners;
