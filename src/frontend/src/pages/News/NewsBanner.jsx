import React from 'react';
import { formatDate } from '../../utils/formatDate';

const NewsBanner = ({ newsData }) => {
  const displayedNews = newsData.slice(0, 5);

  return (
    <div className="max-w-screen-xl p-5 mx-auto bg-white">
      <div className="grid grid-cols-1 gap-4 md:grid-cols-2 lg:grid-cols-4 md:gap-0 lg:grid-rows-2">
        {displayedNews.map((news, index) => (
          <a
            href={`/news/${news.urlSlug}`}
            key={news.id}
            className={`relative flex items-end justify-start w-full text-left bg-center bg-cover cursor-pointer h-96 group
              ${index === 0 ? 'md:col-span-2 lg:row-span-2 lg:h-full' : 'hidden lg:block'}`}
            style={{ backgroundImage: `url(${news.image})` }}
          >
            <div className="absolute top-0 bottom-0 left-0 right-0 bg-gradient-to-b from-transparent to-black-100 transition-opacity duration-300 group-hover:opacity-70"></div>
            <div className="absolute top-0 left-0 right-0 flex items-center justify-between mx-5 mt-3">
              <a href={`/news/${news.urlSlug}`} className="px-3 py-2 text-xs font-semibold tracking-wider uppercase hover:underline text-gray-800 bg-violet-600">
                {formatDate(news.createdAt)}
              </a>
              <div className="flex flex-col justify-start text-center text-gray-800">
                <span className="text-3xl font-semibold leading-none tracking-wide"></span>
                <span className="leading-none uppercase"></span>
              </div>
            </div>
            <h2 className="z-10 pl-5 py-5 pt-12">
              <a href={`/news/${news.urlSlug}`} className="font-medium text-lg group-hover:underline text-white">{news.title}</a>
            </h2>
            <div className="absolute inset-0 bg-black opacity-0 transition-opacity duration-300 group-hover:opacity-30"></div>
          </a>
        ))}
      </div>
    </div>
  );
}

export default NewsBanner;
