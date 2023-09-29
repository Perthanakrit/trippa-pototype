import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import { RouterProvider, createBrowserRouter } from "react-router-dom";
import Home from "./components/pages/home/Home.tsx";
import FavEat from "./components/pages/faveat/FavEat.tsx";
import FavNature from "./components/pages/favnature/FavNature.tsx";
import NotFound from "./NotFound.tsx";
import Favmu from "./components/pages/muteuh/Favmu.tsx";
import TripInfo from "./components/feature/tripdetail/TripInfo.tsx";
import Community from "./components/pages/community/Community.tsx";

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
    path: "/community",
    element: <Community />,
  },
  {
    path: "/trips",
    children: [
      {
        index: true,
        element: <NotFound />,
      },
      {
        path: ":tripId",
        element: <TripInfo />,
      },
    ],
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
