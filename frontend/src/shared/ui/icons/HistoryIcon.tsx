
type HistoryIconProps = {
  className?: string
}

export function HistoryIcon({ className = '' }: HistoryIconProps) {
  return (
    <svg
      width="28"
      height="28"
      viewBox="0 0 28 28"
      fill="none"
      xmlns="http://www.w3.org/2000/svg"
      className={className}
    >
      <path
        d="M17.0625 16.625L13.125 15.3125V9.82435M23.625 14C23.625 8.20101 18.924 3.5 13.125 3.5C7.32601 3.5 2.625 8.20101 2.625 14C2.625 19.799 7.32601 24.5 13.125 24.5C17.0115 24.5 20.4048 22.3885 22.2203 19.25M20.7446 12.8477L23.3696 15.4727L25.9946 12.8477"
        stroke="currentColor"
        strokeWidth="2"
        strokeLinecap="round"
        strokeLinejoin="round"
      />
    </svg>
  )
}