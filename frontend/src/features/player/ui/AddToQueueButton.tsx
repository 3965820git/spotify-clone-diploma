'use client'

import { useAddToQueue } from '@/features/player/lib/useAddToQueue'

type Props = {
  trackId: string
  className?: string
}

export function AddToQueueButton({ trackId, className }: Props) {
  const { addToQueue, isPending } = useAddToQueue()

  const handleClick = async () => {
    await addToQueue(trackId)
  }

  return (
    <button
      type="button"
      onClick={handleClick}
      disabled={isPending}
      className={className}
    >
      Add to queue
    </button>
  )
}