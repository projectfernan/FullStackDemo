import { Menu, User } from "lucide-react";
import { Button } from "@/components/ui/button";

interface TopBarProps {
  onToggleSidebar: () => void;
}

export default function TopBar({ onToggleSidebar }: TopBarProps) {
  return (
    <header className="h-14 border-b border-border bg-card flex items-center justify-between px-4">
      <Button variant="ghost" size="icon" onClick={onToggleSidebar} className="md:hidden">
        <Menu className="h-5 w-5" />
      </Button>
      <div className="flex-1" />
      <div className="flex items-center gap-2 text-sm text-muted-foreground">
        <User className="h-4 w-4" />
        <span>Guest</span>
      </div>
    </header>
  );
}
