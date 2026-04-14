"use client";
import Link from "next/link";
import { useEffect } from "react";
import { useRouter } from "next/navigation";
import { useAuth } from "@/features/auth/model/auth-context";
import { AuthShell } from "@/shared/ui/layout/AuthShell";
import { AuthPageHero } from "@/features/auth/ui/AuthPageHero";
import { AuthPageActions } from "@/features/auth/ui/AuthPageActions";
import { Button } from "@/shared/ui/button";


export default function WelcomePage() {
  const { isAuthed } = useAuth();
  const router = useRouter();

  useEffect(() => {
    if (isAuthed) router.replace("/home");
  }, [isAuthed, router]);

  return (
    <AuthShell>
      <div className="flex min-h-dvh flex-col">
        <div className="h-[50px]" />

        <div className="flex flex-1 flex-col items-center">
          <div className="h-[110px]" />

          <AuthPageHero
            title="Почни слухати зараз"
            subtitle="Музика, що відчуває тебе"
          />

          <div className="flex-1" />

          <AuthPageActions>
           <div className="w-full max-w-[370px] space-y-4 pb-[calc(60px+env(safe-area-inset-bottom))]">
  <Link href="/register" className="block">
    <Button variant="primary" size="lg" className="max-w-none">
      Зареєструватися безкоштовно
    </Button>
  </Link>

  <Link href="/login" className="block">
    <Button variant="outline" size="lg" className="max-w-none">
      Увійти
    </Button>
  </Link>
</div>
          </AuthPageActions>
        </div>
      </div>
    </AuthShell>
  );
}