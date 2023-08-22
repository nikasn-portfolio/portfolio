
import { useEffect } from "react";

const useOutsideClick = (ref : any, callback : any) => {
    let listOfAllowedClassNames = ["dropdownprofile", "dropbtnprofile", "profileIcon", "profileUsername","dropdown-content-profile"]
  const handleClick = (e : any) => {
    if (ref.current && !ref.current.contains(e.target) && !listOfAllowedClassNames.some((className) =>
    e.target.classList.contains(className))) {
      callback();
    }
  };

  useEffect(() => {
    document.addEventListener("click", handleClick);

    return () => {
      document.removeEventListener("click", handleClick);
    };
  });
};

export default useOutsideClick;