import { useParams } from "react-router-dom";
import { Header } from "../components/Utils/Header";
import { Movies } from "../components/Movies";
import { MovieScreen } from "../components/MovieScreen";
import { Sidebar } from "../components/Utils/Sidebar";


export function Event() {
  const { slug } = useParams<{slug : string}>();

  return (
    <div className="flex flex-col min-h-screen">
      <Header />
      <main className="flex flex-1">
        <Sidebar />
        { slug ? <MovieScreen movieSlug={slug}/> : <Movies/>} 
      </main>
    </div>
  )
}