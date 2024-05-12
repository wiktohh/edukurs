import Link from "next/link";
import SubmitButton from "../components/SubmitButton";
import Input from "../components/Input";

const SignInPage = () => {
  return (
    <div className="w-screen h-screen flex flex-col justify-center items-center">
      <h2 className="text-2xl mb-4">Zaloguj się do Edukurs</h2>
      <form className="w-1/6 flex flex-col">
        <Input type="email" placeholder="Email" />
        <Input type="password" placeholder="Hasło" />
        <div className="text-center mb-2">
          Nie masz jeszcze konta? <Link href="sign-up">Zarejestruj się</Link>
        </div>
        <SubmitButton content="Zaloguj" />
      </form>
    </div>
  );
};

export default SignInPage;
