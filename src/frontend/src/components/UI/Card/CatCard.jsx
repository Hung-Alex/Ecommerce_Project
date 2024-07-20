import { Link } from "react-router-dom";
import img from "../../../assets/Home/Card/card6.png"

const CatCard = ({ item }) => {
  const { image, name } = item;
  return (
    <div className="select-none place-self-center text-center flex flex-col justify-center items-center cursor-pointer">
      <Link onClick={() => window.scrollTo({ top: 0, behavior: 'smooth' })} to={`/category/${name}`} className="w-16 lg:w-36">
        <img className="w-full h-36 " src={image} />
      </Link>
      <p className="text-sm">{name}</p>
    </div>
  );
};

export default CatCard;
