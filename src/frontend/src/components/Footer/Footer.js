import React from 'react';
import './Footer.css'; // CSS formatting for footer

const Footer = () => {
    return (
        <div className=' line'>
            <footer className="footer container">
                <div className="main-footer--top">
                    <div className="container-fluid">
                        <div className="row">
                            <div className="col-sm-12 col-md-6 col-lg-2 col-lg">
                                <div className="footer-col footer-link toggle-footer toggle-first">
                                    <div className="footer-title">
                                        <h4>Về MemoryZone</h4>
                                        <span className="icon-title"></span>
                                    </div>
                                    <div className="footer-content">
                                        <ul className='list-unstyled'>
                                            <li className="item"><a href="/pages/gioi-thieu-gearvn" title="Giới thiệu">Giới thiệu</a></li>
                                            <li className="item"><a href="https://tuyendung.gearvn.com/" title="Tuyển dụng">Tuyển dụng</a></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                            <div className="col-sm-12 col-md-6 col-lg-2 col-lg">
                                <div className="footer-col footer-link toggle-footer">
                                    <div className="footer-title">
                                        <h4>Chính sách</h4>
                                        <span className="icon-title"></span>
                                    </div>
                                    <div className="footer-content">
                                        <ul className='list-unstyled'>
                                            <li className="item"><a href="/pages/chinh-sach-bao-hanh" title="Chính sách bảo hành">Chính sách bảo hành</a></li>
                                            <li className="item"><a href="/pages/huong-dan-thanh-toan-gearvn" title="Chính sách thanh toán">Chính sách thanh toán</a></li>
                                            <li className="item"><a href="/pages/chinh-sach-giao-hang" title="Chính sách giao hàng">Chính sách giao hàng</a></li>
                                            <li className="item"><a href="/pages/chinh-sach-bao-mat" title="Chính sách bảo mật">Chính sách bảo mật</a></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                            <div className="col-sm-12 col-md-6 col-lg-2 col-lg">
                                <div className="footer-col footer-link toggle-footer">
                                    <div className="footer-title">
                                        <h4>Thông tin</h4>
                                        <span className="icon-title"></span>
                                    </div>
                                    <div className="footer-content">
                                        <ul className='list-unstyled'>
                                            <li className="item"><a href="/pages/he-thong-showroom-gearvn" title="Hệ thống cửa hàng">Hệ thống cửa hàng</a></li>
                                            <li className="item"><a href="/pages/huong-dan-mua-hang" title="Hướng dẫn mua hàng">Hướng dẫn mua hàng</a></li>
                                            <li className="item"><a href="/pages/trung-tam-ho-tro-tra-cuu-thong-tin-bao-hanh-san-pham-chinh-hang" title="Tra cứu địa chỉ bảo hành">Tra cứu địa chỉ bảo hành</a></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                            <div className="col-sm-12 col-md-6 col-lg-3 col-lg">
                                <div className="footer-col footer-block toggle-footer">
                                    <div className="footer-title">
                                        <h4>TỔNG ĐÀI HỖ TRỢ <span>(8:00 - 21:00)</span></h4>
                                    </div>
                                    <div className="footer-content">
                                        <div className="list-info">
                                            <p><span>Mua hàng:</span> <a href="tel:19005301">1900.5301</a></p>
                                            <p><span>Bảo hành:</span> <a href="tel:19005325">1900.5325</a></p>
                                            <p><span>Khiếu nại:</span> <a href="tel:18006173">1800.6173</a></p>
                                            <p><span>Email:</span> <a href="mailto:cskh@gearvn.com">cskh@gearvn.com</a></p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div className="col-sm-12 col-md-6 col-lg-3 col-lg">
                                <div className="footer-col footer-block toggle-footer">
                                    <div className="footer-title">
                                        <h4>Đơn vị vận chuyển</h4>
                                    </div>
                                    <div className="footer-content">
                                        <ul className="list-ship list-unstyled">
                                            <li><img src="//theme.hstatic.net/200000722513/1001090675/14/ship_1.png?v=5650" alt="Hình thức vận chuyển 1" /></li>
                                            <li><img src="//theme.hstatic.net/200000722513/1001090675/14/ship_2.png?v=5650" alt="Hình thức vận chuyển 2" /></li>
                                            <li><img src="//theme.hstatic.net/200000722513/1001090675/14/ship_3.png?v=5650" alt="Hình thức vận chuyển 3" /></li>
                                            <li><img src="//theme.hstatic.net/200000722513/1001090675/14/ship_4.png?v=5650" alt="Hình thức vận chuyển 4" /></li>
                                        </ul>
                                    </div>
                                </div>
                                <div className="footer-col footer-block toggle-footer">
                                    <div className="footer-title">
                                        <h4>Cách thức thanh toán</h4>
                                    </div>
                                    <div className="footer-content">
                                        <ul className="list-pay list-unstyled ">
                                            <li><img src="//theme.hstatic.net/200000722513/1001090675/14/pay_1.png?v=5650" alt="Phương thức thanh toán 1" /></li>
                                            <li><img src="//theme.hstatic.net/200000722513/1001090675/14/pay_2.png?v=5650" alt="Phương thức thanh toán 2" /></li>
                                            <li><img src="//theme.hstatic.net/200000722513/1001090675/14/pay_3.png?v=5650" alt="Phương thức thanh toán 3" /></li>
                                            <li><img src="//theme.hstatic.net/200000722513/1001090675/14/pay_4.png?v=5650" alt="Phương thức thanh toán 4" /></li>
                                            <li><img src="//theme.hstatic.net/200000722513/1001090675/14/pay_5.png?v=5650" alt="Phương thức thanh toán 5" /></li>
                                            <li><img src="//theme.hstatic.net/200000722513/1001090675/14/pay_6.png?v=5650" alt="Phương thức thanh toán 6" /></li>
                                            <li><img src="//theme.hstatic.net/200000722513/1001090675/14/pay_7.png?v=5650" alt="Phương thức thanh toán 7" /></li>
                                            <li><img src="//theme.hstatic.net/200000722513/1001090675/14/pay_8.png?v=5650" alt="Phương thức thanh toán 8" /></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div className="line">
                    <div className="d-flex align-items-lg-center flex-lg-nowrap flex-wrap">
                        <h4 className="footer-title">Kết nối với chúng tôi</h4>
                        <div className="footer-content d-flex align-items-center justify-content-lg-between">
                            <div className="social-list">
                                <a href="https://www.facebook.com/gearvnhcm" target="_blank" rel="nofollow">
                                    <img src="https://file.hstatic.net/200000636033/file/facebook_1_0e31d70174824ea184c759534430deec.png" alt="Gearvn" />
                                </a>
                                <a href="https://www.tiktok.com/@gearvn.store" target="_blank" rel="nofollow">
                                    <img src="https://file.hstatic.net/200000722513/file/tiktok-logo_fe1e020f470a4d679064cec31bc676e4.png" alt="Gearvn" />
                                </a>
                                <a href="https://bit.ly/GearvnVideos" target="_blank" rel="nofollow">
                                    <img src="https://file.hstatic.net/200000636033/file/youtube_1_d8de1f41ca614424aca55aa0c2791684.png" alt="Gearvn" />
                                </a>
                                <a href="https://zalo.me/450955578960321912" target="_blank" rel="nofollow">
                                    <img src="https://file.hstatic.net/200000722513/file/icon_zalo__1__f5d6f273786c4db4a3157f494019ab1e.png" alt="Gearvn" />
                                </a>
                                <a href="https://www.facebook.com/groups/VietnamGamingConner" target="_blank" rel="nofollow">
                                    <img src="https://file.hstatic.net/200000636033/file/group_1_54d23abd89b74ead806840aa9458661d.png" alt="Gearvn" />
                                </a>
                            </div>

                            <div className="logo-footer">
                                <a rel="nofollow" target="_blank" href="http://online.gov.vn/Home/WebDetails/74686">
                                    <img src="//theme.hstatic.net/200000722513/1001090675/14/logo-bct.png?v=5650" alt="Bộ Công Thương" />
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </footer>
        </div>
    );
}

export default Footer;
