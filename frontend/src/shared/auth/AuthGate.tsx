'use client'

import { useEffect } from 'react'
import { useRouter } from 'next/navigation'
import { useAuthStore } from '@/shared/stores/auth.store'

export function AuthGate({ children }: { children: React.ReactNode }) {
  const router = useRouter()
  const isHydrated = useAuthStore((s) => s.isHydrated)
  const accessToken = useAuthStore((s) => s.accessToken)

  useEffect(() => {
    if (!isHydrated) return
    if (!accessToken) {
      router.replace('/login/email')
    }
  }, [isHydrated, accessToken, router])

  if (!isHydrated) return null
  if (!accessToken) return null

  return <>{children}</>
}