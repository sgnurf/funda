import * as React from 'react';
import { FunctionComponent } from 'react';
import { Alert, Spinner } from 'reactstrap';
import { stateStatus } from './topAgentsSlice';
import { Agent } from './topAgentTypes';

export interface TopAgentsTableProps {
    agents: Agent[],
    status: stateStatus
}

const AgentTable: FunctionComponent = (props) => (
    <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
            <tr>
                <th>Agent</th>
                <th>Items in offer</th>
            </tr>
        </thead>
        < tbody >
            {props.children}
        </tbody >
    </table>)

function TopAgentsTable(props: TopAgentsTableProps) {

    let tableContent

    if (props.status === 'error') {
        tableContent = <tr><td colSpan={2}><Alert color="danger">We were unable to retrieve the data. Please try again later.</Alert></td></tr>
    }
    else if (props.status === 'loading') {
        tableContent = <tr><td colSpan={2} style={{ textAlign: 'center' }}>
            Please wait, this may take several minutes<br />
            <Spinner color="primary" />
        </td></tr>
    }
    else {
        tableContent = props.agents.map(a =>
            <tr key={a.agentId}>
                <td>{a.agentName}</td>
                <td>{a.offerCount}</td>
            </tr>)
    }

    return (
        <AgentTable>
            {tableContent}
        </AgentTable>
    );
}

export default TopAgentsTable