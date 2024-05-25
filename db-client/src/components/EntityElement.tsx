import { HTMLAttributes } from 'react'
import { Doctor } from '../model/Doctor'
import { Diagnose } from '../model/Diagnose'
import { DoctorDiagnose } from '../model/DoctorDiagnose'
import { Entity, isTable } from '../shared'

interface ElementProps<T> extends HTMLAttributes<HTMLElement> { e: T }

export function EntityElement(props: ElementProps<Entity>) {
    let content = ""
    if (isTable<Doctor>(props.e, "specializations")) content = props.e.id + ". " + props.e.name
    if (isTable<Diagnose>(props.e, "date")) content = props.e.id + ". " + props.e.body + " - " + props.e.date
    if (isTable<DoctorDiagnose>(props.e, "docId")) content = props.e.id + ". " + props.e.diagId + " - " + props.e.docId
    return <div className='transition-all cursor-pointer p-2 px-4 rounded-full text-fpurple outline outline-2 -outline-offset-2 outline-fpurple hover:bg-fpurple hover:text-fbeige flex items-center bg-fbeige' {...props}>{content}</div>
}
  
