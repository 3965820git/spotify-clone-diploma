type Props = {
  value: string
  label: string
}

export function ReviewStatCard({ value, label }: Props) {
  const isNumber = !isNaN(Number(value))

  return (
    <div className="flex h-[79px] flex-1 flex-col items-center justify-center rounded-[10px] bg-groov-primary text-center">
      
      <div
        className={
          isNumber
            ? 'text-[32px] font-bold leading-[30px] text-groov-accent'
            : 'text-[20px] font-semibold leading-[30px] text-groov-accent'
        }
      >
        {value}
      </div>

      <div className="mt-[2px] text-[14px] leading-[17px] text-groov-accent">
        {label}
      </div>
    </div>
  )
}