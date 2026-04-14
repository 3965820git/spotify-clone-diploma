type IconProps = {
  className?: string
}

export function DownloadIcon({ className }: IconProps) {
  return (
    <svg
      viewBox="0 0 28 28"
      fill="none"
      className={className}
      aria-hidden="true"
    >
      <path
        d="M7 24.5H21M14 3.5V19.8333M14 19.8333L19.8333 14M14 19.8333L8.16667 14"
        stroke="currentColor"
        strokeWidth="2"
        strokeLinecap="round"
        strokeLinejoin="round"
      />
    </svg>
  )
}