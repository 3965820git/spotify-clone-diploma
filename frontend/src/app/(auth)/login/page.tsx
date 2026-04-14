"use client";
import { useSearchParams } from 'next/navigation'
import { AuthShell } from "@/shared/ui/layout/AuthShell";
import { AuthOptionsScreen } from "@/features/auth/ui/auth-options-screen";

export default function LoginPage() {
    const searchParams = useSearchParams()
  const error = searchParams.get('error')
  return (
    <AuthShell>
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
    </AuthShell>
  );
}
