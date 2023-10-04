import { useContext, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { AppContext } from "./Root";

export const OrderListPage = () => {
    const {jwt} = useContext(AppContext)
    const navigate = useNavigate();
    useEffect(() => {
        if(jwt == null || jwt == undefined){
            navigate("/home")
        }
    }, [jwt])

    const handleArrowClick = () =>{
        let arrow = document.getElementById("1");
        if(arrow?.classList.contains("down")){
            arrow?.classList.replace("down", "right")
            return;
        }
        arrow?.classList.replace("right", "down")
    }

    return(
        <div className="orders-container">
            <div className="order-list-line">
                <i className="dropdown-icon right" id="1" onClick={() => handleArrowClick()}></i>
                <div className="order-line-text">Date: asdasd</div>
                <div className="order-line-text">Order number: asdasd</div>
                <div className="order-line-text">Status: asdasd</div>
                <button className="order-line-button">
                    <span className="order-line-text">Pay</span>
                </button>
            </div>
            <div>
                
            </div>
        </div>
    );
}