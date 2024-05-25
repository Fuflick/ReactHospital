import { ButtonHTMLAttributes } from 'react'

interface ButtonProps extends ButtonHTMLAttributes<HTMLButtonElement> {
    stretched?: boolean
}

export default function Button({ stretched = false, ...props}: ButtonProps) {
    return (
        <button className={`${stretched ? "w-full px-4 p-2" : "max-w-max aspect-square p-3"} transition-all grid place-items-center justify-self-end rounded-full font-bold outline outline-fpurple outline-2 text-fpurple -outline-offset-2 bg-fbeige hover:bg-fpurple hover:text-fbeige`} {...props}>{props.children}</button>
    )
}
