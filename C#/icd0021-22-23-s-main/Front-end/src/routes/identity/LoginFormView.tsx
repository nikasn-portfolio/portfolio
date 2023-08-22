import React from "react";
import { redirect } from "react-router-dom";
import { ILoginData } from "../../dto/ILoginData";

interface IProps {
    values: ILoginData;

    validationErrors: string[];

    handleChange: (target: EventTarget & HTMLInputElement) => void;

    //here some custom MouseEvent type is used instead of the default one
    onSubmit: (event: any) => void;

    //jwt: any;

}

export const LoginFormView = (props: IProps) => {
    return (
        <div b-wrkb45jn1q className="container">
            <main b-wrkb45jn1q role="main" className="pb-3">


                <h1>Log in</h1>
                <div className="row">
                    <div className="col-md-4">
                        <section>
                            <form id="account" method="post">
                                <h2>Use a local account to log in.</h2>
                                <hr />

                                <ul style={{ 'display': props.validationErrors.length == 0 ? 'none' : '' }}>
                                    {props.validationErrors.map((error) => { return <li>{error}</li> })}
                                </ul>

                                <div className="form-floating mb-3">
                                    <input className="form-control" autoComplete="username" onChange={(e) => props.handleChange(e.target)}
                                        aria-required="true" placeholder="name@example.com" type="email"
                                        id="Input_Email" name="email" value={props.values.email} />
                                    <label className="form-label" id="Input_Email">Email</label>
                                </div>
                                <div className="form-floating mb-3">
                                    <input className="form-control" autoComplete="current-password" aria-required="true" onChange={(e) => props.handleChange(e.target)}
                                        placeholder="password" type="password" id="Input_Password" name="password" value={props.values.password} />
                                    <label className="form-label" id="Input_Password">Password</label>
                                </div>
                                <div className="checkbox mb-3">
                                    <label className="form-label" id="Input_RememberMe">
                                        <input className="form-check-input" type="checkbox" data-val="true" data-val-required="The Remember me? field is required." id="Input_RememberMe" name="Input.RememberMe" value="true" />
                                        Remember me?
                                    </label>
                                </div>
                                <div>
                                    <button id="login-submit" type="submit" className="w-100 btn btn-lg btn-primary" onClick={(e) => props.onSubmit(e)}  >Login</button>
                                </div>
                                <div>
                                    <p>
                                        <a id="idgot-password" href="/Identity/Account/idgotPassword">idgot your password?</a>
                                    </p>
                                    <p>
                                        <a href="/Identity/Account/Register?returnUrl=%2F">Register as a new user</a>
                                    </p>
                                    <p>
                                        <a id="resend-confirmation" href="/Identity/Account/ResendEmailConfirmation">Resend email confirmation</a>
                                    </p>
                                </div>
                                <input name="__RequestVerificationToken" type="hidden" value="CfDJ8EHBI2yIA7RGjJPWM4qjsms9oZIFBpQni1etIurea8jiBuhkeQjpCO4KfeYVD2KtUXXqQTnPXc4Huvwds7xxts1Ri0QL3nJyfogm4ezy6qBlk0wXn_sl7N1HdSuDOXGmbSg2faaAmyfsyGa3lVY9DeQ" /><input name="Input.RememberMe" type="hidden" value="false" />
                            </form>
                        </section>
                    </div>
                    <div className="col-md-6 col-md-offset-2">
                        <section>
                            <h3>Use another service to log in.</h3>
                            <hr />
                            <div>
                                <p>
                                    There are no external authentication services configured. See this <a href="https://go.microsoft.com/fwlink/?LinkID=532715">article
                                        about setting up this ASP.NET application to support logging in via external services</a>.
                                </p>
                            </div>
                        </section>
                    </div>
                </div>


            </main>
        </div>
    );
}