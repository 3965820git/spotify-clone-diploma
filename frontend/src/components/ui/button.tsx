import * as React from 'react'
import { cn } from '@/lib/cn'

export function Button({
  className,
  ...props
}: React.ButtonHTMLAttributes<HTMLButtonElement> & { className?: string }) {
  return (
    <button
      className={cn(
        'inline-flex items-center justify-center rounded-md bg-white px-4 py-2 text-sm font-medium text-black hover:opacity-90 disabled:opacity-60',
        className
      )}
      {...props}
    />
  )
}
