import { useParams } from "react-router-dom";
import { Frame } from "../components/Frame";
import { Header } from "../components/Utils/Header";

export function IFrame() {
  const { slug } = useParams<{slug : string}>();
  return (
    <div className="flex flex-col min-h-screen">
      <Header />
      <main className="flex flex-1 justify-center items-center">
        { slug ? <Frame MovieSlug={slug}/> : <div>Смотреть онлайн</div>} 
      </main>
    </div>
  )
}