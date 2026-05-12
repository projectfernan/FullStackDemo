export default function PageFooter() {
  return (
    <footer className="border-t border-border py-4 px-6 text-center text-xs text-muted-foreground">
      &copy; {new Date().getFullYear()} FullStack Demo &mdash;{" "}
      <a
        href="https://github.com/fernan-x"
        target="_blank"
        rel="noopener noreferrer"
        className="underline hover:text-foreground transition-colors"
      >
        Fernan Cabrera
      </a>
    </footer>
  );
}
