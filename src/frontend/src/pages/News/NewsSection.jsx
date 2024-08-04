import React from 'react';
import { Link } from 'react-router-dom';
import { formatDate } from '../../utils/formatDate';

const NewsSection = ({ newsData }) => {
  // Utility function to format the title for the URL
  const formatTitleForURL = (title) => {
    return title.toLowerCase().replace(/\s+/g, '-');
  };

  // Ensure newsData is an array before mapping
  const validNewsData = Array.isArray(newsData) ? newsData : [];

  return (
    <section className="bg-white">
      <div className="container px-6 py-10 mx-auto">
        <div className="grid grid-cols-1 gap-8 mt-8 md:mt-16 md:grid-cols-3">
          {validNewsData.map((post, index) => (
            <Link
              key={index}
              to={`/news/${post.urlSlug}`}
              className="block hover:bg-gray-100 rounded-lg transition-colors duration-300"
            >
              <div className="overflow-hidden rounded-lg">
                <img
                  className="object-cover w-full h-48 md:h-64 lg:h-72 xl:h-80 2xl:h-96 transition-transform duration-300 ease-in-out hover:scale-105"
                  src={post.image}
                  alt={post.title}
                />
              </div>
              <div className="flex flex-col justify-between py-6 mx-6">
                <span className="text-xl font-semibold text-gray-800 hover:underline">
                  {post.title}
                </span>
                <span className="text-md text-gray-600 hover:underline hidden md:block">
                  {post.shortDescription}
                </span>
                <span className="text-sm text-gray-500">on: {formatDate(post.createdAt)}</span>
              </div>
            </Link>
          ))}
        </div>
      </div>
    </section>
  );
};

export default NewsSection;
