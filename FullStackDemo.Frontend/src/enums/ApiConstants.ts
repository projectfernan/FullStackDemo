export const ApiEndPoints = {
  TOKEN_REQ_END_POINT: "/api/Security/GetJwtToken",
  GET_MOBILE_SUITS_PAGES_END_POINT: "/api/MobileSuit/GetMobileSuitsPaginated",
  CREATE_MOBILE_SUIT_END_POINT: "/api/MobileSuit/CreateMobileSuit",
  UPDATE_MOBILE_SUIT_END_POINT: "/api/MobileSuit/UpdateMobileSuit",
  DELETE_MOBILE_SUIT_END_POINT: "/api/MobileSuit/DeleteMobileSuit",
} as const;
