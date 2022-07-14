import { Footer } from "../components/Utils/Footer";
import { Logo } from "../components/Utils/Logo";
import background from "../assets/Background.png";
import { useNavigate } from 'react-router-dom';
import { gql, useMutation, useQuery } from "@apollo/client";
import { FormEvent, useState } from "react";

const CREATE_MEMBER_MUTATION = gql`
  mutation createNewMember($name: String!, $email: String!) {
    createMember(data: {name: $name, email: $email}) {
      id
    }
  }
`;

const GET_MEMBERS = gql`
  query GetMembers($email: String, $name: String) {
    members(where: {email: $email, name: $name}) {
      email
      name
    }
  }

`;

interface GetMembersQueryResponse {
  members: {
    id: string;
    email: string;
    name: string;
  }[]
}

export function Home() {
  const navigate = useNavigate();

  const [name, setName] = useState("");
  const [email, setEmail] = useState("");

  const [createMember, {loading}] = useMutation(CREATE_MEMBER_MUTATION);
  const { data } = useQuery<GetMembersQueryResponse>(GET_MEMBERS, {
    variables: {
      name: name,
      email: email
    }
  });

  async function handleSubmit(event: FormEvent) {
    if(!data){
      event.preventDefault();
      try{
        await createMember({
          variables: {
            name,
            email,
          },
        });
        navigate("/movies");
      } catch (error) {
        alert("Блин, опять регистрация сломалась :(\nПриди позже, ок?");
      }
    }else{
      navigate("/movies");
      console.log("Такой аккаунт уже зареган могу пароль дать, нада?");
      
    }
  }

  return (
    <div className="flex flex-col min-h-screen">
        <main className="flex flex-1 justify-around bg-no-repeat bg-cover" style={{backgroundImage: `url(${background})`}}>
          <div className="mt-20 w-full flex justify-around items-start">
            <div className="w-full flex justify-around items-center">
              {/* Text */}
              <div className="w-[45%]">
                <Logo />
                <h1 className="text-3xl mt-3 mb-4">
                  Нам сказали, что декстопа мало и мы создали <del>(позаимствовали)</del> это.
                </h1>
              </div>
              {/* Form */}
              <div className="flex flex-col bg-gray-700 p-6 w-[28%]">
                <span className="font-bold text-lg mb-4">
                  Авторизация
                </span>
                <form onSubmit={handleSubmit} className="flex flex-col">
                  <input onChange={(event) => setName(event.target.value)} required className="input" type="text" placeholder="Логин сюда"/>
                  <input onChange={(event) => setEmail(event.target.value)} required className="input" type="text" placeholder="Пароль сюды"/>
                  <button type="submit" className="input-submit">{!loading ? "Войти" : "Авторизация..."}</button>
                </form>
              </div>
            </div>
          </div>
        </main>
        <div className="pl-4 pr-4">
          <Footer />
        </div>
    </div>
  )
}
