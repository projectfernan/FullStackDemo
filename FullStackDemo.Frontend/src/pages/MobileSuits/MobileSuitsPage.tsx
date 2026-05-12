import { useState, useEffect } from "react";
import { useForm, FormProvider } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { z } from "zod";
import { Plus, ChevronLeft, ChevronRight, Search } from "lucide-react";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogFooter,
} from "@/components/ui/dialog";
import MobileSuitList from "./MobileSuitList";
import MobileSuitFormSubmit from "./MobileSuitFormSubmit";
import {
  GetMobileSuitPages,
  CreateMobileSuit,
} from "@services/pages/MobileSuitsService";
import type { IMobileSuitData, IMobileSuitForm } from "@/types/pages/MobileSuits/IMobileSuits";

const PAGE_LENGTH = Number(import.meta.env.VITE_TABLE_ROW_LIMIT) || 5;

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

export default function MobileSuitsPage() {
  const [data, setData] = useState<IMobileSuitData[]>([]);
  const [page, setPage] = useState(0);
  const [maxPage, setMaxPage] = useState(0);
  const [search, setSearch] = useState("");
  const [showCreate, setShowCreate] = useState(false);

  const formMethods = useForm<IMobileSuitForm>({ resolver: zodResolver(schema) });

  useEffect(() => {
    GetMobileSuitPages(page, PAGE_LENGTH, search).then((res) => {
      if (res.success) {
        setData((res.data as IMobileSuitData[]) ?? []);
        setMaxPage(res.totalCount ?? 0);
      }
    });
  }, [page, search]);

  async function onSubmit(values: IMobileSuitForm) {
    const res = await CreateMobileSuit(values);
    if (res.success) {
      setShowCreate(false);
      formMethods.reset();
      window.location.reload();
    }
  }

  const totalPages = Math.ceil(maxPage / PAGE_LENGTH);
  const currentPage = Math.floor(page / PAGE_LENGTH) + 1;

  return (
    <div className="p-6 space-y-4">
      <div className="flex items-center justify-between">
        <h1 className="text-2xl font-semibold">Mobile Suits</h1>
        <Button onClick={() => { formMethods.reset(); setShowCreate(true); }}>
          <Plus className="h-4 w-4 mr-2" />
          Add
        </Button>
      </div>

      <Card>
        <CardHeader className="pb-3">
          <CardTitle className="text-base font-medium">Records</CardTitle>
          <div className="flex items-center gap-2 pt-1">
            <Search className="h-4 w-4 text-muted-foreground" />
            <Input
              placeholder="Search..."
              className="max-w-xs"
              value={search}
              onChange={(e) => { setSearch(e.target.value); setPage(0); }}
            />
          </div>
        </CardHeader>
        <CardContent className="p-0 overflow-x-auto">
          <MobileSuitList data={data} />
        </CardContent>
      </Card>

      {/* Pagination */}
      <div className="flex items-center justify-between text-sm text-muted-foreground">
        <span>
          Page {currentPage} of {totalPages || 1} &mdash; {maxPage} record{maxPage !== 1 ? "s" : ""}
        </span>
        <div className="flex gap-2">
          <Button
            variant="outline"
            size="icon"
            disabled={page === 0}
            onClick={() => setPage((p) => Math.max(0, p - PAGE_LENGTH))}
          >
            <ChevronLeft className="h-4 w-4" />
          </Button>
          <Button
            variant="outline"
            size="icon"
            disabled={page + PAGE_LENGTH >= maxPage}
            onClick={() => setPage((p) => p + PAGE_LENGTH)}
          >
            <ChevronRight className="h-4 w-4" />
          </Button>
        </div>
      </div>

      {/* Create Modal */}
      <Dialog open={showCreate} onOpenChange={setShowCreate}>
        <DialogContent className="max-w-2xl">
          <DialogHeader>
            <DialogTitle>Add Mobile Suit</DialogTitle>
          </DialogHeader>
          <FormProvider {...formMethods}>
            <form id="create-form" onSubmit={formMethods.handleSubmit(onSubmit)}>
              <MobileSuitFormSubmit />
            </form>
          </FormProvider>
          <DialogFooter>
            <Button variant="outline" onClick={() => setShowCreate(false)}>
              Cancel
            </Button>
            <Button type="submit" form="create-form">
              Save
            </Button>
          </DialogFooter>
        </DialogContent>
      </Dialog>
    </div>
  );
}
