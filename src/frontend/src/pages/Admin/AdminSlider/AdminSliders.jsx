import React, { useState, useEffect, useCallback } from "react";
import Table from "../comp/Table.jsx";
import AddSlideForm from "./AddSlideForm";
import { deleteSlide, fetchSlides } from '../../../api';

const AdminSlides = () => {
  const [showForm, setShowForm] = useState(false);
  const [editingSlide, setEditingSlide] = useState(null);
  const [refresh, setRefresh] = useState(""); // State to trigger refresh

  const handleAdd = useCallback(() => {
    setEditingSlide(null);
    setShowForm(true);
  }, []);

  const handleEdit = useCallback((row) => {
    setEditingSlide(row.id);
    setShowForm(true);
  }, []);

  const handleDelete = useCallback(async (row) => {
    await deleteSlide(row.id);
    setRefresh(prev => !prev); // Trigger refresh
  }, []);

  const handleCloseForm = useCallback(() => {
    setRefresh(prev => !prev); // Trigger refresh
    setEditingSlide(null);
    setShowForm(false);
  }, []);

  return (
    <div className='md:p-6'>
      <Table
        apiUrl="/slides"
        columns={[
          { header: 'ID', accessor: 'id' },
          { header: 'Image', accessor: 'image' },
          { header: 'Title', accessor: 'title' },
          { header: 'Description', accessor: 'description' },
          { header: 'Active', accessor: 'isActive' }
        ]}
        onEdit={handleEdit}
        onDelete={handleDelete}
        onAdd={handleAdd}
        searchParam="Title" // Specify the search parameter name as per the API requirement
        refresh={refresh} // Pass refresh state to Table component
      />
      {showForm && (
        <AddSlideForm
          slideId={editingSlide}
          onClose={handleCloseForm}
        />
      )}
    </div>
  );
};

export default AdminSlides;
