"use client";

import { Suspense } from "react";
import { useSearchParams } from 'next/navigation'
import { AuthShell } from "@/shared/ui/layout/AuthShell";
import { AuthOptionsScreen } from "@/features/auth/ui/auth-options-screen";

function LoginContent() {
  const searchParams = useSearchParams()
  const error = searchParams.get('error')

  return (
    <AuthOptionsScreen
      title="Увійти в GROOV"
      emailHref="/login/email"
      bottomHref="/register"
      bottomText="Зареєструватися"
      bottomVariant="plain"
      subtitle={
        error === 'auth_error' || error === 'google_auth_failed'
          ? 'Не вдалося увійти через Google. Спробуй ще раз.'
          : undefined
      }
    />
  );
}

export default function LoginPage() {
  return (
    <AuthShell>
      <Suspense fallback={<div className="flex h-full items-center justify-center text-white">Завантаження...</div>}>
        <LoginContent />
      </Suspense>
    </AuthShell>
  );
}
