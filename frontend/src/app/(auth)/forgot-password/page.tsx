"use client";

import { useMemo, useState } from "react";
import { useRouter } from "next/navigation";
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import { AuthShell } from "@/shared/ui/layout/AuthShell";
import { AuthFormShell } from "@/features/auth/ui/AuthFormShell";
import { Button } from "@/shared/ui/button";

type FormValues = {
  email: string;
};

function MailIcon() {
  return (
    <svg width="20" height="20" viewBox="0 0 24 24" fill="none">
      <path
        d="M4 6.5h16v11H4v-11Z"
        stroke="currentColor"
        strokeWidth="2"
        strokeLinejoin="round"
      />
      <path
        d="m4.5 7 7.2 6.1c.2.2.5.2.7 0L19.5 7"
        stroke="currentColor"
        strokeWidth="2"
        strokeLinejoin="round"
      />
    </svg>
  );
}

function InfoIcon() {
  return (
    <svg width="20" height="20" viewBox="0 0 24 24" fill="none">
      <circle cx="12" cy="12" r="9" stroke="currentColor" strokeWidth="2" />
      <path
        d="M12 10.5V16"
        stroke="currentColor"
        strokeWidth="2"
        strokeLinecap="round"
      />
      <circle cx="12" cy="7.5" r="1" fill="currentColor" />
    </svg>
  );
}

export default function ForgotPasswordPage() {
  const router = useRouter();
  const [serverError, setServerError] = useState<string | null>(null);

  const schema = useMemo(
    () =>
      yup.object({
        email: yup
          .string()
          .trim()
          .required("Введи email")
          .email("Невірний формат email"),
      }),
    []
  );

  const {
    register,
    handleSubmit,
    formState: { errors, isSubmitting },
  } = useForm<FormValues>({
    resolver: yupResolver(schema),
    mode: "onChange",
    defaultValues: { email: "" },
  });

  const onSubmit = async (data: FormValues) => {
    setServerError(null);

    try {
      const response = await fetch("http://localhost:5000/api/v1/auth/password-reset/request", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          email: data.email.trim(),
        }),
      });

      if (!response.ok) {
        throw new Error("Не вдалося надіслати код. Спробуй ще раз.");
      }

      router.push(
        `/forgot-password/verify?email=${encodeURIComponent(data.email.trim())}`
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
        <form className="mt-[46px]" onSubmit={handleSubmit(onSubmit)}>
          <div className="w-full max-w-[370px] text-[20px] font-semibold leading-[24px] text-groov-accent">
            Введіть email,
            <br />
            прив’язаний до вашого акаунту
          </div>

          <div className="mt-[16px] w-full max-w-[370px]">
            <div className="relative">
              <input
                {...register("email")}
                autoComplete="email"
                placeholder="user@gmail.com"
                className="h-[50px] w-full rounded-[16px] border border-[#778DA9] bg-[#1B263B] pl-[44px] pr-4 text-[#F0EEE9] placeholder:text-[#F0EEE980] outline-none focus:ring-2 focus:ring-[#778DA9]"
              />
              <div className="pointer-events-none absolute inset-y-0 left-4 flex items-center text-groov-accent">
                <MailIcon />
              </div>
            </div>

            {errors.email?.message ? (
              <div className="mt-2 w-full groov-error text-left">
                {errors.email.message}
              </div>
            ) : null}
          </div>

          <div className="mt-[16px] flex w-full max-w-[370px] items-start gap-2 text-groov-accent">
            <div className="mt-[1px] flex-shrink-0">
              <InfoIcon />
            </div>
            <p className="text-[14px] leading-[18px] text-groov-accent">
              Ми надішлемо код підтвердження на ваш email для безпеки
            </p>
          </div>

          {serverError ? (
            <div className="mt-4 w-full max-w-[370px] groov-error text-left">
              {serverError}
            </div>
          ) : null}

          <div className="h-[40px]" />

          <Button
            type="submit"
            variant="light"
            size="lg"
            className="max-w-none"
            disabled={isSubmitting}
          >
            Надіслати код
          </Button>
        </form>
      </AuthFormShell>
    </AuthShell>
  );
}