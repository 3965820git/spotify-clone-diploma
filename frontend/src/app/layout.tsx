import localFont from 'next/font/local'
import './globals.css'
import { AppProviders } from '@/providers'

const sfPro = localFont({
  src: [
    {
      path: '../fonts/SF-Pro-Display-Regular.otf',
      weight: '400',
      style: 'normal',
    },
    {
      path: '../fonts/SF-Pro-Display-Medium.otf',
      weight: '500',
      style: 'normal',
    },
    {
      path: '../fonts/SF-Pro-Display-Semibold.otf',
      weight: '590',
      style: 'normal',
    },
  ],
  display: 'swap',
  variable: '--font-sf',
})

export default function RootLayout({
  children,
}: {
  children: React.ReactNode
}) {
  return (
    <html lang="uk">
     <body className={sfPro.className}>
        <AppProviders>{children}</AppProviders>
      </body>
    </html>
  )
}