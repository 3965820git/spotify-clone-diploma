import type { ButtonHTMLAttributes, ReactNode } from 'react'

type Variant = 'circle' | 'round'
type Size = 'xs' | 'sm' | 'md' | 'lg'

type Props = ButtonHTMLAttributes<HTMLButtonElement> & {
  children: ReactNode
  variant?: Variant
  size?: Size
}

const sizeClasses: Record<Size, string> = {
  xs: 'h-[24px] w-[24px]',
  sm: 'h-[28px] w-[28px]',
  md: 'h-[40px] w-[40px]',
  lg: 'h-[80px] w-[80px]',
}

const radiusClasses: Record<Variant, string> = {
  circle: 'rounded-full',
  round: 'rounded-[18px]',
}

export function PlayButtonSurface({
  children,
  variant = 'circle',
  size = 'md',
  className = '',
  type = 'button',
  ...props
}: Props) {
  return (
    <button
      type={type}
      className={`flex items-center justify-center bg-groov-accent text-groov-textDark transition duration-200 active:scale-[0.96] ${sizeClasses[size]} ${radiusClasses[variant]} ${className}`}
      {...props}
    >
      {children}
    </button>
  )
}