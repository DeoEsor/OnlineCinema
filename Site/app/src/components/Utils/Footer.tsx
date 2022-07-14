import { Logo } from "./Logo";

export function Footer() {
  return (
    <div className="flex justify-between items-center p-4 text-gray-300 border-t border-gray-300">
      <div className="flex items-center gap-7">
        <Logo />
        <span className="text-sm">813 Cinema - проект по практике</span>
      </div>
     <div>
     <span>Team Lead </span>
      <a href="https://github.com/DeoEsor"><span className="text-red-400">@DeoEsor</span></a>
      <br/>
      <span>Frontenders </span>
      <a href="https://github.com/vecherochek"><span className="text-red-400">@vecherochek</span></a>
      <a href="https://github.com/JoFNash"><span className="text-gray-100"> @JoFNash</span></a>
      <br/>
      <span>Backenders </span>
      <a href="https://github.com/DeoEsor"><span className="text-gray-100">@DeoEsor</span></a>
      <a href="https://github.com/sornick01"><span className="text-red-400"> @sornick01</span></a>
     </div>
    </div>
  )
}