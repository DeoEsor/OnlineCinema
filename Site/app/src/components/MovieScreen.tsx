import { gql, useQuery } from "@apollo/client";
import { SelectedMovie } from "../components/SelectedMovie";

const GET_MOVIE_BY_SLUG = gql`
  query GetMovie($slug: String) {
    movie(where: {slug: $slug}) {
      image
      movieUrl
      released
      title
      description
      id
      slug
    }
  }
`;

interface GetMovieBySlug {
  movie: {
    title: string;
    movieUrl: string;
    description: string;
    released: Date;
    image: string;
    id: string;
    slug: string;
  }
}

interface MovieProps {
  movieSlug: string;
}

export function MovieScreen(props : MovieProps) {
  const { data } = useQuery<GetMovieBySlug>(GET_MOVIE_BY_SLUG, {
    variables: {
      slug: props.movieSlug
    }
  })
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
    <div className="flex flex-1">
      <SelectedMovie 
        title={data.movie.title}
        description={data.movie.description}
        imageUrl={data.movie.image}
        movieUrl={data.movie.movieUrl}
        id={data.movie.id}
        slug={data.movie.slug}
        released={new Date(data.movie.released)}
      />
    </div>
      
  )
}