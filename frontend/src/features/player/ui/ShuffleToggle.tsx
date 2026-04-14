'use client'

import { usePlayerControls } from '@/features/player/lib/usePlayerControls'
import { IconButton } from '@/shared/ui/buttons/IconButton'
import { ShuffleIcon } from '@/shared/ui/icons/ShuffleIcon'

export function ShuffleToggle() {
  const { isShuffled, handleToggleShuffle } = usePlayerControls()

  return (
    <IconButton
      type="button"
      onClick={() => { void handleToggleShuffle() }}
      className="h-[28px] w-[28px] text-groov-accent"
    >
      <ShuffleIcon
        className="h-[24px] w-[24px]"
        isActive={isShuffled}
      />
    </IconButton>
  )
}
