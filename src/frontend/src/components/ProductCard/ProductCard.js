import React from 'react';
import './ProductCard.css';
import TagNav from '../TagNav/TagNav';

const products = [
  {
    id: 1,
    hotGift: 'Quà tặng HOT',
    preOrder: 'Pre-order',
    giftIcon: 'icon-gift', // class name for the gift icon
    brandLogo: 'https://file.hstatic.net/200000722513/file/asus_195eebbae0a44f84b7131bec913a6884.png',
    productImage: 'https://product.hstatic.net/200000722513/product/thumb_project_zero_c58860d9fa3a409294c17ab45f46f612_medium.png',
    promotionBanner: 'TẶNG BỘ QUÀ TRỊ GIÁ 5 TRIỆU',
    productName: 'PC GVN x MSI PROJECT ZERO WHITE (Intel i5-...)',
    specs: ['i5 14400F', 'RTX 4060', 'B760', '16GB', '1TB'],
    oldPrice: '30.230.000₫',
    newPrice: '28.990.000₫',
    discount: '-4%',
    ratingValue: '4.9',
    ratingCount: '(34 đánh giá)'
  },
  {
    id: 2,
    hotGift: 'Quà tặng HOT',
    preOrder: 'Pre-order',
    giftIcon: 'icon-gift', // class name for the gift icon
    brandLogo: 'https://file.hstatic.net/200000722513/file/asus_195eebbae0a44f84b7131bec913a6884.png',
    productImage: 'https://product.hstatic.net/200000722513/product/thumb_project_zero_c58860d9fa3a409294c17ab45f46f612_medium.png',
    promotionBanner: 'TẶNG BỘ QUÀ TRỊ GIÁ 5 TRIỆU',
    productName: 'Laptop XYZ (AMD Ryzen 7, GTX 3080, 32GB RAM, 1TB SSD)',
    specs: ['AMD Ryzen 7', 'GTX 3080', '32GB RAM', '1TB SSD'],
    oldPrice: '40.000.000₫',
    newPrice: '38.990.000₫',
    discount: '-2%',
    ratingValue: '4.8',
    ratingCount: '(25 đánh giá)'
  },
  {
    id: 3,
    hotGift: 'Quà tặng HOT',
    preOrder: 'Pre-order',
    giftIcon: 'icon-gift', // class name for the gift icon
    brandLogo: 'https://file.hstatic.net/200000722513/file/asus_195eebbae0a44f84b7131bec913a6884.png',
    productImage: 'https://product.hstatic.net/200000722513/product/thumb_project_zero_c58860d9fa3a409294c17ab45f46f612_medium.png',
    promotionBanner: 'TẶNG BỘ QUÀ TRỊ GIÁ 5 TRIỆU',
    productName: 'PC GVN x MSI PROJECT ZERO BLACK (Intel i7-...)',
    specs: ['i7 14700F', 'RTX 4070', 'B770', '32GB', '2TB'],
    oldPrice: '35.000.000₫',
    newPrice: '32.990.000₫',
    discount: '-6%',
    ratingValue: '4.7',
    ratingCount: '(45 đánh giá)'
  },
  {
    id: 4,
    hotGift: 'Quà tặng HOT',
    preOrder: 'Pre-order',
    giftIcon: 'icon-gift', // class name for the gift icon
    brandLogo: 'https://file.hstatic.net/200000722513/file/asus_195eebbae0a44f84b7131bec913a6884.png',
    productImage: 'https://product.hstatic.net/200000722513/product/thumb_project_zero_c58860d9fa3a409294c17ab45f46f612_medium.png',
    promotionBanner: 'TẶNG BỘ QUÀ TRỊ GIÁ 5 TRIỆU',
    productName: 'Laptop ABC (AMD Ryzen 5, GTX 3060, 16GB RAM, 512GB SSD)',
    specs: ['AMD Ryzen 5', 'GTX 3060', '16GB RAM', '512GB SSD'],
    oldPrice: '25.000.000₫',
    newPrice: '23.990.000₫',
    discount: '-4%',
    ratingValue: '4.6',
    ratingCount: '(38 đánh giá)'
  },
  {
    id: 5,
    hotGift: 'Quà tặng HOT',
    preOrder: 'Pre-order',
    giftIcon: 'icon-gift', // class name for the gift icon
    brandLogo: 'https://file.hstatic.net/200000722513/file/asus_195eebbae0a44f84b7131bec913a6884.png',
    productImage: 'https://product.hstatic.net/200000722513/product/thumb_project_zero_c58860d9fa3a409294c17ab45f46f612_medium.png',
    promotionBanner: 'TẶNG BỘ QUÀ TRỊ GIÁ 5 TRIỆU',
    productName: 'PC GVN x MSI PROJECT ZERO RED (Intel i9-...)',
    specs: ['i9 14900F', 'RTX 4080', 'B780', '64GB', '4TB'],
    oldPrice: '50.000.000₫',
    newPrice: '48.990.000₫',
    discount: '-2%',
    ratingValue: '4.9',
    ratingCount: '(50 đánh giá)'
  },
  {
    id: 6,
    hotGift: 'Quà tặng HOT',
    preOrder: 'Pre-order',
    giftIcon: 'icon-gift', // class name for the gift icon
    brandLogo: 'https://file.hstatic.net/200000722513/file/asus_195eebbae0a44f84b7131bec913a6884.png',
    productImage: 'https://product.hstatic.net/200000722513/product/thumb_project_zero_c58860d9fa3a409294c17ab45f46f612_medium.png',
    promotionBanner: 'TẶNG BỘ QUÀ TRỊ GIÁ 5 TRIỆU',
    productName: 'Laptop EFG (Intel i7, GTX 3070, 16GB RAM, 1TB SSD)',
    specs: ['Intel i7', 'GTX 3070', '16GB RAM', '1TB SSD'],
    oldPrice: '35.000.000₫',
    newPrice: '33.990.000₫',
    discount: '-3%',
    ratingValue: '4.7',
    ratingCount: '(42 đánh giá)'
  },
  {
    id: 7,
    hotGift: 'Quà tặng HOT',
    preOrder: 'Pre-order',
    giftIcon: 'icon-gift', // class name for the gift icon
    brandLogo: 'https://file.hstatic.net/200000722513/file/asus_195eebbae0a44f84b7131bec913a6884.png',
    productImage: 'https://product.hstatic.net/200000722513/product/thumb_project_zero_c58860d9fa3a409294c17ab45f46f612_medium.png',
    promotionBanner: 'TẶNG BỘ QUÀ TRỊ GIÁ 5 TRIỆU',
    productName: 'PC GVN x MSI PROJECT ZERO BLUE (AMD Ryzen 9, RTX 4090, 128GB RAM, 8TB SSD)',
    specs: ['AMD Ryzen 9', 'RTX 4090', '128GB RAM', '8TB SSD'],
    oldPrice: '80.000.000₫',
    newPrice: '78.990.000₫',
    discount: '-1%',
    ratingValue: '5.0',
    ratingCount: '(60 đánh giá)'
  },
  {
    id: 8,
    hotGift: 'Quà tặng HOT',
    preOrder: 'Pre-order',
    giftIcon: 'icon-gift', // class name for the gift icon
    brandLogo: 'https://file.hstatic.net/200000722513/file/asus_195eebbae0a44f84b7131bec913a6884.png',
    productImage: 'https://product.hstatic.net/200000722513/product/thumb_project_zero_c58860d9fa3a409294c17ab45f46f612_medium.png',
    promotionBanner: 'TẶNG BỘ QUÀ TRỊ GIÁ 5 TRIỆU',
    productName: 'Laptop KLM (Intel i5, GTX 3060, 16GB RAM, 512GB SSD)',
    specs: ['Intel i5', 'GTX 3060', '16GB RAM', '512GB SSD'],
    oldPrice: '28.000.000₫',
    newPrice: '26.990.000₫',
    discount: '-4%',
    ratingValue: '4.8',
    ratingCount: '(55 đánh giá)'
  }
];

