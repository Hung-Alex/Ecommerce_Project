import React from 'react';

const newsData = [
    {
        id: 1,
        category: "Art",
        date: "31 Jul",
        title: "Fuga ea ullam earum assumenda, beatae labore eligendi.",
        imageUrl: "https://source.unsplash.com/random/245x320"
    },
    {
        id: 2,
        category: "Politics",
        date: "04 Aug",
        title: "Autem sunt tempora mollitia magnam non voluptates",
        imageUrl: "https://source.unsplash.com/random/240x320"
    },
    {
        id: 3,
        category: "Health",
        date: "01 Aug",
        title: "Inventore reiciendis aliquam excepturi",
        imageUrl: "https://source.unsplash.com/random/241x320"
    },
    {
        id: 4,
        category: "Science",
        date: "28 Jul",
        title: "Officiis mollitia dignissimos commodi optio vero animi",
        imageUrl: "https://source.unsplash.com/random/242x320"
    },
    {
        id: 5,
        category: "Sports",
        date: "19 Jul",
        title: "Doloribus sit illo necessitatibus architecto exercitationem enim",
        imageUrl: "https://source.unsplash.com/random/243x320"
    },
];

const NewsBanner = () => {
    return (
        <div className="max-w-screen-xl p-5 mx-auto bg-white">
            <div className="grid grid-cols-1 gap-4 md:grid-cols-2 lg:grid-cols-4 md:gap-0 lg:grid-rows-2">
                {newsData.map((news, index) => (
                    <div
                        key={news.id}
                        className={`relative flex items-end justify-start w-full text-left bg-center bg-cover cursor-pointer h-96 group
                            ${index === 0 ? 'md:col-span-2 lg:row-span-2 lg:h-full' : 'hidden lg:block'}`}
                        style={{ backgroundImage: `url(${news.imageUrl})` }}
                    >
                        <div className="absolute top-0 bottom-0 left-0 right-0 bg-gradient-to-b from-transparent to-gray-100"></div>
                        <div className="absolute top-0 left-0 right-0 flex items-center justify-between mx-5 mt-3">
                            <a href="#" className="px-3 py-2 text-xs font-semibold tracking-wider uppercase hover:underline text-gray-800 bg-violet-600">{news.category}</a>
                            <div className="flex flex-col justify-start text-center text-gray-800">
                                <span className="text-3xl font-semibold leading-none tracking-wide">{news.date.split(' ')[0]}</span>
                                <span className="leading-none uppercase">{news.date.split(' ')[1]}</span>
                            </div>
                        </div>
                        <h2 className="z-10 p-5">
                            <a href="#" className="font-medium text-md group-hover:underline text-gray-800">{news.title}</a>
                        </h2>
                    </div>
                ))}
            </div>
        </div>
    );
}

export default NewsBanner;
