import React, { useRef } from "react";
import { Link } from "react-router-dom";
import { twMerge } from "tailwind-merge";
import { ClassValue, clsx } from "clsx";
const articles = [
  {
    title:
      "Building a fully customisable carousel slider with swipe gestures and navigation using Framer Motion",
    url: "https://medium.com/@jeyprox/building-a-fully-customisable-input-component-with-nextjs-reacthookfrom-tailwindcss-and-ts-58874a2e3450",
  },
  {
    title:
      "Building a customisable Input component with NextJS, ReactHookForm, TailwindCSS and TypeScript",
    url: "https://medium.com/@jeyprox/building-a-fully-customisable-input-component-with-nextjs-reacthookfrom-tailwindcss-and-ts-58874a2e3450",
  },
  {
    title: "Handling Forms in NextJS with busboy, ReactHookForm and TypeScript",
    url: "https://medium.com/@jeyprox/handling-forms-in-nextjs-with-busboy-reacthookform-and-ts-3f86c70545b3",
  },
];
// this function is used to combine (conditional) classNames and uses clsx and tailwind-merge
function cn(...inputs: ClassValue[]) {
  return twMerge(clsx(inputs));
}

export default function TripRecommedation() {
  return (
    <div className="wrapper">
      <div></div>
    </div>
  );
}
