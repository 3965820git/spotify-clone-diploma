'use client'

import { HeartIcon } from '@/shared/ui/icons/HeartIcon'
import { ShuffleIcon } from '@/shared/ui/icons/ShuffleIcon'
import { NextIcon } from '@/shared/ui/icons/NextIcon'
import { PrevIcon } from '@/shared/ui/icons/PrevIcon'
import { RepeatIcon } from '@/shared/ui/icons/RepeatIcon'
import { TrackPlayToggle } from '@/features/player/ui/TrackPlayToggle'
import { TrackPlayerTabBar } from '@/features/player/ui/TrackPlayerTabBar'

type Props = {
  title: string
  artistLabel: string
  trackId: string
  contextExternalId: string | null
  isLiked: boolean
  isHeartAnimating: boolean
  currentTimeSec: number
  durationSec: number
  formattedCurrentTime: string
  formattedDuration: string
  progressPercent: number
  isShuffled: boolean
  repeatMode: string
  isDisabled: boolean
  activeTab: 'next' | 'lyrics' | 'related' | null
  onToggleLike: () => void
  onSeek: (clientX: number) => Promise<void>
  onToggleShuffle: () => void
  onPrevious: () => void
  onNext: () => void
  onToggleRepeat: () => void
  onOpenNext: () => void
  onOpenLyrics: () => void
  onOpenRelated: () => void
  nextTracksLabel: string
}

export function TrackPlayerMainView({
  title,
  artistLabel,
  trackId,
  contextExternalId,
  isLiked,
  isHeartAnimating,
  formattedCurrentTime,
  formattedDuration,
  progressPercent,
  isShuffled,
  repeatMode,
  isDisabled,
  activeTab,
  onToggleLike,
  onSeek,
  onToggleShuffle,
  onPrevious,
  onNext,
  onToggleRepeat,
  onOpenNext,
  onOpenLyrics,
  onOpenRelated,
  nextTracksLabel,
}: Props) {
  return (
    <div className="px-[16px] pt-[24px]">
      <div className="overflow-hidden rounded-[24px]">
        <div className="flex h-[340px] w-full items-center justify-center bg-groov-surface text-groov-muted">
          Album cover
        </div>
      </div>

      <div className="mt-[20px] flex items-start justify-between">
        <div className="min-w-0">
          <h1 className="max-w-[280px] truncate text-[23px] font-normal leading-[1.15] text-groov-accent">
            {title}
          </h1>

          <p className="mt-[10px] max-w-[280px] truncate text-[16px] font-normal leading-[1.2] text-groov-muted">
            {artistLabel}
          </p>
        </div>

        <button
          type="button"
          aria-label={isLiked ? 'Прибрати з улюблених' : 'Додати в улюблені'}
          onClick={onToggleLike}
          className="mt-[3px] flex h-[24px] w-[24px] items-center justify-center"
        >
          <HeartIcon
            filled={isLiked}
            className={[
              'h-[24px] w-[24px] origin-center text-groov-accent transition-all duration-200 ease-out',
              isHeartAnimating ? 'animate-heart-pop' : '',
            ].join(' ')}
          />
        </button>
      </div>

      <div className="mt-[30px]">
        <div
          id="track-player-progress"
          role="slider"
          aria-label="Track progress"
          tabIndex={0}
          className="relative h-[10px] w-full cursor-pointer"
          onClick={(e) => {
            void onSeek(e.clientX)
          }}
        >
          <div className="absolute top-1/2 h-[4px] w-full -translate-y-1/2 rounded-full bg-groov-primary/70" />
          <div
            className="absolute top-1/2 h-[4px] -translate-y-1/2 rounded-full bg-groov-secondary"
            style={{ width: `${progressPercent}%` }}
          />
          <div
            className="absolute top-1/2 h-[7px] w-[7px] -translate-y-1/2 rounded-full bg-groov-secondary"
            style={{ left: `calc(${progressPercent}% - 3.5px)` }}
          />
        </div>

        <div className="mt-[20px] flex items-center justify-between text-[16px] font-normal leading-none text-groov-accent">
          <span className="min-w-[52px] text-left">{formattedCurrentTime}</span>
          <span className="min-w-[52px] text-right">{formattedDuration}</span>
        </div>
      </div>

      <div className="mt-[40px] flex items-center justify-between px-[1px]">
        <button
          type="button"
          aria-label="Shuffle"
          onClick={onToggleShuffle}
          disabled={isDisabled}
          className={[
            'flex h-[24px] w-[24px] items-center justify-center text-groov-accent',
            isShuffled ? 'opacity-100' : 'opacity-60',
            isDisabled ? 'pointer-events-none opacity-40' : '',
          ].join(' ')}
        >
          <ShuffleIcon />
        </button>

        <button
          type="button"
          aria-label="Previous"
          onClick={onPrevious}
          disabled={isDisabled}
          className={[
            'flex h-[24px] w-[24px] items-center justify-center text-groov-accent',
            isDisabled ? 'pointer-events-none opacity-40' : '',
          ].join(' ')}
        >
          <PrevIcon className="h-[24px] w-[24px]" />
        </button>

        <TrackPlayToggle
          trackId={trackId}
          source="player"
          contextExternalId={contextExternalId}
          variant="circle"
          size="lg"
        />

        <button
          type="button"
          aria-label="Next"
          onClick={onNext}
          disabled={isDisabled}
          className={[
            'flex h-[24px] w-[24px] items-center justify-center text-groov-accent',
            isDisabled ? 'pointer-events-none opacity-40' : '',
          ].join(' ')}
        >
          <NextIcon className="h-[24px] w-[24px]" />
        </button>

        <button
          type="button"
          aria-label="Repeat"
          onClick={onToggleRepeat}
          disabled={isDisabled}
          className={[
            'flex h-[24px] w-[24px] items-center justify-center text-groov-accent',
            repeatMode !== 'Off' ? 'opacity-100' : 'opacity-60',
            isDisabled ? 'pointer-events-none opacity-40' : '',
          ].join(' ')}
        >
          <RepeatIcon className="h-[24px] w-[24px]" />
        </button>
      </div>

      <div className="mt-auto pb-[max(env(safe-area-inset-bottom),16px)] pt-[80px]">
        <TrackPlayerTabBar
          activeTab={activeTab}
          onNextClick={onOpenNext}
          onLyricsClick={onOpenLyrics}
          onRelatedClick={onOpenRelated}
        />

        {activeTab === 'related' ? (
          <div className="px-[2px] pt-[18px] text-[16px] leading-[1.45] text-groov-muted">
            Схожі треки скоро з’являться
          </div>
        ) : null}

        {activeTab === null ? (
          <div className="px-[2px] pt-[18px] text-[16px] leading-[1.45] text-groov-muted">
            {nextTracksLabel}
          </div>
        ) : null}
      </div>
    </div>
  )
}