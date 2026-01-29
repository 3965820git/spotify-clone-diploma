import { create } from 'zustand'
import { persist } from 'zustand/middleware'

type PlayerState = {
  url: string | null
  isPlaying: boolean
  volume: number // 0..100
  currentTime: number
  duration: number

  setUrl: (url: string | null) => void
  setPlaying: (v: boolean) => void
  setVolume: (v: number) => void
  setCurrentTime: (v: number) => void
  setDuration: (v: number) => void
}

export const usePlayerStore = create<PlayerState>()(
  persist(
    (set) => ({
      url: null,
      isPlaying: false,
      volume: 80,
      currentTime: 0,
      duration: 0,

      setUrl: (url) => set({ url }),
      setPlaying: (v) => set({ isPlaying: v }),
      setVolume: (v) => set({ volume: v }),
      setCurrentTime: (v) => set({ currentTime: v }),
      setDuration: (v) => set({ duration: v }),
    }),
    {
      name: 'player-store',
      partialize: (s) => ({
        url: s.url,
        isPlaying: s.isPlaying,
        volume: s.volume,
        currentTime: s.currentTime,
        duration: s.duration,
      }),
    }
  )
)
