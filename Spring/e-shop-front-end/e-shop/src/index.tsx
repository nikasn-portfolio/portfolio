import React, { Suspense } from 'react';
import ReactDOM from 'react-dom/client';
import './style.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/js/bootstrap.bundle.min.js';
import App from './App';
import reportWebVitals from './reportWebVitals';
import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import ErrorPage from './routes/ErrorPage';
import Root from './routes/Root';
import path from 'path';
import ItemPage from './routes/ItemPage';

import ProductListPage from './routes/ProductListPage';
import HomePage from './routes/HomePage';
import { LoginForm } from './routes/LoginForm';
import { RegisterForm } from './routes/RegisterForm';
import ProfileForm from './routes/ProfileForm';
import CheckOut from './routes/CheckOut';

const router = createBrowserRouter([
  {
      path: "/",
      element: <Root />,
      errorElement: <ErrorPage />,
      children: [
        {
          path: "/home/",
          element: <HomePage></HomePage>, 
        },
        {
          path: "/login",
          element: <LoginForm></LoginForm>,
        },
        {
          path: "/register",
          element: <RegisterForm></RegisterForm>,
        },
        {
          path: "/products/:id",
          element: <ProductListPage></ProductListPage>, 
        },
        {
          path: "/product/:id",
          element: <ItemPage></ItemPage>, 
        },
        {
          path: "/profile",
          element: <ProfileForm></ProfileForm>
        },
        {
          path: "/checkout",
          element : <CheckOut></CheckOut>
        }
          
      ]
  },


]);

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);
root.render(
  <React.StrictMode>
    <Suspense fallback={<div>Loading....</div>}>
      <RouterProvider router={router} />
    </Suspense>
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
