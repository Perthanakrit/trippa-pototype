import "react-responsive-carousel/lib/styles/carousel.min.css"; // requires a loader
import { Carousel } from "react-responsive-carousel";

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
    <div>
      {/* className=" flex justify-center items-center h-[250px] w-full my-0 mx-[0.9rem] text-[4rem]" */}
      <Carousel showArrows={true} autoPlay infiniteLoop showThumbs={false}>
        {trips.map((trip: any) => (
          <div key={trip.id} className=" flex items-center w-full my-0 mx-auto">
            <img
              className="w-full h-[400px] object-cover "
              src={trip.image}
              alt={trip.title}
            />
          </div>
        ))}
      </Carousel>
    </div>
  );
}
