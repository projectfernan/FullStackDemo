import { useFormContext } from "react-hook-form";
import { useEffect } from "react";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import type { IMobileSuitForm } from "@/types/pages/MobileSuits/IMobileSuits";

const fields: { name: keyof IMobileSuitForm; label: string }[] = [
  { name: "ModelCode", label: "Model Code" },
  { name: "ModelName", label: "Model Name" },
  { name: "OperatingSystem", label: "Operating System" },
  { name: "PowerOutput", label: "Power Output" },
  { name: "Armor", label: "Armor" },
  { name: "Height", label: "Height" },
  { name: "Weight", label: "Weight" },
  { name: "Manufacturer", label: "Manufacturer" },
];

interface Props {
  defaultValues?: IMobileSuitForm;
}

export default function MobileSuitFormSubmit({ defaultValues }: Props) {
  const { register, setValue, formState: { errors } } = useFormContext<IMobileSuitForm>();

  useEffect(() => {
    if (defaultValues) {
      fields.forEach(({ name }) => setValue(name, defaultValues[name]));
    }
  }, [defaultValues, setValue]);

  return (
    <div className="grid grid-cols-2 gap-4">
      {fields.map(({ name, label }) => (
        <div key={name} className="flex flex-col gap-1">
          <Label htmlFor={name}>{label}</Label>
          <Input id={name} {...register(name)} />
          {errors[name] && (
            <p className="text-xs text-destructive">{errors[name]?.message}</p>
          )}
        </div>
      ))}
    </div>
  );
}
