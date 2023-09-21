import React, { useEffect, useState } from 'react'
import { useTranslation } from 'react-i18next'
import { useNavigate, useParams } from 'react-router-dom';
import { ItemCard } from '../components/ItemCard';
import { IProduct } from '../domain/IProduct';
import { HelperMethods } from '../helpers/Helpers';
import { ProductService } from '../services/ProductService';

interface IItemCard {
    id: string,
    title: string,
    description: string,
    price: number,
    image: string,
}

const ProductListPage = () => {


    const { t } = useTranslation();    
    const productService = new ProductService();
    const {id} = useParams();

    const [products, setProducts] = useState([] as IProduct[])

    const [card, setCard] = useState({
        id: '1',
        title: 'titleaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa',
        description: 'description',
        price: 100,
        image: 'image',
    } as IItemCard)

    /*const element = document.getElementById('main');
    if (element !== null) {
        HelperMethods.setAmountOfProductInRow(element);
    }

    window.onresize = function () {

        const element = document.getElementById('main');
        if (element !== null) {
            
            HelperMethods.setAmountOfProductInRow(element);
        }
    };*/

    useEffect(() => {
        console.log("useEffect running...");
    
        const getProducts = async () => {
            console.log("Fetching products...");
            const products = await productService.getAllProductsBelongsToCategory(id as string);
            if (products != undefined) {
                setProducts(products);
            }
        };
    
        getProducts();
    }, []);


    useEffect(() => {
        console.log("Component is rendered;");
    }, []);




    return (
        <>
        <div className='page-cards'>
            {products.map(product => 
                (<ItemCard 
                    product={product}
                />))}
        </div>
        </>
        


    )
}

export default ProductListPage;
