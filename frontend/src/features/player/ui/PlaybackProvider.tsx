'use client'

import React from 'react'
import { TrackPlayer } from '@/features/player/ui/TrackPlayer'
import { GlobalPlayerBar } from '@/features/player/ui/GlobalPlayerBar'

type Props = {
  children: React.ReactNode
}

export function PlaybackProvider({ children }: Props) {
  return (
    <>
      <TrackPlayer />
      <GlobalPlayerBar />
      {children}
    </>
  )
}