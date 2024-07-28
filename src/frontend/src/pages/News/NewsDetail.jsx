// components/NewsDetail/NewsDetail.js
import React from 'react';
import { useParams } from 'react-router-dom';
import { fetchNewsSlug } from "../../api";
import { useEffect, useState } from 'react';

const NewsDetail = () => {
    const [data, setData] = useState({});
    const { slug } = useParams(); // Destructure slug from useParams

    useEffect(() => {
        const fetchData = async () => {
            try {
                const result = await fetchNewsSlug(slug);
                setData(result);
            } catch (error) {
                console.error("Error fetching news data:", error);
            }
        };

        fetchData();
    }, [slug]); // Add slug to dependency array

    const date = new Date(data.createdAt);

    const day = date.getDate();
    const month = date.toLocaleString('default', { month: 'short' });

    const formattedDate = `${day} ${month}`;

    return (
        <div className="max-w-screen-xl mx-auto bg-white">

            {/* Main Content */}
            <main className="mt-10">
                <div className="mb-4 md:mb-0 w-full max-w-screen-xl mx-auto relative" style={{ height: '24em' }}>
                    <div className="absolute left-0 bottom-0 w-full h-full z-10"
                        style={{ backgroundImage: 'linear-gradient(180deg,transparent,rgba(0,0,0,.7))' }}></div>
                    <img src={data.imageUrl} className="absolute left-0 top-0 w-full h-full z-0 object-cover" />
                    <div className="p-4 absolute bottom-0 left-0 z-20">
                        <a href="#"
                            className="px-4 py-1 bg-black text-gray-200 inline-flex items-center justify-center mb-2"></a>
                        <h2 className="text-4xl font-semibold text-gray-100 leading-tight">
                            {data.title}
                        </h2>
                        <div className="flex mt-3">
                            <img src={data.imageOfCreator}
                                className="h-10 w-10 rounded-full mr-2 object-cover" />
                            <div>
                                <p className="font-semibold text-gray-200 text-sm"> {data.createdByName} </p>
                                <p className="font-semibold text-gray-400 text-xs"> {formattedDate} </p>
                            </div>
                        </div>
                    </div>
                </div>

                <div className="px-4 lg:px-0 mt-12 text-gray-700 max-w-screen-lg mx-auto text-lg leading-relaxed" >
                    {/* <img src={data.imageUrl} alt="" className='w-full h-auto' /> */}
                    <strong>{data.shortDescription}</strong>
                </div>
                <div className="px-4 lg:px-0 mt-12 text-gray-700 max-w-screen-lg mx-auto text-lg leading-relaxed" dangerouslySetInnerHTML={{ __html: data.description }}>
                </div>
            </main>
            {/* Main Content ends here */}


        </div>
    );
};

export default NewsDetail;
