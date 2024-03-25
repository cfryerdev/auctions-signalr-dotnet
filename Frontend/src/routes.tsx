import { Routes, Route } from "react-router";

import Index from "./pages";
import List from "./pages/list";
import Participate from "./pages/participate";
import NotFound from "./pages/not-found";

const Routing = () => (
  <Routes>
    <Route path="/" element={<Index />} />
    <Route path="/list" element={<List />} />
    <Route path="/participate/:id" element={<Participate />} />
    <Route path="*" element={<NotFound />} />
  </Routes>
);

export default Routing;