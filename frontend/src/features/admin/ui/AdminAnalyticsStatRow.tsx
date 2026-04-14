type Props = {
  value: number
  label: string
}

export function AdminAnalyticsStatRow({ value, label }: Props) {
  return (
    <div>
      <div className="text-[24px] font-medium leading-[19px] text-groov-accent">
        {value}
      </div>
      <div className="mt-[4px] text-[16px] py-[4px] leading-[19px] text-groov-accent">
        {label}
      </div>
    </div>
  )
}