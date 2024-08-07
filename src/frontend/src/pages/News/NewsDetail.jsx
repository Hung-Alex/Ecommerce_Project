// components/NewsDetail/NewsDetail.js
import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { fetchNewsSlug } from '../../api';
import { DEFAULT_IMAGE_URLS } from '../../constants/imageUrls';
import CommentSection from './CommentSection';

const NewsDetail = () => {
    const [data, setData] = useState({});
    const { slug } = useParams();

    useEffect(() => {
        const fetchData = async () => {
            try {
                const result = await fetchNewsSlug(slug);
                setData(result.data);
            } catch (error) {
                console.error("Error fetching news data:", error);
            }
        };

        fetchData();
    }, [slug]);

    const date = new Date(data.createdAt);
    const day = date.getDate();
    const month = date.toLocaleString('default', { month: 'short' });
    const formattedDate = `${day} ${month}`;

    return (
        <div className="max-w-screen-xl mx-auto bg-white">
            <main className="">
                <div className="w-full relative" style={{ height: '40em' }}>
                    <div className="absolute left-0 bottom-0 w-full h-full z-10" style={{ backgroundImage: 'linear-gradient(180deg,transparent,rgba(0,0,0,.7))' }}></div>
                    <img src={data.imageUrl} className="absolute left-0 top-0 w-full h-full z-0 object-cover" />
                    <div className="p-4 absolute bottom-0 left-0 z-20">
                        <h2 className="text-4xl font-semibold text-gray-100 leading-tight">{data.title}</h2>
                        <div className="flex mt-3">
                            <img src={data.imageOfCreator !== DEFAULT_IMAGE_URLS.null ? data.imageOfCreator : DEFAULT_IMAGE_URLS.avatar} className="h-10 w-10 rounded-full mr-2 object-cover" />
                            <div>
                                <p className="font-semibold text-gray-200 text-sm">{data.createdByName}</p>
                                <p className="font-semibold text-gray-400 text-xs">{formattedDate}</p>
                            </div>
                        </div>
                    </div>
                </div>
                <div className="px-4 lg:px-0 mt-12 text-gray-700 max-w-screen-lg mx-auto text-lg leading-relaxed">
                    <strong>{data.shortDescription}</strong>
                </div>
                <div className="px-4 lg:px-0 mt-12 text-gray-700 max-w-screen-lg mx-auto text-lg leading-relaxed" dangerouslySetInnerHTML={{ __html: data.description }}></div>
                <CommentSection postId={data.id} />
            </main>
        </div>
    );
};

export default NewsDetail;
