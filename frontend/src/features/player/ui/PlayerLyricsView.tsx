type Props = {
  lyrics: string[] | null
}

export function PlayerLyricsView({ lyrics }: Props) {
  if (!lyrics) {
    return (
      <div className="mt-[24px] text-center text-groov-muted">
        Текст недоступний
      </div>
    )
  }

  return (
    <div className="mt-[20px] space-y-[12px] text-[20px] leading-[1.5] text-groov-accent">
      {lyrics.map((line, index) => (
        <p key={index} className={line === '' ? 'h-[12px]' : ''}>
          {line}
        </p>
      ))}
    </div>
  )
}