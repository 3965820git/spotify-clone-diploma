"use client";

import Image from "next/image";
import Link from "next/link";
import { AuthLinkButton } from "@/shared/ui/auth-link-button";
import { AuthOptionButton } from "@/features/auth/ui/auth-option-button";

type Props = {
  title: string;
  emailHref: string;
  bottomHref: string;
  bottomText: string;
  bottomVariant?: "outline" | "plain" | "filled";
  subtitle?: string;
  hideSocials?: boolean;
};

function MailIcon() {
  return (
    <svg width="22" height="22" viewBox="0 0 24 24" fill="none">
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

function GoogleIcon() {
  return <span className="text-lg font-extrabold leading-none">G</span>;
}

function AppleIcon() {
  return (
    <svg width="22" height="22" viewBox="0 0 24 24" fill="currentColor">
      <path d="M16.7 13.2c0-2 1.7-3 1.8-3.1-1-1.4-2.6-1.6-3.1-1.6-1.3-.1-2.6.8-3.3.8-.7 0-1.8-.8-2.9-.8-1.5 0-2.9.9-3.7 2.2-1.6 2.7-.4 6.7 1.1 8.9.7 1.1 1.6 2.3 2.8 2.2 1.1 0 1.6-.7 3-0.7 1.4 0 1.8.7 3 .7 1.2 0 2-.9 2.7-2 .8-1.2 1.2-2.4 1.2-2.5-.1 0-2.6-1-2.6-4.1zM14.8 6.7c.6-.7 1-1.7.9-2.7-.9.1-1.9.6-2.5 1.3-.6.7-1.1 1.7-.9 2.7 1 .1 1.9-.5 2.5-1.3z" />
    </svg>
  );
}

export function AuthOptionsScreen({
  title,
  emailHref,
  bottomHref,
  bottomText,
  bottomVariant = "plain",
  subtitle,
  hideSocials = false,
}: Props) {
  return (
    <div className="groov-page">
      <div className="phone-container">
        <div className="flex min-h-dvh flex-col">
          <div className="h-[50px]" />
          <div className="h-[130px]" />

          <div className="mx-auto relative h-[98px] w-[98px] overflow-hidden rounded-[20px]">
            <Image
              src="/groov-logo.png"
              alt="Groov Logo"
              fill
              priority
              className="object-cover"
            />
          </div>

          <div className="h-[20px]" />

          <h1 className="phone-content text-center text-[24px] font-semibold leading-[24px] text-groov-accent">
            {title}
          </h1>

          {subtitle ? (
            <p className="phone-content mt-2 text-center text-[16px] leading-[24px] text-groov-accent/90">
              {subtitle}
            </p>
          ) : null}

          <div className="h-[193px]" />

          {!hideSocials ? (
            <div className="phone-content space-y-[10px]">
              <Link href={emailHref} className="block w-full">
                <AuthOptionButton
                  icon={<MailIcon />}
                  variant="filled"
                  className="w-full"
                >
                  Продовжити за допомогою <br /> електронної пошти
                </AuthOptionButton>
              </Link>

              <AuthOptionButton
  icon={<GoogleIcon />}
  variant="outline"
  className="w-full"
  onClick={() => {
    window.location.href =
      "http://localhost:5000/api/v1/auth/login-google";
  }}
>
  Продовжити за допомогою Google
</AuthOptionButton>

              <AuthOptionButton
                icon={<AppleIcon />}
                variant="outline"
                className="w-full"
              >
                Продовжити за допомогою Apple
              </AuthOptionButton>
            </div>
          ) : null}

          <div className="phone-content mt-[16px]">
            <AuthLinkButton
              href={bottomHref}
              variant={bottomVariant}
              className="w-full"
            >
              {bottomText}
            </AuthLinkButton>
          </div>

          <div className="h-[34px]" />
        </div>
      </div>
    </div>
  );
}