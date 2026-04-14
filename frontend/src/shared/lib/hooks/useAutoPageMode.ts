'use client'

import { useEffect, useLayoutEffect, useRef, useState } from 'react'

type ResolvedPageMode = 'scroll' | 'screen'

export function useAutoPageMode(enabled: boolean) {
  const measureRef = useRef<HTMLDivElement | null>(null)
  const [resolvedMode, setResolvedMode] = useState<ResolvedPageMode>('screen')

  const measure = () => {
    const element = measureRef.current
    if (!element) return

    const viewportHeight = window.innerHeight
    const contentHeight = element.scrollHeight

    setResolvedMode(contentHeight > viewportHeight ? 'scroll' : 'screen')
  }

  useLayoutEffect(() => {
    if (!enabled) return
    measure()
  }, [enabled])

  useEffect(() => {
    if (!enabled) return

    const element = measureRef.current
    if (!element) return

    const resizeObserver = new ResizeObserver(() => {
      measure()
    })

    resizeObserver.observe(element)
    window.addEventListener('resize', measure)

    return () => {
      resizeObserver.disconnect()
      window.removeEventListener('resize', measure)
    }
  }, [enabled])

  return {
    measureRef,
    resolvedMode,
  }
}