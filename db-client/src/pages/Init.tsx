import LinkButton from '../components/LinkButton'
import Main from '../components/Main'

export default function Init() {
    return <Main init heading='HospitalForm'>
        <div className='flex gap-4 flex-col'>
            <LinkButton to="/doctor">Доктора</LinkButton>
            <LinkButton to="/diagnose">Диагнозы</LinkButton>
            <LinkButton to="/docdiagnose">Связь</LinkButton>
        </div>
    </Main>
}
