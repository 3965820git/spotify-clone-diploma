'use client'

import { useRouter } from 'next/navigation'
import { PremiumPlan } from '@/features/premium/mock/premium-plans.mock'
import { usePremiumStore } from '@/features/premium/model/usePremiumStore'
import { Button } from '@/shared/ui/button'
import { cn } from '@/shared/lib/cn'

type Props = {
  plan: PremiumPlan
}

export function PremiumPlanCard({ plan }: Props) {
  const router = useRouter()

  const activePlan = usePremiumStore((state) => state.activePlan)
  const activatePremium = usePremiumStore((state) => state.activatePremium)
  const cancelPremium = usePremiumStore((state) => state.cancelPremium)

  const isActive = activePlan === plan.id

  const handleClick = () => {
    if (isActive) {
      cancelPremium()
      return
    }

    activatePremium(plan.id as 'standard' | 'student' | 'duo' | 'family')
    router.push('/home')
  }

  return (
    <div
      className={cn(
        'overflow-hidden rounded-[20px] bg-groov-surface text-groov-accent',
        isActive && 'ring-1 ring-groov-accent/40'
      )}
    >
      {plan.oldPrice && (
        <div className="bg-groov-accent px-4 py-[8px] text-center text-[15px] font-normal leading-[20px] text-groov-textDark">
          {plan.price}
        </div>
      )}

      <div className="px-4 pb-4 pt-4">
        <div>
          <div className="flex items-center gap-[12px]">
            <div className="flex h-[40px] w-[40px] shrink-0 items-center justify-center overflow-hidden rounded-[10px] bg-groov-bg">
              <img
                src="/groov-logo.png"
                alt="Groov"
                className="h-full w-full object-cover"
              />
            </div>

            <p className="text-[20px] font-normal leading-[20px] text-groov-accent">
              {plan.badge}
            </p>
          </div>

          <h3 className="mt-[8px] text-[24px] font-normal leading-[24px] text-groov-accent">
            {plan.title}
          </h3>
        </div>

        {plan.oldPrice ? (
          <>
            <p className="mt-4 text-[16px] font-normal leading-[20px] text-groov-accent">
              {plan.price}
            </p>
            <p className="mt-[4px] text-[16px] font-normal leading-[20px] text-groov-accent/85">
              {plan.oldPrice}
            </p>
          </>
        ) : (
          <p className="mt-4 text-[16px] font-normal leading-[20px] text-groov-accent">
            {plan.price}
          </p>
        )}

        <div className="mt-3 h-px w-full bg-groov-accent/25" />

        <ul className="mt-4 space-y-3">
          {plan.features.map((feature) => (
            <li
              key={feature}
              className="flex items-start gap-2 text-[16px] font-normal leading-[21px] text-groov-accent"
            >
              <span className="mt-[8px] h-[4px] w-[4px] shrink-0 rounded-full bg-groov-accent" />
              <span>{feature}</span>
            </li>
          ))}
        </ul>

        <Button
          type="button"
          variant="light"
          size="lg"
          onClick={handleClick}
          className="mt-5 max-w-none rounded-[18px] text-[20px] font-normal leading-[20px]"
        >
          {isActive ? 'Скасувати підписку' : plan.buttonText}
        </Button>

        <p className="mt-4 text-[13px] font-normal leading-[17px] text-groov-accent/85">
          {plan.description}{' '}
          <span className="underline">Діють умови.</span>
        </p>
      </div>
    </div>
  )
}