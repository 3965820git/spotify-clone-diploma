type IconProps = {
  className?: string
}

export function PlayIcon({ className = '' }: IconProps) {
  return (
    <svg
      viewBox="0 0 38 38"
      fill="none"
      className={className}
      aria-hidden="true"
    >
      <path
        d="M6.3335 18.9999V13.3632C6.3335 6.36487 11.2893 3.49904 17.3535 6.99821L22.246 9.81654L27.1385 12.6349C33.2027 16.134 33.2027 21.8657 27.1385 25.3649L22.246 28.1832L17.3535 31.0015C11.2893 34.5007 6.3335 31.6349 6.3335 24.6365V18.9999Z"
        fill="currentColor"
        stroke="currentColor"
        strokeWidth="1.5"
        strokeMiterlimit="10"
        strokeLinecap="round"
        strokeLinejoin="round"
      />
    </svg>
  )
}