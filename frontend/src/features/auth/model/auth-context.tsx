'use client'

import React, { createContext, useContext, useMemo, useState, useEffect } from 'react'
import { tokenStore } from '@/lib/tokenStore'

type AuthContextValue = {
  accessToken: string | null
  isAuthed: boolean
  setAccessToken: (token: string | null, expiresAt?: string | null) => void
  logout: () => void
}

const AuthContext = createContext<AuthContextValue | null>(null)

export function AuthProvider({ children }: { children: React.ReactNode }) {
  const [accessToken, setAccessTokenState] = useState<string | null>(null)

  useEffect(() => {
    const token = tokenStore.get()
    if (token) setAccessTokenState(token)
  }, [])

  const setAccessToken = (token: string | null, expiresAt?: string | null) => {
    tokenStore.set(token, expiresAt)
    setAccessTokenState(token)
  }

  const logout = () => {
    tokenStore.clear()
    setAccessTokenState(null)
  }

  const value = useMemo(
    () => ({
      accessToken,
      isAuthed: Boolean(accessToken),
      setAccessToken,
      logout,
    }),
    [accessToken]
  )

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>
}

export function useAuth() {
  const ctx = useContext(AuthContext)
  if (!ctx) throw new Error('useAuth must be used within AuthProvider')
  return ctx
}