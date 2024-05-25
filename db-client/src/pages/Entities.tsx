import { useLoaderData, useLocation, useNavigate } from 'react-router-dom'
import { EntityElement } from '../components/EntityElement'
import { EntityArray, Table, TableEnum } from '../shared'
import Main from '../components/Main'
import Button from '../components/Button'

export default function Entities() {
    const data: EntityArray = useLoaderData() as EntityArray
    const table = useLocation().pathname.split("/")[1] as Table
    const navigate = useNavigate()
    return <Main heading={TableEnum[table]}>
        <Button stretched onClick={() => navigate("create")}>Создать</Button>
        <div className='flex flex-col gap-4 h-full'>
            {data.map(e => <EntityElement e={e} key={e.id} onClick={() => navigate(String(e.id))}/>)}
            {data.length == 0 && <div className='flex items-center rounded-xl w-full h-fullrounded-4 justify-center text-fpurple'>Нет данных</div>}
        </div>
    </Main>
}
