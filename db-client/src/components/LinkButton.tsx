import { IoMdArrowRoundForward } from 'react-icons/io'
import { Link, LinkProps } from 'react-router-dom'

export default function LinkButton(props: LinkProps) {
    const {children, ...p} = props
    return <Link className='flex items-center justify-between px-6 py-3 rounded-3xl bg-fbeige hover:text-fbeige transition-all text-2xl outline outline-2 -outline-offset-2 font-semibold hover:bg-fpurple text-fpurple group-has-[input:checked]:text-fbeige group-has-[input:checked]:font-bold outline-fpurple' {...p}>
        {children}
        <IoMdArrowRoundForward/>
    </Link>
}
