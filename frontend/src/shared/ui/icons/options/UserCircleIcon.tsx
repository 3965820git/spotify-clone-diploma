type Props = { className?: string }

export function UserCircleIcon({ className = '' }: Props) {
  return (
    <svg className={className} viewBox="0 0 24 24" fill="none">
      <path d="M12 12C14.7614 12 17 9.76142 17 7C17 4.23858 14.7614 2 12 2C9.23858 2 7 4.23858 7 7C7 9.76142 9.23858 12 12 12Z" stroke="currentColor" strokeWidth="1.8" strokeLinecap="round" strokeLinejoin="round"/>
      <path d="M3.41016 22C3.41016 18.13 7.26016 15 12.0002 15C16.7402 15 20.5902 18.13 20.5902 22" stroke="currentColor" strokeWidth="1.8" strokeLinecap="round" strokeLinejoin="round"/>
    </svg>
  )
}