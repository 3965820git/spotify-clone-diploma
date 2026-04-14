type Props = {
  className?: string
}

export function PrevIcon({ className }: Props) {
  return (
    <svg
      className={className}
      viewBox="0 0 28 28"
      fill="none"
    >
      <path
        d="M23.6133 8.42331V19.5883C23.6133 21.875 21.1283 23.31 19.145 22.1666L14.3033 19.3783L9.46162 16.5783C7.47829 15.435 7.47829 12.5766 9.46162 11.4333L14.3033 8.6333L19.145 5.84499C21.1283 4.70166 23.6133 6.12498 23.6133 8.42331Z"
        fill="currentColor"
        stroke="currentColor"
        strokeWidth="1.5"
        strokeLinecap="round"
        strokeLinejoin="round"
      />
      <path
        d="M4.38672 21.21V6.79004"
        stroke="currentColor"
        strokeWidth="1.5"
        strokeLinecap="round"
        strokeLinejoin="round"
      />
    </svg>
  )
}