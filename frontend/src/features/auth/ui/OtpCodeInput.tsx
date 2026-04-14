"use client";

import { useRef } from "react";

type Props = {
  value: string[];
  onChange: (next: string[]) => void;
  length?: number;
};

export function OtpCodeInput({
  value,
  onChange,
  length = 6,
}: Props) {
  const refs = useRef<Array<HTMLInputElement | null>>([]);

  const updateAt = (index: number, char: string) => {
    const next = [...value];
    next[index] = char;
    onChange(next);
  };

  const handleChange = (index: number, raw: string) => {
    const char = raw.replace(/\D/g, "").slice(-1);

    updateAt(index, char);

    if (char && index < length - 1) {
      refs.current[index + 1]?.focus();
    }
  };

  const handleKeyDown = (
    index: number,
    e: React.KeyboardEvent<HTMLInputElement>
  ) => {
    if (e.key === "Backspace") {
      if (value[index]) {
        updateAt(index, "");
        return;
      }

      if (index > 0) {
        refs.current[index - 1]?.focus();
        updateAt(index - 1, "");
      }
    }

    if (e.key === "ArrowLeft" && index > 0) {
      refs.current[index - 1]?.focus();
    }

    if (e.key === "ArrowRight" && index < length - 1) {
      refs.current[index + 1]?.focus();
    }
  };

  const handlePaste = (e: React.ClipboardEvent<HTMLInputElement>) => {
    e.preventDefault();

    const pasted = e.clipboardData
      .getData("text")
      .replace(/\D/g, "")
      .slice(0, length)
      .split("");

    const next = Array.from({ length }, (_, i) => pasted[i] ?? "");
    onChange(next);

    const focusIndex = Math.min(pasted.length, length - 1);
    refs.current[focusIndex]?.focus();
  };

  return (
    <div className="flex w-full max-w-[370px] items-center justify-between gap-2">
      {Array.from({ length }).map((_, index) => (
        <input
          key={index}
          ref={(el) => {
            refs.current[index] = el;
          }}
          type="text"
          inputMode="numeric"
          autoComplete="one-time-code"
          maxLength={1}
          value={value[index] ?? ""}
          onChange={(e) => handleChange(index, e.target.value)}
          onKeyDown={(e) => handleKeyDown(index, e)}
          onPaste={handlePaste}
          className="h-[50px] w-[50px] rounded-[14px] border border-[#778DA9] bg-[#1B263B] text-center text-[22px] font-semibold text-groov-accent outline-none focus:ring-2 focus:ring-[#778DA9]"
        />
      ))}
    </div>
  );
}