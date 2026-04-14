import { create } from 'zustand'
import type {
  PlaybackQueueResponse,
  PlaybackSession,
} from '@/entities/playback/model/types'

type PlayerStore = {
  playback: PlaybackSession | null
  queue: PlaybackQueueResponse | null
  currentStreamUrl: string | null
  currentTrackId: string | null
  startPositionMs: number | null
  currentTimeSec: number
  durationSec: number
  isLoading: boolean
  isPlayerReady: boolean
  isBlockedByAnotherDevice: boolean

  setPlayback: (playback: PlaybackSession | null) => void
  setQueue: (queue: PlaybackQueueResponse | null) => void
  setCurrentStreamUrl: (url: string | null) => void
  setCurrentTrackId: (trackId: string | null) => void
  setStartPositionMs: (value: number | null) => void
  setCurrentTimeSec: (value: number) => void
  setDurationSec: (value: number) => void
  setIsLoading: (value: boolean) => void
  setIsPlayerReady: (value: boolean) => void
  setIsBlockedByAnotherDevice: (value: boolean) => void

  resetPlayerState: () => void
}

export const usePlayerStore = create<PlayerStore>((set) => ({
  playback: null,
  queue: null,
  currentStreamUrl: null,
  currentTrackId: null,
  startPositionMs: null,
  currentTimeSec: 0,
  durationSec: 0,
  isLoading: false,
  isPlayerReady: false,
  isBlockedByAnotherDevice: false,

  setPlayback: (playback) => set({ playback }),
  setQueue: (queue) => set({ queue }),
  setCurrentStreamUrl: (currentStreamUrl) => set({ currentStreamUrl }),
  setCurrentTrackId: (currentTrackId) => set({ currentTrackId }),
  setStartPositionMs: (startPositionMs) => set({ startPositionMs }),
  setCurrentTimeSec: (currentTimeSec) => set({ currentTimeSec }),
  setDurationSec: (durationSec) => set({ durationSec }),
  setIsLoading: (isLoading) => set({ isLoading }),
  setIsPlayerReady: (isPlayerReady) => set({ isPlayerReady }),
  setIsBlockedByAnotherDevice: (isBlockedByAnotherDevice) =>
    set({ isBlockedByAnotherDevice }),

  resetPlayerState: () =>
    set({
      playback: null,
      queue: null,
      currentStreamUrl: null,
      currentTrackId: null,
      startPositionMs: null,
      currentTimeSec: 0,
      durationSec: 0,
      isLoading: false,
      isPlayerReady: false,
      isBlockedByAnotherDevice: false,
    }),
}))