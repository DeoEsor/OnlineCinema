import { Logo } from "./Logo";

export function Header() {
  return (
    <header className="fixed w-full py-2 flex justify-center items-center bg-gray-700 border-b border-gray-600">
      <a href="/movies">
        <Logo />
      </a>
    </header>
  )
}