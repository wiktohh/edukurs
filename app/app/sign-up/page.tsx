import Link from "next/link";
import Input from "../components/Input";
import SubmitButton from "../components/SubmitButton";

const SignUpPage = () => {
  return (
    <div className="w-screen h-screen flex flex-col justify-center items-center">
      <h1 className="text-4xl mb-4">Zarejestruj się do Edukurs</h1>
      <form className="w-1/5 flex flex-col">
        <div className="w-100 flex gap-2">
          <Input type="text" placeholder="Imię" halfWidth={true} />
          <Input type="text" placeholder="Nazwisko" halfWidth={true} />
        </div>
        <Input type="email" placeholder="Email" />
        <Input type="password" placeholder="Hasło" />
        <div className="text-center mb-2">
          Masz już konto? <Link href="sign-up">Zaloguj się</Link>
        </div>
        <SubmitButton content="Zarejestruj" />
      </form>
    </div>
  );
};

export default SignUpPage;
