import 'jquery';
import 'popper.js';
import 'bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css';
import 'font-awesome/css/font-awesome.min.css';

import './site.css';

import React from 'react';
import ReactDOM from 'react-dom/client';
import {
    createBrowserRouter,
    Router,
    RouterProvider,
} from "react-router-dom";

import Root from './routes/Root';
import ErrorPage from './routes/ErrorPage';
import Login from './routes/identity/Login';
import Register from './routes/identity/Register';
import Privacy from './routes/Privacy';
import SelectableCalendar from './routes/MyCalendar';
import { luxonLocalizer, momentLocalizer } from 'react-big-calendar';
import moment from 'moment';
import CreateRecord from './routes/CreateRecord';
import { DateTime } from 'luxon';
import Receipts from './routes/Receipts';
import { ReceiptView } from './routes/ReceiptView';

const router = createBrowserRouter([
    {
        path: "/",
        element: <Root />,
        errorElement: <ErrorPage />,
        children: [
            {
                path: "login/",
                element: <Login />,
            },
            {
                path: "register/",
                element: <Register />,
            },
            {
                path: "privacy/:id",
                element: <Privacy />,
            },
            {
                path: "calendar/:date?",
                element: <SelectableCalendar localizer={luxonLocalizer(DateTime)}/>
            },
            {
                path: "receipts",
                element : <Receipts />
            },
            {
                path: "receiptView/:id",
                element : <ReceiptView/>
            },
            {
                path: "calendar/createRecord/",
                element: <CreateRecord />,
            }
        ]
    },


]);

const root = ReactDOM.createRoot(
    document.getElementById('root') as HTMLElement
);
root.render(
    <React.StrictMode>
        <RouterProvider router={router} />
    </React.StrictMode>
);