const ProductCard = ({ product }) => {
  const { hotGift, preOrder, giftIcon, brandLogo, productImage, promotionBanner, productName, specs, oldPrice, newPrice, discount, ratingValue, ratingCount } = product;


  return (
    <div className="product-card">
      <div className="card-header">
        {hotGift && <span className="hot-gift">{hotGift}</span>}
        {preOrder && <span className="pre-order">{preOrder}</span>}
        {giftIcon && <i className={giftIcon}></i>}
      </div>
      <div className="brand-logo">
        {/* {brandLogo && <img src={brandLogo} alt="Brand Logo" />} */}
      </div>
      <div className="product-image">
        {productImage && <img src={productImage} alt="Product" />}
      </div>
      <div className="promotion-banner">
        {promotionBanner && <span>{promotionBanner}</span>}
      </div>
      <div className="product-info">
        {productName && <h2>{productName}</h2>}
        <div className="specs">
          {specs && specs.map((spec, index) => (
            <span key={index}>{spec}</span>
          ))}
        </div>
        <div className="price-section">
          {oldPrice && <span className="old-price">{oldPrice}</span>}
          {newPrice && <span className="new-price">{newPrice}</span>}
          {discount && <span className="discount">{discount}</span>}
        </div>
        <div className="rating">
          {ratingValue && <span className="rating-value">{ratingValue}</span>}
          {ratingCount && <span className="rating-count">{ratingCount}</span>}
        </div>
      </div>
    </div>
  );
}

const ProductList = () => {
  return (
    <div className='row'>
      <div><h1>laptop</h1><TagNav /></div>

      {products.map((product) => (
        <ProductCard key={product.id} product={product} />
      ))}
    </div>
  );
}

export default ProductList;