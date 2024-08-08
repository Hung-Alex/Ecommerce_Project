import "./Offer.css";

/**
 * OfferCard Component
 *
 * This component displays an offer card with a background image, title, and description.
 * It uses the `bannerData` prop to receive data needed to render the card.
 *
 * @param {Object} props - Component props.
 * @param {Object} props.bannerData - Data object containing `logoImageUrl`, `title`, and `description` for the offer card.
 * @param {string} props.bannerData.logoImageUrl - URL of the background image for the card.
 * @param {string} props.bannerData.title - Title text to be displayed on the card.
 * @param {string} props.bannerData.description - Description text to be displayed on the card.
 *
 * @returns {JSX.Element} The rendered offer card component.
 */

const OfferCard = ({ bannerData }) => {
  const { logoImageUrl, title, description } = bannerData;
  return (

    <div className="md:flex items-center justify-center gap-8 p-8">
      <div className="flex-1">
        <img src={logoImageUrl} alt="Description" className="w-full h-auto rounded-lg hover:scale-105" />
      </div>
      <div className="flex-1 flex flex-col items-center justify-center text-center">
        <h1 className="text-3xl font-bold text-[#7EB693] mt-4 mb-4">{title}</h1>
        <p className="text-lg text-gray-600">{description}</p>
      </div>
    </div>
  );
};

export default OfferCard;
