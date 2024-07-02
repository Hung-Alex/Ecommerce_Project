import React from 'react';
import "./TagNav.css"

const TagNav = () => {
    // Array of items for navigation
    const items = [
        { id: 1, title: "PC Basic", link: "/pc-basic" },
        { id: 2, title: "PC Văn Phòng", link: "/pc-mercury-series" },
        { id: 3, title: "PC Gaming Esports", link: "/pc-gaming" },
        { id: 4, title: "𝗣𝗖 𝗣𝗔𝗡𝗢 🫧🫧", link: "/pc-pano" },
        { id: 5, title: "PC Gaming AAA", link: "/pc-gaming-aaa" },
        { id: 6, title: "PC Creator", link: "/pc-creator" },
        { id: 7, title: "PC Gaming MSI", link: "/pc-gaming-msi" },
        { id: 8, title: "PC Gaming Asus", link: "/pc-gaming-asus" },
        { id: 9, title: "PC Đồ Họa / Render", link: "/pc-do-hoa-render" },
    ];

    return (
        <div className="tagnav--horizontal d-lg-flex align-items-center mt-3">
            <div className="tagnav-wrapper overflow">
                <ul className="mt-2 mt-sm-0 tabs tabs-title list-unstyled mb-0 d-flex align-items-center overflow-auto">
                    {items.map((item) => (
                        <li key={item.id} className="menu-item ega-small tab-link link mr-2 mt-2111">
                            <a href={item.link} className="menu-item__link tag-item-link" title={item.title}>{item.title}</a>
                        </li>
                    ))}
                </ul>
            </div>
            <div className="navigation-arrows">
                <i className="fas fa-chevron-left prev disabled"></i>
                <i className="fas fa-chevron-right next"></i>
            </div>
        </div>
    );
}

export default TagNav;
