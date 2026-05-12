import { useState } from "react";
import { useForm, FormProvider } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { z } from "zod";
import { Pencil, Trash2 } from "lucide-react";
import { Button } from "@/components/ui/button";
import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogFooter,
} from "@/components/ui/dialog";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import MobileSuitFormSubmit from "./MobileSuitFormSubmit";
import MobileSuitDelete from "./MobileSuitDelete";
import { UpdateMobileSuit, DeleteMobileSuit } from "@services/pages/MobileSuitsService";
import { FormatDateTime } from "@utils/DateHelper";
import type { IMobileSuitData, IMobileSuitForm } from "@/types/pages/MobileSuits/IMobileSuits";

const schema = z.object({
  ModelCode: z.string().min(1, "Required"),
  ModelName: z.string().min(1, "Required"),
  OperatingSystem: z.string().min(1, "Required"),
  PowerOutput: z.string().min(1, "Required"),
  Armor: z.string().min(1, "Required"),
  Height: z.string().min(1, "Required"),
  Weight: z.string().min(1, "Required"),
  Manufacturer: z.string().min(1, "Required"),
});

interface Props {
  data: IMobileSuitData[];
}

export default function MobileSuitList({ data }: Props) {
  const [editTarget, setEditTarget] = useState<IMobileSuitData | null>(null);
  const [deleteTarget, setDeleteTarget] = useState<IMobileSuitData | null>(null);

  const formMethods = useForm<IMobileSuitForm>({ resolver: zodResolver(schema) });

  async function onUpdate(values: IMobileSuitForm) {
    if (!editTarget) return;
    await UpdateMobileSuit(values, editTarget.id, editTarget.dateCreated, editTarget.createdBy);
    setEditTarget(null);
    window.location.reload();
  }

  async function onDelete() {
    if (!deleteTarget) return;
    await DeleteMobileSuit(deleteTarget.id);
    setDeleteTarget(null);
    window.location.reload();
  }

  return (
    <>
      <Table>
        <TableHeader>
          <TableRow>
            <TableHead>Model Code</TableHead>
            <TableHead>Model Name</TableHead>
            <TableHead>OS</TableHead>
            <TableHead>Power</TableHead>
            <TableHead>Armor</TableHead>
            <TableHead>Height</TableHead>
            <TableHead>Weight</TableHead>
            <TableHead>Manufacturer</TableHead>
            <TableHead>Created</TableHead>
            <TableHead>Created By</TableHead>
            <TableHead>Edited</TableHead>
            <TableHead>Edited By</TableHead>
            <TableHead className="text-right">Actions</TableHead>
          </TableRow>
        </TableHeader>
        <TableBody>
          {(data ?? []).map((row) => (
            <TableRow key={row.id}>
              <TableCell>{row.modelCode}</TableCell>
              <TableCell>{row.modelName}</TableCell>
              <TableCell>{row.operatingSystem}</TableCell>
              <TableCell>{row.powerOutput}</TableCell>
              <TableCell>{row.armor}</TableCell>
              <TableCell>{row.height}</TableCell>
              <TableCell>{row.weight}</TableCell>
              <TableCell>{row.manufacturer}</TableCell>
              <TableCell>{FormatDateTime(row.dateCreated)}</TableCell>
              <TableCell>{row.createdBy}</TableCell>
              <TableCell>{FormatDateTime(row.dateEdited)}</TableCell>
              <TableCell>{row.editedBy}</TableCell>
              <TableCell className="text-right">
                <div className="flex justify-end gap-2">
                  <Button
                    size="icon"
                    variant="outline"
                    onClick={() => {
                      formMethods.reset();
                      setEditTarget(row);
                    }}
                  >
                    <Pencil className="h-4 w-4" />
                  </Button>
                  <Button
                    size="icon"
                    variant="destructive"
                    onClick={() => setDeleteTarget(row)}
                  >
                    <Trash2 className="h-4 w-4" />
                  </Button>
                </div>
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>

      {/* Edit Modal */}
      <Dialog open={!!editTarget} onOpenChange={(open) => !open && setEditTarget(null)}>
        <DialogContent className="max-w-2xl">
          <DialogHeader>
            <DialogTitle>Edit Mobile Suit</DialogTitle>
          </DialogHeader>
          <FormProvider {...formMethods}>
            <form id="edit-form" onSubmit={formMethods.handleSubmit(onUpdate)}>
              <MobileSuitFormSubmit
                defaultValues={
                  editTarget
                    ? {
                        ModelCode: editTarget.modelCode,
                        ModelName: editTarget.modelName,
                        OperatingSystem: editTarget.operatingSystem,
                        PowerOutput: editTarget.powerOutput,
                        Armor: editTarget.armor,
                        Height: editTarget.height,
                        Weight: editTarget.weight,
                        Manufacturer: editTarget.manufacturer,
                      }
                    : undefined
                }
              />
            </form>
          </FormProvider>
          <DialogFooter>
            <Button variant="outline" onClick={() => setEditTarget(null)}>
              Cancel
            </Button>
            <Button type="submit" form="edit-form">
              Save
            </Button>
          </DialogFooter>
        </DialogContent>
      </Dialog>

      {/* Delete Modal */}
      <Dialog open={!!deleteTarget} onOpenChange={(open) => !open && setDeleteTarget(null)}>
        <DialogContent className="max-w-sm">
          <DialogHeader>
            <DialogTitle>Delete Mobile Suit</DialogTitle>
          </DialogHeader>
          {deleteTarget && (
            <MobileSuitDelete
              ModelCode={deleteTarget.modelCode}
              ModelName={deleteTarget.modelName}
            />
          )}
          <DialogFooter>
            <Button variant="outline" onClick={() => setDeleteTarget(null)}>
              Cancel
            </Button>
            <Button variant="destructive" onClick={onDelete}>
              Delete
            </Button>
          </DialogFooter>
        </DialogContent>
      </Dialog>
    </>
  );
}
