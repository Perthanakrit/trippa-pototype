import { useState } from "react";
import "./App.css";
import Layout from "./components/layout/Layout";

function App() {
  const [count, setCount] = useState(0);

  return (
    <Layout>
      <div>
        <h1>Home</h1>
      </div>
    </Layout>
  );
}

export default App;
