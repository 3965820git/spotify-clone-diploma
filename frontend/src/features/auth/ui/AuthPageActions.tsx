type Props = {
  children: React.ReactNode
}

export function AuthPageActions({ children }: Props) {
  return (
    <div className="w-full max-w-[370px] space-y-4 pb-[calc(60px+env(safe-area-inset-bottom))]">
      {children}
    </div>
  )
}