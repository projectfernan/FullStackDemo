import { makeRequest } from "@utils/AxiosBasicAuth";
import { ApiEndPoints } from "@enums/ApiConstants";
import type { IApiBodyResponse } from "@/types/common/IApiBodyResponse";

export async function GetJwtToken(): Promise<IApiBodyResponse> {
  return makeRequest("post", ApiEndPoints.TOKEN_REQ_END_POINT);
}
