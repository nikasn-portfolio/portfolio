import { use } from "i18next";
import { useContext, useEffect, useState } from "react";
import { useTranslation } from 'react-i18next'
import { PasswordUpdateDTO } from "../dto/PasswordUpdateDTO";
import { PhoneNumberUpdateDTO } from "../dto/PhoneNumberUpdateDTO";
import lockIcon from '../images/loginForm/lock-closed-svgrepo-com.svg';
import phoneIcon from '../images/loginForm/phone-svgrepo-com.svg'
import { UserService } from "../services/UserService";
import { AppContext } from "./Root";
import { ProfileNotification } from "../domain/ProfileNotification";

const ProfileForm = () => {
    const { username, userPhoneNumber, userFullname, setUserPhoneNumber } = useContext(AppContext);
    const { t } = useTranslation();
    const [notification, setNotification] = useState({
        message: "",
        classname: ""
    } as ProfileNotification);
    const userService = new UserService();
    const [passwordData, setPasswordData] = useState({
        username: username,
        oldPassword: "",
        newPassword: ""
    } as PasswordUpdateDTO)

    const [phoneData, setPhoneData] = useState({
        username: username,
        newPhoneNumber: "",
    } as PhoneNumberUpdateDTO)

    const handlePasswordChange = (target: EventTarget & HTMLInputElement) => {
        setPasswordData({ ...passwordData, [target.name]: target.value })
    }
    const handlePasswordChangeSubmit = async () => {
        if (passwordData.newPassword.length < 5) {
            //TODO: show error message
            setNotification((prevNotification) => ({
                ...prevNotification,
                message: "New password length must be 5.",
                classname: "red"
            }))
            return;
        }
        const response = await userService.updateUserPassword(passwordData);

        if (response === undefined) {
            //TODO: show error message
            setNotification((prevNotification) => ({
                ...prevNotification,
                message: "Your old password is not the same, try again or contact us!",
                classname: "red"
            }))
            return;
        }

        setNotification((prevNotification) => ({
            ...prevNotification,
            message: "Your password was updated successfully",
            classname: "green"
        }))
    }
    const handlePhoneChange = (target: EventTarget & HTMLInputElement) => {
        setPhoneData({ ...phoneData, [target.name]: target.value })
    }
    const handlePhoneChangeSubmit = async () => {
        if (!phoneData.newPhoneNumber.includes("+")) {
            setNotification((prevNotification) => ({
                ...prevNotification,
                message: "Your new phone should contain country code!",
                classname: "red"
            }))
            return;
        }
        const response = await userService.updateUserPhone(phoneData);
        if (response === undefined) {
            //TODO: show error message
            setNotification((prevNotification) => ({
                ...prevNotification,
                message: "While updating your phone number occurred error, try again or contact us.",
                classname: "red"
            }))
            return;
        }
        if (setUserPhoneNumber) {
            setUserPhoneNumber(response?.newPhoneNumber as string);
        }
        //while successfull
        setNotification((prevNotification) => ({
            ...prevNotification,
            message: "Your phone number was updated successfully",
            classname: "green"
        }))


    }

    useEffect(() => {
        const timer = setTimeout(() => {
            setNotification((prevNotification) => ({
                ...prevNotification,
                message: "",
                classname: ""
            }))
        }, 4000);
        return () => clearTimeout(timer);
    }, [notification]);

    useEffect(() => {
        if (userPhoneNumber === phoneData.newPhoneNumber) {
            setPhoneData((prevPhoneData) => ({
                ...prevPhoneData,
                newPhoneNumber: "",
            }));
        }
    }, [userPhoneNumber]);
    useEffect(() => {
        console.log(phoneData)
        console.log(userPhoneNumber as string)
    }, [phoneData]);
    return (
        <>
            <div className="profileNotificationContainer">
                <div className={`profileNotificationContent ${notification.classname}`}>{notification.message}</div>
            </div>
            <div className="profileContainer">
                <div className="profileHeader">Account</div>
                <div className="profileContent">
                    <div className="passwordForm">
                        <div className="passwordFormHeaderContainer">
                            <span className="passwordFormHeaderContent">{t("Change password")}</span>
                        </div>
                        <div className="passwordFormBodyInput">
                            <label htmlFor="oldPassword">{t("Enter old password")}</label>
                            <div className="inputContainer">
                                <img src={lockIcon} alt="" className="loginFormIcon" />
                                <input type="text" id="oldPassword" name="oldPassword" style={{ width: "85%" }} onChange={e => handlePasswordChange(e.target)} className="inputBorder" />
                            </div>
                        </div>
                        <div className="passwordFormBodyInput">
                            <label htmlFor="oldPassword">{t("Enter new password")}</label>
                            <div className="inputContainer">
                                <img src={lockIcon} alt="" className="loginFormIcon" />
                                <input type="text" id="newPassword" name="newPassword" style={{ width: "85%" }} onChange={e => handlePasswordChange(e.target)} className="inputBorder" />
                            </div>
                        </div>
                        <div className="passwordFormButtonContainer">
                            <button className="passwordFormButton" onClick={e => handlePasswordChangeSubmit()}>Save</button>
                        </div>
                    </div>
                    <div className="phoneForm">
                        <div className="passwordFormHeaderContainer">
                            <span className="passwordFormHeaderContent">{t("Change phone number")}</span>
                        </div>
                        <div className="passwordFormBodyInput">
                            <label htmlFor="oldPhone">{t("Current phone number")}</label>
                            <div className="inputContainer">
                                <img src={phoneIcon} alt="" className="loginFormIcon" />
                                <input readOnly type="text" id="oldPhone" name="oldPhone" style={{ width: "85%" }} value={userPhoneNumber as string} className="inputBorder" />
                            </div>
                        </div>
                        <div className="passwordFormBodyInput">
                            <label htmlFor="newPhone">{t("Enter new phone number")}</label>
                            <div className="inputContainer">
                                <img src={phoneIcon} alt="" className="loginFormIcon" />
                                <input type="text" id="newPhoneNumber" name="newPhoneNumber" style={{ width: "85%" }} className="inputBorder" onChange={e => handlePhoneChange(e.target)} placeholder="+372*******" value={phoneData.newPhoneNumber} />
                            </div>
                        </div>
                        <div className="passwordFormButtonContainer">
                            <button className="passwordFormButton" onClick={handlePhoneChangeSubmit}>Save</button>
                        </div>
                    </div>
                </div>
            </div>

        </>

    );
};

export default ProfileForm;