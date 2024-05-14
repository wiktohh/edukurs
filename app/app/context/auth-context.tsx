"use client";

import { useAxios } from "../hooks/use-axios";
import { useRouter } from "next/navigation";
import { createContext, useContext, useEffect, useState } from "react";

type UserDataType = {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
  role: string;
};

type AuthContextProps = {
  user: UserDataType | null;
  token: string;
  logout: () => void;
};

export const AuthContext = createContext<AuthContextProps>({
  user: null,
  token: "",
  logout: () => null,
});

type AuthContextProviderProps = {
  children: React.ReactNode;
};

export const useAuth = () => useContext(AuthContext);

export const AuthContextProvider: React.FC<AuthContextProviderProps> = ({
  children,
}) => {
  const [user, setUser] = useState<UserDataType | null>(null);
  const [token, setToken] = useState("");
  const [loaded, setLoaded] = useState(false);

  const router = useRouter();
  const axios = useAxios();

  const logout = () => {
    setUser(null);
    setToken("");
    localStorage.removeItem("token");
    router.push("/sign-in");
  };

  useEffect(() => {
    const getUser = async () => {
      const token = localStorage.getItem("token");
      if (token) {
        setToken(() => token);
        try {
          const response = await axios.get("/api/User/me");
          setUser(response.data);
        } catch (err) {
          console.log(err);
        }
      }
      setLoaded(true);
    };
    getUser();
  });

  const values: AuthContextProps = {
    user,
    token,
    logout,
  };

  return (
    loaded && (
      <AuthContext.Provider value={values}>{children}</AuthContext.Provider>
    )
  );
};

export default AuthContextProvider;
