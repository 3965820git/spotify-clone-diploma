"use client";

import React from "react";
import { cn } from "@/shared/lib/cn";

type Variant = "primary" | "light" | "outline" | "secondary" | "dark";
/*primary → синя кнопка #415A77
light → світла кнопка #F0EEE9
outline → прозора з бордером
secondary → світло-синя #778DA9
dark → disabled/dark*/
type Size = "md" | "lg";

type Props = React.ButtonHTMLAttributes<HTMLButtonElement> & {
  variant?: Variant;
  size?: Size;
};

const base =
  "inline-flex items-center justify-center font-semibold transition-all duration-150 active:scale-[0.98] disabled:cursor-not-allowed";
const sizes: Record<Size, string> = {
  md: "h-[50px] w-full max-w-[214px] rounded-[16px]",
  lg: "h-[50px] w-full max-w-[370px] rounded-[16px]",
};

const variants: Record<Variant, string> = {
  primary:
    "bg-groov-primary text-groov-accent active:bg-groov-secondary active:text-white disabled:bg-[#4A4A4A] disabled:text-[#2F2F2F]",

  light:
"bg-groov-accent text-groov-textDark active:bg-[#778DA9] active:text-white disabled:bg-[#4A4A4A] disabled:text-[#2F2F2F]",

  outline:
"border border-groov-accent/60 bg-transparent text-groov-accent active:bg-groov-accent active:text-groov-textDark disabled:border-[#4A4A4A] disabled:text-[#2F2F2F]",

  secondary:
    "bg-groov-secondary text-white active:opacity-90 disabled:bg-[#4A4A4A] disabled:text-[#2F2F2F]",

  dark:
    "bg-[#2F2F2F] text-[#6B7280]",
};

export function Button({
  variant = "primary",
  size = "md",
  className,
  children,
  ...props
}: Props) {
  return (
    <button
      className={cn(base, sizes[size], variants[variant], className)}
      {...props}
    >
      {children}
    </button>
  );
}