import React, { useState, useCallback } from "react";
import DashboardLayout from "../../../layout/DashboardLayout.jsx";
import Table from "../comp/Table.jsx";
import AddNewsForm from "./AddNewsForm";
import { toast } from "react-hot-toast";
import { deleteNews } from "../../../api";

const AdminNews = () => {
  const [showForm, setShowForm] = useState(false);
  const [editingPost, setEditingPost] = useState(null);
  const [refresh, setRefresh] = useState(""); // State to trigger refresh

  // Handle editing of a post
  const handleEdit = useCallback((row) => {
    setEditingPost(row.id);
    setShowForm(true);
  }, []);

  // Handle deletion of a post
  const handleDelete = useCallback(async (row) => {
    const res = await deleteNews(row.id);
    if (res?.isSuccess) {
      setRefresh(prev => !prev); // Trigger refresh
      toast.success('Post deleted successfully');
    }
  }, []);

  // Show the form for adding a new post
  const handleAddPost = useCallback(() => {
    setEditingPost(null);
    setShowForm(true);
  }, []);

  // Close the form and reset editing state
  const handleCloseForm = useCallback(() => {
    setShowForm(false);
    setEditingPost(null);
    setRefresh(prev => !prev); // Trigger refresh
  }, []);

  return (
    <DashboardLayout>
      <div className='p-6'>
        <Table
          apiUrl="/posts" // Specify the API URL directly in the Table component
          columns={[
            { header: 'ID', accessor: 'id' },
            { header: 'Image', accessor: 'image' },
            { header: 'Title', accessor: 'title' },
            { header: 'URL Slug', accessor: 'urlSlug' },
            { header: 'Short Description', accessor: 'shortDescription' },
            { header: 'Published', accessor: 'published' }
          ]}
          onEdit={handleEdit}
          onDelete={handleDelete}
          onAdd={handleAddPost}
          refresh={refresh} // Pass refresh state to Table component if needed
        />
        {showForm && (
          <AddNewsForm
            postId={editingPost}
            onClose={handleCloseForm}
          />
        )}
      </div>
    </DashboardLayout>
  );
};

export default AdminNews;
