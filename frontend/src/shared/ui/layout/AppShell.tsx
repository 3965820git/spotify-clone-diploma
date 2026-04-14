'use client'

import type { ReactNode } from 'react'
import { BottomNav } from '@/shared/ui/BottomNav'
import { useAutoPageMode } from '@/shared/lib/hooks/useAutoPageMode'

type Props = {
  children: ReactNode
  mobileMaxWidth?: number
  contentClassName?: string
  withDefaultPadding?: boolean
  withSafeAreaTop?: boolean
  withSafeAreaBottom?: boolean
  withBottomNavSpacing?: boolean
  pageMode?: 'scroll' | 'screen' | 'auto'
  mobileTopOffset?: number
}

export function AppShell({
  children,
  mobileMaxWidth = 402,
  contentClassName = '',
  withDefaultPadding = true,
  withSafeAreaTop = true,
  withSafeAreaBottom = true,
  withBottomNavSpacing = true,
  pageMode = 'auto',
  mobileTopOffset = 50,
}: Props) {
  const topPadding = withSafeAreaTop
    ? `max(env(safe-area-inset-top), ${mobileTopOffset}px)`
    : '0px'

  const baseBottomPadding = withSafeAreaBottom
    ? 'max(env(safe-area-inset-bottom), 12px)'
    : '0px'

  const bottomNavPadding = withBottomNavSpacing ? '84px' : '0px'

  const { measureRef, resolvedMode } = useAutoPageMode(pageMode === 'auto')

  const actualPageMode = pageMode === 'auto' ? resolvedMode : pageMode
  const isScreen = actualPageMode === 'screen'

  return (
    <div className="bg-groov-bg text-groov-accent">
      {pageMode === 'auto' && (
        <div className="pointer-events-none absolute left-0 top-0 -z-10 w-full opacity-0">
          <div className="mx-auto flex w-full max-w-[1440px]">
            <aside className="hidden w-[260px] shrink-0 lg:flex" />
            <main className="min-w-0 flex-1">
              <div
                ref={measureRef}
                className={`mx-auto w-full ${
                  withDefaultPadding ? 'px-4' : 'px-0'
                } md:max-w-[720px] md:px-6 lg:max-w-[1100px] lg:px-8 ${contentClassName}`}
                style={{
                  maxWidth: `${mobileMaxWidth}px`,
                  paddingTop: topPadding,
                  paddingBottom: `calc(${baseBottomPadding} + ${bottomNavPadding})`,
                }}
              >
                {children}
              </div>
            </main>
          </div>
        </div>
      )}

      <div className={isScreen ? 'h-dvh overflow-hidden' : 'min-h-dvh'}>
        <div
          className={`mx-auto flex w-full max-w-[1440px] ${
            isScreen ? 'h-dvh overflow-hidden' : 'min-h-dvh'
          }`}
        >
          <aside className="hidden w-[260px] shrink-0 border-r border-groov-accent/10 px-6 py-8 lg:flex">
            <div className="text-[24px] font-semibold tracking-[-0.03em] text-groov-accent">
              GROOV
            </div>
          </aside>

          <main
            className={`min-w-0 flex-1 ${
              isScreen ? 'h-full overflow-hidden' : ''
            }`}
          >
            <div
              className={`mx-auto w-full ${
                isScreen ? 'h-full overflow-hidden' : ''
              } ${
                withDefaultPadding ? 'px-4' : 'px-0'
              } md:max-w-[720px] md:px-6 lg:max-w-[1100px] lg:px-8 ${contentClassName}`}
              style={{
                maxWidth: `${mobileMaxWidth}px`,
                paddingTop: topPadding,
                paddingBottom: `calc(${baseBottomPadding} + ${bottomNavPadding})`,
              }}
            >
              {children}
            </div>
          </main>
        </div>
      </div>

           {withBottomNavSpacing && (
        <div className="lg:hidden">
          <BottomNav />
        </div>
      )}
    </div>
  )
}