import * as React from "react";
import { Link } from "react-router-dom";
import { Bill, Party } from '../components/Types';
import * as axios from 'axios';
import { PartyList } from "../components/PartyList";
import { BillList } from "../components/BillList";
import { Loading } from "../components/Loading";

export const ListPage = () => {
    const address = 'https://localhost:44305';
    const [parties, setParties] = React.useState<Party[]>();

    React.useEffect(() => {
        axios.default.get(`${address}/parties`)
            .then(({ data: parties }) => {
                setParties(parties);
            });
    }, []);

    const [bills, setBills] = React.useState<Bill[]>();

    React.useEffect(() => {
        axios.default.get(`${address}/bills`)
            .then(({ data: bill }) => {
                setBills(bill);
            });
    }, []);

    return <div>
        <h2>Parties</h2>
        {parties ? <PartyList parties={parties} /> : <Loading />}
        <h2>Bills</h2>
        {bills ? <BillList bills={bills} /> : <Loading />}
    </div>
};