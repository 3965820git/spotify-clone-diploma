'use client'

import { Bell } from 'lucide-react'

export function HomeTopBar() {
  return (
    <div className="flex items-center justify-between lg:pt-4">
      <div className="text-[20px] font-semibold leading-[100%] tracking-[-0.03em] text-groov-accent lg:hidden">
        GROOV
      </div>

      <div className="hidden text-[28px] font-semibold leading-[100%] tracking-[-0.03em] text-groov-accent lg:block">
        Головна
      </div>

      <button
        type="button"
        className="grid h-[24px] w-[24px] place-items-center text-groov-accent md:h-[28px] md:w-[28px]"
        aria-label="Notifications"
      >
        <Bell size={20} strokeWidth={2} />
      </button>
    </div>
  )
}