import React, { useState, useCallback } from "react";
import Table from "../comp/Table.jsx";
import AddBannerForm from "./AddBannerForm";
import { deleteBanner } from '../../../api';

const AdminBanners = () => {
  const [showForm, setShowForm] = useState(false);
  const [refresh, setRefresh] = useState(""); // State to trigger refresh
  const [editingBanner, setEditingBanner] = useState(null); // State for the banner being edited

  const handleAdd = useCallback(() => {
    setEditingBanner(null);
    setShowForm(true);
  }, []);

  const handleEdit = useCallback((row) => {
    setEditingBanner(row.id);
    setShowForm(true);
  }, []);

  const handleDelete = useCallback(async (row) => {
    await deleteBanner(row.id);
    setRefresh(prev => !prev); // Trigger refresh
  }, []);

  const handleCloseForm = useCallback(() => {
    setRefresh((prev) => !prev); // Trigger refresh
    setEditingBanner(null);
    setShowForm(false);
  }, []);

  return (
      <div className='md:p-6'>
        <Table
          apiUrl="/banners"
          columns={[
            { header: 'ID', accessor: 'id' },
            { header: 'Image', accessor: 'logoImageUrl' },
            { header: 'Title', accessor: 'title' },
            { header: 'Description', accessor: 'description' },
            { header: 'Visible', accessor: 'isVisible' }
          ]}
          onEdit={handleEdit}
          onDelete={handleDelete}
          onAdd={handleAdd}
          searchParam="Title" // Specify the search parameter name as per the API requirement
          refresh={refresh} // Pass refresh state to Table component
        />
        {showForm && (
          <AddBannerForm
            bannerId={editingBanner}
            onClose={handleCloseForm}
          />
        )}
      </div>
  );
};

export default AdminBanners;
