import { SelectHTMLAttributes, useEffect, useState } from 'react'
import { Entity, SERVER_HOST, SERVER_PORT, Table, TableEnum } from '../shared'

interface TableListProps extends SelectHTMLAttributes<HTMLSelectElement> {
    table: Table
}

export default function TableList({ table, ...props }: TableListProps) {
    const [data, setData] = useState<Entity[]>()
    useEffect(() => {
        const get = async () => {
            const res = await fetch(SERVER_HOST + ":" + SERVER_PORT + "/api/" + table + "s/")
            setData((await res.json()).result as Entity[])
        }
        get()
    }, [])
    return (
        <select className="transition-all w-full bg-fbeige p-2 px-3 flex-1 rounded-full text-fpurple focus:bg-fav font-bold outline outline-2 -outline-offset-2 hover:outline-fpurple outline-transparent" {...props}>
            {data && data.map(e => <option value={e.id}>{TableEnum[table]} с ID {e.id}</option>)}
        </select>
    )
}
