import axios from "axios";
import type { IApiBodyResponse } from "@/types/common/IApiBodyResponse";

const VITE_API_URL = import.meta.env.VITE_API_URL as string;
const VITE_BASIC_AUTH_API_UID = import.meta.env.VITE_BASIC_AUTH_API_UID as string;
const VITE_BASIC_AUTH_API_PWD = import.meta.env.VITE_BASIC_AUTH_API_PWD as string;

const instance = axios.create({
  baseURL: VITE_API_URL,
  headers: { "Content-Type": "application/json" },
});

instance.interceptors.request.use((config) => {
  config.auth = { username: VITE_BASIC_AUTH_API_UID, password: VITE_BASIC_AUTH_API_PWD };
  return config;
});

instance.interceptors.response.use(
  (res) => res,
  (err) => Promise.resolve(err.response ?? { data: { success: false, message: err.message } })
);

export async function makeRequest(
  method: "get" | "post" | "put" | "delete",
  url: string,
  data?: unknown
): Promise<IApiBodyResponse> {
  const res = await instance({ method, url, data });
  return res.data as IApiBodyResponse;
}
