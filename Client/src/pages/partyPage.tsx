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
    let address = '';
    let { partyId } = useParams<ParamTypes>();

    const [party, setParty] = React.useState<Party>();
    React.useEffect(() => {
        axios.default.get(`${address}/api/users/${partyId}`)
            .then(({ data: party }) => {
                setParty(party);
            });
    }, []);

    const [billsByDrawer, setBillsByDrawer] = React.useState<Bill[]>();
    React.useEffect(() => {
        axios.default.get(`${address}/api/users/${partyId}`)
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
