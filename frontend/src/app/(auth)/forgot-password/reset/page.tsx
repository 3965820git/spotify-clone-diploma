"use client";

import { useMemo, useState } from "react";
import { useRouter, useSearchParams } from "next/navigation";
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import { AuthShell } from "@/shared/ui/layout/AuthShell";
import { AuthFormShell } from "@/features/auth/ui/AuthFormShell";
import { Button } from "@/shared/ui/button";
import { Input } from "@/shared/ui/input";

type FormValues = {
  password: string;
  confirmPassword: string;
};

export default function ResetPasswordPage() {
  const router = useRouter();
  const searchParams = useSearchParams();

  const email = searchParams.get("email") ?? "";
  const code = searchParams.get("code") ?? "";

  const [serverError, setServerError] = useState<string | null>(null);

  const schema = useMemo(
    () =>
      yup.object({
        password: yup
          .string()
          .required("Створіть новий пароль")
          .min(6, "Пароль має бути мінімум 6 символів"),
        confirmPassword: yup
          .string()
          .required("Підтверди пароль")
          .oneOf([yup.ref("password")], "Паролі не співпадають"),
      }),
    []
  );

  const {
    register,
    handleSubmit,
    formState: { errors, isSubmitting, isValid },
  } = useForm<FormValues>({
    resolver: yupResolver(schema),
    mode: "onChange",
    defaultValues: {
      password: "",
      confirmPassword: "",
    },
  });

  const onSubmit = async (data: FormValues) => {
    if (!email || !code) {
      setServerError("Не вистачає даних для скидання пароля.");
      return;
    }

    setServerError(null);

    try {
      const response = await fetch(
        "http://localhost:5000/api/v1/auth/password-reset/confirm",
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({
            email,
            code,
            newPassword: data.password,
          }),
        }
      );

      if (!response.ok) {
        throw new Error("Не вдалося змінити пароль. Спробуй ще раз.");
      }

      router.replace("/login/email");
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
            Новий пароль
          </div>

          <div className="mt-[16px] w-full max-w-[370px]">
            <Input
              type="password"
              {...register("password")}
              error={errors.password?.message}
            />
          </div>

          <div className="mt-[16px] w-full max-w-[370px]">
            <div className="w-full max-w-[370px] text-[20px] font-semibold leading-[24px] text-groov-accent">
            Підтвердіть новий пароль
          </div>
          <div className="mt-[16px] w-full max-w-[370px]"></div>
            <Input
              type="password"
              {...register("confirmPassword")}
              error={errors.confirmPassword?.message}
            />
          </div>

          {serverError ? (
            <div className="mt-4 w-full max-w-[370px] groov-error">
              {serverError}
            </div>
          ) : null}

          <div className="h-[40px]" />

          <Button
            type="submit"
            variant="light"
            size="lg"
            className="max-w-none"
            disabled={!isValid || isSubmitting}
          >
            Зберегти новий пароль
          </Button>
        </form>
      </AuthFormShell>
    </AuthShell>
  );
}