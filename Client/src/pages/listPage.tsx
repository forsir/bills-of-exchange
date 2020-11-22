import * as React from "react";
import { Link } from "react-router-dom";
import { Bill, Party } from '../components/Types';
import * as axios from 'axios';
import { PartyList } from "../components/PartyList";
import { BillList } from "../components/BillList";
import { Loading } from "../components/Loading";
import { Pager } from "../components/Pager";

export const ListPage = () => {
    const address = 'https://localhost:44305';
    const [parties, setParties] = React.useState<Party[]>();
    const [partiesIndex, setPartiesIndex] = React.useState(0);
    React.useEffect(() => {
        axios.default.get(`${address}/parties/${partiesIndex}`)
            .then(({ data: parties }) => {
                setParties(parties);
            });
    }, [partiesIndex]);

    const [bills, setBills] = React.useState<Bill[]>();
    const [billsIndex, setBillsIndex] = React.useState(0);
    React.useEffect(() => {
        axios.default.get(`${address}/bills/${billsIndex}`)
            .then(({ data: bill }) => {
                setBills(bill);
            });
    }, [billsIndex]);

    return <div>
        <h2>Parties</h2>
        {parties ? <PartyList parties={parties} /> : <Loading />}
        {parties ? <Pager currentPage={partiesIndex} setPage={setPartiesIndex} showNextPage={parties && parties.length == 10} /> : ""}
        <h2>Bills</h2>
        {bills ? <BillList bills={bills} /> : <Loading />}
        {bills ? <Pager currentPage={billsIndex} setPage={setBillsIndex} showNextPage={bills && bills.length == 10} /> : ""}
    </div>
};