'use client'

import { MediaCircleButton } from '@/shared/ui/buttons/MediaCircleButton'
import { PlayIcon } from '@/shared/ui/icons/PlayIcon'
import type { PlaybackContextType } from '@/entities/playback/model/types'
import { usePlayTrack } from '@/features/player/lib/usePlayTrack'

type Props = {
  trackId: string
  contextType: PlaybackContextType
  contextExternalId: string | null
  className?: string
}

export function PlayTrackButton({
  trackId,
  contextType,
  contextExternalId,
  className,
}: Props) {
  const { playTrack, isPending } = usePlayTrack()

  const handleClick = async () => {
    await playTrack({
      trackId,
      contextType,
      contextExternalId,
    })
  }

  return (
    <MediaCircleButton
      onClick={handleClick}
      disabled={isPending}
      className={className}
    >
      <PlayIcon className="h-5 w-5" />
    </MediaCircleButton>
  )
}