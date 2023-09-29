import React from "react";
import Header from "../header/Header";

import "./layout.css";
import Footer from "../footer/Footer";

type Props = {
  children: React.ReactNode;
};

export default function Layout({ children }: Props) {
  return (
    <div className="mybg">
      <Header />
      <div className=" mx-auto max-w-[850px] p-4">{children}</div>
      <Footer />
    </div>
  );
}
