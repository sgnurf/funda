import { FilterTypeProps } from "./filterTypes";
import React, { useState } from "react"
import { CustomInput } from "reactstrap";

export const GardenFilter = (props: FilterTypeProps) => {

    const [withGarden, setWithGarden] = useState(false)

    const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const newValue = event.target.checked
        setWithGarden(newValue)
        props.setFilter(
            newValue ? "garden" : null
        )
    }

    return <CustomInput type="switch" id="withGarden" label="Only consider properties with a garden" checked={withGarden} onChange={handleChange} />
}