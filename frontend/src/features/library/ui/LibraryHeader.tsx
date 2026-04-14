import { IconButton } from '@/shared/ui/buttons/IconButton'
import { HistoryIcon } from '@/shared/ui/icons/HistoryIcon'

export function LibraryHeader() {
  return (
    <div className="flex items-center justify-between">
      <h1 className="text-[24px] font-semibold leading-[22px] tracking-[-0.02em]">
        Бібліотека
      </h1>
<button className="flex h-[36px] w-[36px] items-center justify-center rounded-full transition active:scale-[0.95]">
  <HistoryIcon className="h-[24px] w-[24px] text-groov-accent" />
</button>

    </div>
  )
}