'use client'

import { create } from 'zustand'
import { persist } from 'zustand/middleware'

export type PremiumPlanType = 'standard' | 'student' | 'duo' | 'family' | null

type PremiumState = {
  hasPremium: boolean
  activePlan: PremiumPlanType
  activatePremium: (plan: PremiumPlanType) => void
  cancelPremium: () => void
  clearPremium: () => void
}

export const usePremiumStore = create<PremiumState>()(
  persist(
    (set) => ({
      hasPremium: false,
      activePlan: null,

      activatePremium: (plan) =>
        set({
          hasPremium: true,
          activePlan: plan,
        }),

      cancelPremium: () =>
        set({
          hasPremium: false,
          activePlan: null,
        }),

      clearPremium: () =>
        set({
          hasPremium: false,
          activePlan: null,
        }),
    }),
    {
      name: 'premium-storage',
    }
  )
)