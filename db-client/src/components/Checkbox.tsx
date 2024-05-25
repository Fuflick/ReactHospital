import { InputHTMLAttributes } from 'react'

interface CheckboxProps extends InputHTMLAttributes<HTMLInputElement> {
    type?: never
    label: string
}

export default function Checkbox({ type, ...props }: CheckboxProps) {
    return (
        <label className='transition-all p-2 px-4 grid rounded-full cursor-pointer place-items-center text-fpurple bg-fbeige outline outline-2 -outline-offset-2 outline-transparent hover:outline-fpurple has-[input:checked]:bg-fpurple has-[input:checked]:text-fbeige has-[input:checked]:font-bold'>
            {props.label}
            <input type="checkbox" name="specializations" className='hidden' {...props}/>
        </label>
    )
}