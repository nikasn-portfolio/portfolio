import { createContext, useEffect, useState } from "react";
import { Outlet, useNavigate } from "react-router-dom";
import { CrossLine } from "../components/CrossLine";
import Footer from "../components/Footer";
import Navbar from "../components/Navbar";
import { ICartItem } from "../domain/ICartItem";
import { JwtResponse } from "../domain/JwtResponse";
import jwtToken from "jsonwebtoken";
import { MontonioPaymentService } from "../services/MontonioService";


export const AppContext = createContext<{
    isScreenSmall: boolean,
    setIsScreenSmall: ((data: boolean) => void),
    cartList: ICartItem[],
    setCartList: ((data: ICartItem[]) => void),
    jwt: JwtResponse | null,
    setJwt: ((data: JwtResponse | null) => void) | null,
    username: string | null,
    setUsername: ((data: string | null) => void) | null
    userPhoneNumber: string | null,
    setUserPhoneNumber: ((data: string | null) => void) | null,
    userFullname: string | null,
    setUserFullname: ((data: string | null) => void) | null,
    showCart: boolean | null,
    setShowCart: ((data: boolean | null) => void) | null

}>({
    isScreenSmall: false, setIsScreenSmall: () => { }, cartList: [], setCartList: () => { },
    jwt: null, setJwt: () => { }, username: null, setUsername: null, userPhoneNumber: null,
    setUserPhoneNumber: null, userFullname: null, setUserFullname: null, showCart: null, setShowCart: () => { }
});

const Root = () => {

    const [isScreenSmall, setIsScreenSmall] = useState(false);

    const [cartList, setCartList] = useState(() => {
        const cartListFromLocalStorage = localStorage.getItem("cartList");
        return cartListFromLocalStorage ? JSON.parse(cartListFromLocalStorage) as ICartItem[] : [] as ICartItem[];
    });
    const [jwt, setJwt] = useState(null as JwtResponse | null);

    const [username, setUsername] = useState(null as string | null);

    const [userPhoneNumber, setUserPhoneNumber] = useState(null as string | null);

    const [userFullname, setUserFullname] = useState(null as string | null);

    const [showCart, setShowCart] = useState(false as boolean | null);

    const navigate = useNavigate();


    useEffect(() => {

        if (Number(localStorage.getItem("cartExpired")) < Math.floor(Date.now() / 1000)) {
            localStorage.setItem("cartList", JSON.stringify([]));
        }
        if (localStorage.getItem("cartList") !== null) {
            setCartList(JSON.parse(localStorage.getItem("cartList") as string) as ICartItem[]);
        }
        console.log(localStorage.getItem("lang"));
        navigate(`/home/?lang=${localStorage.getItem("lang") === null ? "en" : localStorage.getItem("lang")}`);
    }, []);

    useEffect(() => {
        console.log(showCart);
    }, [showCart])

    const handleCrossIconClick = () => {
        console.log("close")
        setShowCart(false);
    }

    const handlePlus = (currentItem: ICartItem) => {
        const updatedList = cartList.map(item => {
            if (item === currentItem) {
                return { ...item, quantity: item.quantity + 1, totalItemPrice: item.totalItemPrice + item.item.price }
            }
            return item;
        })
        setCartList(updatedList);
    }

    const handleMinus = (currentItem: ICartItem) => {

        let updatedList = cartList.map(item => {
            if (item === currentItem) {
                return { ...item, quantity: item.quantity - 1, totalItemPrice: item.totalItemPrice - item.item.price };
            }
            return item;
        })
        if (currentItem.quantity === 1) {
            console.log("item is 0")
            updatedList = updatedList.filter(item => item.quantity !== 0);
        }
        setCartList(updatedList);
    }

    useEffect(() => {
        localStorage.setItem('cartList', JSON.stringify(cartList));
    }, [cartList])

    const handleCartItemCrossIconClick = (currentItem: ICartItem) => {
        const updatedList = cartList.filter(item => item !== currentItem);
        setCartList(updatedList);
    }

    const handleCartButtonClick = () => {
        navigate("/checkout")
        setShowCart(false);
        
    }

    return (
        <>
            <AppContext.Provider value={{ isScreenSmall, setIsScreenSmall, cartList, setCartList, jwt, setJwt, username, setUsername, userPhoneNumber, setUserPhoneNumber, userFullname, setUserFullname, showCart, setShowCart }}>
                {showCart ?
                    (cartList.length > 0 ? (<div className="cart">
                        <CrossLine handleCrossIconClick={handleCrossIconClick} />
                        <div className="cartContainer">
                            {cartList.map(item => (
                                <>
                                    <div className="itemInCart">
                                        <CrossLine handleCrossIconClick={() => handleCartItemCrossIconClick(item)} />
                                        <div className="cartImageAndTittleContainer">
                                            <img src={item.item.image} alt="" className="cart-img" />
                                            <div className="cartTitleContent">
                                                {item.item.name}
                                            </div>
                                        </div>
                                        <div className="cartSizeContainer">
                                            <div className="cartSizeContent">
                                                Size : {item.item.sizeName}
                                            </div>
                                        </div>
                                        <div className="cartPriceContainer">
                                            <div className="cartPriceContent">
                                                Price : {item.item.price}
                                            </div>
                                        </div>
                                        <div className="cartCounterContainer">
                                            <button className="cartCounterButton" onClick={() => handleMinus(item)}>-</button>
                                            <div className="counterValue">{item.quantity}</div>
                                            <button className="cartCounterButton" onClick={() => handlePlus(item)}>+</button>
                                        </div>
                                    </div>
                                </>

                            ))}

                        </div>
                        <button className="cartButton"  onClick={handleCartButtonClick} ><span className="cartButtonContent">Total: {cartList.reduce((total, item) => total + item.totalItemPrice, 0)}€</span></button>
                    </div>
                    ) :
                        (
                            <div className="cart">
                                <CrossLine handleCrossIconClick={handleCrossIconClick} />
                                <div className="cartContainer">
                                    <div className="emptyCart">Cart is empty</div>
                                </div>
                            </div>

                        )
                    ) : null
                }
                <Navbar />

                <div className="wrapper">
                    <div className="container">
                        <main role="main" id="main">
                            <Outlet />
                        </main>
                    </div>
                </div>

                <Footer />
            </AppContext.Provider>
        </>

    );
}

export default Root;