import React, { useState, useCallback, useEffect } from "react";
import DashboardLayout from "../../../layout/DashboardLayout.jsx";
import Table from "../comp/Table.jsx";
import AddNewsForm from "./AddNewsForm";
import axios from "../../../utils/axios";

const AdminNews = () => {
  const [posts, setPosts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [showForm, setShowForm] = useState(false);
  const [editingPost, setEditingPost] = useState(null);

  const fetchPosts = useCallback(async () => {
    try {
      const response = await axios.get("/posts");
      setPosts(response.data.data);
      setLoading(false);
    } catch (error) {
      setError(error);
      setLoading(false);
    }
  }, []);

  useEffect(() => {
    fetchPosts();
  }, [fetchPosts]);

  const addPost = async (post) => {
    try {
      const formData = new FormData();
      formData.append("title", post.title);
      formData.append("shortDescription", post.shortDescription);
      formData.append("description", post.description);
      formData.append("urlSlug", post.urlSlug);
      formData.append("pulished", post.pulished);
      if (post.image) {
        formData.append("image", post.image);
      }

      await axios.post("/posts", formData, {
        headers: {
          "Content-Type": "multipart/form-data"
        }
      });

      await fetchPosts();
      handleCloseForm();
    } catch (error) {
      console.error("Error adding post:", error);
    }
  };

  const updatePost = async (id, updatedPost) => {
    try {
      const formData = new FormData();
      formData.append("id", id);
      formData.append("title", updatedPost.title);
      formData.append("shortDescription", updatedPost.shortDescription);
      formData.append("description", updatedPost.description);
      formData.append("urlSlug", updatedPost.urlSlug);
      formData.append("pulished", updatedPost.published);
      if (updatedPost.image) {
        formData.append("image", updatedPost.image);
      }

      const response = await axios.put(`/posts/${id}`, formData, {
        headers: {
          "Content-Type": "multipart/form-data"
        }
      });

      setPosts(prevList =>
        prevList.map(post =>
          post.id === id ? { ...post, ...response.data.data } : post
        )
      );
    } catch (error) {
      console.error("Error updating post:", error);
    }
  };

  const deletePost = async (id) => {
    try {
      await axios.delete(`/posts/${id}`);
      setPosts(prevList => prevList.filter(post => post.id !== id));
    } catch (error) {
      console.error("Error deleting post:", error);
    }
  };

  const handleEdit = useCallback((row) => {
    setEditingPost(row);
    setShowForm(true);
  }, []);

  const handleDelete = useCallback(async (row) => {
    try {
      await deletePost(row.id);
    } catch (error) {
      console.error('Error deleting post:', error);
    }
  }, []);

  const handleAddPost = useCallback(() => {
    setEditingPost(null);
    setShowForm(true);
  }, []);

  const handleCloseForm = useCallback(() => {
    setShowForm(false);
    setEditingPost(null);
  }, []);

  if (loading) return <p>Loading...</p>;
  if (error) console.log(error.message);

  return (
    <DashboardLayout>
      <div className='p-6'>
        <Table
          columns={[
            { header: 'ID', accessor: 'id' },
            { header: 'Image', accessor: 'imageUrl' },
            { header: 'Title', accessor: 'title' },
            { header: 'Short Description', accessor: 'shortDescription' },
            { header: 'Description', accessor: 'description' },
            { header: 'Published', accessor: 'pulished' }
          ]}
          data={posts}
          onEdit={handleEdit}
          onDelete={handleDelete}
          onAdd={handleAddPost}
        />
        {showForm && (
          <AddNewsForm
            post={editingPost}
            onClose={handleCloseForm}
            addPost={addPost}
            updatePost={updatePost}
          />
        )}
      </div>
    </DashboardLayout>
  );
};

export default AdminNews;
