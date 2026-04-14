import Link from "next/link";
import { cn } from "@/shared/lib/cn";

type Props = {
  href: string;
  children: React.ReactNode;
  variant?: "outline" | "plain" | "filled";
  className?: string;
};

export function AuthLinkButton({
  href,
  children,
  variant = "plain",
  className,
}: Props) {
  return (
    <Link
      href={href}
      className={cn(
        "auth-link",
        variant === "outline" && "auth-link-outline",
        variant === "filled" &&
          "flex h-[56px] items-center justify-center rounded-[20px] bg-groov-primary text-groov-accent",
        variant === "plain" && "auth-link-plain",
        className
      )}
    >
      {children}
    </Link>
  );
}