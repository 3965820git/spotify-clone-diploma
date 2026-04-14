'use client'

import { AdvancedFilterChip } from './AdvancedFilterChip'

type Props = {
  items: string[]
  onSelect?: (value: string) => void
}

export function AdvancedFiltersGrid({ items, onSelect }: Props) {
  return (
    <div className="grid grid-cols-2 gap-x-[12px] gap-y-[16px]">
      {items.map((item) => (
        <AdvancedFilterChip
          key={item}
          label={item}
          onClick={() => onSelect?.(item)}
        />
      ))}
    </div>
  )
}