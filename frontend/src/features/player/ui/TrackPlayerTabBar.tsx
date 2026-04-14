'use client'

import type { PlayerTab } from '@/features/player/model/player-screen.types'

type TabButtonProps = {
  label: string
  isActive: boolean
  onClick: () => void
}

function TabButton({ label, isActive, onClick }: TabButtonProps) {
  return (
    <button
      type="button"
      onClick={onClick}
      className={[
        'flex h-[44px] items-center justify-center text-center text-[18px] leading-[22px] transition-all duration-300 [transition-timing-function:cubic-bezier(0.25,0.1,0.25,1)]',
        isActive
          ? 'font-medium text-groov-accent'
          : 'font-normal text-groov-accent',
      ].join(' ')}
    >
      {label}
    </button>
  )
}

type Props = {
  activeTab: PlayerTab
  lyricsMode?: boolean
  onNextClick: () => void
  onLyricsClick: () => void
  onRelatedClick: () => void
}

export function TrackPlayerTabBar({
  activeTab,
  lyricsMode = false,
  onNextClick,
  onLyricsClick,
  onRelatedClick,
}: Props) {
  const indicatorLeft = lyricsMode
    ? '33.333%'
    : activeTab === 'next'
      ? '0%'
      : activeTab === 'related'
        ? '66.666%'
        : '0%'

  const indicatorOpacity = lyricsMode
    ? 1
    : activeTab === 'next' || activeTab === 'related'
      ? 1
      : 0

  return (
    <div className="mt-[18px]">
      <div className="grid grid-cols-3">
        <TabButton
          label="Далі"
          isActive={lyricsMode ? false : activeTab === 'next'}
          onClick={onNextClick}
        />
        <TabButton
          label="Текст"
          isActive={lyricsMode}
          onClick={onLyricsClick}
        />
        <TabButton
          label="Схожі"
          isActive={lyricsMode ? false : activeTab === 'related'}
          onClick={onRelatedClick}
        />
      </div>

      <div className="relative h-[2px] w-full bg-groov-accent/40">
        <span
          className="absolute top-0 h-full w-[33.333%] rounded-full bg-groov-accent transition-all duration-300 [transition-timing-function:cubic-bezier(0.25,0.1,0.25,1)]"
          style={{
            left: indicatorLeft,
            opacity: indicatorOpacity,
          }}
        />
      </div>
    </div>
  )
}