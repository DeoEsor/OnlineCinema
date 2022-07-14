import background from "../assets/Background.png";
import { Footer } from "./Utils/Footer";
import { MovieRow } from "./MovieRow";
import { format } from "date-fns";
import ptBR from "date-fns/locale/pt-BR";
import { Link } from "react-router-dom";

interface SelectedMovieProps {
  title: string;
  imageUrl: string;
  description: string;
  movieUrl: string;
  released: Date;
  slug: string;
  id: string;
}

export function SelectedMovie(props : SelectedMovieProps) {
  const DateFormatted = format(props.released, "MMM '.' dd ',' uuuu", {
    locale: ptBR
  });
  return (
    <div className="ml-[348px] mt-[68px] bg-no-repeat bg-cover" style={{backgroundImage: `url(${background})`, width: 'calc(100% - 348px)'}}>
      <div className="flex flex-col justify-between h-full">
        <div className="p-6 h-full">
          {/* SelectedMovie */}
          <div className="w-full bg-gray-700 p-6 rounded">
            <div className="flex flex-col">
              <div className="flex items-center gap-2 mb-4">
                <div className="w-1 h-8 bg-red-300"></div>
                <span className="title">{props.title}</span>
              </div>
              <div className="flex gap-6 h-[300px]">
                <div className="">
                  <img className="rounded-md w-[185px] h-[278px] shadow-md" src={props.imageUrl} />
                </div>
                <div className="grid grid-rows-2 grid-cols-1 w-[80%]">
                  <div className="flex flex-col">
                    <span className="title">Assista <span className="text-red-300 font-bold text-2xl">{props.title}</span> Online</span>
                  <span className="text-xs text-gray-300">{DateFormatted}</span>
                    <span className="text-sm text-gray-300 mt-4">{props.description}</span>
                  </div>
                  <div className="flex flex-col gap-4 mt-2">
                    <Link to={`/movies/watch/${props.slug}`}>
                      <button className="text-center p-4 w-[25%] bg-red-300 text-white rounded font-bold text-sm">ASSISTIR DUBLADO</button>
                    </Link>
                    <a target="_blank" href="#" className="text-center p-4 w-[25%] border border-red-300 text-red-300 rounded font-bold text-sm">ASSISTIR LEGENDADO</a>
                  </div>
                </div>
              </div>
            </div>
          </div>
          {/* MovieRow */}
          <div className="mt-[150px]">
            <MovieRow  title="Últimos filmes"/>
          </div>
        </div>
        <div className="pr-5 pl-5"> 
          <Footer />
        </div>
      </div>
    </div>
  )
}