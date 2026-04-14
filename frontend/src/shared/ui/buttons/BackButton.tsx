'use client'

import { useRouter } from 'next/navigation'
import { BackIcon } from '@/shared/ui/icons/BackIcon'

type BackButtonProps = {
  className?: string
  fallbackHref?: string
}

export function BackButton({
  className,
  fallbackHref = '/search',
}: BackButtonProps) {
  const router = useRouter()

  const handleBack = () => {
    if (window.history.length > 1) {
      router.back()
      return
    }

    router.push(fallbackHref)
  }

  return (
    <button
      type="button"
      onClick={handleBack}
      className={className}
      aria-label="Назад"
    >
      <BackIcon className="h-[14px] w-[16px]" />
    </button>
  )
}