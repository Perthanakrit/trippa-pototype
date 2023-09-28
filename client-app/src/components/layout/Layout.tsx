import React from "react";
import Header from "../header/Header";

import "./layout.css";

type Props = {
  children: React.ReactNode;
};

export default function Layout({ children }: Props) {
  return (
    <>
      <Header />
      <div className="mybg">
        <div className=" mx-auto max-w-[850px] p-4">{children}</div>
      </div>
    </>
  );
}
