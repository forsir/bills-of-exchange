import * as React from "react";
import { Link } from "react-router-dom";

type Bill = {
    id: number;
    name: string;
}

export const List = () => {
    const [bills, setBills] = React.useState<Bill[]>([]);

    React.useEffect(() => {
        setBills([{ id: 1, name: "Jedna" }, { id: 2, name: "Dva" }]);
    }, []);
    bills.map(b => b.id);
    return <div>
        <table>
            <tbody>
                {bills.map((b, i) => <tr key={i}>
                    <td>{b.id}</td>
                    <td><Link to={{ pathname: `/bill/${b.id}` }}>{b.name}</Link></td>
                </tr>
                )}
            </tbody>
        </table>
    </div>
};