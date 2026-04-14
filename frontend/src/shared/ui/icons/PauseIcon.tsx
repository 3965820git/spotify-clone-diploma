type IconProps = {
  className?: string
}

export function PauseIcon({ className = '' }: IconProps) {
  return (
    <svg
      viewBox="0 0 38 38"
      fill="none"
      className={className}
      aria-hidden="true"
    >
      <path
        d="M16.8625 30.2575V7.7425C16.8625 5.605 15.96 4.75 13.68 4.75H7.9325C5.6525 4.75 4.75 5.605 4.75 7.7425V30.2575C4.75 32.395 5.6525 33.25 7.9325 33.25H13.68C15.96 33.25 16.8625 32.395 16.8625 30.2575Z"
        fill="currentColor"
      />
      <path
        d="M33.2502 30.2575V7.7425C33.2502 5.605 32.3477 4.75 30.0677 4.75H24.3202C22.056 4.75 21.1377 5.605 21.1377 7.7425V30.2575C21.1377 32.395 22.0402 33.25 24.3202 33.25H30.0677C32.3477 33.25 33.2502 32.395 33.2502 30.2575Z"
        fill="currentColor"
      />
    </svg>
  )
}