import { Link } from "react-router-dom";
import Layout from "./components/layout/Layout";

export default function NotFound() {
  return (
    <Layout>
      <div className="my-[21%]">
        <div className="flex justify-center items-center gap-4 text-4xl font-semibold ">
          <h1 className="text-[#ffdac1]">404</h1>
          <p className="text-[#fdf185]">|</p>
          <h1 className=" text-zinc-50">Not Found</h1>
        </div>
        <Link to="/" className=" block text-center mt-4 underline">
          Back to home
        </Link>
      </div>
    </Layout>
  );
}
