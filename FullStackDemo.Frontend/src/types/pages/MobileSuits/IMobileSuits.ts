export interface IMobileSuitData {
  id: number;
  modelCode: string;
  modelName: string;
  operatingSystem: string;
  powerOutput: string;
  armor: string;
  height: string;
  weight: string;
  manufacturer: string;
  dateCreated: string;
  createdBy: string;
  dateEdited: string;
  editedBy: string;
}

export interface IMobileSuitForm {
  ModelCode: string;
  ModelName: string;
  OperatingSystem: string;
  PowerOutput: string;
  Armor: string;
  Height: string;
  Weight: string;
  Manufacturer: string;
}

export interface IMobileSuitDelete {
  ModelCode: string;
  ModelName: string;
}
