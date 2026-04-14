'use client'

import type { PlaybackContextType } from '@/entities/playback/model/types'
import { PlayTrackButton } from '@/features/player/ui/PlayTrackButton'
import { AddToQueueButton } from '@/features/player/ui/AddToQueueButton'

type Props = {
  trackId: string
  contextType: PlaybackContextType
  contextExternalId: string | null
}

export function TrackPlaybackActions({
  trackId,
  contextType,
  contextExternalId,
}: Props) {
  return (
    <div className="flex items-center gap-2">
      <PlayTrackButton
        trackId={trackId}
        contextType={contextType}
        contextExternalId={contextExternalId}
      />
      <AddToQueueButton trackId={trackId} />
    </div>
  )
}