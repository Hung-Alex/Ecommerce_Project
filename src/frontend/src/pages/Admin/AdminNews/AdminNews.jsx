import React, { useState, useCallback, useEffect } from "react";
import DashboardLayout from "../../../layout/DashboardLayout.jsx";
import Table from "../comp/Table.jsx";
import AddNewsForm from "./AddNewsForm";
import {toast} from "react-hot-toast";
import {
  fetchNewsData,
  deleteNews,
} from "../../../api"

const AdminNews = () => {
  const [posts, setPosts] = useState([]);
  const [showForm, setShowForm] = useState(false);
  const [editingPost, setEditingPost] = useState(null);

  // Fetch news data from the server
  const fetchPosts = useCallback(async () => {
    try {
      const response = await fetchNewsData();
      setPosts(response.data);
    } catch (error) {
      toast.error('Error fetching posts');
      console.error('Error fetching posts:', error);
    }
  }, [showForm  ]);

  useEffect(() => {
    fetchPosts();
  }, [fetchPosts]);

  // Handle editing of a post
  const handleEdit = useCallback((row) => {
    setEditingPost(row.id);
    setShowForm(true);
  }, []);

  // Handle deletion of a post
  const handleDelete = useCallback(async (row) => {
    deleteNews(row.id).then(res => {
      // console.log(res);
        if (res?.isSuccess) {
          setPosts((prevList) => prevList.filter((post) => row.id !== post.id));
        }
    })
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
  }, []);

  return (
    <DashboardLayout>
      <div className='p-6'>
        <Table
          columns={[
            { header: 'ID', accessor: 'id' },
            { header: 'Image', accessor: 'image' },
            { header: 'Title', accessor: 'title' },
            { header: 'urlSlug', accessor: 'urlSlug' },
            { header: 'Short Description', accessor: 'shortDescription' },
            { header: 'published', accessor: 'published' }
          ]}
          data={posts}
          onEdit={handleEdit}
          onDelete={handleDelete}
          onAdd={handleAddPost}
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
