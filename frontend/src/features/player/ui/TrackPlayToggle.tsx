'use client'

import { useMemo } from 'react'
import { usePlayerStore } from '@/features/player/model/usePlayerStore'
import { usePlaybackActions } from '@/features/player/api/usePlaybackActions'
import { PlayButtonSurface } from '@/shared/ui/buttons/PlayButtonSurface'
import { PlayIcon } from '@/shared/ui/icons/PlayIcon'
import { PauseIcon } from '@/shared/ui/icons/PauseIcon'
import type { PlaybackContextType } from '@/entities/playback/model/types'

type Variant = 'circle' | 'round'
type Size = 'xs' | 'sm' | 'md' | 'lg'
type Source = PlaybackContextType | 'player' | null

type Props = {
  trackId: string
  source?: Source
  contextExternalId?: string | null
  variant?: Variant
  size?: Size
  className?: string
  disabled?: boolean
}

const iconSizes: Record<Size, string> = {
  xs: 'h-[12px] w-[12px]',
  sm: 'h-[14px] w-[14px]',
  md: 'h-[18px] w-[18px]',
  lg: 'h-[34px] w-[34px]',
}

export function TrackPlayToggle({
  trackId,
  source = null,
  contextExternalId = null,
  variant = 'circle',
  size = 'md',
  className = '',
  disabled = false,
}: Props) {
  const currentTrackId = usePlayerStore((state) => state.currentTrackId)
  const playback = usePlayerStore((state) => state.playback)
  const isLoading = usePlayerStore((state) => state.isLoading)
  const isBlockedByAnotherDevice = usePlayerStore(
    (state) => state.isBlockedByAnotherDevice
  )

  const {
    startPlaybackMutation,
    pausePlaybackMutation,
    resumePlaybackMutation,
  } = usePlaybackActions()

  const isCurrentTrack = currentTrackId === trackId
  const isPlaying = playback?.isPlaying ?? false
  const isActive = isCurrentTrack && isPlaying

  const contextType: PlaybackContextType = useMemo(() => {
    if (source === 'playlist') return 'playlist'
    if (source === 'album') return 'album'
    return 'search'
  }, [source])

  const isPending =
    startPlaybackMutation.isPending ||
    pausePlaybackMutation.isPending ||
    resumePlaybackMutation.isPending

  const isDisabled =
    disabled || isLoading || isBlockedByAnotherDevice || isPending || !trackId

  const handleClick = async () => {
    if (isDisabled) return

    if (isCurrentTrack) {
      if (isPlaying) {
        await pausePlaybackMutation.mutateAsync()
        return
      }

      await resumePlaybackMutation.mutateAsync()
      return
    }

    await startPlaybackMutation.mutateAsync({
      contextType,
      contextExternalId,
      startTrackId: trackId,
    })
  }

  return (
    <PlayButtonSurface
      onClick={handleClick}
      aria-label={isActive ? 'Пауза' : 'Відтворити'}
      variant={variant}
      size={size}
      className={className}
      disabled={isDisabled}
    >
      {isActive ? (
        <PauseIcon className={iconSizes[size]} />
      ) : (
        <PlayIcon className={iconSizes[size]} />
      )}
    </PlayButtonSurface>
  )
}