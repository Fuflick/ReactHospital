import { FormEvent } from "react"
import Form from "../components/Form"
import Button from "../components/Button"
import { useLocation, useNavigate } from "react-router-dom"
import { SERVER_HOST, SERVER_PORT, Table, TableEnum, getFormData, getFormFields } from "../shared"
import Main from "../components/Main"

export default function Create() {
    const navigate = useNavigate()
    const table = useLocation().pathname.split("/")[1] as Table
    const create = async (e: FormEvent) => {
        e.preventDefault()
        const fd = new FormData(e.target as HTMLFormElement)
        await fetch(SERVER_HOST + ":" + SERVER_PORT + "/api/" + table + "s/", { method: "POST", body: JSON.stringify(getFormData(fd)), headers: {
            "Content-Type": "application/json"
        }})
        navigate(-1)
    }
    return <Main heading={"Создать " + TableEnum[table]}>
        <Form onSubmit={create}>
            {getFormFields(table)}
            <Button stretched>Создать</Button>
        </Form>
    </Main>
}
