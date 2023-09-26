import React from "react";
import Header from "../header/Header";

type Props = {
  children: React.ReactNode;
};

export default function Layout({ children }: Props) {
  return (
    <div>
      <Header />
      {children}
    </div>
  );
}
