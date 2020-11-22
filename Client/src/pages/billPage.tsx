import * as React from "react";
import { Link, useParams } from "react-router-dom";
import * as axios from 'axios';
import { Bill, Endorsement } from "../components/Types";
import { BillDetail } from "../components/BillDetail";
import { Loading } from "../components/Loading";
import { EndorsementList } from "../components/EndorsementList";
import { Result } from "../components/Result";

interface ParamTypes {
    billId: string
}

export const BillPage = () => {
    const address = 'https://localhost:44305';
    let { billId } = useParams<ParamTypes>();

    const [bill, setBill] = React.useState<Bill>();
    const [billError, setBillError] = React.useState<string>();

    React.useEffect(() => {
        axios.default.get(`${address}/bill/${billId}`)
            .then(({ data: bill }) => {
                setBill(bill);
            })
            .catch(err => {
                if (err.response && (err.response.status == 500)) {
                    setBillError(err.response.data);
                } else {
                    setBillError(err.message);
                }
            });
    }, []);

    const [endorsements, setEndorsements] = React.useState<Endorsement[]>();
    const [endorsementsError, setEndorsementsError] = React.useState<string>();

    React.useEffect(() => {
        axios.default.get(`${address}/endorsement/bybill/${billId}`)
            .then(({ data: endorsements }) => {
                setEndorsements(endorsements);
            })
            .catch(err => {
                if (err.response && (err.response.status == 500)) {
                    setEndorsementsError(err.response.data);
                } else {
                    setEndorsementsError(err.message);
                }
            });
    }, []);

    return <div>
        <h2>Bill id {billId}</h2>
        <Result isLoading={!bill} errorText={billError}>
            <BillDetail bill={bill} />
        </Result>
        <h2>Endorsements</h2>
        <Result isLoading={!endorsements} errorText={endorsementsError}>
            <EndorsementList endorsements={endorsements} />
        </Result>
        <br />
        <Link to="/">Back</Link>
    </div>
}
