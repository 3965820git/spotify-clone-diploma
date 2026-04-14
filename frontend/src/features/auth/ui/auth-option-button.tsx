import React from "react";
import { cn } from "@/shared/lib/cn";

type Props = React.ButtonHTMLAttributes<HTMLButtonElement> & {
  icon: React.ReactNode;
  children: React.ReactNode;
  variant?: "filled" | "outline";
};

export function AuthOptionButton({
  icon,
  children,
  variant = "outline",
  className,
  ...props
}: Props) {
  const isFilled = variant === "filled";

  return (
    <button
      className={cn(
        "auth-option w-full",
        isFilled ? "auth-option-filled" : "auth-option-outline",
        className
      )}
      {...props}
    >
      {/* icon */}
      <span className="auth-option-icon flex-shrink-0">{icon}</span>

      {/* text */}
      <span
        className={cn(
          "auth-option-label flex-1 text-center leading-[1.2] break-words",
          isFilled ? "auth-option-label-filled" : "auth-option-label-outline"
        )}
      >
        {children}
      </span>
    </button>
  );
}