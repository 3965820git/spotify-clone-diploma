'use client'

import { IconButton } from '@/shared/ui/buttons/IconButton'
import { MoreIcon } from '@/shared/ui/icons/MoreIcon'
import { ChevronDownIcon } from '@/shared/ui/icons/ChevronDownIcon'
import { TrackPlayToggle } from '@/features/player/ui/TrackPlayToggle'
import { TrackPlayerTabBar } from '@/features/player/ui/TrackPlayerTabBar'

type Props = {
  trackId: string
  title: string
  artistLabel: string
  lyrics: string[] | null
  contextExternalId: string | null
  onClose: () => void
  onOpenOptions: () => void
  onOpenNext: () => void
  onOpenRelated: () => void
}

function LyricsContent({ lyrics }: { lyrics: string[] | null }) {
  if (!lyrics) {
    return (
      <div className="px-[2px] pt-[18px] text-[16px] leading-[1.5] text-groov-muted">
        Текст недоступний
      </div>
    )
  }

  return (
    <div className="space-y-[10px] px-[2px] pt-[18px] text-[16px] leading-[1.35] text-groov-accent">
      {lyrics.map((line, index) =>
        line === '' ? (
          <div key={index} className="h-[14px]" />
        ) : (
          <p key={index}>{line}</p>
        )
      )}
    </div>
  )
}

export function TrackPlayerLyricsView({
  trackId,
  title,
  artistLabel,
  lyrics,
  contextExternalId,
  onClose,
  onOpenOptions,
  onOpenNext,
  onOpenRelated,
}: Props) {
  return (
    <div className="flex min-h-[100dvh] flex-col overflow-hidden bg-groov-bg text-groov-text">
      <div className="flex items-center justify-between px-[16px] pt-[6px]">
        <IconButton
          aria-label="Закрити плеєр"
          onClick={onClose}
          className="flex h-[24px] w-[24px] items-center justify-center p-0 text-groov-accent"
        >
          <ChevronDownIcon className="h-[16px] w-[16px]" />
        </IconButton>

        <IconButton
          aria-label="Меню"
          onClick={onOpenOptions}
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
                {title}
              </p>
              <p className="mt-[4px] truncate text-[14px] leading-[1.2] text-groov-muted">
                {artistLabel}
              </p>
            </div>
          </div>

          <div className="ml-[12px] flex items-center gap-[18px]">
            <TrackPlayToggle
              trackId={trackId}
              source="player"
              contextExternalId={contextExternalId}
              variant="circle"
              size="sm"
            />
          </div>
        </div>

        <TrackPlayerTabBar
          activeTab="lyrics"
          lyricsMode
          onNextClick={onOpenNext}
          onLyricsClick={() => {}}
          onRelatedClick={onOpenRelated}
        />

        <div className="min-h-0 flex-1 overflow-y-auto pb-[max(env(safe-area-inset-bottom),18px)]">
          <LyricsContent lyrics={lyrics} />
        </div>
      </div>
    </div>
  )
}