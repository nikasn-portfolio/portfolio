import { useEffect } from "react";
import { ICartItem } from "../domain/ICartItem";

interface Props{
    cartIcon : string
    cartList : ICartItem[]
    setShowCart : ((data : boolean | null) => void) | null
}

export const CartIcon = (props : Props) => {
    useEffect(() => {
        console.log(props.cartList)
    },[props.cartList])
    const handleClick = () => {
        console.log("clicked")
        props.setShowCart!(true);
    }
    return (
        <div className='cartIconContainer hover-effect' onClick={handleClick}>
            <span className='circle'>
                <span className='cartIconContent'>{props.cartList.reduce((total, item) => total + item.quantity, 0)}</span>
            </span>
            <img src={props.cartIcon} alt="" className='language-image' />
        </div>
    );
}