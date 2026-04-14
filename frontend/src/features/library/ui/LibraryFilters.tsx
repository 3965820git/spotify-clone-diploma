'use client'

import type { LibraryTab, LibraryTabId } from '../model/types'

type Props = {
  tabs: LibraryTab[]
  activeTab: LibraryTabId
  onChange: (tab: LibraryTabId) => void
}

export function LibraryFilters({ tabs, activeTab, onChange }: Props) {
  return (
    <div className="mt-[16px] flex gap-[8px] overflow-x-auto pb-[2px] scrollbar-none">
      {tabs.map((tab) => {
        const isActive = tab.id === activeTab

        return (
          <button
            key={tab.id}
            type="button"
            onClick={() => onChange(tab.id)}
            className={`shrink-0 rounded-[10px] px-[10px] py-[6px] text-[16px]  leading-[16px] transition-colors ${
              isActive
                ? 'bg-groov-secondary text-groov-accent'
                : 'bg-groov-primary/70 text-groov-accent'
            }`}
          >
            {tab.label}
          </button>
        )
      })}
    </div>
  )
}