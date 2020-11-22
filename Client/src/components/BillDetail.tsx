import * as React from "react";
import { Link } from "react-router-dom";
import { Bill } from "./Types";

type ParamType = {
    bill: Bill;
}

export const BillDetail = (params: ParamType) => {
    const { bill } = params;

    return <table>
        <tbody>
            <tr >
                <td>drawer</td>
                <td><Link to={{ pathname: `/party/${bill.drawerId}` }}>{bill.drawer}</Link></td>
            </tr>
            <tr>
                <td>beneficiary</td>
                <td><Link to={{ pathname: `/party/${bill.beneficiaryId}` }}>{bill.beneficiary}</Link></td>
            </tr>
            <tr>
                <td>amount</td>
                <td>{bill.amount}</td>
            </tr>
        </tbody>
    </table>
};