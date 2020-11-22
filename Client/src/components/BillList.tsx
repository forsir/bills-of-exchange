import * as React from "react";
import { Link } from "react-router-dom";
import { Bill } from "./Types";

type ParamType = {
    bills: Bill[];
}

export const BillList = (params: ParamType) => {
    const { bills } = params;

    return <div>
        <table>
            <thead>
                <tr>
                    <th>id</th>
                    <th>drawer</th>
                    <th>beneficiary</th>
                    <th>amount</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                {bills.map((b, i) => <tr key={i}>
                    <td>{b.id}</td>
                    <td><Link to={{ pathname: `/party/${b.drawerId}` }}>{b.drawer}</Link></td>
                    <td><Link to={{ pathname: `/party/${b.beneficiaryId}` }}>{b.beneficiary}</Link></td>
                    <td>{b.amount}</td>
                    <td><Link to={{ pathname: `/bill/${b.id}` }}>link</Link></td>
                </tr>
                )}
            </tbody>
        </table>
    </div>
};