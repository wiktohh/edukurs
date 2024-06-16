"use client";
import Link from "next/link";
import SubmitButton from "@/components/SubmitButton";
import Input from "@/components/Input";
import axios from "axios";
import { useEffect, useState } from "react";
import { useRouter } from "next/navigation";
import LoadingSpinner from "@/components/LoadingSpinner";

type UserData = {
  email: string;
  password: string;
};

const SignInPage = () => {
  const [userData, setUserData] = useState<UserData>({
    email: "",
    password: "",
  });
  const [loading, setLoading] = useState(false);

  const router = useRouter();

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    try {
      setLoading(true);
      const response = await axios.post(
        "http://localhost:5018/api/User/signin",
        userData
      );
      setLoading(false);
      const token = response.data.accessToken;
      localStorage.setItem("token", token);
      router.push("/home");
    } catch (err) {
      console.log(err);
    }
  };

  return (
    <div className="w-screen h-screen flex flex-col justify-center items-center">
      <h1 className="text-3xl mb-4">Zaloguj się do Edukurs</h1>
      <form onSubmit={handleSubmit} className="w-1/6 flex flex-col">
        <Input
          onChange={(e) => setUserData({ ...userData, email: e.target.value })}
          type="email"
          placeholder="Email"
        />
        <Input
          onChange={(e) =>
            setUserData({ ...userData, password: e.target.value })
          }
          type="password"
          placeholder="Hasło"
        />
        <div className="text-center mb-2">
          Nie masz jeszcze konta? <Link href="sign-up">Zarejestruj się</Link>
        </div>
        <SubmitButton>
          {loading ? (
            <LoadingSpinner width={1.25} height={1.25} />
          ) : (
            "Zaloguj się"
          )}
        </SubmitButton>
      </form>
    </div>
  );
};

export default SignInPage;
