import React, { useState } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import './CategoryBar.css'; // Tạo file CSS cho các style tùy chỉnh
import Banner from '../Banner/Banner';
import SubBanner from '../SubBanner/SubBanner ';

const categories = [
    {
        id: 1,
        name: 'Chuột-Bàn phím-Tai nghe',
        link: '/working-gaming-gear',
        imgSrc: '//bizweb.dktcdn.net/100/329/122/themes/951253/assets/menu_icon_1.png?1719463557044',
        subcategories: [
            {
                name: 'Chuột',
                link: '/chuot-gaming-van-phong',
                items: [
                    { name: 'Logitech', link: '/chuot-logitech' },
                    { name: 'Razer', link: '/chuot-razer' },
                    { name: 'Asus', link: '/chuot-asus' },
                    { name: 'Corsair', link: '/chuot-corsair' },
                    { name: 'HyperX', link: '/chuot-hyperx' },
                    { name: 'Dell', link: '/chuot-dell' },
                    { name: 'AOC', link: '/chuot-aoc' }
                ]
            },
            {
                name: 'Chuột',
                link: '/chuot-gaming-van-phong',
                items: [
                    { name: 'Logitech', link: '/chuot-logitech' },
                    { name: 'Razer', link: '/chuot-razer' },
                    { name: 'Asus', link: '/chuot-asus' },
                    { name: 'Corsair', link: '/chuot-corsair' },
                    { name: 'HyperX', link: '/chuot-hyperx' },
                    { name: 'Dell', link: '/chuot-dell' },
                    { name: 'AOC', link: '/chuot-aoc' }
                ]
            },
            // Thêm các danh mục con khác ở đây
        ]
    },
    {
        id: 1,
        name: 'Chuột-Bàn phím-Tai nghe',
        link: '/working-gaming-gear',
        imgSrc: '//bizweb.dktcdn.net/100/329/122/themes/951253/assets/menu_icon_1.png?1719463557044',
        subcategories: [
            {
                name: 'gà',
                link: '/chuot-gaming-van-phong',
                items: [
                    { name: 'Logitech', link: '/chuot-logitech' },
                    { name: 'Razer', link: '/chuot-razer' },
                    { name: 'Asus', link: '/chuot-asus' },
                    { name: 'Corsair', link: '/chuot-corsair' },
                    { name: 'HyperX', link: '/chuot-hyperx' },
                    { name: 'Dell', link: '/chuot-dell' },
                    { name: 'AOC', link: '/chuot-aoc' }
                ]
            },
            {
                name: 'Chuột',
                link: '/chuot-gaming-van-phong',
                items: [
                    { name: 'Logitech', link: '/chuot-logitech' },
                    { name: 'Razer', link: '/chuot-razer' },
                    { name: 'Asus', link: '/chuot-asus' },
                    { name: 'Corsair', link: '/chuot-corsair' },
                    { name: 'HyperX', link: '/chuot-hyperx' },
                    { name: 'Dell', link: '/chuot-dell' },
                    { name: 'AOC', link: '/chuot-aoc' }
                ]
            },
            // Thêm các danh mục con khác ở đây
        ]
    },
    // Thêm các danh mục chính khác ở đây
];

const CategoryBar = () => {
    const [hoveredCategory, setHoveredCategory] = useState(null);

    return (
        <div>
            <div className="container-fluid">
                <div className="row" onMouseEnter={() => setHoveredCategory(null)}
                    onMouseLeave={() => setHoveredCategory(null)}>
                    <div className="col-lg-3 category-menu">
                        <nav className="h-100">
                            <ul className="navigation list-group list-group-flush">
                                {categories.map(category => (
                                    <li
                                        key={category.id}
                                        className="menu-item list-group-item"
                                        onMouseEnter={() => setHoveredCategory(category)}
                                    >
                                        <a href={category.link} className="menu-item__link" title={category.name}>
                                            <img
                                                width="24"
                                                height="24"
                                                src={category.imgSrc}
                                                alt={category.name}
                                            />
                                            <span>{category.name}</span>
                                        </a>
                                    </li>
                                ))}
                            </ul>
                        </nav>
                    </div>
                    <div className="col-lg-6 subcategory-menu">
                        {hoveredCategory && (
                            <div className="submenu-container">
                                {hoveredCategory.subcategories.map((subcategory, index) => (
                                    <div key={index}  className="submenu-item">
                                        <span className="submenu-title">{subcategory.name}</span>
                                        <div className="subitem-container">
                                            {subcategory.items.map((item, subIndex) => (
                                                <div key={subIndex} className="subitem">
                                                    <a className="link" href={item.link} title={item.name}>
                                                        {item.name}
                                                    </a>
                                                </div>
                                            ))}
                                        </div>
                                    </div>
                                ))}
                            </div>
                        )}
                    </div>
                    <Banner />
                </div>
            </div>
        </div>
    );
};

export default CategoryBar;
