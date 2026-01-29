import * as React from 'react'
import { cn } from '@/lib/cn'

export function Label({
  className,
  ...props
}: React.LabelHTMLAttributes<HTMLLabelElement> & { className?: string }) {
  return <label className={cn('text-sm text-neutral-200', className)} {...props} />
}
