'use client'

import { useEffect, useRef, useState } from 'react'
import { adminTabs, type AdminTabId } from '@/features/admin/model/admin-tabs'

type Props = {
  activeTab: AdminTabId
  onChange: (tab: AdminTabId) => void
}

export function AdminTabs({ activeTab, onChange }: Props) {
  const [indicatorStyle, setIndicatorStyle] = useState({
    left: 0,
    width: 0,
  })

  const containerRef = useRef<HTMLDivElement | null>(null)
  const buttonRefs = useRef<Record<string, HTMLButtonElement | null>>({})

  useEffect(() => {
    const updateIndicator = () => {
      const container = containerRef.current
      const activeButton = buttonRefs.current[activeTab]

      if (!container || !activeButton) {
        return
      }

      const containerRect = container.getBoundingClientRect()
      const activeRect = activeButton.getBoundingClientRect()

      setIndicatorStyle({
        left: activeRect.left - containerRect.left,
        width: activeRect.width,
      })
    }

    updateIndicator()
    window.addEventListener('resize', updateIndicator)

    return () => {
      window.removeEventListener('resize', updateIndicator)
    }
  }, [activeTab])

  return (
    <div className="mt-[18px]">
      <div
        ref={containerRef}
        className="flex items-center gap-[18px] overflow-x-auto whitespace-nowrap"
      >
        {adminTabs.map((tab) => (
          <button
            key={tab.id}
            ref={(element) => {
              buttonRefs.current[tab.id] = element
            }}
            type="button"
            onClick={() => onChange(tab.id)}
            className={[
              'shrink-0 text-[16px] leading-[19px]',
              activeTab === tab.id
                ? 'font-medium text-groov-accent'
                : 'text-groov-accent/90',
            ].join(' ')}
          >
            {tab.label}
          </button>
        ))}
      </div>

      <div className="relative mt-[8px] w-full">
        <div className="h-[1px] w-full bg-groov-accent" />

        <span
          className="absolute top-1/2 h-[3px] -translate-y-1/2 rounded-full bg-groov-accent transition-all duration-300"
          style={{
            left: `${indicatorStyle.left}px`,
            width: `${indicatorStyle.width}px`,
          }}
        />
      </div>
    </div>
  )
}