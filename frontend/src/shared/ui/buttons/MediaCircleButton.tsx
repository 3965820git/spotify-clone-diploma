import { cn } from '@/shared/lib/cn'

type Props = React.ButtonHTMLAttributes<HTMLButtonElement>

export function MediaCircleButton({ className, ...props }: Props) {
  return (
    <button
      {...props}
      className={cn(
        'flex h-[40px] w-[40px] items-center justify-center rounded-[18px]',
        'bg-[#F0EEE9] text-[#041C32]',
        'transition active:scale-95',
        className
      )}
    />
  )
}