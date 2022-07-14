import { format } from "date-fns";
import ptBR from "date-fns/locale/pt-BR";
import { Link } from "react-router-dom";

interface MovieProps {
  title: string;
  image: string;
  slug: string;
  date: Date;
}

export function Movie(props : MovieProps) {
  const availableDateFormatted = format(props.date, "MMM '.' dd ',' uuuu", {
    locale: ptBR
  });
  return (
    <div className="w-[185px] flex flex-col items-center">
      <Link to={`/movies/${props.slug}`} className="border-[1.5px] border-red-300 rounded-[7px]">
        <img className="rounded-md w-[185px] h-[278px] hover:brightness-50 hover:" src={props.image}/>
      </Link>
      <div className="w-[185px] mt-2 flex flex-col justify-center items-center">
        <Link className="w-[185px] text-center" to={`/movies/${props.slug}`}>
          <a className="inline-block w-[185px] whitespace-nowrap text-ellipsis overflow-hidden font-light text-lg text-center  hover:text-red-300 transition-colors">{props.title}</a>
        </Link>  
        <span className="text-gray-400 text-xs">{availableDateFormatted[0].toUpperCase() + availableDateFormatted.substring(1)}</span>
      </div>
    </div>
  )
}