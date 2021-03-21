import * as React from 'react';
import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import CityFilter from '../filters/CityFilter';
import FilterPanel from '../filters/FilterPanel';
import { GardenFilter } from '../filters/GardenFilter';
import { fetchTopAgents, selectTopAgents, selectTopAgentsStatus } from "./topAgentsSlice";
import TopAgentsTable from './TopAgentsTable';

function TopAgents() {
    const dispatch = useDispatch()
    const agents = useSelector(selectTopAgents)
    const status = useSelector(selectTopAgentsStatus)
    const [filters, setFilters] = React.useState<string[]>(["Amsterdam"])

    useEffect(() => {
            dispatch(fetchTopAgents({ count: 10, filter: filters }),
            )
        }
        , [filters])
     

    return (
        <div>
            <FilterPanel filterChanged={(newFilter) => setFilters(newFilter)}>
                {[ /*TODO: Inject List of Filters*/
                    CityFilter,
                    GardenFilter
                ]}
            </FilterPanel>
            <TopAgentsTable agents={agents} status={status} />
        </div>
    );
}

export default TopAgents