type Props = {
  title: string
  children: React.ReactNode
  onBack?: () => void
}

function BackIcon() {
  return (
    <svg width="24" height="24" viewBox="0 0 24 24" fill="none">
      <path
        d="M15 18l-6-6 6-6"
        stroke="#F0EEE9"
        strokeWidth="2"
        strokeLinecap="round"
        strokeLinejoin="round"
      />
    </svg>
  )
}

export function AuthFormShell({ title, children, onBack }: Props) {
  return (
    <div className="flex min-h-dvh flex-col">
      <div className="h-[50px]" />

      <div className="flex items-center gap-3">
        <button
          type="button"
          onClick={onBack}
          className="grid h-[24px] w-[24px] place-items-center"
          aria-label="Back"
        >
          <BackIcon />
        </button>

        <div className="w-full max-w-[254px] text-center text-[20px] font-semibold leading-[20px] text-groov-accent">
          {title}
        </div>
      </div>

      {children}

      <div className="flex-1" />
      <div className="h-[34px]" />
    </div>
  )
}