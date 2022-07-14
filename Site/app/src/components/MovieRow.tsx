import { gql, useQuery } from "@apollo/client";
import { Link, Navigate, useNavigate, useParams } from "react-router-dom";
import { Movie } from "./Movie";

const GET_ALL_MOVIES = gql`
  query GetMovies {
    movies {
      id
      image
      movietype
      released
      slug
      title
    }
  }
`;

interface GetMoviesQueryResponse {
  movies: {
    id: string;
    slug: string;
    title: string;
    released: Date;
    image: string;
    movietype: [string];
  }[]
}

interface MovieRowProps {
  title: string;
}

export function MovieRow(props : MovieRowProps) {
  const navigate = useNavigate();
  const { data } = useQuery<GetMoviesQueryResponse>(GET_ALL_MOVIES);
  const { type } = useParams<{type : string}>();
  const { name } = useParams<{name : string}>();
  
  if(!data){
    return (
    <div className="flex-1">
      <h1>
        Carregando filmes...
      </h1>
    </div>
    )
  }
  return (
    <section className="mb-6">
      <div className="flex items-center gap-2 mb-4">
        <div className="w-1 h-8 bg-red-300"></div>
        <span className="title">{props.title}</span>
      </div>
      <div className="w-full ml-4 gap-5 flex justify-end flex-wrap flex-row-reverse float-left">
        {data?.movies.map(movies =>{
          if(type){
            if(type != "All"){
              if(movies.movietype.includes(type)){
                return(
                  <Movie title={movies.title} date={new Date(movies.released)} image={movies.image} slug={movies.slug}/>
                )
              }
            }else{
              navigate("/movies");
            }
          }else{
            if(name){
              if(movies.title.toLowerCase().includes(name.toLowerCase())){
                return(
                  <Movie title={movies.title} date={new Date(movies.released)} image={movies.image} slug={movies.slug}/>
                )
              }
            }else{
              return(
                <Movie title={movies.title} date={new Date(movies.released)} image={movies.image} slug={movies.slug}/>
              )
            }
          }
        })}
      </div>
    </section>
  )
}