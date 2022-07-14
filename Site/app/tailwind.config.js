/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    './src/**/*.tsx'
  ],
  theme: {
    extend: {
      fontFamily: {
        sans: 'Roboto, sans-serif',
      },
      colors: {
        red: {
          100: '#F78181',
          200: '#F75A68',
          300: '#B30000',
          400: '#F55252',
          500: '#870000',
          700: '#5F0101',
        },
        orange: {
          500: '#FBA94C',
        },
        gray: {
          100: '#E1E1E6',
          200: '#C4C4CC',
          300: '#8D8D99',
          500: '#323238',
          600: '#29292E',
          700: '#121214',
          900: '#09090A'
        }
      },
    },
  },
  plugins: [],
}
