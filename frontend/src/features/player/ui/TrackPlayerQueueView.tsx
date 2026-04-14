'use client'

import { IconButton } from '@/shared/ui/buttons/IconButton'
import { MoreIcon } from '@/shared/ui/icons/MoreIcon'
import { ChevronDownIcon } from '@/shared/ui/icons/ChevronDownIcon'
import { MenuLinesIcon } from '@/shared/ui/icons/MenuLinesIcon'
import { TrackPlayToggle } from '@/features/player/ui/TrackPlayToggle'
import { TrackPlayerTabBar } from '@/features/player/ui/TrackPlayerTabBar'
import type { QueueTrackItem } from '@/features/player/model/player-screen.types'

function QueueRow({
  track,
  isCurrent,
  onOpenOptions,
}: {
  track: QueueTrackItem
  isCurrent: boolean
  onOpenOptions: (trackId: string) => void
}) {
  const artistLabel =
    track.mainArtists?.length > 0 ? track.mainArtists.join(', ') : 'Unknown artist'

  return (
    <div
      className={[
        'flex items-center gap-[12px] px-[14px] py-[10px]',
        isCurrent ? 'bg-groov-primary/80' : 'bg-transparent',
      ].join(' ')}
    >
      <div className="flex h-[50px] w-[50px] shrink-0 items-center justify-center overflow-hidden rounded-[4px] bg-groov-surface text-groov-muted">
        ♪
      </div>

      <div className="min-w-0 flex-1">
        <p className="truncate text-[16px] leading-[1.2] text-groov-accent">
          {track.title}
        </p>
        <p className="mt-[6px] truncate text-[14px] leading-[1.2] text-groov-accent/85">
          {artistLabel}
        </p>
      </div>

      <button
        type="button"
        onClick={() => onOpenOptions(track.id)}
        className="flex h-[24px] w-[24px] shrink-0 items-center justify-center text-groov-accent"
        aria-label="Опції треку"
      >
        <MenuLinesIcon className="h-[24px] w-[24px]" />
      </button>
    </div>
  )
}

type Props = {
  currentTrack: QueueTrackItem
  playbackContextExternalId: string | null
  tracks: QueueTrackItem[]
  onBack: () => void
  onClose: () => void
  onOpenOptions: (trackId: string) => void
  onOpenLyrics: () => void
  onOpenRelated: () => void
}

export function TrackPlayerQueueView({
  currentTrack,
  playbackContextExternalId,
  tracks,
  onBack,
  onClose,
  onOpenOptions,
  onOpenLyrics,
  onOpenRelated,
}: Props) {
  const artistLabel =
    currentTrack.mainArtists?.length > 0
      ? currentTrack.mainArtists.join(', ')
      : 'Unknown artist'

  return (
    <div className="flex min-h-[100dvh] flex-col overflow-hidden bg-groov-bg text-groov-text">
      <div className="flex items-center justify-between px-[16px] pt-[6px]">
        <IconButton
          aria-label="Назад"
          onClick={onBack}
          className="flex h-[24px] w-[24px] items-center justify-center p-0 text-groov-accent"
        >
          <ChevronDownIcon className="h-[16px] w-[16px]" />
        </IconButton>

        <IconButton
          aria-label="Меню"
          onClick={() => onOpenOptions(currentTrack.id)}
          className="flex h-[24px] w-[24px] items-center justify-center p-0 text-groov-accent"
        >
          <MoreIcon />
        </IconButton>
      </div>

      <div className="flex min-h-0 flex-1 flex-col px-[16px] pt-[14px]">
        <div className="flex items-center justify-between">
          <div className="flex min-w-0 items-center gap-[12px]">
            <div className="flex h-[52px] w-[52px] shrink-0 items-center justify-center overflow-hidden rounded-[4px] bg-groov-surface text-groov-muted">
              ♪
            </div>

            <div className="min-w-0">
              <p className="truncate text-[17px] font-normal leading-[1.2] text-groov-accent">
                {currentTrack.title}
              </p>
              <p className="mt-[4px] truncate text-[14px] leading-[1.2] text-groov-muted">
                {artistLabel}
              </p>
            </div>
          </div>

          <div className="ml-[12px] flex items-center gap-[18px]">
            <TrackPlayToggle
              trackId={currentTrack.id}
              source="player"
              contextExternalId={playbackContextExternalId}
              variant="circle"
              size="sm"
            />
          </div>
        </div>

        <TrackPlayerTabBar
          activeTab="next"
          onNextClick={() => {}}
          onLyricsClick={onOpenLyrics}
          onRelatedClick={onOpenRelated}
        />

        <div className="min-h-0 flex-1 overflow-y-auto pb-[max(env(safe-area-inset-bottom),18px)]">
          <div className="mt-[12px] overflow-hidden rounded-[2px]">
            <QueueRow
              track={currentTrack}
              isCurrent
              onOpenOptions={onOpenOptions}
            />

            {tracks.map((track) => (
              <QueueRow
                key={track.id}
                track={track}
                isCurrent={false}
                onOpenOptions={onOpenOptions}
              />
            ))}
          </div>

          <button
            type="button"
            className="mt-[18px] w-full py-[10px] text-center text-[20px] leading-[1.2] text-groov-accent"
          >
            Завантажити ще
          </button>
        </div>
      </div>
    </div>
  )
}