type IconProps = {
  className?: string
}

export function SharedAccessIcon({ className = '' }: IconProps) {
  return (
    <svg
      viewBox="0 0 40 40"
      fill="none"
      xmlns="http://www.w3.org/2000/svg"
      className={className}
    >
      <path
        d="M35 33.3334C35 30.4307 32.2173 27.9613 28.3333 27.0461M25 33.3335C25 29.6516 20.5228 26.6668 15 26.6668C9.47715 26.6668 5 29.6516 5 33.3335M25 21.6668C28.6819 21.6668 31.6667 18.6821 31.6667 15.0002C31.6667 11.3183 28.6819 8.3335 25 8.3335M15 21.6668C11.3181 21.6668 8.33333 18.6821 8.33333 15.0002C8.33333 11.3183 11.3181 8.3335 15 8.3335C18.6819 8.3335 21.6667 11.3183 21.6667 15.0002C21.6667 18.6821 18.6819 21.6668 15 21.6668Z"
        stroke="currentColor"
        strokeWidth="2"
        strokeLinecap="round"
        strokeLinejoin="round"
      />
    </svg>
  )
}