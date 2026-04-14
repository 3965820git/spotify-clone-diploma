type ArrowIconProps = {
  className?: string
}

export function ArrowIcon({ className = '' }: ArrowIconProps) {
  return (
    <svg
      viewBox="0 0 47 47"
      fill="none"
      xmlns="http://www.w3.org/2000/svg"
      className={className}
    >
      <path
        d="M26 31L33.5 23.5M33.5 23.5L26 16M33.5 23.5H13.5M46 23.5C46 11.0736 35.9264 1 23.5 1C11.0736 1 1 11.0736 1 23.5C1 35.9264 11.0736 46 23.5 46C35.9264 46 46 35.9264 46 23.5Z"
        stroke="currentColor"
        strokeWidth="2"
        strokeLinecap="round"
        strokeLinejoin="round"
      />
    </svg>
  )
}