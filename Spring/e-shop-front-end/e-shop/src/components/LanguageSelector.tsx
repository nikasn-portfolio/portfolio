import { ChangeEvent, useEffect, useState } from "react";
import i18n from "../i18n";
import { Link, useLocation, useParams } from "react-router-dom";
import EE from "../images/languages/EE.svg";
import GB from "../images/languages/GB.svg";
import RU from "../images/languages/RU.svg";

const LanguageSelector = () => {

    const [selectedLanguage, setSelectedLanguage] = useState(i18n.language); // i18n.language contains the language assigned to lng in i18n.js file.
    const location = useLocation();
    const searchParams = new URLSearchParams(location.search);
    const lang = searchParams.get('lang');
    const languages = {
        enSrc: GB,
        enAlt: 'English',
        estSrc: EE,
        estAlt: 'Estonian',
        ruSrc: RU,
        ruAlt: 'Russian',
        // Add more key-value pairs as needed
    };

    useEffect(() => {
        if(lang === null) {
            return;
        }
        i18n.changeLanguage(lang as string);   // i18n.changeLanguage() is used to change the language assigned to lng in i18n.js file.
        setSelectedLanguage(lang as string);
        localStorage.setItem("lang", lang as string); // localStorage is used to store the selected language in the browser.
    }, [lang]);

    return (
        <li className="nav-item dropdown">
            <a className="nav-link dropdown-toggle" href="#" id="navbarDropdownMenuLink" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                {(() => {
                    switch (selectedLanguage ?? lang) {
                        case 'est':
                            return <img src={languages.estSrc} alt={languages.estAlt} className="language-image" />;
                        case 'ru':
                            return <img src={languages.ruSrc} alt={languages.ruAlt} className="language-image" />;
                        default:
                            return <img src={languages.enSrc} alt={languages.enAlt} className="language-image" />
                    }
                })()}
            </a>
            <ul className="dropdown-menu" aria-labelledby="navbarDropdownMenuLink">
            <Link to={"./products/?lang=en"}><li><a className="dropdown-item"><img src={languages.enSrc} alt={languages.enAlt} className="language-image" /></a></li></Link>
                <Link to={"./products/?lang=est"}><li><a className="dropdown-item"><img src={languages.estSrc} alt={languages.estAlt} className="language-image" /></a></li></Link>
                <Link to={"./products/?lang=ru"}><li><a className="dropdown-item"><img src={languages.ruSrc} alt={languages.ruAlt} className="language-image" /></a></li></Link>
            </ul>
        </li>



    );
};

export default LanguageSelector;