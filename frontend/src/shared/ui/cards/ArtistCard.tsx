type Props = {
  name: string
  followers: string
  image: string
  isFollowing?: boolean
}

export function ArtistCard({
  name,
  followers,
  image,
  isFollowing = true,
}: Props) {
  return (
    <button
      type="button"
      className="group flex h-[192px] w-full flex-col items-center text-center"
    >
      <div className="h-[120px] w-full overflow-hidden rounded-[14px]">
        <img
          src={image}
          alt={name}
          className="h-full w-full object-cover"
        />
      </div>

      <div className="mt-[8px] flex h-[36px] w-full flex-col items-center px-[2px]">
        <div className="line-clamp-1 text-[14px] leading-[17px]  ">
          {name}
        </div>

        <div className="mt-[4px] w-full truncate text-[12px] leading-[14px]  /80">
          {followers}
        </div>
      </div>

      <div className="mt-[14px] flex w-full justify-center">
        <div className="flex h-[27px] w-full items-center justify-center rounded-full bg-groov-accent px-[10px] text-[12px] leading-[14px] text-groov-textDark transition-transform duration-200 group-active:scale-[0.97]">
          {isFollowing ? 'Відстежується' : 'Відстежити'}
        </div>
      </div>
    </button>
  )
}