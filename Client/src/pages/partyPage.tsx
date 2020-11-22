import * as React from "react";
import { Link, useParams } from "react-router-dom";
import * as axios from 'axios';
import { Bill, Party } from "../components/Types";
import { BillList } from "../components/BillList";
import { Result } from "../components/Result";

interface ParamTypes {
    partyId: string
}

export const PartyPage = () => {
    const address = 'https://localhost:44305';
    let { partyId } = useParams<ParamTypes>();

    const [party, setParty] = React.useState<Party>();
    React.useEffect(() => {
        axios.default.get(`${address}/party/${partyId}`)
            .then(({ data: party }) => {
                setParty(party);
            });
    }, [partyId]);

    const [billsByDrawer, setBillsByDrawer] = React.useState<Bill[]>();
    const [billsByDrawerError, setBillsByDrawerError] = React.useState<string>();
    React.useEffect(() => {
        axios.default.get(`${address}/bills/bydrawer/${partyId}`)
            .then(({ data: bills }) => {
                setBillsByDrawer(bills);
            })
            .catch(err => {
                if (err.response && (err.response.status == 500)) {
                    setBillsByDrawerError(err.response.data);
                } else {
                    setBillsByDrawerError(err.message);
                }
            });
    }, [partyId]);

    return <div>
        <h2>Party {party ? party.name : '...'} ({partyId})</h2>
        <h3>Issued Bills</h3>
        <Result isLoading={!billsByDrawer} isEmpty={billsByDrawer && billsByDrawer.length == 0} errorText={billsByDrawerError} >
            <BillList bills={billsByDrawer} />
        </Result>
        <br />
        <Link to="/">Back</Link>
    </div>
}
