"use client";

import Link from "next/link";
import { Button } from "@/shared/ui/button";
import { useAuth } from "@/features/auth/model/auth-context";

export function Topbar() {
  const { isAuthed, setAccessToken } = useAuth();

  return (
    <div className="sticky top-0 z-20 flex items-center justify-between gap-3 bg-black/20 backdrop-blur px-4 py-3">
      <div className="text-sm text-white/70">Browse</div>

      <div className="flex items-center gap-2">
        {!isAuthed ? (
          <>
            <Link href="/login" className="w-[120px]">
              <Button variant="outline" size="md">Увійти</Button>
            </Link>

            <Link href="/register" className="w-[180px]">
             <Button variant="primary" size="md">Реєстрація</Button>
            </Link>
          </>
        ) : (
          <div className="w-[120px]">
            <Button variant="light" size="md" onClick={() => setAccessToken(null)}>Вийти</Button>
          </div>
        )}
      </div>
    </div>
  );
}
