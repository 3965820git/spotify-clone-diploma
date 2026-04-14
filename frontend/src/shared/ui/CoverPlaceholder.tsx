export function CoverPlaceholder({ variant = 1 }: { variant?: 1 | 2 | 3 }) {
  const gradients =
    variant === 1
      ? 'from-groov-blue1/60 to-groov-primary'
      : variant === 2
        ? 'from-groov-blue2/60 to-groov-primary'
        : 'from-groov-accent/20 to-groov-primary'

  return <div className={`h-full w-full bg-gradient-to-br ${gradients}`} />
}