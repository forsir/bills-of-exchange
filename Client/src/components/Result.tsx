import * as React from "react";
import { DisplayError } from "./DisplayError";
import { Loading } from "./Loading";

type Props = {
    isLoading: boolean;
    isEmpty?: boolean;
    errorText: string;
    children: React.ReactNode;
};

export const Result = (props: Props) => {
    let { isLoading, isEmpty, errorText, children } = props;
    if (errorText) {
        return <DisplayError errorText={errorText} />;
    }
    if (isLoading) {
        return <Loading />
    }
    if (isEmpty) {
        return <div>There is no data</div>
    }
    return <div>{children}</div>;
};