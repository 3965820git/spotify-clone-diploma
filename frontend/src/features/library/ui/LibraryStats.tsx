import type { LibraryStat } from '../model/types'

type Props = {
  stats: LibraryStat[]
}

export function LibraryStats({ stats }: Props) {
  return (
    <div className="mt-[16px] flex gap-[8px] overflow-x-auto pb-[2px] pr-[10px] scrollbar-none">
      {stats.map((stat) => (
        <div
          key={stat.id}
          className="flex h-[80px] min-w-[136px] shrink-0 flex-col items-center rounded-[10px] bg-groov-surface pt-[14px]"
        >
          <div className="text-[32px] font-semibold leading-[29px]">
            {stat.value}
          </div>

          <div className="mt-[6px] text-center text-[16px] leading-[17px]">
            {stat.label}
          </div>
        </div>
      ))}
    </div>
  )
}