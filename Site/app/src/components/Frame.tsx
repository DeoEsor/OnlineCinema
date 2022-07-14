import { gql, useQuery } from "@apollo/client";

const GET_MOVIE_BY_SLUG = gql`
  query GetMovie($slug: String) {
    movie(where: {slug: $slug}) {
      movieUrl
      title
      id
    }
  }
`;

interface GetMovieBySlug {
  movie: {
    title: string;
    movieUrl: string;
    id: string;
  }
}

interface FrameProps {
  MovieSlug: string;
}

export function Frame(props : FrameProps) {
  const { data } = useQuery<GetMovieBySlug>(GET_MOVIE_BY_SLUG, {
    variables: {
      slug: props.MovieSlug
    }
  })
  if(!data){
    return (
    <div className="flex-1">
      <h1>
        Carregando filme...
      </h1>
    </div>
    )
  }
  
  return (
    <div>
      <iframe src={data.movie.movieUrl} height="435" scrolling="no" width="600" allowFullScreen />
    </div>
  )
}