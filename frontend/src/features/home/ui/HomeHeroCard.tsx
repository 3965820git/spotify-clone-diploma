'use client'

export function HomeHeroCard() {
  return (
    <div className="mt-5 rounded-[16px] bg-gradient-to-br from-groov-surface to-groov-secondary px-[16px] py-[16px] shadow-[0_12px_30px_rgba(0,0,0,0.18)] md:px-6 md:py-5">
      <div className="text-[17px] font-medium leading-[100%] tracking-[0] text-groov-accent md:text-[20px]">
        Ваша вечірня добірка
      </div>

      <div className="mt-[8px] max-w-[260px] text-[12px] font-normal leading-[135%] tracking-[0] text-groov-accent/80 md:max-w-[420px] md:text-[14px]">
        Ретельно підібрані треки для релаксу та натхнення
      </div>

      <button
        type="button"
        className="mt-[16px] inline-flex h-[40px] items-center gap-[8px] rounded-[12px] bg-groov-accent px-[14px] text-[16px] font-normal leading-[100%] tracking-[0] text-groov-textDark active:scale-[0.98]"
      >
        <svg width="14" height="14" viewBox="0 0 14 14" fill="none">
          <path d="M3 2L11 7L3 12V2Z" fill="#0D1B2A" />
        </svg>
        Відтворити
      </button>
    </div>
  )
}