import React, { useContext, useEffect, useState } from "react";
import { useLocation, useParams } from "react-router-dom";
import ButtonTemplate from "../components/ButtonTemplate";
import { IItemCard } from "../domain/IItemCard";
import image_not_found from '../images/test_cat_img.png';
import image_not_found2 from '../images/image_not_found.png';
import { AppContext } from "./Root";
import ImageSelector from "../components/ImageSelector";
import { IProduct } from "../domain/IProduct";
import { IItem } from "../domain/IItem";
import { ICandy } from "../domain/ICandy";
import { CandyService } from "../services/CandyService";




const ItemPage = () => {
    const { cartList, setCartList, isScreenSmall } = useContext(AppContext)
    const [selectedSizeId, setSelectedSizeId] = useState("")
    const location = useLocation();
    const { cardProps } = location.state as { cardProps: IProduct };
    const [price, setPrice] = useState(Math.min(...cardProps.productSizes.filter(productSize => productSize.quantity > 0).map(productSize => productSize.price)));
    const [slideIndex, setSlideIndex] = useState(1);
    const [touchStartX, setTouchStartX] = useState(0);
    const [touchEndX, setTouchEndX] = useState(0);
    const [listOfCandies, setListOfCandies] = useState([] as ICandy[]);

    const handleSizeSelectorClick = (event: React.MouseEvent<HTMLButtonElement>) => {
        const selectedButton = document.querySelector(".itemSizeSelectorButtonSelected");

        if (selectedButton !== null) {
            selectedButton.classList.remove("itemSizeSelectorButtonSelected");
        }

        const clickedButton = event.currentTarget;
        clickedButton.classList.add("itemSizeSelectorButtonSelected");
        setPrice(cardProps.productSizes.filter(productSize => productSize.size.id === event.currentTarget.id)[0].emptyPrice);
        setSelectedSizeId(clickedButton.id);
    };
    // TODO: Add a request for card data by id

    const handleAddToCartClick = () => {
        if (selectedSizeId == "") {
            return;
        }
        //&& item.item.size == cardProps.size also check size
        const itemInCart = cartList.find(item => item.item.id === cardProps.id && item.item.sizeId === selectedSizeId);
        const copyOfProduct = JSON.parse(JSON.stringify(cardProps)) as IProduct;;
        console.log(selectedSizeId);
        console.log(copyOfProduct.productSizes)
        console.log(copyOfProduct.productSizes.filter(productSize => productSize.size.id === selectedSizeId));
        copyOfProduct.productSizes = copyOfProduct.productSizes.filter(productSize => productSize.size.id === selectedSizeId);
        const itemForCart = convertToIItem(copyOfProduct);


        if (itemInCart === undefined) {

            setCartList([...cartList, { item: itemForCart, quantity: 1, totalItemPrice: itemForCart.price }]);
            console.log(cartList);
        } else {
            setCartList(cartList.map(item => {
                if (item.item.id === cardProps.id && item.item.sizeId === selectedSizeId) {
                    return { ...item, quantity: item.quantity + 1, totalItemPrice: (item.item.price * (item.quantity + 1)) };
                }
                return item;
            }));
            // Convert the updated cartList to a string

        }
        localStorage.setItem("cartExpired", (Math.floor(Date.now() / 1000) + 5).toString());
    }





    const image = [image_not_found, image_not_found2, image_not_found, image_not_found2, image_not_found, image_not_found2]



    // Iterate through each thumbnail and add a click event listener
    const [mainImage, setMainImage] = useState(image[0]);
    const handleThumbnailClick = (thumbnailSrc: string) => {
        setMainImage(thumbnailSrc);
    };

    function convertToIItem(product: IProduct): IItem {
        return {
            id: product.id,
            name: product.name,
            sizeId: product.productSizes[0].size.id,
            sizeName: product.productSizes[0].size.name,
            image: product.images[0].image,
            price: product.productSizes[0].price
        };
    }

    const showSlides = () => {
        const slides = document.getElementsByClassName("mySlides") as HTMLCollectionOf<HTMLElement>;
        const dots = document.getElementsByClassName("dot") as HTMLCollectionOf<HTMLElement>;
        let n = slideIndex

        if (slideIndex > slides.length) {
            n = 1;
            setSlideIndex(1);
        }
        if (slideIndex < 1) {
            n = slides.length;
            setSlideIndex(slides.length);
        }
        console.log(slideIndex)
        if (slides.length > 0 && dots.length > 0) {
            for (let i = 0; i < slides.length; i++) {
                slides[i].style.display = "none";
            }
            for (let i = 0; i < dots.length; i++) {
                dots[i].classList.remove("active");
            }

            slides[n - 1].style.display = "block";
            dots[n - 1].classList.add("active");


        }


    };

    useEffect(() => {
        const getCandies = async () => {
           const candies =  await new CandyService().getAllCandies();
           if(candies !== undefined){
            setListOfCandies(candies);
           }
        }
        getCandies();
    }, [])

    useEffect(() => {
        showSlides(); // Call your function with the updated slideNumber
    }, []);

    useEffect(() => {
        showSlides();
    }, [slideIndex]);

    const handleArrowClick = (n: number) => {
        // Check if not currently touching
        setSlideIndex(slideIndex + n)

    };

    // Thumbnail image controls
    function currentSlide(n: number) {
        setSlideIndex(n);
    }

    const handleTouchStart = (e: React.TouchEvent) => {
        console.log(e.touches[0].clientX + " start")
        setTouchStartX(e.touches[0].clientX);

    };

    const handleTouchMove = (e: React.TouchEvent) => {
        console.log(e.touches[0].clientX + " end")
        setTouchEndX(e.touches[0].clientX);
    };

    const handleTouchEnd = () => {
        if (touchStartX != 0 && touchEndX != 0 && touchEndX - touchStartX > 50) {
            // Swipe right, go to previous slide
            setSlideIndex(slideIndex - 1);
            setTouchEndX(0)
            setTouchStartX(0)
        } else if (touchStartX != 0 && touchEndX != 0 && touchStartX - touchEndX > 50) {
            // Swipe left, go to next slide
            setSlideIndex(slideIndex + 1);
            setTouchEndX(0)
            setTouchStartX(0)
            
        }
    };

    return (
        <>

            <div className="itemPageContainer">
                <div className="slideshow-container" onTouchStart={handleTouchStart}
                    onTouchMove={handleTouchMove}
                    onTouchEnd={handleTouchEnd}>

                    <div className="mySlides animation-fade">
                        <div className="numbertext">1 / 3</div>
                        <img src={cardProps.images[0].image} className="itemViewImg" />
                    </div>

                    <div className="mySlides animation-fade">
                        <div className="numbertext">2 / 3</div>
                        <img src={cardProps.images[0].image} style={{ width: "100%" }} />
                    </div>

                    <div className="mySlides animation-fade">
                        <div className="numbertext">3 / 3</div>
                        <img src={image_not_found} style={{ width: "100%" }} />
                    </div>

                    <div className="prev" onClick={() => handleArrowClick(-1)}>❮</div>
                    <div className="next" onClick={() => handleArrowClick(1)}>❯</div>

                    <br />

                    <div className="dot-container">
                        <span className="dot" onClick={() => currentSlide(1)} ></span>
                        <span className="dot" onClick={() => currentSlide(2)} ></span>
                        <span className="dot" onClick={() => currentSlide(3)} ></span>
                    </div>

                </div>



                <div className="item-view-container-buttons">
                    <div className="itemViewTitleContainer">
                        <h5 className="itemViewTitle">{cardProps.name}</h5>
                    </div>
                    <div className="itemViewPrice">Price: {price}€</div>
                    <div className="sizeTitle">Size:</div>
                    <div className="itemSizeSelector">
                        {cardProps.productSizes.map(productSize => (productSize.quantity > 0 ?
                            (
                                <button className="itemSizeSelectorButton" title={productSize.size.name} onClick={event => handleSizeSelectorClick(event)} id={productSize.size.id}>
                                    {productSize.size.name}
                                </button>
                            ) :
                            (
                                <button className="itemSizeSelectorButton" title={productSize.size.name} id={productSize.size.id} disabled>
                                    {productSize.size.name}
                                </button>
                            )


                        ))}
                    </div>
                    <div className="addToCartButtonContainer">
                        <ButtonTemplate onClick={handleAddToCartClick} text="Add to cart" />
                    </div>
                    <div className="detailsTitle">Details:</div>
                    <div className="Accordion_Collapse__Vyg85" data-testid="accordion-collapse">
                        <div id="description" className="detailsContent" role="region" aria-labelledby="description-accordion" data-testid="accordion-content">
                            <div className="Typography_Typography__jHH_m ck-alias_Typography__u21R5 Typography_body__kV8NM Typography_bodyMedium__g_5G8" data-testid="typography-div">
                                Young in attitude. Amplified classics. Calvin Klein 1996 is defined by bold prints and colours, featuring the iconic monogram logo framed in a box.<br /> These trunks are crafted with 88% sustainably sourced fabric.<br /><br />• microfiber jersey<br />• low rise waist<br />• Calvin Klein signature elastic waistband Our model is 1.86m (6ft 1in) and is wearing a size M.<br /><br />  88% recycled polyester 12% elastane machine wash tumble dry low fits true to size country of origin: India Boxers, trunks and briefs can only be returned if unopened in original packaging, unworn and in the same condition as delivered, with all tags attached.
                            </div>
                        </div>
                    </div>
                </div>
            </div>


        </>
    );


}

export default ItemPage;