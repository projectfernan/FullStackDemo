import axios from "axios";
import type { IApiBodyResponse } from "@/types/common/IApiBodyResponse";
import type { IJwtToken } from "@/types/common/IJwtToken";

const VITE_API_URL = import.meta.env.VITE_API_URL as string;

const instance = axios.create({
  baseURL: VITE_API_URL,
  headers: { "Content-Type": "application/json" },
});

instance.interceptors.request.use((config) => {
  const raw = localStorage.getItem("bearer");
  if (raw) {
    const jwt = JSON.parse(raw) as IJwtToken;
    config.headers["Authorization"] = `Bearer ${jwt.token}`;
  }
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
