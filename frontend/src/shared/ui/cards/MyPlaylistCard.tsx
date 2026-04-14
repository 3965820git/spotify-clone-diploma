type Props = {
  title: string
  author: string
  tracksCount: number
  cover?: string
  accentColor?: string
}

export function MyPlaylistCard({
  title,
  author,
  tracksCount,
  cover,
  accentColor,
}: Props) {
  return (
    <button
      type="button"
      className="group block h-[200px] w-full overflow-hidden rounded-[16px] bg-groov-surface text-left"
    >
      <div
        className="h-[130px] w-full overflow-hidden"
        style={{ backgroundColor: accentColor ?? undefined }}
      >
        {cover ? (
          <img
            src={cover}
            alt={title}
            className="h-full w-full object-cover"
          />
        ) : null}
      </div>

      <div className="flex h-[70px] flex-col justify-start px-[12px] pt-[10px]">
        <div className="truncate text-[14px] leading-[17px] text-groov-accent">
          {title}
        </div>

        <div className="mt-[6px] truncate text-[12px] leading-[14px] text-groov-primary">
          {author}
        </div>

        <div className="mt-[2px] text-[12px] leading-[14px] text-groov-primary">
          {tracksCount} треків
        </div>
      </div>
    </button>
  )
}