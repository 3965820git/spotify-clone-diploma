type Props = {
  value: number
  label: string
  withBorder?: boolean
}

export function AdminStatsCard({ value, label, withBorder = false }: Props) {
  return (
    <div
      className={[
        'flex min-h-[64px] flex-col items-center justify-center',
      ].join(' ')}
    >
      <span className="text-[24px] font-medium leading-[24px]   ">
        {value}
      </span>

      <span className="mt-[6px] text-[15px] leading-[14px]   ">
        {label}
      </span>
    </div>
  )
}