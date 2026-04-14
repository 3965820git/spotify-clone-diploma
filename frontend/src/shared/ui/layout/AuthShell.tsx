type Props = {
  children: React.ReactNode
}

export function AuthShell({ children }: Props) {
  return (
    <div className="min-h-dvh bg-groov-bg text-groov-accent">
      <div className="mx-auto flex min-h-dvh w-full max-w-[1440px]">
        <div className="hidden w-1/2 items-center justify-center border-r border-groov-accent/10 px-10 lg:flex">
          <div className="max-w-[420px]">
            <div className="text-[40px] font-semibold leading-[1.05] tracking-[-0.03em] text-groov-accent">
              GROOV
            </div>
            <div className="mt-4 text-[18px] leading-[1.4] text-groov-muted">
              Музика, що відчуває тебе
            </div>
          </div>
        </div>

        <div className="flex w-full items-stretch justify-center lg:w-1/2">
          <div className="w-full max-w-[430px] px-4 md:px-6">
            {children}
          </div>
        </div>
      </div>
    </div>
  )
}