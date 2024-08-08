import { BsArrowRightShort } from "react-icons/bs";
import Button from "../../../components/UI/Buttons/Button";
import { Link } from "react-router-dom";

const NewsCard = ({ newsData }) => {
  const { image, title, createdAt, createdByName, urlSlug, shortDescription } = newsData;
  console.log(newsData);

  const formattedDate = new Date(createdAt).toLocaleDateString('en-US', {
    day: '2-digit',
    month: 'short',
    year: 'numeric',
  }).split(' ');

  const day = formattedDate[1];
  const month = formattedDate[0];

  return (
    <div className="relative rounded-xl mx-auto p-2  w-full max-w-md bg-white pb-12  duration-200 md:max-w-lg lg:max-w-xl">
      <div className="overflow-hidden rounded-xl">
        <Link to={`/news/${urlSlug}`}>
          <figure className="relative">
            <div className="absolute top-4 left-4 bg-white w-12 h-12 rounded-full flex flex-col justify-center items-center shadow-md z-10">
              <p className="text-sm font-bold">{day}</p>
              <p className="text-xs font-semibold">{month}</p>
            </div>
            <img className="rounded-xl w-full h-[250px] md:h-[350px] object-cover transition-transform duration-300 hover:scale-105" src={image} alt={title} />
          </figure>
        </Link>
      </div>
      <div className="absolute bottom-4 left-1/2 transform -translate-x-1/2 bg-white w-11/12 rounded-xl p-4 shadow-lg">
        <p className="text-xs text-gray-500">By {createdByName}</p>
        <h3 className="font-semibold text-md my-1 text-gray-800">{title}</h3>
        <p className="text-sm text-gray-600 mb-3 h-[80px] line-clamp-3">{shortDescription}</p>
        <Link to={`/news/${urlSlug}`}>
          <Button className="w-full max-w-xs mt-4 bg-[#EFD372] text-[#274c5b] text-sm hover:bg-[#D4B948] hover:text-white transition-colors duration-200 flex items-center justify-center">
            More News
            <BsArrowRightShort className="bg-[#335B6B] text-white rounded-full ml-1" />
          </Button>
        </Link>
      </div>
    </div>
  );
};

export default NewsCard;
