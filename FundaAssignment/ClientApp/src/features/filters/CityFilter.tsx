import { FilterTypeProps } from "./filterTypes";
import React, { useEffect } from "react"

export const CityFilter = (props: FilterTypeProps) => {
    const city = "Amsterdam"
    useEffect(()=>
        props.setFilter(city)
        , [city]
    )

    //TODO: Make it an input so that user can choose location
    return (<p>Searching for the top agents in { city }.</p>)
}

export default CityFilter