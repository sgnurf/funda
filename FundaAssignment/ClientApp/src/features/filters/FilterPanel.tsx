import { FilterTypeProps } from "./filterTypes";
import React, { FunctionComponent, useState } from "react"

export interface FilterPanelProps {
    filterChanged: (filters: string[]) => void,
    children: (FunctionComponent<FilterTypeProps>)[]
}

export const FilterPanel = (props: FilterPanelProps) => {

    const [indexedFilters, setIndexedFilters] = useState([] as string[])

    const onFilterStateChange = (index: number, newValue: string|null) => {
        const newIndexedFilter = Object.assign([], indexedFilters, { [index]: newValue });
        setIndexedFilters(newIndexedFilter)
        props.filterChanged(newIndexedFilter.filter(f => f))
    }

    return (
        <div>
            {
                props.children.map((FilterComponent, index) =>
                    <FilterComponent key={ index } setFilter={(newValue) => onFilterStateChange(index, newValue)} />
                )
            }
        </div>
    )
}

export default FilterPanel