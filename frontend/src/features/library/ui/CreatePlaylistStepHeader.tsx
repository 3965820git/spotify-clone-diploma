import Link from 'next/link'
import { IconButton } from '@/shared/ui/buttons/IconButton'
import { ChevronDownIcon } from '@/shared/ui/icons/ChevronDownIcon'

type Props = {
  backHref: string
  stepLabel: string
  currentStep: number
  totalSteps: number
}

export function CreatePlaylistStepHeader({
  backHref,
  stepLabel,
  currentStep,
  totalSteps,
}: Props) {
  return (
    <>
      <div className="flex items-center justify-between">
        <Link href={backHref}>
          <IconButton className="flex h-[24px] w-[24px] items-center justify-center p-0 text-groov-accent">
            <ChevronDownIcon className="h-[16px] w-[16px] rotate-90" />
          </IconButton>
        </Link>

        <div className="text-[20px] font-medium leading-[24px] text-groov-accent">
          {stepLabel}
        </div>

        <div className="h-[24px] w-[24px]" />
      </div>

      <div className="mt-[10px] flex justify-center gap-[8px]">
        {Array.from({ length: totalSteps }).map((_, index) => {
          const stepIndex = index + 1
          const isActive = stepIndex <= currentStep

          return (
            <span
              key={stepIndex}
              className={`h-[8px] w-[8px] rounded-full ${
                isActive ?'bg-groov-secondary': 'bg-groov-accent' 
              }`}
            />
          )
        })}
      </div>
    </>
  )
}