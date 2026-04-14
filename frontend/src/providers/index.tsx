'use client'

import React from 'react'
import { QueryClientProvider } from '@tanstack/react-query'
import { makeQueryClient } from '@/api/query-client'
import { AuthProvider } from '@/features/auth/model/auth-context'
import { PlaybackProvider } from '@/features/player/ui/PlaybackProvider'

const queryClient = makeQueryClient()

export function AppProviders({ children }: { children: React.ReactNode }) {
  return (
    <QueryClientProvider client={queryClient}>
      <AuthProvider>
        <PlaybackProvider>{children}</PlaybackProvider>
      </AuthProvider>
    </QueryClientProvider>
  )
}