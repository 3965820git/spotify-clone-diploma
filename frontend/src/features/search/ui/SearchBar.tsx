'use client'

import { SearchIcon } from './icons'

type Props = {
  query: string
  onChange: (value: string) => void
}

export function SearchBar({ query, onChange }: Props) {
  return (
    <div className="relative w-full">
      <div className="pointer-events-none absolute left-4 top-1/2 -translate-y-1/2 text-[#5F6673]">
        <SearchIcon />
      </div>

      <input
        value={query}
        onChange={(e) => onChange(e.target.value)}
        placeholder="Шукати треки, альбоми, виконавців..."
        className="h-[44px] w-full rounded-[16px] bg-groov-accent pl-[42px] pr-4 text-[16px] leading-[20px] text-groov-textDark outline-none placeholder:text-[#5F6673]"
      />
    </div>
  )
}