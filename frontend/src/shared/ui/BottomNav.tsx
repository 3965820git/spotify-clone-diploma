"use client";

import Link from "next/link";
import { usePathname } from "next/navigation";
import { usePremiumStore } from "@/features/premium/model/usePremiumStore";

type NavItemProps = {
  href: string;
  label: string;
  active: boolean;
  icon: (active: boolean) => React.ReactNode;
};

function NavItem({ href, label, active, icon }: NavItemProps) {
  return (
    <Link
      href={href}
      className={`flex min-w-0 flex-1 flex-col items-center justify-center gap-[4px] ${
        active ? "text-groov-accent" : "text-groov-muted"
      }`}
    >
      <div className="flex h-6 w-6 items-center justify-center">
        {icon(active)}
      </div>
      <span className="text-center text-[10px] font-medium leading-[12px]">
        {label}
      </span>
    </Link>
  );
}

function HomeIcon(active: boolean) {
  return (
    <svg
      width="22"
      height="22"
      viewBox="0 0 24 24"
      fill={active ? "currentColor" : "none"}
      stroke="currentColor"
      strokeWidth="2"
      strokeLinecap="round"
      strokeLinejoin="round"
    >
      <path d="M3 10.5L12 3l9 7.5" />
      <path d="M5.5 9.5V20h13V9.5" />
      <path d="M9.5 20v-6h5v6" />
    </svg>
  );
}

function SearchIcon(active: boolean) {
  return (
    <svg
      width="22"
      height="22"
      viewBox="0 0 24 24"
      fill="none"
      stroke="currentColor"
      strokeWidth="2.2"
      strokeLinecap="round"
      strokeLinejoin="round"
    >
      <circle
        cx="11"
        cy="11"
        r="6.5"
        fill={active ? "currentColor" : "none"}
      />
      <path d="M16 16l4 4" />
    </svg>
  );
}

function PremiumIcon(active: boolean) {
  return (
    <svg
      width="22"
      height="22"
      viewBox="0 0 24 24"
      fill={active ? "currentColor" : "none"}
      stroke="currentColor"
      strokeWidth="2"
      strokeLinecap="round"
      strokeLinejoin="round"
    >
      <path d="M9 18V5l10-2v13" />
      <path d="M9 10l10-2" />
      <ellipse cx="6.5" cy="18" rx="3.5" ry="2.5" />
      <ellipse cx="16.5" cy="16" rx="3.5" ry="2.5" />
    </svg>
  );
}

function LibraryIcon(active: boolean) {
  return (
    <svg
      width="22"
      height="22"
      viewBox="0 0 24 24"
      fill={active ? "currentColor" : "none"}
      stroke="currentColor"
      strokeWidth="2"
      strokeLinecap="round"
      strokeLinejoin="round"
    >
      <rect x="6" y="4" width="12" height="16" rx="2" />
      <path d="M9 4v16" />
    </svg>
  );
}

function ProfileIcon(active: boolean) {
  return (
    <svg
      width="22"
      height="22"
      viewBox="0 0 24 24"
      fill="none"
      stroke="currentColor"
      strokeWidth="2"
      strokeLinecap="round"
      strokeLinejoin="round"
    >
      <circle cx="12" cy="8" r="3.5" fill={active ? "currentColor" : "none"} />
      <path d="M5 19c1.8-3 4.2-4.5 7-4.5s5.2 1.5 7 4.5" />
    </svg>
  );
}

const ROOT_PATHS = new Set([
  "/home",
  "/search",
  "/premium",
  "/library",
  "/profile",
]);

export function BottomNav() {
  const pathname = usePathname();
  const hasPremium = usePremiumStore((state) => state.hasPremium);

  if (!pathname || !ROOT_PATHS.has(pathname)) {
    return null;
  }

  const isActive = (href: string) => pathname === href;

  return (
    <nav className="fixed bottom-0 left-0 right-0 z-50 flex h-[78px] w-full items-start bg-groov-bg px-4 pb-[10px] pt-[8px]">
      <NavItem
        href="/home"
        label="Головна"
        active={isActive("/home")}
        icon={HomeIcon}
      />

      <NavItem
        href="/search"
        label="Пошук"
        active={isActive("/search")}
        icon={SearchIcon}
      />

      {!hasPremium && (
        <NavItem
          href="/premium"
          label="Преміум"
          active={isActive("/premium")}
          icon={PremiumIcon}
        />
      )}

      <NavItem
        href="/library"
        label="Бібліотека"
        active={isActive("/library")}
        icon={LibraryIcon}
      />

      <NavItem
        href="/profile"
        label="Профіль"
        active={isActive("/profile")}
        icon={ProfileIcon}
      />
    </nav>
  );
}