import Link from "next/link";

const nav = [
  { href: "/home", label: "Home" },
  { href: "/search", label: "Search" },
  { href: "/library", label: "Your Library" },
];


export function Sidebar() {
  return (
    <aside className="hidden md:flex md:w-64 lg:w-72 flex-col gap-3 p-4">
      <div className="rounded-2xl bg-[rgb(var(--surface))] p-4">
        <div className="text-lg font-semibold">Spotify Clone</div>
        <div className="mt-1 text-sm text-[rgb(var(--muted))]">Diploma</div>
      </div>

      <nav className="rounded-2xl bg-[rgb(var(--surface))] p-2">
        {nav.map((i) => (
          <Link
            key={i.href}
            href={i.href}
            className="block rounded-xl px-3 py-2 text-sm hover:bg-[rgb(var(--surface-2))]"
          >
            {i.label}
          </Link>
        ))}
      </nav>
    </aside>
  );
}
