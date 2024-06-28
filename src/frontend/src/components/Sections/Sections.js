import React from 'react'
import CategoryBar from '../CategoryBar/CategoryBar'
import Banner from '../Banner/Banner'
import CollectionSlider from '../CollectionSlider/CollectionSlider'

const Sections = () => {
  return (
    <div className='container'>
      <div className='row'>
        <CategoryBar />
        <CollectionSlider />
      </div>
    </div>
  )
}

export default Sections
