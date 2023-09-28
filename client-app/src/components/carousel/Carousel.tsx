//import { useState } from "react";

//import "./Carousel.css";

type Props = {
  data: any;
};

export const Carousel = ({ data }: Props) => {
  //const [slide, setSlide] = useState(0);

  // const nextSlide = () => {
  //   setSlide(slide === data.length - 1 ? 0 : slide + 1);
  // };

  // const prevSlide = () => {
  //   setSlide(slide === 0 ? data.length - 1 : slide - 1);
  // };

  console.log(data);

  return (
    <></>
    // <div className="carousel">
    //   <BsArrowLeftCircleFill onClick={prevSlide} className="arrow arrow-left" />
    //   {data.map((item: any, idx: any) => {
    //     return (
    //       <img
    //         src={item.src}
    //         alt={item.alt}
    //         key={idx}
    //         className={slide === idx ? "slide" : "slide slide-hidden"}
    //       />
    //     );
    //   })}
    //   <BsArrowRightCircleFill
    //     onClick={nextSlide}
    //     className="arrow arrow-right"
    //   />
    //   <span className="indicators">
    //     {data.map((_, idx) => {
    //       return (
    //         <button
    //           key={idx}
    //           className={
    //             slide === idx ? "indicator" : "indicator indicator-inactive"
    //           }
    //           onClick={() => setSlide(idx)}
    //         ></button>
    //       );
    //     })}
    //   </span>
    // </div>
  );
};
