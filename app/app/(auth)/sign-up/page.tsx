"use client";
import Link from "next/link";
import Input from "../../../components/Input";
import SubmitButton from "../../../components/SubmitButton";
import { useEffect, useState } from "react";
import axios from "axios";
import { useRouter } from "next/navigation";
import LoadingSpinner from "@/components/LoadingSpinner";

type UserData = {
  firstName: string;
  lastName: string;
  email: string;
  password: string;
  role: string;
};

const SignUpPage = () => {
  const [userData, setUserData] = useState<UserData>({
    firstName: "",
    lastName: "",
    email: "",
    password: "",
    role: "Student",
  });
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");

  const router = useRouter();

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    try {
      setLoading(true);
      const response = await axios.post(
        "http://localhost:5018/api/User/signup",
        userData
      );
      setLoading(false);
      router.push("/sign-in");
    } catch (err: any) {
      setError(err.response.data.message);
      setLoading(false);
      console.log(err);
    }
  };

  return (
    <div className="w-screen h-screen flex flex-col justify-center items-center">
      <h1 className="text-3xl mb-4">Zarejestruj się do Edukurs</h1>
      <form onSubmit={handleSubmit} className="w-1/5 flex flex-col">
        <div className="w-100 flex gap-2">
          <Input
            onChange={(e) =>
              setUserData({ ...userData, firstName: e.target.value })
            }
            type="text"
            placeholder="Imię"
            halfWidth={true}
          />
          <Input
            onChange={(e) =>
              setUserData({ ...userData, lastName: e.target.value })
            }
            type="text"
            placeholder="Nazwisko"
            halfWidth={true}
          />
        </div>
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
          Masz już konto? <Link href="sign-in">Zaloguj się</Link>
        </div>
        <SubmitButton>
          {loading ? (
            <LoadingSpinner width={1.25} height={1.25} />
          ) : (
            "Zarejestruj się"
          )}
        </SubmitButton>
        {error && (
          <p className="text-red-500 mt-2 text-sm text-center">{error}</p>
        )}
      </form>
    </div>
  );
};

export default SignUpPage;
