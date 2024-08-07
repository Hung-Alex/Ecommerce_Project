// components/CommentSection/CommentSection.js
import React, { useEffect, useState } from 'react';
import ReactQuill from 'react-quill';
import 'react-quill/dist/quill.snow.css';
import { fetchComments, postComment } from '../../api';

const CommentSection = ({ postId }) => {
    const [comments, setComments] = useState([]);
    const [page, setPage] = useState(1);
    const [content, setContent] = useState('');
    const [totalPages, setTotalPages] = useState(1);

    useEffect(() => {
        if (postId) {
            fetchCommentsData(postId, page);
        }
    }, [postId, page]);

    const fetchCommentsData = async (postId, page) => {
            const result = await fetchComments(postId, page);
            setComments(result.data || []); // Ensure comments is an array
            setTotalPages(result.totalPages || 1);

    };

    const handlePageChange = (newPage) => {
        setPage(newPage);
    };

    const handleSubmit = async () => {
            await postComment(postId, content);
            setContent('');
            fetchCommentsData(postId, 1); // Refresh comments
    };

    return (
        <div className="m-12 text-gray-700 max-w-screen-lg mx-auto text-lg leading-relaxed">
            <h2 className="text-lg mx-2 lg:text-2xl font-bold text-black">Comments ({comments?.length})</h2>
            <div className="mt-8 mx-2 min-h-20">
                <ReactQuill
                    className="h-full overflow-auto"
                    value={content}
                    onChange={setContent}
                />


                <button
                    className="my-4 px-4 pb-2 text-xl rounded-xl bg-green-300"
                    onClick={handleSubmit}
                >
                    Post Comment
                </button>
            </div>
            {comments.length === 0 ? (
                <p className="text-gray-500">No comments yet. Be the first to comment!</p>
            ) : (
                comments.map((comment) => (
                    <article key={comment.id} className="p-6 mx-2 text-base bg-slate-300 rounded-lg mb-4">
                        <footer className="flex justify-between items-center mb-2">
                            <div className="flex items-center">
                                <img
                                    src={comment.imageOfCreator}
                                    alt={comment.createdByName}
                                    className="mr-2 w-6 h-6 rounded-full"
                                />
                                <p className="text-sm text-gray-900 dark:text-white font-semibold">
                                    {comment.createdByName}
                                </p>
                                <p className="text-sm text-gray-600 dark:text-gray-400 ml-2">
                                    <time dateTime={comment.createdAt} title={new Date(comment.createdAt).toLocaleDateString()}>
                                        <span class="mr-2">{new Date(comment.createdAt).toLocaleDateString()}</span>
                                        <span>{new Date(comment.createdAt).toLocaleTimeString()}</span>
                                    </time>

                                </p>
                            </div>


                        </footer>
                        <div
                            dangerouslySetInnerHTML={{ __html: comment.content }}
                            className="mb-2 max-h-[70vh] overflow-auto"
                        />
                        <div className="flex items-center mt-4 space-x-4">
                            <button
                                type="button"
                                className="flex items-center text-sm text-gray-500 hover:underline dark:text-gray-400 font-medium"
                            >
                                <svg className="mr-1.5 w-3.5 h-3.5" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 20 18">
                                    <path stroke="currentColor" strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M5 5h5M5 8h2m6-3h2m-5 3h6m2-7H2a1 1 0 0 0-1 1v9a1 1 0 0 0 1 1h3v5l5-5h8a1 1 0 0 0 1-1V2a1 1 0 0 0-1-1Z" />
                                </svg>
                                Reply
                            </button>
                        </div>
                    </article>
                ))
            )}
            <div className="flex justify-center mt-6">
                <button
                    onClick={() => handlePageChange(page - 1)}
                    disabled={page === 1}
                    className="px-4 py-2 mx-2 rounded hover:bg-gray-400"
                >
                    Previous
                </button>
                <button
                    onClick={() => handlePageChange(page + 1)}
                    disabled={page === totalPages}
                    className="px-4 py-2 rounded hover:bg-gray-400"
                >
                    Next
                </button>
            </div>
        </div>
    );
};

export default CommentSection;
