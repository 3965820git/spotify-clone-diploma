type IconProps = {
  className?: string
}

export function BackIcon({ className }: IconProps) {
  return (
    <svg
      viewBox="0 0 16 14"
      fill="none"
      className={className}
      aria-hidden="true"
    >
      <path
        d="M15 7H1M1 7L7 13M1 7L7 1"
        stroke="currentColor"
        strokeWidth="2"
        strokeLinecap="round"
        strokeLinejoin="round"
      />
    </svg>
  )
}