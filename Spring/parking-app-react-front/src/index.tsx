import React from 'react';
import './react-app-env.d';
import ReactDOM from 'react-dom/client';
import ErrorPage from './ErrorPage';
import './site.css';
import {
  createBrowserRouter,
  Router,
  RouterProvider,
} from "react-router-dom";

import { LocationChoice } from './LocationChoice';
import Root from './Root';
import 'jquery';
import 'popper.js';
import 'bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css'; 
import 'font-awesome/css/font-awesome.min.css';
import { ParkingValidation } from './ParkingValidation';
import { TimeValidation } from './TimeValidation';


const router = createBrowserRouter([
  {
    path: "/",
    element: <Root />,
    errorElement: <ErrorPage />,
    children: [
      {
        path: "location/",
        element: <LocationChoice />,
      },
      {
        path: "parking/",
        element: <ParkingValidation />,
      },
      {
        path: "time/",
        element: <TimeValidation />,
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

