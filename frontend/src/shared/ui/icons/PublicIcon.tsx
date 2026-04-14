type IconProps = {
  className?: string
}

export function PublicIcon({ className = '' }: IconProps) {
  return (
    <svg
      viewBox="0 0 32 32"
      fill="none"
      xmlns="http://www.w3.org/2000/svg"
      className={className}
    >
      <path
        d="M1 16H9.33333M1 16C1 24.2843 7.71573 31 16 31M1 16C1 7.71573 7.71573 1 16 1M9.33333 16H22.6667M9.33333 16C9.33333 24.2843 12.3181 31 16 31M9.33333 16C9.33333 7.71573 12.3181 1 16 1M22.6667 16H31M22.6667 16C22.6667 7.71573 19.6819 1 16 1M22.6667 16C22.6667 24.2843 19.6819 31 16 31M31 16C31 7.71573 24.2843 1 16 1M31 16C31 24.2843 24.2843 31 16 31"
        stroke="currentColor"
        strokeWidth="2"
        strokeLinecap="round"
        strokeLinejoin="round"
      />
    </svg>
  )
}