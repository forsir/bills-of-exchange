import { number } from "prop-types"

export type Bill = {
    id: number;
    drawerId: number;
    drawer: string;
    beneficiaryId: number;
    beneficiary: string;
    amount: number;
}

export type Party = {
    id: number;
    name: string;
}

export type Endorsement = {
    id: number;
    newBeneficiaryId: number;
    newBeneficiary: string;
}