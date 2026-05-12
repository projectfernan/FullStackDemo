import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { Badge } from "@/components/ui/badge";
import { Separator } from "@/components/ui/separator";

const techStack = [
  { category: "Frontend", items: ["React 18", "TypeScript", "Vite", "shadcn/ui", "Tailwind CSS", "React Hook Form", "Zod", "Axios"] },
  { category: "Backend", items: ["ASP.NET Core 8", "Dapper", "Entity Framework Core", "AutoMapper", "log4net", "JWT Bearer"] },
  { category: "Database", items: ["SQL Server (local)", "PostgreSQL (production)"] },
  { category: "DevOps", items: ["Docker", "Docker Compose"] },
];

const features = [
  "CRUD operations with paginated search",
  "JWT Bearer + Basic Auth dual authentication",
  "Domain-Driven Design (DDD) backend",
  "CQRS pattern — reads via Dapper, writes via EF Core",
  "Fully containerized with Docker Compose",
];

export default function AboutPage() {
  return (
    <div className="p-6 space-y-6 max-w-3xl">
      <div>
        <h1 className="text-2xl font-semibold">About</h1>
        <p className="text-muted-foreground mt-1 text-sm">
          A full-stack portfolio demo showcasing modern web development practices.
        </p>
      </div>

      <Card>
        <CardHeader>
          <CardTitle className="text-base">Key Features</CardTitle>
        </CardHeader>
        <CardContent>
          <ul className="space-y-2 text-sm">
            {features.map((f) => (
              <li key={f} className="flex items-start gap-2">
                <span className="mt-0.5 text-primary">&#x2022;</span>
                {f}
              </li>
            ))}
          </ul>
        </CardContent>
      </Card>

      <Card>
        <CardHeader>
          <CardTitle className="text-base">Tech Stack</CardTitle>
        </CardHeader>
        <CardContent className="space-y-4">
          {techStack.map(({ category, items }, i) => (
            <div key={category}>
              {i > 0 && <Separator className="mb-4" />}
              <p className="text-xs font-semibold uppercase text-muted-foreground mb-2">{category}</p>
              <div className="flex flex-wrap gap-2">
                {items.map((item) => (
                  <Badge key={item} variant="secondary">{item}</Badge>
                ))}
              </div>
            </div>
          ))}
        </CardContent>
      </Card>
    </div>
  );
}
