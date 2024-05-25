import { HTMLAttributes } from 'react'
import Button from './Button'
import { IoMdArrowRoundBack } from 'react-icons/io'
import { useNavigate } from 'react-router-dom'

interface MainProps extends HTMLAttributes<HTMLElement> {
    heading: string
    init?: boolean
}

export default function Main({ heading, init = false, ...props }: MainProps) {
    const navigate = useNavigate()
    return <main className='flex justify-center py-[10%] w-screen h-screen'>
        <div className='flex flex-col gap-6'>
            <div className='flex gap-6'>
                {!init && <Button onClick={() => navigate(-1)}><IoMdArrowRoundBack size={20}/></Button>}
                <div className='text-fpurple text-3xl font-bold text-ellipsis'>{heading}</div>
            </div>
            <div className='w-[400px] flex flex-col gap-4' {...props}/>
        </div>
    </main>
}