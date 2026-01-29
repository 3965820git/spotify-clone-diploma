import type { Metadata } from 'next'
import './globals.css'
import { QueryProvider } from '@/app/providers/QueryProvider'


export const metadata: Metadata = {
  title: 'Spotify Clone',
  description: 'Auth starter',
}

export default function RootLayout({ children }: { children: React.ReactNode }) {
  return (
    <html lang="uk" suppressHydrationWarning>
      <body className="min-h-screen antialiased">
        <QueryProvider>{children}</QueryProvider>
      </body>
    </html>
  )
}
