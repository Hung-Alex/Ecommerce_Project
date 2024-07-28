import React, { useState, useCallback, useEffect } from "react";
import DashboardLayout from "../../../layout/DashboardLayout.jsx";
import Table from "../comp/Table.jsx";
import AddNewsForm from "./AddNewsForm";
import {toast} from "react-hot-toast";
import {
  fetchNewsData,
  updateNews,
  deleteNews,
  createNews
} from "../../../api"

const AdminNews = () => {
  const [posts, setPosts] = useState([]);
  const [showForm, setShowForm] = useState(false);
  const [editingPost, setEditingPost] = useState(null);

  // Fetch news data from the server
  const fetchPosts = useCallback(async () => {
    try {
      const response = await fetchNewsData();
      setPosts(response);
    } catch (error) {
      toast.error('Error fetching posts');
      console.error('Error fetching posts:', error);
    }
  }, []);

  useEffect(() => {
    fetchPosts();
  }, [fetchPosts]);

  // Add a new post
  const addPost = async (post) => {
    try {
      const formData = new FormData();
      formData.append('title', post.title);
      formData.append('shortDescription', post.shortDescription);
      formData.append('description', post.description);
      formData.append('urlSlug', post.urlSlug);
      formData.append('pulished', post.pulished);
      if (post.image) formData.append('image', post.image);

      await createNews(formData);
      fetchPosts();
      handleCloseForm();
      toast.success('Post created successfully');
    } catch (error) {
      toast.error('Error creating post');
      console.error('Error creating post:', error);
    }
  };

  // Update an existing post
  const updatePost = async (id, updatedPost) => {
    try {
      const formData = new FormData();
      formData.append('id', id);
      formData.append('title', updatedPost.title);
      formData.append('shortDescription', updatedPost.shortDescription);
      formData.append('description', updatedPost.description);
      formData.append('urlSlug', updatedPost.urlSlug);
      formData.append('pulished', updatedPost.pulished);
      if (updatedPost.image) formData.append('image', updatedPost.image);

      await updateNews(id, formData);
      fetchPosts();
      toast.success('Post updated successfully');
    } catch (error) {
      toast.error('Error updating post');
    }
  };

  // Delete a post
  const deletePost = async (id) => {
    try {
      await deleteNews(id);
      setPosts((prevList) => prevList.filter((post) => post.id !== id));
      toast.success('Post deleted successfully');
    } catch (error) {
      toast.error('Error deleting post');
    }
  };

  // Handle editing of a post
  const handleEdit = useCallback((post) => {
    setEditingPost(post);
    setShowForm(true);
  }, []);

  // Handle deletion of a post
  const handleDelete = useCallback(async (post) => {
    await deletePost(post.id);
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
            { header: 'Image', accessor: 'imageUrl' },
            { header: 'Title', accessor: 'title' },
            { header: 'urlSlug', accessor: 'urlSlug' },
            { header: 'Short Description', accessor: 'shortDescription' },
            { header: 'Description', accessor: 'description' },
            { header: 'pulished', accessor: 'pulished' }
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
