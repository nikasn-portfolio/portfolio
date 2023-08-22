import LanguageSelector from "./LanguageSelector";
import facebookIcon from "../images/social-media/icons8-facebook.svg";
import instagramIcon from "../images/social-media/icons8-instagram.svg";
import twitterIcon from "../images/social-media/icons8-twitter.svg";
import youtubeIcon from "../images/social-media/icons8-youtube.svg";
import { AppContext } from "../routes/Root";
import { useContext } from "react";


const Footer = () => {
    const { isScreenSmall } = useContext(AppContext);
    return (
        /*<footer className="d-flex flex-wrap justify-content-between align-items-center py-3 border-top bg-light">
            <div className="col-md-4 d-flex align-items-center">
                <a href="/" className="mb-3 me-2 mb-md-0 text-muted text-decoration-none lh-1">
                    <svg className="bi" width="30" height="24">
                        <use xlinkHref="#bootstrap" />
                    </svg>
                </a>
                <span className="mb-md-0 text-muted">&copy; 2022 Company, Inc</span>
            </div>

            <ul className="nav col-md-4 justify-content-end list-unstyled d-flex flex-wrap-no-wrap">
                <li className="social-media-container">
                    <a className="text-muted rounded-button" href="#">
                        <img src={facebookIcon} alt="Facebook" className="social-media-btn"/>
                    </a>
                </li>
                <li className="social-media-container">
                    <a className="text-muted rounded-button" href="#">
                        <img src={instagramIcon} alt="Facebook" className="social-media-btn"/>
                    </a>
                </li>
                <li className="social-media-container">
                    <a className="text-muted rounded-button" href="#">
                        <img src={twitterIcon} alt="Facebook" className="social-media-btn"/>
                    </a>
                </li>
                <li className="social-media-container">
                    <a className="text-muted rounded-button" href="#">
                        <img src={youtubeIcon} alt="Facebook" className="social-media-btn"/>
                    </a>
                </li>
                { !isScreenSmall && (<LanguageSelector />)}
            </ul>
        </footer>*/
        <div className="footer-container">
            <footer className="py-3 my-4">
                <ul className="nav justify-content-center border-bottom pb-3 mb-3">
                    <li className="nav-item"><a href="#" className="nav-link px-2 text-muted">Home</a></li>
                    <li className="nav-item"><a href="#" className="nav-link px-2 text-muted">Features</a></li>
                    <li className="nav-item"><a href="#" className="nav-link px-2 text-muted">Pricing</a></li>
                    <li className="nav-item"><a href="#" className="nav-link px-2 text-muted">FAQs</a></li>
                    <li className="nav-item"><a href="#" className="nav-link px-2 text-muted">About</a></li>
                </ul>
                <p className="text-center text-muted">&copy; 2022 Company, Inc</p>
            </footer>
        </div>
    );
};

export default Footer;
