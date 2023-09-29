import "react-responsive-carousel/lib/styles/carousel.min.css"; // requires a loader
import { Carousel } from "react-responsive-carousel";
import { Link } from "react-router-dom";

type Props = {
  trips: any;
};

// const breakPoints = [
//   { width: 1, itemsToShow: 1 },
//   { width: 550, itemsToShow: 2 },
//   { width: 768, itemsToShow: 3 },
//   { width: 1200, itemsToShow: 4 },
// ];

export default function TripCarousel({ trips }: Props) {
  return (
    <>
      {/* className=" flex justify-center items-center h-[250px] w-full my-0 mx-[0.9rem] text-[4rem]" */}
      <Carousel autoPlay infiniteLoop showThumbs={false} showStatus={false}>
        {trips.map((trip: any) => (
          <Link
            key={trip.id}
            className=" flex items-center w-full my-0 mx-auto"
            to={`trips/${trip.id}`}
          >
            <img
              className="w-full h-[400px] object-cover "
              src={trip.image}
              alt={trip.title}
            />
            <p className="legend">{trip.name}</p>
          </Link>
        ))}
      </Carousel>
    </>
  );
}
