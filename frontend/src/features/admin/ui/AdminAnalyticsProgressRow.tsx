import type { AdminAnalyticsProgressItem } from '@/features/admin/api/admin.types'

type Props = {
  item: AdminAnalyticsProgressItem
}

export function AdminAnalyticsProgressRow({ item }: Props) {
  return (
    <div>
      <div className="flex items-start justify-between gap-[12px]">
        <div className="min-w-0">
          <div className="text-[15px] leading-[18px] text-groov-accent">
            {item.title}
          </div>
        </div>

        <div className="shrink-0 text-[15px] leading-[18px] text-groov-accent">
          {item.value}%
        </div>
      </div>

      <div className="mt-[6px] h-[4px] w-full rounded-full bg-groov-primary">
        <div
          className="h-[4px] rounded-full bg-groov-accent transition-all duration-300"
          style={{ width: `${item.value}%` }}
        />
      </div>

      <div className="mt-[6px] text-[12px] leading-[14px] text-groov-accent/80">
        {item.description}
      </div>
    </div>
  )
}