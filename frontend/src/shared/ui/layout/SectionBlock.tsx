type Props = {
  children: React.ReactNode
  className?: string
}

export function SectionBlock({ children, className = "" }: Props) {
  return <section className={`mt-7 space-y-3 ${className}`}>{children}</section>
}