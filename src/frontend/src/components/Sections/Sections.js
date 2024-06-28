import React from 'react'
import CategoryBar from '../CategoryBar/CategoryBar'
import CollectionSlider from '../CollectionSlider/CollectionSlider'
import SaleBanner from '../SaleBanner/SaleBanner'
import AdvertisingBanner from '../AdvertisingBanner/AdvertisingBanner'

const Sections = () => {
  return (
    <div className='container'>
      <div className='row'>
        <CategoryBar />
        <CollectionSlider />
        <SaleBanner />
        <AdvertisingBanner />
      </div>
    </div>
  )
}

export default Sections
