
import Typewriter from 'react-ts-typewriter'

export function Logo() {
  return (
    <h1 className="text-3xl mt-3 mb-4 text-red-400 font-bold">
                  <Typewriter 
                          text={["813 ", "cinema", "never", "die", "813 Cinema Site"]}
                          speed={180}
                          />
                </h1>
  );
}
