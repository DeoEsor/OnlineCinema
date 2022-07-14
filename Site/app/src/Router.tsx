import { Route, Routes } from "react-router-dom";
import { Event } from "./pages/Event";
import { Home } from "./pages/Home";
import { IFrame } from "./pages/IFrame";

export function Router() {
  return (
    <Routes>
      <Route path="/" element={<Home />}/>
      <Route path="/movies" element={<Event />}/>
      <Route path="/movies/:slug" element={<Event />}/>
      <Route path="/movies/watch/:slug" element={<IFrame />}/>
      <Route path="/movies/search/genre/:type" element={<Event/>}/>
      <Route path="/movies/search=:name" element={<Event/>}/>
    </Routes>
  )
}