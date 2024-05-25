import { FormHTMLAttributes } from 'react'

export default function Form(props: FormHTMLAttributes<HTMLFormElement>) {
    return (
        <form className='flex flex-col gap-4' {...props}/>
    )
}
