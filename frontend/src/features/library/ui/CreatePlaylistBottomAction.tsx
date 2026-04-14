import type { ReactNode } from 'react'

type Props = {
  children: ReactNode
  className?: string
  mode?: 'static' | 'sticky'
  bottomOffset?: number
}

export function CreatePlaylistBottomAction({
  children,
  className = '',
  mode = 'static',
  bottomOffset = 12,
}: Props) {
  if (mode === 'sticky') {
    return (
      <div
        className={`sticky bottom-0 left-0 w-full bg-gradient-to-t from-groov-bg via-groov-bg/90 to-transparent pt-[12px] ${className}`}
        style={{
          paddingBottom: `calc(env(safe-area-inset-bottom) + ${bottomOffset}px)`,
        }}
      >
        {children}
      </div>
    )
  }

  return (
    <div
      className={`mt-auto ${className}`}
      style={{
        paddingBottom: `calc(env(safe-area-inset-bottom) + ${bottomOffset}px)`,
      }}
    >
      {children}
    </div>
  )
}