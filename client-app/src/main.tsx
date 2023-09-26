import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App.tsx";
import "./index.css";
import { RouterProvider, createBrowserRouter } from "react-router-dom";
import Home from "./components/pages/home/Home.tsx";
import FavEat from "./components/pages/faveat/FavEat.tsx";
import FavNature from "./components/pages/favnature/FavNature.tsx";
import NotFound from "./NotFound.tsx";
import Favmu from "./components/pages/muteuh/favmu.tsx";

const router = createBrowserRouter([
  {
    path: "/",
    element: <Home />,
  },
  {
    path: "/faveat",
    element: <FavEat />,
  },
  {
    path: "/favnature",
    element: <FavNature />,
  },
  {
    path: "/favmu",
    element: <Favmu />,
  },
  {
    path: "*",
    element: <NotFound />,
  },
]);

ReactDOM.createRoot(document.getElementById("root")!).render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>
);
