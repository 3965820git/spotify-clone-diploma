type Props = { className?: string }

export function DownloadTrackIcon({ className = '' }: Props) {
  return (
    <svg className={className} viewBox="0 0 24 24" fill="none">
      <path d="M6 21H18M12 3V17M12 17L17 12M12 17L7 12" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round"/>
    </svg>
  )
}