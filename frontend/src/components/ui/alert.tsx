import * as React from 'react'
import { cn } from '@/lib/cn'

export function Alert({
  className,
  ...props
}: React.HTMLAttributes<HTMLDivElement> & { className?: string }) {
  return (
    <div
      role="alert"
      className={cn(
        'rounded-md border border-neutral-800 bg-neutral-900 px-4 py-3 text-sm text-neutral-100',
        className
      )}
      {...props}
    />
  )
}

export function AlertDescription({
  className,
  ...props
}: React.HTMLAttributes<HTMLParagraphElement> & { className?: string }) {
  return <p className={cn('text-neutral-100', className)} {...props} />
}
