import * as React from "react";
import { Link, useParams } from "react-router-dom";
import * as axios from 'axios';
import { Bill } from "../components/Types";
import { BillDetail } from "../components/BillDetail";
import { Loading } from "../components/Loading";

interface ParamTypes {
    billId: string
}

export const BillPage = () => {
    const address = 'https://localhost:44305';
    let { billId } = useParams<ParamTypes>();
    const [bill, setBill] = React.useState<Bill>();

    React.useEffect(() => {
        axios.default.get(`${address}/bill/${billId}`)
            .then(({ data: bill }) => {
                setBill(bill);
            });
    }, []);

    return <div>
        <h2>Bill {billId}</h2>
        {bill ? <BillDetail bill={bill} /> : <Loading />}
        <br />
        <Link to="/">Back</Link>
    </div>
}
