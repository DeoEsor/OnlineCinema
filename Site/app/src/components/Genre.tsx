import { X } from "phosphor-react";

interface GenreProps {
  genre: string;
}

export function Genre(props : GenreProps) {
  return (
    <div className="h-1 p-4 flex justify-around items-center rounded-full bg-gray-900">
      <a href="#"><X size={13} className="text-red-400" /></a>
      <span className="pl-1 text-xs">{props.genre}</span>
    </div>
  )
}