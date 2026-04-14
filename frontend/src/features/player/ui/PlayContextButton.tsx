'use client'

import { MediaCircleButton } from '@/shared/ui/buttons/MediaCircleButton'
import { PlayIcon } from '@/shared/ui/icons/PlayIcon'
import type { PlaybackContextType } from '@/entities/playback/model/types'
import { usePlaybackActions } from '@/features/player/api/usePlaybackActions'

type Props = {
  contextType: PlaybackContextType
  contextExternalId: string | null
  startTrackId: string | null
  className?: string
}

export function PlayContextButton({
  contextType,
  contextExternalId,
  startTrackId,
  className,
}: Props) {
  const { startPlaybackMutation } = usePlaybackActions()

  const handleClick = async () => {
    await startPlaybackMutation.mutateAsync({
      contextType,
      contextExternalId,
      startTrackId,
    })
  }

  return (
    <MediaCircleButton
      onClick={handleClick}
      disabled={startPlaybackMutation.isPending}
      className={className}
    >
      <PlayIcon className="h-6 w-6" />
    </MediaCircleButton>
  )
}