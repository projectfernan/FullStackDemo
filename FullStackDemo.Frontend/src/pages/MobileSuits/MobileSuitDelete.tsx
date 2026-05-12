import type { IMobileSuitDelete } from "@/types/pages/MobileSuits/IMobileSuits";

export default function MobileSuitDelete({ ModelCode, ModelName }: IMobileSuitDelete) {
  return (
    <p className="text-sm">
      Are you sure you want to delete{" "}
      <span className="font-semibold">{ModelCode}</span>{" "}
      <span className="font-semibold">{ModelName}</span>?
    </p>
  );
}
