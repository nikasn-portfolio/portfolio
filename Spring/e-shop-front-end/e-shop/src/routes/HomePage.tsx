import image_not_found from '../images/test_cat_img.png';
import test_image from '../images/category_images/category_underwear_mens.png';
import testLogo from '../images/category_images/logo3.png'
import { useEffect, useState } from 'react';
import { CategoryService } from '../services/CategoryService';
import { ICategory } from '../domain/ICategory';
import { HomePageScrolItem } from '../components/HomePageScrolItem';
import { useNavigate } from 'react-router-dom';


const HomePage = () => {

    const handleScroll = (event: any) => {
        event.preventDefault(); // Prevent default scroll behavior to avoid double scrolling
        event.currentTarget.scrollLeft += event.deltaY
    }
    const categoryService = new CategoryService();
    const [categoryList, setCategoryList] = useState([] as ICategory[])
    const navigate = useNavigate();

    useEffect(() => {
        const container = document.getElementById('scrollMenu');
    
        const options = { passive: false };
        if(container == null) return; 
        container.addEventListener('wheel', handleScroll, options);
    
        return () => {
          container.removeEventListener('wheel', handleScroll);
        };
      }, []);

    useEffect(() => {
       const getCategories = async () => {
            const list = await categoryService.getAllCategories();
            if(list != undefined){
                setCategoryList(list);
            }
       } 
       getCategories();
    },[])
    
    const handleCategoryClick = (event : any) => {
        navigate(`/products/${event}`);
    }

    return (
        <>
            <div className="home-page-content-container">
                <div className="home-page-title">
                    <p>FEATURED COLLECTIONS</p>
                    <h1>Summer</h1>
                    <h1>Collections</h1>
                </div>
                <div className="category-scroll-container" id='scrollMenu'>
                    {categoryList.map(category => (<HomePageScrolItem category={category} handleClick={handleCategoryClick}/>))}
                </div>
            </div>

        </>

    );
}

export default HomePage;