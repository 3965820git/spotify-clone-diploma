'use client'

import type { ThemeOption } from '@/features/settings/model/settings.types'
import { getSettingsIcon } from '@/features/settings/lib/getSettingsIcon'

type Props = {
  option: ThemeOption
}

export function ThemeOptionCard({ option }: Props) {
  const Icon = getSettingsIcon(option.icon)

  return (
    <button
      type="button"
      className={[
        'flex h-[97px] flex-1 flex-col items-center justify-center rounded-[14px] border border-transparent',
        option.active ? 'bg-[#4E6A8F]' : 'bg-[#F0EEE9] text-groov-textDark',
      ].join(' ')}
    >
      <Icon
        className={[
          'h-[30px] w-[30px]',
          option.active ? 'text-groov-accent' : 'text-groov-textDark',
        ].join(' ')}
      />
      <span
        className={[
          'mt-[12px] text-[16px] font-medium leading-[16px]',
          option.active ? 'text-groov-accent' : 'text-groov-textDark',
        ].join(' ')}
      >
        {option.label}
      </span>
    </button>
  )
}