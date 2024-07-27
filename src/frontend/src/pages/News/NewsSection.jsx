import React from 'react';
import { Link } from 'react-router-dom';

const NewsSection = () => {
  const newsPosts = [
    {
      title: "How to use sticky note for problem solving",
      date: "20 October 2019",
      image: "https://images.unsplash.com/photo-1515378960530-7c0da6231fb1?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1470&q=80",
    },
    {
      title: "How to use sticky note for problem solving",
      date: "20 October 2019",
      image: "https://images.unsplash.com/photo-1497032628192-86f99bcd76bc?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1470&q=80",
    },
    {
      title: "Morning routine to boost your mood",
      date: "25 November 2020",
      image: "https://images.unsplash.com/photo-1544654803-b69140b285a1?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1470&q=80",
    },
    {
      title: "All the features you want to know",
      date: "30 September 2020",
      image: "https://images.unsplash.com/photo-1530099486328-e021101a494a?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1547&q=80",
    },
    {
      title: "Minimal workspace for your inspirations",
      date: "13 October 2019",
      image: "https://images.unsplash.com/photo-1521737604893-d14cc237f11d?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1484&q=80",
    },
    {
      title: "What do you want to know about Blockchain",
      date: "20 October 2019",
      image: "https://images.unsplash.com/photo-1624996379697-f01d168b1a52?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1470&q=80",
    },
  ];

  // Utility function to format the title for the URL
  const formatTitleForURL = (title) => {
    return title.toLowerCase().replace(/\s+/g, '-');
  };

  return (
    <section className="bg-white">
      <div className="container px-6 py-10 mx-auto">
        <h1 className="text-3xl font-semibold text-gray-800 capitalize lg:text-4xl">From the news</h1>
        <div className="grid grid-cols-1 gap-8 mt-8 md:mt-16 md:grid-cols-2">
          {newsPosts.map((post, index) => (
            <Link
              key={index}
              to={`/news/${formatTitleForURL(post.title)}`}
              className="2xl:flex block hover:bg-gray-100 rounded-lg transition-colors duration-300"
            >
              <div className="overflow-hidden rounded-lg 2xl:w-64">
                <img
                  className="object-cover w-full h-56 transform transition-transform duration-300 ease-in-out hover:scale-105"
                  src={post.image}
                  alt={post.title}
                />
              </div>
              <div className="flex flex-col justify-between py-6 2xl:mx-6">
                <span className="text-xl font-semibold text-gray-800 hover:underline">
                  {post.title}
                </span>
                <span className="text-sm text-gray-500">On: {post.date}</span>
              </div>
            </Link>
          ))}
        </div>
      </div>
    </section>
  );
};

export default NewsSection;
