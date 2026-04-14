'use client'

import { useRouter } from 'next/navigation'

function BackIcon() {
  return (
    <svg width="20" height="20" viewBox="0 0 24 24" fill="none">
      <path
        d="M15 6L9 12L15 18"
        stroke="currentColor"
        strokeWidth="2"
        strokeLinecap="round"
        strokeLinejoin="round"
      />
    </svg>
  )
}

type Props = {
  title?: string
}

export function AdvancedFiltersHeader({
  title = 'Розширені фільтри',
}: Props) {
  const router = useRouter()

  return (
    <div className="sticky top-0 z-20 -mx-4 h-[52px] bg-groov-surface">
      <div className="flex h-full items-center px-4">
        <button
          type="button"
          onClick={() => router.back()}
          className="mr-[14px] flex h-[20px] w-[20px] items-center justify-center text-groov-accent"
        >
          <BackIcon />
        </button>

        <h1 className="text-left text-[24px] font-normal leading-[22px] tracking-[-0.01em] text-groov-accent">
          {title}
        </h1>
      </div>
    </div>
  )
}