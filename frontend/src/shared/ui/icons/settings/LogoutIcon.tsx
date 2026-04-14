import type { SVGProps } from 'react'

export function LogoutIcon(props: SVGProps<SVGSVGElement>) {
  return (
    <svg
      width="30"
      height="30"
      viewBox="0 0 30 30"
      fill="none"
      xmlns="http://www.w3.org/2000/svg"
      {...props}
    >
      <path
        d="M15 18.75L18.75 15M18.75 15L15 11.25M18.75 15H5M5 9.06003V9.00024C5 7.60011 5 6.89953 5.27248 6.36475C5.51217 5.89434 5.89434 5.51217 6.36475 5.27248C6.89953 5 7.60011 5 9.00024 5H21.0002C22.4004 5 23.0995 5 23.6342 5.27248C24.1046 5.51217 24.4881 5.89434 24.7278 6.36475C25 6.899 25 7.59874 25 8.99614V21.0045C25 22.4019 25 23.1006 24.7278 23.6349C24.4881 24.1053 24.1046 24.4881 23.6342 24.7278C23.1 25 22.4013 25 21.0039 25H8.99614C7.59874 25 6.899 25 6.36475 24.7278C5.89434 24.4881 5.51217 24.1049 5.27248 23.6345C5 23.0997 5 22.4001 5 21V20.9375"
        stroke="currentColor"
        strokeWidth="2"
        strokeLinecap="round"
        strokeLinejoin="round"
      />
    </svg>
  )
}