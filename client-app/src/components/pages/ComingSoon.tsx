import { useNavigate } from "react-router-dom";

export default function ComingSoon() {
  const navigate = useNavigate();
  return (
    <div className="w-full mx-auto my-auto text-center h-[80vh]">
      <h1 className="text-4xl font-bold text-slate-50">Coming Soon</h1>
      <a
        onClick={(e: React.FormEvent) => {
          e.preventDefault();
          navigate(-1);
        }}
        className=" text-slate-300 "
      >
        Back to home page
      </a>
    </div>
  );
}
