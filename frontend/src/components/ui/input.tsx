import * as React from 'react'
import { cn } from '@/lib/cn'

export const Input = React.forwardRef<
  HTMLInputElement,
  React.InputHTMLAttributes<HTMLInputElement> & { className?: string }
>(({ className, ...props }, ref) => {
  return (
    <input
      ref={ref}
      className={cn(
        'h-10 w-full rounded-md border border-neutral-800 bg-neutral-950 px-3 text-sm outline-none ring-offset-neutral-950 placeholder:text-neutral-500 focus:border-neutral-600',
        className
      )}
      {...props}
    />
  )
})
Input.displayName = 'Input'
