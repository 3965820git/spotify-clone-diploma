'use client'

import dynamic from 'next/dynamic'

const PlayClient = dynamic(() => import('./play-client'), { ssr: false })

export default function PlayShell() {
  return <PlayClient />
}