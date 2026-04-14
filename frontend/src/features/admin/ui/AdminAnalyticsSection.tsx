import { AdminAnalyticsIcon } from '@/shared/ui/icons/admin/AdminAnalyticsIcon'
import {
  adminAnalyticsProgressMock,
  adminAnalyticsSummaryMock,
} from '@/features/admin/api/admin.mock'
import { AdminAnalyticsStatRow } from './AdminAnalyticsStatRow'
import { AdminAnalyticsProgressRow } from './AdminAnalyticsProgressRow'
import { AdminAnalyticsTips } from './AdminAnalyticsTips'

export function AdminAnalyticsSection() {
  return (
    <div className="mt-[18px]">
      <div className="flex items-center gap-[12px]">
        <div className="flex h-[34px] w-[34px] shrink-0 items-center justify-center rounded-[10px] bg-groov-surface text-groov-accent">
          <AdminAnalyticsIcon className="h-[24px] w-[24px]" />
        </div>

        <h2 className="text-[18px] font-medium leading-[22px] text-groov-accent">
          Статистика профілю
        </h2>
      </div>

      <div className="mt-[16px] overflow-hidden rounded-[14px] bg-groov-surface">
        <div className="flex items-center justify-between border-b border-groov-accent px-[16px] py-[12px]">
          <h3 className="text-[18px] font-medium leading-[20px] text-groov-accent">
            Загальна статистика
          </h3>

          <span className="rounded-[8px] bg-groov-accent px-[14px] py-[4px] text-[12px] leading-[16px] text-groov-textDark">
            Початок
          </span>
        </div>

        <div className="space-y-[16px] px-[16px] py-[16px]">
          <AdminAnalyticsStatRow
            value={adminAnalyticsSummaryMock.publishedTracks}
            label="Опублікованих треків"
          />
          <AdminAnalyticsStatRow
            value={adminAnalyticsSummaryMock.followers}
            label="Підписників"
          />
          <AdminAnalyticsStatRow
            value={adminAnalyticsSummaryMock.listens}
            label="Прослуховувань"
          />
          <AdminAnalyticsStatRow
            value={adminAnalyticsSummaryMock.likes}
            label="Лайків"
          />
        </div>
      </div>

      <div className="mt-[14px] overflow-hidden rounded-[14px] bg-groov-surface">
        <div className="flex items-center justify-between border-b border-groov-accent px-[16px] py-[12px]">
          <h3 className="text-[18px] font-medium leading-[20px] text-groov-accent">
            Прогрес
          </h3>

          <span className="rounded-[8px] bg-groov-accent px-[14px] py-[4px] text-[12px] leading-[16px] text-groov-textDark">
            Новачок
          </span>
        </div>

        <div className="space-y-[16px] px-[16px] py-[16px]">
          {adminAnalyticsProgressMock.map((item) => (
            <AdminAnalyticsProgressRow key={item.id} item={item} />
          ))}
        </div>
      </div>

      <AdminAnalyticsTips />
    </div>
  )
}