'use client'

import { AppShell } from '@/shared/ui/layout/AppShell'
import { BottomNav } from '@/shared/ui/BottomNav'
import { premiumPlans } from '@/features/premium/mock/premium-plans.mock'
import { PremiumPlanCard } from '@/features/premium/ui/PremiumPlanCard'
import { Button } from '@/shared/ui/button'
import { usePremiumStore } from '@/features/premium/model/usePremiumStore'
import { useRouter } from 'next/navigation'

export default function PremiumPage() {
  const router = useRouter()
  const activatePremium = usePremiumStore((state) => state.activatePremium)

  const handleHeroActivate = () => {
    activatePremium('standard')
    router.push('/home')
  }

  return (
  <AppShell
  withSafeAreaTop={false}
  mobileMaxWidth={402}
  withBottomNavSpacing
  withDefaultPadding={false}
  contentClassName="overflow-hidden pb-[98px]"

    >
<div className="relative -mt-[12px] bg-groov-bg">
  <section className="relative h-[249px] overflow-hidden">
    <img
      src="/premium.png"
      alt="Premium"
      className="absolute inset-0 block h-full w-full object-cover"
    />

    <div className="pointer-events-none absolute inset-x-0 top-0 bottom-[-12px] z-[1] bg-[linear-gradient(180deg,rgba(255,255,255,0.16)_0%,rgba(255,255,255,0.10)_50%,rgba(13,27,42,0.65)_70%,rgba(13,27,42,0.92)_86%,#0D1B2A_100%)]" />

    <div
      className="relative z-[2] flex h-full flex-col justify-end px-4 pb-[12px] text-groov-accent"
      style={{ paddingTop: 'max(env(safe-area-inset-top), 12px)' }}
    >
      <h1 className="text-[24px] font-normal leading-[20px]">
        Premium
      </h1>

      <p className="mt-[6px] text-[20px] font-normal leading-[21px]">
        Лише ти і музика в GROOV
        <br />
        Твій перший місяць Premium — 0₴
      </p>
    </div>
  </section>

  <div className="px-4 pt-[16px]">
    <Button
      type="button"
      variant="light"
      size="lg"
      onClick={handleHeroActivate}
      className="max-w-none rounded-[18px] text-[20px] font-normal leading-[20px]"
    >
      Спробувати 1 місяць за 0₴
    </Button>

    <p className="mt-[12px] text-[13px] font-normal leading-[16px] text-groov-accent">
      0₴ за 1 місяць, потім 199,99₴ на місяць. Пропозиція доступна лише
      для тих хто не користувався підпискою Premium і оформили її через
      GROOV. Пропозиції в App Store можуть відрізнятися.{' '}
      <span className="underline">Діють умови.</span>
    </p>
  </div>
</div>

      <section className="px-4 pt-2">
        <div className="rounded-[20px] bg-groov-surface px-4 pb-4 pt-4 text-groov-accent">
          <h2 className="text-[24px] font-normal leading-[24px]">
            Переваги Premium-підписки
          </h2>

          <div className="mt-3 h-px w-full bg-groov-accent/25" />

          <ul className="mt-4 space-y-3">
            <li className="flex items-start gap-2 text-[16px] leading-[21px]">
              <span className="mt-[6px] h-[4px] w-[4px] shrink-0 rounded-full bg-groov-accent" />
              <span>Без реклами: Безперервний потік музики.</span>
            </li>
            <li className="flex items-start gap-2 text-[16px] leading-[21px]">
              <span className="mt-[6px] h-[4px] w-[4px] shrink-0 rounded-full bg-groov-accent" />
              <span>Офлайн-режим: Музика завжди під рукою без інтернету.</span>
            </li>
            <li className="flex items-start gap-2 text-[16px] leading-[21px]">
              <span className="mt-[6px] h-[4px] w-[4px] shrink-0 rounded-full bg-groov-accent" />
              <span>Вільний порядок: Перемикай та вибирай будь-що.</span>
            </li>
            <li className="flex items-start gap-2 text-[16px] leading-[21px]">
              <span className="mt-[6px] h-[4px] w-[4px] shrink-0 rounded-full bg-groov-accent" />
              <span>Макс-якість: Найвищий рівень звучання.</span>
            </li>
            <li className="flex items-start gap-2 text-[16px] leading-[21px]">
              <span className="mt-[6px] h-[4px] w-[4px] shrink-0 rounded-full bg-groov-accent" />
              <span>Party Mode: Транслюй музику друзям у реальному часі.</span>
            </li>
            <li className="flex items-start gap-2 text-[16px] leading-[21px]">
              <span className="mt-[6px] h-[4px] w-[4px] shrink-0 rounded-full bg-groov-accent" />
              <span>Розумна черга: Легко керуй порядком треків.</span>
            </li>
          </ul>
        </div>
      </section>

      <section className="px-4 pt-6">
        <h2 className="text-[24px] font-normal leading-[24px] text-groov-accent">
          Доступні підписки
        </h2>

        <div className="mt-4 space-y-4">
          {premiumPlans.map((plan) => (
            <PremiumPlanCard key={plan.id} plan={plan} />
          ))}
        </div>
      </section>

      <BottomNav />
    </AppShell>
  )
}