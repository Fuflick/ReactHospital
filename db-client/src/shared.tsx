import Input from "./components/Input";
import { Diagnose } from "./model/Diagnose";
import { Doctor } from "./model/Doctor";
import { DoctorDiagnose } from "./model/DoctorDiagnose";
import Specialization from "./model/Specialization";
import Checkbox from "./components/Checkbox";
import Select from "./components/TableList";

export enum TableEnum {
    "doctor" = "Доктор",
    "diagnose" = "Диагноз",
    "docdiagnose" = "Связь"
}

export const [SERVER_HOST, SERVER_PORT] = ["http://localhost", 5000]

export type Table = "doctor" | "diagnose" | "docdiagnose"

export type EntityArray = Doctor[] | Diagnose[] | DoctorDiagnose[]

export type Entity = Doctor | Diagnose | DoctorDiagnose

export type PartialEntityRecord = Partial<Record<keyof Doctor | keyof Diagnose | keyof DoctorDiagnose, Doctor[keyof Doctor] | Diagnose[keyof Diagnose] | DoctorDiagnose[keyof DoctorDiagnose] | null>>

export type EntityFields = keyof Doctor & keyof Diagnose & keyof DoctorDiagnose

export function isTable<T>(e: any, f: keyof T): e is T { return f in e }

export function getFormFields(t: Table, init?: Entity) {
    switch(t) {
        case "doctor": return <>
            <Input type="text" name="name" placeholder="Имя" required defaultValue={init && (init as Doctor).name}/>
            <div className="font-bold text-fpurple">Специализация:</div>
            <div className="flex flex-wrap gap-2">
                {Specialization.map((e, i) => <Checkbox key={e} name="specializations" value={i} label={e} defaultChecked={init && (init as Doctor).specializations.includes(i)}/>)}
            </div>
            
        </>
        case "diagnose": return <>
            <Input type="text" name="body" placeholder="Содержание" required defaultValue={init && (init as Diagnose).body}/>
            <Input type="date" name="date" required defaultValue={init && (init as Diagnose).date}/>

        </>
        case "docdiagnose": 
            return <>
            {/* <Input type="number" name="docId" placeholder="ID доктора" defaultValue={init && (init as DoctorDiagnose).docId}/>
            <Input type="number" name="diagId" placeholder="ID диагноза" defaultValue={init && (init as DoctorDiagnose).diagId}/> */}
            <Select name="docId" table="doctor" required/>
            <Select name="diagId" table="diagnose" required/>
        </>
    }
}

export function getFormData(e: FormData) {
    const data: PartialEntityRecord = {};
    for(let k of e.keys()) {
        if (k == "specializations") {
            data[k as EntityFields] = (e.getAll(k) as Exclude<FormDataEntryValue, File>[]).map(e => Number(e))
        } else {
            data[k as EntityFields] = e.get(k) as Exclude<FormDataEntryValue, File>
        }
    }
    return data
}
