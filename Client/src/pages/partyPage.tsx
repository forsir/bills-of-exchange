import * as React from "react";
import { Link, useParams } from "react-router-dom";
import * as axios from 'axios';
import { Bill, Party } from "../components/Types";
import { BillList } from "../components/BillList";
import { Loading } from "../components/Loading";

interface ParamTypes {
    partyId: string
}

export const PartyPage = (params) => {
    const address = 'https://localhost:44305';
    let { partyId } = useParams<ParamTypes>();

    const [party, setParty] = React.useState<Party>();
    React.useEffect(() => {
        axios.default.get(`${address}/party/${partyId}`)
            .then(({ data: party }) => {
                setParty(party);
            });
    }, []);

    const [billsByDrawer, setBillsByDrawer] = React.useState<Bill[]>();
    React.useEffect(() => {
        axios.default.get(`${address}/bills/bydrawer/${partyId}`)
            .then(({ data: bills }) => {
                setBillsByDrawer(bills);
            });
    }, []);

    return <div>
        <h2>Party {party ? party.name : '...'} ({partyId})</h2>
        <h3>Issued Bills</h3>
        {billsByDrawer ? <BillList bills={billsByDrawer} /> : <Loading />}
        <br />
        <Link to="/">Back</Link>
    </div>
}
