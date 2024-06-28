import React from 'react';
import "./SubBanner.css"

const SubBanner = () => {
  const subBannerItems = [
    {
      id: 1,
      imageUrl: '//bizweb.dktcdn.net/100/329/122/themes/951253/assets/bottom_banner_1.jpg?1719541902656',
      title: 'Ổ cứng chính hãng',
      link: 'https://go.mmz.vn/o-cung-ssd-di-dong-web?_gl=1*1q5eide*_gcl_au*MTA3ODg5ODMwMS4xNzE5NTA3MjYw'
    },
    {
      id: 2,
      imageUrl: '//bizweb.dktcdn.net/100/329/122/themes/951253/assets/bottom_banner_2.jpg?1719541902656',
      title: 'Thẻ nhớ chính hãng',
      link: 'https://go.mmz.vn/the-nho-luu-tru-web'
    },
    {
      id: 3,
      imageUrl: '//bizweb.dktcdn.net/100/329/122/themes/951253/assets/bottom_banner_3.jpg?1719541902656',
      title: 'Chuột - Bàn phím - Tai nghe',
      link: 'https://go.mmz.vn/Cate-Gear?_gl=1*mvpkrk*_gcl_au*MTA3ODg5ODMwMS4xNzE5NTA3MjYw'
    }
  ];

  return (
    <div className="col-lg-9 sub_banner sub_banner--bottom d-none d-lg-flex">
      {subBannerItems.map(item => (
        <a key={item.id} className="sub_banner__item banner" href={item.link} title={item.title}>
          <picture>
            <source media="(max-width: 600px)" srcSet={item.imageUrl} />
            <img className="img-fluid" src={item.imageUrl} alt={item.title} width="355" height="172" />
          </picture>
        </a>
      ))}
    </div>
  );
};

export default SubBanner;
