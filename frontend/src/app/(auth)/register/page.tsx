"use client";

import { AuthShell } from "@/shared/ui/layout/AuthShell";
import { AuthOptionsScreen } from "@/features/auth/ui/auth-options-screen";

export default function LoginPage() {
  return (
    <AuthShell>
      <AuthOptionsScreen
        title="Відкрий світ музики"
        emailHref="/register/email"
        bottomHref="/login"
        bottomText="Увійти"
        bottomVariant="plain"
      />
    </AuthShell>
  );
}