import Link from 'next/link'

type Item = {
  id: string
  title: string
  subtitle: string
  image: string
}

type Props = {
  title: string
  items: Item[]
  variant?: 'default' | 'wide'
}

export function FilterSection({
  title,
  items,
  variant = 'default',
}: Props) {
  const isWide = variant === 'wide'

  return (
    <section className="mt-[26px]">
      <div className="mb-[14px] flex items-start justify-between gap-3">
        <h2
          className={`text-[24px] font-semibold leading-[28px] text-groov-accent ${
            isWide ? 'max-w-[210px]' : 'max-w-[240px]'
          }`}
        >
          {title}
        </h2>

        <button
          type="button"
          className="shrink-0 pt-[6px] text-[13px] font-medium leading-[16px] text-[#8FA3BF]"
        >
          Дивитись все
        </button>
      </div>

      <div className="-mr-4 overflow-hidden">
        <div className="hide-scrollbar flex gap-3 overflow-x-auto pr-4">
          {items.map((item) => (
            <Link
              key={item.id}
              href={`/search/filters/playlists/${item.id}`}
              className={`shrink-0 overflow-hidden rounded-[16px] bg-[#1A2740] ${
                isWide ? 'w-[180px]' : 'w-[120px]'
              }`}
            >
              <div
                className={`w-full overflow-hidden ${
                  isWide ? 'h-[100px] rounded-t-[16px]' : 'h-[96px] rounded-t-[16px]'
                }`}
              >
                <img
                  src={item.image}
                  alt={item.title}
                  className="h-full w-full object-cover"
                />
              </div>

              <div className={`${isWide ? 'px-3 pb-3 pt-[10px]' : 'px-[10px] pb-[10px] pt-[8px]'}`}>
                <h3
                  className={`line-clamp-2 font-medium text-groov-accent ${
                    isWide
                      ? 'text-[14px] leading-[18px]'
                      : 'text-[13px] leading-[16px]'
                  }`}
                >
                  {item.title}
                </h3>

                <p
                  className={`mt-[4px] text-[#B7C2D2] ${
                    isWide
                      ? 'line-clamp-3 text-[12px] leading-[15px]'
                      : 'line-clamp-2 text-[11px] leading-[14px]'
                  }`}
                >
                  {item.subtitle}
                </p>
              </div>
            </Link>
          ))}
        </div>
      </div>
    </section>
  )
}