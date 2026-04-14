'use client'

import { useRouter } from 'next/navigation'
import { ChevronRightSmallIcon } from '@/shared/ui/icons/settings/ChevronRightSmallIcon'
import type { SettingsItem } from '@/features/settings/model/settings.types'
import { getSettingsIcon } from '@/features/settings/lib/getSettingsIcon'

type Props = {
  item: SettingsItem
}

export function SettingsListItem({ item }: Props) {
  const router = useRouter()
  const Icon = getSettingsIcon(item.icon)
  const isDanger = item.variant === 'danger'

  const handleClick = () => {
    if (item.href) {
      router.push(item.href)
    }
  }

  return (
    <button
      type="button"
      onClick={handleClick}
      className="flex w-full items-center gap-[12px] rounded-[18px] px-[12px] py-[14px] text-left"
    >
      <div
        className={[
          'flex h-[50px] w-[50px] shrink-0 items-center justify-center rounded-[16px]',
          isDanger ? 'bg-groov-accent' : 'bg-[#4E6A8F]',
        ].join(' ')}
      >
        <Icon
          className={[
            'h-[30px] w-[30px]',
            isDanger ? 'text-groov-textDark' : 'text-groov-accent',
          ].join(' ')}
        />
      </div>

      <div className="min-w-0 flex-1">
        <p className="truncate text-[16px] font-medium leading-[19px]">
          {item.title}
        </p>
        <p className="mt-[3px] truncate text-[12px] leading-[14px]">
          {item.subtitle}
        </p>
      </div>

      <div className="shrink-0 text-groov-accent">
        <ChevronRightSmallIcon className="h-[24px] w-[24px]" />
      </div>
    </button>
  )
}