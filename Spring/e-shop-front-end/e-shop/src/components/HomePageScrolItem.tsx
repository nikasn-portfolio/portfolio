import {ICategory } from "../domain/ICategory";

interface Props  {
    category : ICategory
    handleClick : (event: any) => void
}

export const HomePageScrolItem = (props : Props) => {
    return (
        <div className="category-scroll-container-item" id={props.category.id} onClick={(() => props.handleClick(props.category.id))} >
            <img src={props.category.image} alt="Image is not available"/>
            <p>{props.category.name}</p>
        </div>
    );
}