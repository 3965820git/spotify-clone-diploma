import { adminStatsMock } from '@/features/admin/api/admin.mock'
import { AdminStatsCard } from './AdminStatsCard'

export function AdminStatsSection() {
  return (
    <div className="mt-[18px] rounded-[14px] bg-groov-surface px-[8px] py-[10px]">
      <div className="grid grid-cols-3">
        <AdminStatsCard value={adminStatsMock.followers} label="Підписників" withBorder />
        <AdminStatsCard value={adminStatsMock.tracks} label="Треків" withBorder />
        <AdminStatsCard value={adminStatsMock.listens} label="Прослуховувань" />
      </div>
    </div>
  )
}