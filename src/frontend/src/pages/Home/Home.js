import React from 'react'
import CategoryBar from '../../components/CategoryBar/CategoryBar'
import CollectionSlider from '../../components/CollectionSlider/CollectionSlider'
import SaleBanner from '../../components/SaleBanner/SaleBanner'
import ProductList from '../../components/ProductCard/ProductCard'
import AdvertisingBanner from '../../components/AdvertisingBanner/AdvertisingBanner'

const Home = () => {
  return (
    <div className='container'>
      <div className='row'>
        <CategoryBar />
        <CollectionSlider />
        <SaleBanner />
        <ProductList />
        <AdvertisingBanner />
      </div>
    </div>
  )
}

export default Home
