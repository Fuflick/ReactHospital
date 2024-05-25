import { InputHTMLAttributes } from 'react'

export default function Input(props: InputHTMLAttributes<HTMLInputElement>) {
    return <input className="transition-all p-2 px-4 font-semibold rounded-full flex-1 placeholder:font-normal bg-fbeige focus:outline-fpurple text-fpurple placeholder:text-fpurple outline outline-2 outline-offset-[-1px] outline-transparent" {...props}/>
}
