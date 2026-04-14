type Props = {
  label: string
  onClick?: () => void
}

export function AdvancedFilterChip({ label, onClick }: Props) {
  return (
    <button
      type="button"
      onClick={onClick}
      className="
        flex h-[70px] w-full items-center justify-center
        rounded-[12px]
        bg-groov-surface
        text-center

        text-[18px] font-normal leading-[22px]
        tracking-[-0.01em]

        text-groov-accent
        transition hover:bg-groov-primary/80
      "
    >
      <span className="line-clamp-2 px-2">
        {label}
      </span>
    </button>
  )
}