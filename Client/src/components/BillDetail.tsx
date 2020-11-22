import * as React from "react";
import { Link } from "react-router-dom";
import { Bill, Endorsement } from "./Types";

type ParamType = {
    bill: Bill;
    lastEndorsement: Endorsement
}

export const BillDetail = (params: ParamType) => {
    const { bill, lastEndorsement } = params;

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
            {lastEndorsement && lastEndorsement.newBeneficiaryId !== bill.beneficiaryId &&
                <tr>
                    <td>last beneficiary</td>
                    <td><Link to={{ pathname: `/party/${lastEndorsement.newBeneficiaryId}` }}>{lastEndorsement.newBeneficiary}</Link></td>
                </tr>
            }
            <tr>
                <td>amount</td>
                <td>{bill.amount}</td>
            </tr>
        </tbody>
    </table>
};