"use client";

import { useEffect, useMemo, useRef, useState } from "react";
import { useRouter } from "next/navigation";
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import { Input } from "@/shared/ui/input";
import { useAuthStore } from "@/shared/stores/auth.store";
import { AuthShell } from "@/shared/ui/layout/AuthShell";
import { AuthFormShell } from "@/features/auth/ui/AuthFormShell";
import { Button } from "@/shared/ui/button";

function CalendarIcon() {
  return (
    <svg width="18" height="18" viewBox="0 0 24 24" fill="none">
      <path
        d="M7 2v2M17 2v2M3 9h18M5 6h14a2 2 0 0 1 2 2v14a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2Z"
        stroke="#F0EEE980"
        strokeWidth="2"
        strokeLinecap="round"
        strokeLinejoin="round"
      />
    </svg>
  );
}

function ChevronDownIcon() {
  return (
    <svg width="18" height="18" viewBox="0 0 24 24" fill="none">
      <path
        d="M6 9l6 6 6-6"
        stroke="currentColor"
        strokeWidth="2"
        strokeLinecap="round"
        strokeLinejoin="round"
      />
    </svg>
  );
}

type GenderValue = "Male" | "Female" | "NonBinary" | "Other" | "Unspecified";

const genderOptions: { label: string; value: GenderValue }[] = [
  { label: "Чоловіча", value: "Male" },
  { label: "Жіноча", value: "Female" },
  { label: "Небінарна", value: "NonBinary" },
  { label: "Інша", value: "Other" },
  { label: "Не вказано", value: "Unspecified" },
];

type FormValues = {
  name: string;
  birthDate: string;
  email: string;
  gender: "" | GenderValue;
  password: string;
  confirmPassword: string;
};

const FIELD_WRAPPER_CLASS = "w-full max-w-[370px]";
const LABEL_CLASS =
  "w-full max-w-[370px] text-[20px] font-semibold leading-[20px] text-groov-accent";
const FIELD_CLASS =
  "h-[50px] w-full rounded-[16px] border border-[#778DA9] bg-[#1B263B] px-4 text-[16px] leading-[20px] text-[#F0EEE9] outline-none transition-colors focus:border-[#778DA9] focus:ring-2 focus:ring-[#778DA9]";
const PLACEHOLDER_CLASS = "text-[#F0EEE980]";

function isValidUaDate(value: string) {
  const match = /^(\d{2})\.(\d{2})\.(\d{4})$/.exec(value);
  if (!match) return false;

  const day = Number(match[1]);
  const month = Number(match[2]);
  const year = Number(match[3]);

  if (year < 1900 || year > 2100) return false;
  if (month < 1 || month > 12) return false;
  if (day < 1 || day > 31) return false;

  const date = new Date(Date.UTC(year, month - 1, day));

  return (
    date.getUTCFullYear() === year &&
    date.getUTCMonth() === month - 1 &&
    date.getUTCDate() === day
  );
}

function uaDateToIsoOffset(value: string) {
  const match = /^(\d{2})\.(\d{2})\.(\d{4})$/.exec(value);
  if (!match) return "";

  const day = Number(match[1]);
  const month = Number(match[2]);
  const year = Number(match[3]);

  const iso = new Date(Date.UTC(year, month - 1, day)).toISOString();
  return iso.replace(".000Z", "+00:00");
}

