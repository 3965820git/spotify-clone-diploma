import Image from "next/image"

type Props = {
  title: string
  subtitle?: string
}

export function AuthPageHero({ title, subtitle }: Props) {
  return (
    <div className="flex flex-col items-center">
      <div className="relative h-[98px] w-[98px] overflow-hidden rounded-[20px]">
        <Image
          src="/groov-logo.png"
          alt="Groov Logo"
          fill
          priority
          className="object-cover"
        />
      </div>

      <h1 className="mt-[18px] w-full max-w-[370px] text-center text-[20px] font-semibold leading-[24px] text-groov-accent md:text-[24px]">
        {title}
      </h1>

      {subtitle ? (
        <p className="mt-2 w-full max-w-[370px] text-center text-[16px] leading-[24px] text-groov-accent/90 md:text-[17px]">
          {subtitle}
        </p>
      ) : null}
    </div>
  )
}