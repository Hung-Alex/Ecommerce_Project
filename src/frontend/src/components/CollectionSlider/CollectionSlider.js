import React from 'react';
import "./CollectionSlider.css";

const CollectionSlider = () => {
  // Array of collection items
  const collectionItems = [
    {
      id: 1,
      title: 'Tự build PC',
      imageUrl: '//bizweb.dktcdn.net/thumb/small/100/329/122/themes/951253/assets/coll_1.jpg?1719494496488',
      link: 'https://go.mmz.vn/47JkJi6'
    },
    {
      id: 2,
      title: 'PC - Máy bộ',
      imageUrl: '//bizweb.dktcdn.net/thumb/small/100/329/122/themes/951253/assets/coll_2.jpg?1719494496488',
      link: 'https://go.mmz.vn/3RbaoVf'
    },
    {
      id: 3,
      title: 'Màn hình',
      imageUrl: '//bizweb.dktcdn.net/thumb/small/100/329/122/themes/951253/assets/coll_3.jpg?1719494496488',
      link: 'https://go.mmz.vn/3t3EjXm'
    },
    {
      id: 4,
      title: 'Laptop',
      imageUrl: '//bizweb.dktcdn.net/thumb/small/100/329/122/themes/951253/assets/coll_4.jpg?1719494496488',
      link: 'https://go.mmz.vn/483ON7X'
    },
    {
      id: 5,
      title: 'Chuột - Phím Tai nghe',
      imageUrl: '//bizweb.dktcdn.net/thumb/small/100/329/122/themes/951253/assets/coll_5.jpg?1719494496488',
      link: 'https://go.mmz.vn/3RqfkH7'
    },
    {
      id: 6,
      title: 'Ổ cứng SSD Gắn trong',
      imageUrl: '//bizweb.dktcdn.net/thumb/small/100/329/122/themes/951253/assets/coll_6.jpg?1719494496488',
      link: 'https://go.mmz.vn/3Rs5fcO'
    },
    {
      id: 7,
      title: 'Ổ cứng SSD Di động',
      imageUrl: '//bizweb.dktcdn.net/thumb/small/100/329/122/themes/951253/assets/coll_7.jpg?1719494496488',
      link: 'https://go.mmz.vn/3t3GxWK'
    },
    {
      id: 8,
      title: 'Thẻ nhớ',
      imageUrl: '//bizweb.dktcdn.net/thumb/small/100/329/122/themes/951253/assets/coll_8.jpg?1719494496488',
      link: 'https://go.mmz.vn/3RvRYA6'
    },
    {
      id: 9,
      title: 'RAM',
      imageUrl: '//bizweb.dktcdn.net/thumb/small/100/329/122/themes/951253/assets/coll_9.jpg?1719494496488',
      link: 'https://go.mmz.vn/3GsIHCz'
    },
    {
      id: 10,
      title: 'NAS',
      imageUrl: '//bizweb.dktcdn.net/thumb/small/100/329/122/themes/951253/assets/coll_10.jpg?1719494496488',
      link: 'https://go.mmz.vn/3NenXly'
    }
  ];

  return (
    <section className="section_collections section">
      <div className="container border-0">
        <div className="mt-2 text-center row flex-nowrap collections-slide slick-initialized slick-slider">
          <div className="slick-list draggable">
            <div className="slick-track" style={{ opacity: 1, width: '100%', transform: 'translate3d(0px, 0px, 0px)' }}>
              {collectionItems.map((item) => (
                <div key={item.id} className="item slick-slide slick-active">
                  <a href={item.link} title={item.title} className="pos-relative d-flex align-items-center">
                    <img
                      className="img-fluid m-auto object-contain mh-100"
                      src={item.imageUrl}
                      alt={item.title}
                      width="100"
                      height="100"
                    />
                  </a>
                  <h3 className="mb-0">
                    <a href={item.link} title={item.title}>
                      {item.title}
                    </a>
                  </h3>
                </div>
              ))}
            </div>
          </div>
        </div>
      </div>
    </section>
  );
};

export default CollectionSlider;