export default function RegisterEmailPage() {
  const router = useRouter();
  const registerUser = useAuthStore((s) => s.register);
  const [serverError, setServerError] = useState<string | null>(null);
  const [isGenderOpen, setIsGenderOpen] = useState(false);
  const genderRef = useRef<HTMLDivElement | null>(null);

  const schema: yup.ObjectSchema<FormValues> = useMemo(
    () =>
      yup.object({
        name: yup.string().trim().required("Введи імʼя"),
        birthDate: yup
          .string()
          .required("Вкажи дату народження")
          .test("ua-date", "Формат: ДД.ММ.РРРР", (v) => !!v && isValidUaDate(v)),
        email: yup
          .string()
          .trim()
          .required("Введи email")
          .email("Невірний формат email"),
        gender: yup
          .mixed<"" | GenderValue>()
          .oneOf(["", "Male", "Female", "NonBinary", "Other", "Unspecified"] as const)
          .required("Обери стать")
          .test("gender-required", "Обери стать", (v) => v !== ""),
        password: yup
          .string()
          .required("Введи пароль")
          .min(6, "Пароль має бути мінімум 6 символів")
          .matches(/[A-Z]/, "Пароль має містити велику букву")
          .matches(/[0-9]/, "Пароль має містити цифру")
          .matches(
            /[!@#$%^&*(),.?":{}|<>_\-\\/[\]=+`~;'|]/,
            "Пароль має містити спеціальний символ"
          ),
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
    setValue,
    watch,
    formState: { errors, isValid, isSubmitting },
  } = useForm<FormValues>({
    resolver: yupResolver(schema),
    mode: "onChange",
    defaultValues: {
      name: "",
      birthDate: "",
      email: "",
      gender: "",
      password: "",
      confirmPassword: "",
    },
  });

  const gender = watch("gender");
  const birthDate = watch("birthDate");

  useEffect(() => {
    const handleClickOutside = (event: MouseEvent) => {
      if (!genderRef.current) return;
      if (!genderRef.current.contains(event.target as Node)) {
        setIsGenderOpen(false);
      }
    };

    document.addEventListener("mousedown", handleClickOutside);
    return () => document.removeEventListener("mousedown", handleClickOutside);
  }, []);

  const onBirthDateChange = (value: string) => {
    const digits = value.replace(/\D/g, "").slice(0, 8);
    const dd = digits.slice(0, 2);
    const mm = digits.slice(2, 4);
    const yyyy = digits.slice(4, 8);
    const formatted = [dd, mm, yyyy].filter(Boolean).join(".");
    setValue("birthDate", formatted, {
      shouldValidate: true,
      shouldDirty: true,
    });
  };

  const onSubmit = async (data: FormValues) => {
    setServerError(null);

    try {
      await registerUser({
        email: data.email.trim(),
        password: data.password,
        displayName: data.name.trim(),
        birthDate: uaDateToIsoOffset(data.birthDate),
        gender: data.gender,
      });

      router.replace("/login/email");
    } catch (e: any) {
      setServerError(e?.message ?? "Сталася невідома помилка. Спробуй ще раз.");
    }
  };

  const selectedGenderLabel =
    genderOptions.find((option) => option.value === gender)?.label ?? "Оберіть";

  return (
    <AuthShell>
      <AuthFormShell title="Створити акаунт" onBack={() => router.back()}>
        <form
          className="mt-[14px] flex w-full flex-col items-center"
          onSubmit={handleSubmit(onSubmit)}
        >
          <div className={LABEL_CLASS}>Ваше ім’я?</div>
          <div className="mt-[10px] w-full flex justify-center">
            <div className={FIELD_WRAPPER_CLASS}>
              <Input
                {...register("name")}
                autoComplete="name"
                error={errors.name?.message}
              />
            </div>
          </div>

          <div className="h-[24px]" />

          <div className={LABEL_CLASS}>Дата народження</div>
          <div className="mt-[10px] w-full flex justify-center">
            <div className={FIELD_WRAPPER_CLASS}>
              <div className="relative w-full">
                <input
                  value={birthDate}
                  onChange={(e) => onBirthDateChange(e.target.value)}
                  inputMode="numeric"
                  placeholder="ДД.ММ.РРРР"
                  className={`${FIELD_CLASS} pr-12 placeholder:text-[#F0EEE980]`}
                />
                <div className="pointer-events-none absolute inset-y-0 right-4 flex items-center">
                  <CalendarIcon />
                </div>
              </div>

              {errors.birthDate?.message ? (
                <div className="mt-2 w-full groov-error text-left">
                  {errors.birthDate.message}
                </div>
              ) : null}
            </div>
          </div>

          <div className="h-[24px]" />

          <div className={LABEL_CLASS}>Адреса електронної пошти</div>
          <div className="mt-[10px] w-full flex justify-center">
            <div className={FIELD_WRAPPER_CLASS}>
              <Input
                {...register("email")}
                autoComplete="email"
                error={errors.email?.message}
              />
            </div>
          </div>

          <div className="h-[24px]" />

          <div className={LABEL_CLASS}>Стать</div>
          <div className="mt-[10px] w-full flex justify-center">
            <div ref={genderRef} className={`${FIELD_WRAPPER_CLASS} relative`}>
              <button
                type="button"
                onClick={() => setIsGenderOpen((prev) => !prev)}
                className={`${FIELD_CLASS} flex items-center justify-between text-left ${
                  gender ? "text-[#F0EEE9]" : PLACEHOLDER_CLASS
                }`}
                aria-haspopup="listbox"
                aria-expanded={isGenderOpen}
              >
                <span>{selectedGenderLabel}</span>
                <span className="text-[#F0EEE980]">
                  <ChevronDownIcon />
                </span>
              </button>

              {isGenderOpen ? (
                <div className="absolute left-0 top-[58px] z-20 w-full overflow-hidden rounded-[16px] border border-[#778DA9] bg-[#1B263B] shadow-lg">
                  {genderOptions.map((option) => (
                    <button
                      key={option.value}
                      type="button"
                      className="flex h-[44px] w-full items-center px-4 text-left text-[16px] text-[#F0EEE9] hover:bg-[#24304A]"
                      onClick={() => {
                        setValue("gender", option.value, {
                          shouldValidate: true,
                          shouldDirty: true,
                        });
                        setIsGenderOpen(false);
                      }}
                    >
                      {option.label}
                    </button>
                  ))}
                </div>
              ) : null}

              {errors.gender?.message ? (
                <div className="mt-2 w-full groov-error text-left">
                  {errors.gender.message}
                </div>
              ) : null}
            </div>
          </div>

          <div className="h-[24px]" />

          <div className={LABEL_CLASS}>Пароль</div>
          <div className="mt-[10px] w-full flex justify-center">
            <div className={FIELD_WRAPPER_CLASS}>
              <Input
                type="password"
                {...register("password")}
                autoComplete="new-password"
                error={errors.password?.message}
              />
            </div>
          </div>

          <div className="h-[24px]" />

          <div className={LABEL_CLASS}>Підтвердіть пароль</div>
          <div className="mt-[10px] w-full flex justify-center">
            <div className={FIELD_WRAPPER_CLASS}>
              <Input
                type="password"
                {...register("confirmPassword")}
                autoComplete="new-password"
                error={errors.confirmPassword?.message}
              />
            </div>
          </div>

          <div className="h-[60px]" />

          <div className="w-full flex justify-center">
            <Button
              type="submit"
              variant="light"
              size="lg"
              disabled={!isValid || isSubmitting}
              className="w-full max-w-[370px]"
            >
              Зареєструватися
            </Button>
          </div>

          {serverError && (
            <div className="mt-4 w-full max-w-[370px] groov-error">
              {serverError}
            </div>
          )}

          <div className="mt-[10px] w-full flex justify-center">
            <div className="w-full max-w-[370px]">
              <p className="m-0 text-center text-[10px] font-semibold leading-[12px] text-[#F0EEE980]">
                Натискаючи &quot;Зареєструватися&quot;, ви погоджуєтеся з
                Умовами використання та Політикою конфіденційності.
              </p>
            </div>
          </div>
        </form>
      </AuthFormShell>
    </AuthShell>
  );
}