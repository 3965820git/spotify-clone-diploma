import Link from 'next/link'
import type { MouseEventHandler } from 'react'

type Props = {
  href: string
  disabled?: boolean
  label?: string
  className?: string
  onClick?: MouseEventHandler<HTMLAnchorElement | HTMLButtonElement>
}

export function CreatePlaylistNextButton({
  href,
  disabled = false,
  label = 'Далі',
  className = '',
  onClick,
}: Props) {
  if (disabled) {
    return (
      <button
        type="button"
        disabled
        onClick={onClick}
        className={`h-[50px] w-full rounded-[14px] bg-groov-secondary text-[16px] font-medium text-groov-accent opacity-50 ${className}`}
      >
        {label}
      </button>
    )
  }

  return (
    <Link
      href={href}
      onClick={onClick as MouseEventHandler<HTMLAnchorElement>}
      className={`flex h-[50px] w-full items-center justify-center rounded-[14px] bg-groov-secondary text-[16px] font-medium text-groov-accent transition-all duration-200 active:opacity-80 ${className}`}
    >
      {label}
    </Link>
  )
}