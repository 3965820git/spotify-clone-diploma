type DownloadIconProps = {
  className?: string
}

export function DownloadLibIcon({ className = '' }: DownloadIconProps) {
  return (
    <svg
      width="44"
      height="44"
      viewBox="0 0 44 44"
      fill="none"
      xmlns="http://www.w3.org/2000/svg"
      className={className}
    >
      <path
        d="M11 38.5H33M22 5.5V31.1667M22 31.1667L31.1667 22M22 31.1667L12.8333 22"
        stroke="currentColor"
        strokeWidth="4"
        strokeLinecap="round"
        strokeLinejoin="round"
      />
    </svg>
  )
}