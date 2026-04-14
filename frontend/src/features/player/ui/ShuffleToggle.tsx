'use client'

import { usePlayerStore } from '@/features/player/model/usePlayerStore'
import { IconButton } from '@/shared/ui/buttons/IconButton'
import { ShuffleIcon } from '@/shared/ui/icons/ShuffleIcon'

export function ShuffleToggle() {
  const shuffle = usePlayerStore((state) => state.shuffle)
  const toggleShuffle = usePlayerStore((state) => state.toggleShuffle)

  return (
    <IconButton
      type="button"
      onClick={toggleShuffle}
      className="h-[28px] w-[28px] text-groov-accent"
    >
      <ShuffleIcon
        className="h-[24px] w-[24px]"
        isActive={shuffle}
      />
    </IconButton>
  )
}