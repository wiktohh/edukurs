"use client";
import axios from "axios";

const getToken = () => {
  const token = localStorage.getItem("token");
  return token;
};

export const useAxios = () => {
  const instance = axios.create({
    baseURL: "http://localhost:5018",
  });

  instance.interceptors.request.use(
    (config) => {
      const token = getToken();
      if (token) {
        config.headers.Authorization = `Bearer ${token}`;
      }
      return config;
    },
    (error) => {
      return Promise.reject(error);
    }
  );

  return instance;
};
