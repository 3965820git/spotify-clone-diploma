import Link from 'next/link'

export function SectionHeader({ title, href }: { title: string; href?: string }) {
  return (
    <div className="flex items-center justify-between">
      <h2 className="text-[16px] font-medium leading-[100%] tracking-[0] text-[#F0EEE9]">
        {title}
      </h2>

      {href ? (
        <Link href={href} className="text-[14px] font-normal leading-[100%] tracking-[0] text-[#778DA9]">
          Дивитись все
        </Link>
      ) : (
        <span className="text-[14px] font-normal leading-[100%] tracking-[0] text-[#778DA9]">
          Дивитись все
        </span>
      )}
    </div>
  )
}