import React from "react";
import { cn } from "@/shared/lib/cn";

type Props = React.InputHTMLAttributes<HTMLInputElement> & {
  error?: string;
};

export const Input = React.forwardRef<HTMLInputElement, Props>(
  ({ className, error, ...props }, ref) => {
    return (
      <div className="w-full">
        <input
          ref={ref}
          className={cn(
            "h-[50px] w-full rounded-[16px]",
            "border border-[#778DA9]",
            "bg-[#1B263B] px-4",
            "text-[16px] leading-[20px] text-[#F0EEE9]",
            "placeholder:text-[#F0EEE980]",
            "outline-none transition-colors",
            "focus:ring-2 focus:ring-[#778DA9]",
            error ? "border-red-400" : "",
            className
          )}
          {...props}
        />
        {error ? <p className="mt-2 text-sm text-red-300">{error}</p> : null}
      </div>
    );
  }
);

Input.displayName = "Input";