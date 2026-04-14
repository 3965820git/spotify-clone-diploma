type Props = {
  className?: string
}

export function NextIcon({ className }: Props) {
  return (
    <svg
      className={className}
      viewBox="0 0 28 28"
      fill="none"
    >
      <path
        d="M4.38672 8.42331V19.5883C4.38672 21.875 6.8717 23.31 8.85504 22.1666L13.6967 19.3783L18.5384 16.5783C20.5217 15.435 20.5217 12.5766 18.5384 11.4333L13.6967 8.6333L8.85504 5.84499C6.8717 4.70166 4.38672 6.12498 4.38672 8.42331Z"
        fill="currentColor"
        stroke="currentColor"
        strokeWidth="1.5"
        strokeLinecap="round"
        strokeLinejoin="round"
      />
      <path
        d="M23.6133 21.21V6.79004"
        stroke="currentColor"
        strokeWidth="1.5"
        strokeLinecap="round"
        strokeLinejoin="round"
      />
    </svg>
  )
}