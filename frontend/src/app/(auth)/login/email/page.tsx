"use client";
import Link from "next/link";
import { useMemo, useState } from "react";
import { useRouter } from "next/navigation";
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import { Input } from "@/shared/ui/input";
import { useAuthStore } from "@/shared/stores/auth.store";
import { AuthShell } from "@/shared/ui/layout/AuthShell";
import { AuthFormShell } from "@/features/auth/ui/AuthFormShell";
import { Button } from "@/shared/ui/button";

type FormValues = {
  login: string;
  password: string;
};

export default function LoginEmailPage() {
  const router = useRouter();
  const loginUser = useAuthStore((s) => s.login);
  const [serverError, setServerError] = useState<string | null>(null);

  const schema = useMemo(
    () =>
      yup.object({
        login: yup.string().trim().required("Введи email"),
        password: yup
          .string()
          .required("Введи пароль")
          .min(6, "Пароль має бути мінімум 6 символів"),
      }),
    []
  );

  const {
    register,
    handleSubmit,
    formState: { errors, isValid, isSubmitting },
  } = useForm<FormValues>({
    resolver: yupResolver(schema),
    mode: "onChange",
    defaultValues: { login: "", password: "" },
  });

  const onSubmit = async (data: FormValues) => {
    setServerError(null);

    try {
      await loginUser({
        identifier: data.login.trim(),
        password: data.password,
      });

      router.replace("/home");
    } catch (e: any) {
      setServerError(e?.message ?? "Сталася невідома помилка. Спробуй ще раз.");
    }
  };

  return (
    <AuthShell>
      <AuthFormShell title="З поверненням" onBack={() => router.back()}>
        <form className="mt-[46px]" onSubmit={handleSubmit(onSubmit)}>
          <div className="w-full max-w-[370px] text-[20px] font-semibold leading-[20px] text-groov-accent">
            Адреса електронної пошти
          </div>

          <div className="mt-[10px] w-full max-w-[370px]">
            <Input
              {...register("login")}
              autoComplete="username"
              error={errors.login?.message}
            />
          </div>

          <div className="h-[24px]" />

          <div className="w-full max-w-[370px] text-[20px] font-semibold leading-[20px] text-groov-accent">
            Пароль
          </div>

          <div className="mt-[10px] w-full max-w-[370px]">
            <Input
              type="password"
              {...register("password")}
              autoComplete="current-password"
              error={errors.password?.message}
            />
          </div>

          <div className="mt-[16px] w-full max-w-[370px]">
            <Link
              href="/forgot-password"
              className="text-[16px] font-normal leading-[100%] text-groov-accent"
            >
              Забулися пароль?
            </Link>
          </div>

          <div className="h-[48px]" />

          <Button
            type="submit"
            variant="light"
            size="lg"
            className="max-w-none"
          >
            Увійти
          </Button>

          {serverError && (
            <div className="mt-4 w-full max-w-[370px] groov-error">
              {serverError}
            </div>
          )}
        </form>
      </AuthFormShell>
    </AuthShell>
  );
}