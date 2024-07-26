import { Link } from "react-router-dom";

const CatCard = ({ item }) => {
  const { image, name, description } = item;

  return (
    <div className="relative overflow-hidden cursor-pointer h-96 w-full group">
      <Link
        onClick={() => window.scrollTo({ top: 0, behavior: 'smooth' })}
        to={`/category/${name}`}
        className="block h-full w-full"
      >
        <div
          className="relative h-full w-full bg-cover bg-center transition-transform duration-300 group-hover:scale-105"
          style={{ backgroundImage: `url(${image})` }}
        >
          <div className="pl-20 absolute inset-0 bg-black bg-opacity-0 transition-opacity duration-300 group-hover:bg-opacity-50 flex flex-col justify-center p-4 text-white">
            <h3 className="text-5xl font-[Yellowtail] transition-transform duration-300 group-hover:text-yellow-400">
              {name}
            </h3>
            <p className="text-sm font-extrabold">{description}</p>
          </div>
        </div>
      </Link>
    </div>
  );
};

export default CatCard;
