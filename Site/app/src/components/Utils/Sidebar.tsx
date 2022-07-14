import { Knife, MagnifyingGlass } from "phosphor-react";
import { Genre } from "../Genre";
import { GenreButton } from "../GenreButton";

import { Scrollbars } from 'react-custom-scrollbars-2';
import { Link, useNavigate } from "react-router-dom";
import { useState } from "react";


export function Sidebar() {
  const navigate = useNavigate();
  const [search, setSearch] = useState<string>();

  function GoToMovies() {
    navigate('/movies')
  }
  
  return (
    <aside className="fixed mt-[68px] overflow-y-auto w-[348px] h-full bg-gray-700 pr-1 pl-6 pt-6 pb-6 border-r border-gray-600">
      <Scrollbars>
        <div className="flex flex-col pr-6">
          <span className="title">
            Pesquisa
          </span>
          <div className="flex justify-between items-center bg-gray-900 p-3 rounded mt-4 ">
            <input value={search} onChange={(event) => setSearch(event.target.value)} className="w-[90%] bg-transparent outline-none" type="text" placeholder="Procurar..."/>
            {search ? 
              <Link onClick={() => setSearch("")} to={`/movies/search=${search}`}>
                <a><MagnifyingGlass /></a>
              </Link> 
              : 
                <a onClick={GoToMovies} className="cursor-pointer"><MagnifyingGlass /></a>
            }
          </div>
          {/* <div className="mt-4 flex gap-1 flex-wrap pb-6 mb-6 border-b border-gray-500 block">
            <Genre genre="Terror" />
            <Genre genre="Suspense" />
            <Genre genre="Ação" />
          </div> */}
          <div>
            <span className="pt-6 mt-6 border-t border-gray-500 block title">
              Gênero
            </span> 
            <div className="mt-4 mb-16 gap-4 flex flex-col pb-6  border-b border-gray-500 block">
              <GenreButton genre="All" icon={<Knife size={21} weight="bold"/>}/>
              <GenreButton genre="Horror" icon={<Knife size={21} weight="bold"/>}/>
              <GenreButton genre="Suspense" icon={<Knife size={21} weight="bold" />}/>
              <GenreButton genre="Comedy" icon={<Knife size={21} weight="bold" />}/>
              <GenreButton genre="Action" icon={<Knife size={21} weight="bold" />}/>
              <GenreButton genre="Family" icon={<Knife size={21} weight="bold" />}/>
              <GenreButton genre="Animation" icon={<Knife size={21} weight="bold" />}/>
            </div>
          </div>
        </div>
      </Scrollbars>
    </aside>
    
  )
}