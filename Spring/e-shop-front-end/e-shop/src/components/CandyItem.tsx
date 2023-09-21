import { useState } from "react";
import { ICandy } from "../domain/ICandy";

interface Props{
    candy : ICandy,
    quantity : number,
    limit : number,
    updateQuantity : ((quantity : number) => void);
    candyList : ICandy[];
    setNewCandyList : ((candy : ICandy[]) => void);
}



export const CandyItem = (props : Props) => {
    const [candyQuantity, setCandyQuantity] = useState(0);

    const handleAdditionSelectedCandies = (candyQuantity : number) => {
        
        if(props.candyList.find(candy => candy.id === props.candy.id) !== undefined){
            const list = props.candyList.map(candy => {
                if(candy.id === props.candy.id){
                    return {...candy, quantity : candyQuantity};
                }else{
                    return candy;
                }
            }).filter(candy => candy.quantity !== 0);
            props.setNewCandyList(list);
        }else{
            const newCandy = props.candy;
            newCandy.quantity = candyQuantity;
            let updatedCandyList = [...props.candyList, newCandy];
            props.setNewCandyList(updatedCandyList);
        }
    }

    const handleCandiesCounterClickIncrease = (e : React.MouseEvent<HTMLButtonElement>) =>{
        if(props.quantity < props.limit){
            setCandyQuantity(candyQuantity + 100);
            props.updateQuantity(props.quantity + 100);
            handleAdditionSelectedCandies(candyQuantity + 100);
        }
    }
    const handleCandiesCounterClickDecrease = (e : React.MouseEvent<HTMLButtonElement>) =>{
        if(props.quantity <= props.limit && props.quantity >= 100 && candyQuantity >= 100){
            setCandyQuantity(candyQuantity - 100);
            props.updateQuantity(props.quantity - 100);
            handleAdditionSelectedCandies(candyQuantity - 100);
        }
    }
    return (
        <div className="candy-item">
            <img src={props.candy.image} className="candy-image" />
            <span className="candy-title">{props.candy.name}</span>
            <div className="cartCounterContainer">
                <button className="cartCounterButton" id={props.candy.id} onClick={e => handleCandiesCounterClickDecrease(e)} >-</button>
                <div className="counterValue">{candyQuantity}</div>
                <button className="cartCounterButton" onClick={e => handleCandiesCounterClickIncrease(e)}>+</button>
            </div>
            <span className="candy-stock">Total avaible: {props.candy.quantity}g</span>
        </div>
    );
}