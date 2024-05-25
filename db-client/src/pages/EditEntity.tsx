import { FormEvent } from "react"
import Form from "../components/Form"
import Button from "../components/Button"
import { useLoaderData, useLocation, useNavigate } from "react-router-dom"
import { Entity, SERVER_HOST, SERVER_PORT, Table, TableEnum, getFormData, getFormFields } from "../shared"
import Main from "../components/Main"

export default function EditEntity() {
    const data: Entity = useLoaderData() as Entity
    const navigate = useNavigate()
    const table = useLocation().pathname.split("/")[1] as Table
    const update = async (e: FormEvent) => {
        e.preventDefault()
        const fd = new FormData(e.target as HTMLFormElement)
        console.log(getFormData(fd))
        await fetch(SERVER_HOST + ":" + SERVER_PORT + "/api/" + table + "s/", { method: "PATCH", body: JSON.stringify(getFormData(fd)), headers: {
            "Content-Type": "application/json"
        }})
        navigate(-1)
    }
    const remove = async () => {
        await fetch(SERVER_HOST + ":" + SERVER_PORT + "/api/" + table + "s/id?id=" + data.id, { method: "DELETE" })
        navigate(-1)
    }
    return <Main heading={TableEnum[table] + " с ID " + data.id}>
        <Form onSubmit={update}>
            <input name="id" value={data.id} className="hidden" type="number" readOnly/>
            {getFormFields(table, data)}
            <Button stretched>Изменить</Button>
        </Form>
        <Button stretched onClick={remove}>Удалить</Button>
    </Main>
}


