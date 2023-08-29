import React from 'react';
import { useTranslation } from 'react-i18next';
import { Link, useNavigate } from 'react-router-dom';
import { IItemCard } from '../domain/IItemCard';
import { IProduct } from '../domain/IProduct';
import image_not_found from '../images/test_cat_img.png';
import ButtonTemplate from './ButtonTemplate';

interface Props{
    product : IProduct
}


export const ItemCard = (props: Props) => {
    const { t } = useTranslation();
    const navigate = useNavigate();

    const handleClick = () => {
        navigate('/product/' + props.product.id, { state: { cardProps: props.product } });
    };
    return (
        <div className="page-card">
            <img src={props.product.images[0].image}
                onError={(e) => {
                    const target = e.target as HTMLImageElement;
                    target.src = image_not_found;
                }}
                alt="Image not found"
                className="card-img"
            />
            <h4 className='itemCardTitle'>{props.product.name}</h4>
            <div className='itemCardSize'>
                <div className='itemCardSizeContent'>
                    <div className='itemCardSizeText'>
                        {props.product.productSizes.map((productSize, index) => productSize.quantity > 0 ? (
                            <>
                                <h5 key={index}>{productSize.size.name}</h5>
                                {index !== props.product.productSizes.length - 1 && (<h5 style={{paddingLeft : "4px", paddingRight : "4px"}}>/</h5>)}
                            </>
                        )
                        : (
                            <>
                                <h5 key={index} style={{color: "darkgrey"}}>{productSize.size.name}</h5>
                                {index !== props.product.productSizes.length - 1 && (<h5 style={{paddingLeft : "4px", paddingRight : "4px"}}>/</h5>)}
                            </>
                        ))  
                        }
                    </div>
                </div>
            </div>
            <h6>Price: {Math.min(...props.product.productSizes.filter(productSize => productSize.quantity > 0).map(productSize => productSize.emptyPrice))}€</h6>
            <ButtonTemplate onClick={handleClick} text={t("Buy")} />

        </div>
    );
}