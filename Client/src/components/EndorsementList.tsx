import * as React from "react";
import { Link } from "react-router-dom";
import { Endorsement } from "./Types";

type ParamType = {
    endorsements: Endorsement[];
}

export const EndorsementList = (params: ParamType) => {
    const { endorsements } = params;

    return <div>
        <table>
            <thead>
                <th>
                    <td>id</td>
                    <td>new beneficiary</td>
                </th>
            </thead>
            <tbody>
                {endorsements.map((b, i) => <tr key={i}>
                    <td>{b.id}</td>
                    <td><Link to={{ pathname: `/party/${b.newBeneficiaryId}` }}>{b.newBeneficiary}</Link></td>
                </tr>
                )}
            </tbody>
        </table>
    </div>
};