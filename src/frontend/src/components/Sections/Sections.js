import React from 'react'
import CategoryBar from '../CategoryBar/CategoryBar'
import CollectionSlider from '../CollectionSlider/CollectionSlider'
import SaleBanner from '../SaleBanner/SaleBanner'
import AdvertisingBanner from '../AdvertisingBanner/AdvertisingBanner'
import TagNav from '../TagNav/TagNav'
import ProductCard from '../ProductCard/ProductCard'
import ProductList from '../ProductCard/ProductCard'

const Sections = () => {
  return (
    <div className='container'>
      <div className='row'>
        <ProductList />
        {/* <ProductCard /> */}
        {/* <TagNav /> */}
        {/* <CategoryBar /> */}
        {/* <CollectionSlider /> */}
        {/* <SaleBanner /> */}
        {/* <AdvertisingBanner /> */}
      </div>
    </div>
  )
}

export default Sections
