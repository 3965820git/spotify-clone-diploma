"use client";

import Link from "next/link";
import { useMemo, useState } from "react";
import { useRouter, useSearchParams } from "next/navigation";
import { AuthShell } from "@/shared/ui/layout/AuthShell";
import { AuthFormShell } from "@/features/auth/ui/AuthFormShell";
import { Button } from "@/shared/ui/button";
import { OtpCodeInput } from "@/features/auth/ui/OtpCodeInput";

function ClockIcon() {
  return (
    <svg width="18" height="18" viewBox="0 0 24 24" fill="none">
      <circle
        cx="12"
        cy="12"
        r="9"
        stroke="currentColor"
        strokeWidth="2"
      />
      <path
        d="M12 7.5V12L15 14"
        stroke="currentColor"
        strokeWidth="2"
        strokeLinecap="round"
        strokeLinejoin="round"
      />
    </svg>
  );
}

function formatTime(totalSeconds: number) {
  const minutes = Math.floor(totalSeconds / 60);
  const seconds = totalSeconds % 60;
  return `${String(minutes).padStart(2, "0")}:${String(seconds).padStart(2, "0")}`;
}

export default function VerifyCodePage() {
  const router = useRouter();
  const searchParams = useSearchParams();
  const email = searchParams.get("email") ?? "";

  const [code, setCode] = useState<string[]>(["", "", "", "", "", ""]);
  const [secondsLeft] = useState(116);
  const [serverError, setServerError] = useState<string | null>(null);

  const joinedCode = useMemo(() => code.join(""), [code]);
  const isValid = joinedCode.length === 6 && /^\d{6}$/.test(joinedCode);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    if (!isValid || !email) return;

    setServerError(null);

    try {
      const response = await fetch(
        "http://localhost:5000/api/v1/auth/password-reset/verify",
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({
            email,
            code: joinedCode,
          }),
        }
      );

      if (!response.ok) {
        throw new Error("Невірний код або код уже недійсний.");
      }

      router.push(
        `/forgot-password/reset?email=${encodeURIComponent(email)}&code=${encodeURIComponent(joinedCode)}`
      );
    } catch (error: unknown) {
      const message =
        error instanceof Error
          ? error.message
          : "Сталася невідома помилка. Спробуй ще раз.";

      setServerError(message);
    }
  };

  return (
    <AuthShell>
      <AuthFormShell title="Відновлення паролю" onBack={() => router.back()}>
        <form className="mt-[46px]" onSubmit={handleSubmit}>
          <div className="w-full max-w-[370px] text-[20px] font-semibold leading-[24px] text-groov-accent">
            Введіть код
          </div>

          <p className="mt-[8px] w-full max-w-[370px] text-[16px] leading-[20px] text-groov-accent/90">
            Ми надіслали код на ваш вказаний email
          </p>

          <div className="mt-[16px]">
            <OtpCodeInput value={code} onChange={setCode} />
          </div>

          <div className="mt-[18px] flex w-full max-w-[370px] items-center justify-center gap-2 text-groov-accent/90">
            <ClockIcon />
            <span className="text-[16px] leading-[18px]">
              Код дійсний: {formatTime(secondsLeft)}
            </span>
          </div>

          {serverError ? (
            <div className="mt-4 w-full max-w-[370px] groov-error text-center">
              {serverError}
            </div>
          ) : null}

          <div className="mt-[40px]">
            <Button
              type="submit"
              variant="light"
              size="lg"
              disabled={!isValid || !email}
              className="max-w-none"
            >
              Підтвердити код
            </Button>
          </div>

          <div className="mt-[20px] w-full max-w-[370px] text-center">
            <Link
              href="/forgot-password"
              className="text-[16px] font-normal leading-[18px] text-groov-accent"
            >
              Надіслати код повторно
            </Link>
          </div>
        </form>
      </AuthFormShell>
    </AuthShell>
  );
}