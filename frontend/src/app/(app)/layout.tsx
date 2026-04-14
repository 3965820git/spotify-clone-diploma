import type { ReactNode } from 'react'
import { AuthGate } from '../../shared/auth/AuthGate'

export default function AppLayout({ children }: { children: ReactNode }) {
  return <AuthGate>{children}</AuthGate>
}