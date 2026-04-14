'use client'

import { useEffect } from 'react'
import { useRouter } from 'next/navigation'
import { useAuth } from '@/features/auth/model/auth-context'

function getCookie(name: string) {
  if (typeof document === 'undefined') return null

  const match = document.cookie
    .split('; ')
    .find((row) => row.startsWith(`${name}=`))

  return match ? decodeURIComponent(match.split('=')[1]) : null
}

function clearCookie(name: string) {
  document.cookie = `${name}=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;`
}

export default function GoogleAuthSuccessPage() {
  const router = useRouter()
  const { setAccessToken } = useAuth()

  useEffect(() => {
    const accessToken = getCookie('tempAccessToken')
    const expiresAt = getCookie('tempExpiresAt')

    
    if (!accessToken) {
      router.replace('/auth?error=google_auth_failed')
      return
    }

    try {
      setAccessToken(accessToken, expiresAt ?? undefined)
    } catch (e) {
      router.replace('/auth?error=google_auth_failed')
      return
    }

  
    clearCookie('tempAccessToken')
    clearCookie('tempExpiresAt')

    router.replace('/home')
  }, [router, setAccessToken])

  return <div className="p-6 text-white">Завершуємо вхід...</div>
}