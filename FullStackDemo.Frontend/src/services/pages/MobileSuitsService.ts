import { makeRequest } from "@utils/AxiosBearer";
import { ApiEndPoints } from "@enums/ApiConstants";
import type { IApiBodyResponse } from "@/types/common/IApiBodyResponse";
import type { IMobileSuitForm } from "@/types/pages/MobileSuits/IMobileSuits";

export async function GetMobileSuitPages(
  start: number,
  length: number,
  search: string
): Promise<IApiBodyResponse> {
  return makeRequest(
    "get",
    `${ApiEndPoints.GET_MOBILE_SUITS_PAGES_END_POINT}?start=${start}&length=${length}&search=${encodeURIComponent(search ?? "")}`
  );
}

export async function CreateMobileSuit(props: IMobileSuitForm): Promise<IApiBodyResponse> {
  return makeRequest("post", ApiEndPoints.CREATE_MOBILE_SUIT_END_POINT, {
    ...props,
    CreatedBy: "Guest",
  });
}

export async function UpdateMobileSuit(
  props: IMobileSuitForm,
  Id: number,
  DateCreated: string,
  CreatedBy: string
): Promise<IApiBodyResponse> {
  return makeRequest("put", ApiEndPoints.UPDATE_MOBILE_SUIT_END_POINT, {
    Id,
    ...props,
    DateCreated,
    CreatedBy,
    EditedBy: "Guest",
  });
}

export async function DeleteMobileSuit(Id: number): Promise<IApiBodyResponse> {
  return makeRequest(
    "delete",
    `${ApiEndPoints.DELETE_MOBILE_SUIT_END_POINT}?Id=${Id}&DeletedBy=Guest`
  );
}
