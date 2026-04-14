import { create } from 'zustand'
import { persist } from 'zustand/middleware'
import { PlayerTrack } from '@/features/player/model/types'

type PlayerState = {
  currentTrack: PlayerTrack | null
  isPlaying: boolean
  currentTime: number
  duration: number
  likedTrackIds: string[]

  setTrack: (track: PlayerTrack | null) => void
  setPlaying: (value: boolean) => void
  setCurrentTime: (value: number) => void
  setDuration: (value: number) => void
  toggleLikedTrack: (trackId: string) => void
  reset: () => void
}

export const usePlayerStore = create<PlayerState>()(
  persist(
    (set) => ({
      currentTrack: null,
      isPlaying: false,
      currentTime: 0,
      duration: 0,
      likedTrackIds: [],

      setTrack: (track) =>
        set({
          currentTrack: track,
          currentTime: 0,
          duration: track?.durationSeconds ?? 0,
        }),

      setPlaying: (value) => set({ isPlaying: value }),
      setCurrentTime: (value) => set({ currentTime: value }),
      setDuration: (value) => set({ duration: value }),

      toggleLikedTrack: (trackId) =>
        set((state) => {
          const alreadyLiked = state.likedTrackIds.includes(trackId)

          return {
            likedTrackIds: alreadyLiked
              ? state.likedTrackIds.filter((id) => id !== trackId)
              : [...state.likedTrackIds, trackId],
          }
        }),

      reset: () =>
        set({
          currentTrack: null,
          isPlaying: false,
          currentTime: 0,
          duration: 0,
        }),
    }),
    {
      name: 'player-store',
      partialize: (state) => ({
        currentTrack: state.currentTrack,
        isPlaying: state.isPlaying,
        currentTime: state.currentTime,
        duration: state.duration,
        likedTrackIds: state.likedTrackIds,
      }),
    }
  )
)