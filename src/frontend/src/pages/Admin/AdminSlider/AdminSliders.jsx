import React, { useState, useEffect, useCallback } from "react";
import DashboardLayout from "../../../layout/DashboardLayout.jsx";
import Table from "../comp/Table.jsx";
import AddSlideForm from "./AddSlideForm";
import axios from "../../../utils/axios";
import toast from "react-hot-toast";

const AdminSlides = () => {
  const [slides, setSlides] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [showForm, setShowForm] = useState(false);
  const [editingSlide, setEditingSlide] = useState(null);

  const fetchSlides = useCallback(async () => {
    try {
      const response = await axios.get("/slides");
      if (response.data.isSuccess) {
        setSlides(response.data.data);
        setLoading(false);
      } else {
        throw new Error("Failed to fetch slides");
      }
    } catch (error) {
      setError(error);
      setLoading(false);
      toast.error(`Error loading slides: ${error.message}`);
    }
  }, []);

  useEffect(() => {
    fetchSlides();
  }, [fetchSlides]);

  const addSlide = async (formData) => {
    try {
      const response = await axios.post("/slides", formData, {
        headers: {
          "Content-Type": "multipart/form-data",
        },
      });
      if (response.data.isSuccess) {
        fetchSlides();
        handleCloseForm();
        toast.success("Slide added successfully");
      } else {
        throw new Error("Failed to add slide");
      }
    } catch (error) {
      console.error("Error adding slide:", error);
      toast.error(`Error adding slide: ${error.message}`);
    }
  };

  const updateSlide = async (id, formData) => {
    try {
      const response = await axios.put(`/slides/${id}`, formData, {
        headers: {
          "Content-Type": "multipart/form-data",
        },
      });
      if (response.data.isSuccess) {
        fetchSlides();
        handleCloseForm();
        toast.success("Slide updated successfully");
      } else {
        throw new Error("Failed to update slide");
      }
    } catch (error) {
      console.error("Error updating slide:", error);
      toast.error(`Error updating slide: ${error.message}`);
    }
  };

  const deleteSlide = async (id) => {
    try {
      const response = await axios.delete(`/slides/${id}`);
      if (response.data.isSuccess) {
        setSlides((prevList) => prevList.filter((slide) => slide.id !== id));
        toast.success("Slide deleted successfully");
      } else {
        throw new Error("Failed to delete slide");
      }
    } catch (error) {
      console.error("Error deleting slide:", error);
      toast.error(`Error deleting slide: ${error.message}`);
    }
  };

  const handleEdit = useCallback((row) => {
    setEditingSlide(row);
    setShowForm(true);
  }, []);

  const handleDelete = useCallback(async (row) => {
    try {
      await deleteSlide(row.id);
    } catch (error) {
      console.error("Error deleting slide:", error);
      toast.error(`Error deleting slide: ${error.message}`);
    }
  }, []);

  const handleAddSlide = useCallback(() => {
    setEditingSlide(null);
    setShowForm(true);
  }, []);

  const handleCloseForm = useCallback(() => {
    setShowForm(false);
    setEditingSlide(null);
  }, []);

  if (loading) return <p>Loading...</p>;
  if (error) console.log(error.message);

  return (
    <DashboardLayout>
      <div className="p-6">
        <Table
          columns={[
            { header: 'ID', accessor: 'id' },
            { header: 'Image', accessor: 'image' },
            { header: 'Title', accessor: 'title' },
            { header: 'Description', accessor: 'description' },
            { header: 'Active', accessor: 'isActive' },
          ]}
          data={slides}
          onEdit={handleEdit}
          onDelete={handleDelete}
          onAdd={handleAddSlide}
        />
        {showForm && (
          <AddSlideForm
            slide={editingSlide}
            onClose={handleCloseForm}
            addSlide={addSlide}
            updateSlide={updateSlide}
          />
        )}
      </div>
    </DashboardLayout>
  );
};

export default AdminSlides;
