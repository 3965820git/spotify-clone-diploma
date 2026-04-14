import { AdminMusicNoteIcon } from '@/shared/ui/icons/settings/AdminMusicNoteIcon'
import { ShareIcon } from '@/shared/ui/icons/ShareIcon'
import { adminAnalyticsTipsMock } from '@/features/admin/api/admin.mock'

export function AdminAnalyticsTips() {
  return (
    <div className="mt-[14px] overflow-hidden rounded-[14px] bg-groov-surface">
      <div className="border-b border-groov-accent px-[16px] py-[12px]">
        <h3 className="text-[18px] font-medium leading-[20px] text-groov-accent">
          Поради для початку
        </h3>
      </div>

      <div className="divide-y divide-groov-accent/20">
        {adminAnalyticsTipsMock.map((tip) => (
          <div key={tip.id} className="flex items-center gap-[14px] px-[16px] py-[12px]">
            <div className="flex h-[38px] w-[38px] shrink-0 items-center justify-center rounded-[10px] bg-groov-accent text-groov-textDark">
              {tip.icon === 'track' ? (
                <AdminMusicNoteIcon className="h-[24px] w-[24px]" />
              ) : (
                <ShareIcon className="h-[24px] w-[24px]" />
              )}
            </div>

            <div className="min-w-0">
              <div className="text-[16px] font-medium leading-[18px] text-groov-accent">
                {tip.title}
              </div>
              <div className="mt-[4px] text-[14px] leading-[16px] text-groov-accent">
                {tip.description}
              </div>
            </div>
          </div>
        ))}
      </div>
    </div>
  )
}