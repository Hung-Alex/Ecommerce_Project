import React, { useState } from 'react';
import "./Header.css"

import { Navbar, Nav, Container } from 'react-bootstrap';
import AuthModals from '../Login/Login';

const Header = () => {
    const [isLoginModalOpen, setIsLoginModalOpen] = useState(false);
    const [isRegisterModalOpen, setIsRegisterModalOpen] = useState(false);

    const openLoginModal = () => setIsLoginModalOpen(true);
    const closeLoginModal = () => setIsLoginModalOpen(false);

    const openRegisterModal = () => {
        setIsLoginModalOpen(false); // Đóng modal đăng nhập trước khi mở modal đăng ký
        setIsRegisterModalOpen(true);
    };
    const closeRegisterModal = () => setIsRegisterModalOpen(false);

    return (
        <div className='mid-header'>
            <div className='container'>
                <div className="row align-items-center">
                    <div className="col-2 col-sm-3 header-right d-lg-none d-block">
                        <div className="toggle-nav btn menu-bar mr-4 ml-0 p-0  d-lg-none d-flex text-white">
                            <span className="bar" />
                            <span className="bar" />
                            <span className="bar" />
                        </div>
                    </div>
                    <div className="col-6 col-lg-2 col-xl-3 header-left">
                        <a href="/" className="logo-wrapper " title="MemoryZone by SieuToc">
                            <img className="img-fluid" src="//bizweb.dktcdn.net/100/329/122/themes/951253/assets/logo.png?1717830509242" alt="logo MemoryZone by SieuToc" width={248} height={56} />
                        </a>
                    </div>
                    <div className="col-lg-6 col-12 header-center pl-lg-0" id="search-header">
                        <ae-widget hydrate="load" component="AeSmartSearchInput" api="https://memoryzone.aecomapp.com/api/" api-key={2} data-v-app><form className="ae-search-group"><input type="text" autoComplete="off" className="ae-search-group__input" required placeholder="Bạn cần tìm gì?" style={{}} /><button type="submit" aria-label="search" className="ae-search-group__button" style={{}}><svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 192.904 192.904" width={18} height={18}><path d="M190.707 180.101l-47.078-47.077c11.702-14.072 18.752-32.142 18.752-51.831C162.381 36.423 125.959 0 81.191 0 36.422 0 0 36.423 0 81.193c0 44.767 36.422 81.187 81.191 81.187 19.688 0 37.759-7.049 51.831-18.751l47.079 47.078a7.474 7.474 0 005.303 2.197 7.498 7.498 0 005.303-12.803zM15 81.193C15 44.694 44.693 15 81.191 15c36.497 0 66.189 29.694 66.189 66.193 0 36.496-29.692 66.187-66.189 66.187C44.693 147.38 15 117.689 15 81.193z" /></svg></button><div className="ae-search" style={{ display: 'none' }}><div className="ae-search__overlay" /><div className="ae-search__container query" style={{ top: '98.8125px', maxHeight: 'calc(-102.812px + 100vh)' }}><div className="ae-search__result"><div className="ae-search__result--keyword"><h3 className="ae-search__title">Từ khoá được tìm nhiều nhất</h3><ul className="ae-search__keywords"><li><a href="/search?query=Bàn phím cơ" className="ae-search__keyword">Bàn phím cơ</a></li><li><a href="/search?query=bàn phím keychron" className="ae-search__keyword">bàn phím keychron</a></li><li><a href="/search?query=Bàn phím" className="ae-search__keyword">Bàn phím</a></li></ul><div className="ae-search__result--object mobile-hidden"><h3 className="ae-search__title"> Danh mục được tìm nhiều nhất </h3><ul><li data-v-1a68482c className="ae-search__object ae-search__object--collection"><a data-v-1a68482c href="/ban-phim-keychron?utm_source=Website&utm_medium=TopSearch"><em>Bàn</em> <em>phím</em> <em>Keychron</em></a></li><li data-v-1a68482c className="ae-search__object ae-search__object--collection"><a data-v-1a68482c href="/ban-phim-custom?utm_source=Website&utm_medium=TopSearch"><em>Bàn</em> <em>phím</em> Custom</a></li><li data-v-1a68482c className="ae-search__object ae-search__object--collection"><a data-v-1a68482c href="/ban-phim-minisize?utm_source=Website&utm_medium=TopSearch"><em>Bàn</em> <em>phím</em> Mini</a></li></ul></div><div className="ae-search__result--promotion mobile-hidden"><h3 className="ae-search__title">Chương trình khuyến mãi</h3><ul><li className="ae-promotion-item"><div><a href="https://memoryzone.com.vn/mua-laptop-lg-gram-nhan-qua-gan-10-trieu-dong" className="ae-promotion-link">Sắm LG Gram, nhận quà tặng ~10 triệu</a></div><div><span className="hot"> Hot </span></div></li><li className="ae-promotion-item"><div><a href="https://memoryzone.com.vn/intel-core-ultra" className="ae-promotion-link">Laptop Intel Core Ultra</a></div><div><span className="hot"> Hot </span></div></li><li className="ae-promotion-item"><div><a href="https://memoryzone.com.vn/o-cung-ssd-di-dong-samsung" className="ae-promotion-link">Mua SSD di động Samsung giảm đến 500K</a></div><div><span className="hot"> Hot </span></div></li><li className="ae-promotion-item"><div><a href="https://memoryzone.com.vn/laptop-asus-zenbook-duo" className="ae-promotion-link">ASUS ZENBOOK DUO tặng chuột ASUS Marshmallow</a></div><div><span className="hot"> Hot </span></div></li><li className="ae-promotion-item"><div><a href="https://memoryzone.com.vn/combo-khong-day-chat-lu" className="ae-promotion-link">Mua phím, chuột Asus Tặng tai nghe không dây</a></div><div><span className="hot"> Hot </span></div></li><li className="ae-promotion-item"><div><a href="https://go.mmz.vn/ssd-di-dong-transcend" className="ae-promotion-link">Mua SSD di động Transcend tặng Voucher 400K</a></div><div><span className="hot"> Hot </span></div></li><li className="ae-promotion-item"><div><a href="https://memoryzone.com.vn/laptop-acer-nitro" className="ae-promotion-link">Mua Nitro V nhận gift code 1 Triệu</a></div><div><span className="hot"> Hot </span></div></li></ul></div></div><div className="ae-search__result--object"><h3 className="ae-search__title"> Sản phẩm được tìm nhiều nhất </h3><ul><li data-v-78a44725 className="ae-search__object ae-search__object--product"><img data-v-78a44725 width={60} height={60} src="https://bizweb.dktcdn.net/thumb/medium/100/329/122/products/ban-phim-co-khong-day-keychron-k4-pro-full-assembled-rgb-hotswap-keychron-k-pro-switch-1.jpg?v=1716603853000" /><div data-v-78a44725><h3 data-v-78a44725 className="object--name"><a data-v-78a44725 href="/ban-phim-co-khong-day-keychron-k4-pro-rgb-hotswap-keychron-k-pro-sw?utm_source=Website&utm_medium=TopSearch"><em>Bàn</em> <em>phím</em> cơ không dây <em>Keychron</em> K4 Pro RGB Hotswap (<em>Keychron</em> K Pro Sw) <em>Keychron</em> K Pro&nbsp;Red</a></h3><div data-v-78a44725 className="ae-price"><div className="tagdacbiet_sale sale-flash"><div className="font16">-20%</div></div><span className="ae-price--primary">2.290.000&nbsp;₫</span><span className="ae-price--compare">2.890.000&nbsp;₫</span></div></div></li><li data-v-78a44725 className="ae-search__object ae-search__object--product"><img data-v-78a44725 width={60} height={60} src="https://bizweb.dktcdn.net/thumb/medium/100/329/122/products/ban-phim-co-khong-day-keychron-k2-pro-full-assembled-rgb-hotswap-keychron-k-pro-switch-1.jpg?v=1713795898787" /><div data-v-78a44725><h3 data-v-78a44725 className="object--name"><a data-v-78a44725 href="/ban-phim-co-khong-day-keychron-k2-pro-aluminum-frame-rgb-hotswap-keychron-k-pro-sw?utm_source=Website&utm_medium=TopSearch"><em>Bàn</em> <em>phím</em> cơ không dây <em>Keychron</em> K2 Pro Aluminum Frame RGB Hotswap (<em>Keychron</em> K Pro Sw) <em>Keychron</em> K Pro&nbsp;Blue</a></h3><div data-v-78a44725 className="ae-price"><div className="tagdacbiet_sale sale-flash"><div className="font16">-30%</div></div><span className="ae-price--primary">2.090.000&nbsp;₫</span><span className="ae-price--compare">2.990.000&nbsp;₫</span></div></div></li><li data-v-78a44725 className="ae-search__object ae-search__object--product"><img data-v-78a44725 width={60} height={60} src="https://bizweb.dktcdn.net/thumb/medium/100/329/122/products/ban-phim-co-khong-day-tkl-keychron-q3-pro-silver-grey-se-rgb-knob-hotswap-3.jpg?v=1717257640327" /><div data-v-78a44725><h3 data-v-78a44725 className="object--name"><a data-v-78a44725 href="/ban-phim-co-khong-day-tkl-keychron-q3-pro-silver-grey-se-rgb-knob-hotswap-keychron-k-pro-sw?utm_source=Website&utm_medium=TopSearch"><em>Bàn</em> <em>phím</em> cơ không dây TKL <em>Keychron</em> Q3 Pro Silver Grey SE RGB Knob Hotswap (<em>Keychron</em> K Pro Sw) <em>Keychron</em> K Pro Brown Switch</a></h3><div data-v-78a44725 className="ae-price"><div className="tagdacbiet_sale sale-flash"><div className="font16">-18%</div></div><span className="ae-price--primary">4.090.000&nbsp;₫</span><span className="ae-price--compare">4.990.000&nbsp;₫</span></div></div></li><li data-v-78a44725 className="ae-search__object ae-search__object--product"><img data-v-78a44725 width={60} height={60} src="https://bizweb.dktcdn.net/thumb/medium/100/329/122/products/ban-phim-co-khong-day-keychron-v1-max-rgb-hotswap-gateron-sw.jpg?v=1717257613693" /><div data-v-78a44725><h3 data-v-78a44725 className="object--name"><a data-v-78a44725 href="/ban-phim-co-khong-day-keychron-v1-max-rgb-hotswap-gateron-sw?utm_source=Website&utm_medium=TopSearch"><em>Bàn</em> <em>phím</em> cơ không dây <em>Keychron</em> V1 Max RGB Hotswap (Gateron Sw) Gateron Jupiter Red</a></h3><div data-v-78a44725 className="ae-price"><div className="tagdacbiet_sale sale-flash"><div className="font16">-23%</div></div><span className="ae-price--primary">2.290.000&nbsp;₫</span><span className="ae-price--compare">2.990.000&nbsp;₫</span></div></div></li></ul><h3 className="ae-search__title mobile-hidden"> Bài viết được tìm nhiều nhất </h3><ul className="mobile-hidden"><li data-v-7a50c279 className="ae-search__object ae-search__object--article"><a data-v-7a50c279 href="/chon-mua-ban-phim-keychron-2024?utm_source=Website&utm_medium=TopSearch">Chọn mua <em>bàn</em> <em>phím</em> <em>Keychron</em> 2024: Nên mua <em>bàn</em> <em>phím</em> <em>keychron</em> nào tối ưu nhất?</a><div data-v-7a50c279><span data-v-7a50c279 className="ae-tag">các loại bàn phím keychron</span><span data-v-7a50c279 className="ae-tag">nên mua bàn phím keychron nào</span></div></li><li data-v-7a50c279 className="ae-search__object ae-search__object--article"><a data-v-7a50c279 href="/ban-phim-cong-thai-hoc-la-gi?utm_source=Website&utm_medium=TopSearch"><em>Bàn</em> <em>phím</em> công thái học là gì? Tìm hiểu sự khác biệt giữa <em>bàn</em> <em>phím</em> thường với <em>bàn</em> <em>phím</em> công thái học</a><div data-v-7a50c279><span data-v-7a50c279 className="ae-tag">bàn phím công thái học</span><span data-v-7a50c279 className="ae-tag">Bàn phím công thái học logitech</span><span data-v-7a50c279 className="ae-tag">Bàn phím công thái học microsoft</span></div></li></ul></div></div></div></div></form></ae-widget>
                        <div className="search-dropdow">
                            <ul className="search__list pl-0 d-flex list-unstyled mb-0 flex-wrap">
                                <li className="mr-2">
                                    <a id="filter-search-ban-phim-keychron" href="/search?query=b%C3%A0n+ph%C3%ADm+keychron&type=product">bàn phím keychron</a>
                                </li>
                                <li className="mr-2">
                                    <a id="filter-search-msi-cyborg-15" href="/search?query=MSI+Cyborg+15&type=product">MSI Cyborg 15</a>
                                </li>
                                <li className="mr-2">
                                    <a id="filter-search-asus-oled" href="/search?query=ASUS+OLED&type=product">ASUS OLED</a>
                                </li>
                                <li className="mr-2">
                                    <a id="filter-search-pc-gaming" href="/search?query=PC+Gaming&type=product">PC Gaming</a>
                                </li>
                                <li className="mr-2">
                                    <a id="filter-search-razer-pro-click" href="/search?query=razer+pro+click&type=product">razer pro click</a>
                                </li>
                                <li className="mr-2">
                                    <a id="filter-search-the-nho" href="/search?query=Th%E1%BA%BB+nh%E1%BB%9B&type=product">Thẻ nhớ</a>
                                </li>
                                <li className="mr-2">
                                    <a id="filter-search-usb" href="/search?query=USB&type=product">USB</a>
                                </li>
                                <li className="mr-2">
                                    <a id="filter-search-loa" href="/search?query=Loa&type=product">Loa</a>
                                </li>
                                <li className="mr-2">
                                    <a id="filter-search-" href="/search?query=&type=product" />
                                </li>
                            </ul>
                        </div>
                    </div>
                    <div className="col-4 col-sm-3 col-lg-6 col-xl-3 pl-0">
                        <ul className="header-right mb-0 list-unstyled d-flex align-items-center">
                            <li className="ml-4 mr-4 mr-md-0 ml-md-3 media d-lg-flex d-none">
                                <img src="//bizweb.dktcdn.net/100/329/122/themes/951253/assets/account-icon.png?1717830509242" width={24} height={24} alt="account_icon" className="mr-1 align-self-center" />
                                <div className="media-body d-md-flex flex-column d-none ">
                                    <a rel="nofollow" href="/account/login" className="d-block" title="Tài khoản">
                                        Tài khoản
                                    </a>
                                    <a onClick={openLoginModal} title="Đăng nhập" className="d-block">
                                        <Nav className="ml-auto">
                                            <AuthModals />
                                        </Nav>
                                    </a>
                                </div>
                            </li>
                            <li className="d-lg-none">
                                <a href="/so-sanh" className="position-relative d-block" title="So sánh sản phẩm">
                                    <img src="//bizweb.dktcdn.net/100/329/122/themes/951253/assets/compare-icon.png?1717830509242" width={24} height={24} className="align-self-center" alt="compare-icon" />
                                    <span className="compare-product__header-count position-absolute d-block">0</span>
                                </a>
                            </li>
                            <li className="cartgroup ml-3">
                                <div className="mini-cart text-xs-center">
                                    <a className="img_hover_cart" href="/cart" title="Giỏ hàng">
                                        <img src="//bizweb.dktcdn.net/100/329/122/themes/951253/assets/cart-icon.png?1717830509242" width={24} height={24} alt="cart_icon" />
                                        <span className="ml-2 d-xl-block d-none">Giỏ hàng</span>
                                        <span className="count_item count_item_pr">0</span>
                                    </a>
                                    <div className="top-cart-content-comment">
                                        <ul id="cart-sidebar" className="mini-products-list count_li list-unstyled"><div className="no-item"><p>Không có sản phẩm nào.</p></div></ul>
                                    </div>
                                </div>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default Header
