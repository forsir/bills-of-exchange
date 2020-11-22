import * as React from "react";

type Props = {
    currentPage: number;
    setPage: (page: number) => void;
    showNextPage: boolean;
};

export const Pager = (props: Props) => {
    let { currentPage, setPage, showNextPage } = props;
    let handleClickBack = (event: any) => {
        event.preventDefault();
        setPage(currentPage - 1);
    }

    let handleClickForward = (event: any) => {
        event.preventDefault();
        setPage(currentPage + 1);
    }

    return <div>
        {currentPage > 0 ? <a href="#" onClick={handleClickBack}>&lt;</a> : <span>&nbsp;</span>}
        <span style={{ fontStyle: 'bold' }}> page {currentPage + 1} </span>
        {showNextPage ? <a href="#" onClick={handleClickForward}>&gt;</a> : ""}
    </div>
};