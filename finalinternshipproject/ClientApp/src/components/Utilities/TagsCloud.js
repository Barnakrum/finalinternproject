
import axios from "axios"
import { useEffect, useState } from "react"
import { TagCloud } from "react-tagcloud"





export default function TagsCloud() {
    useEffect(() => {
        axios.get(process.env.REACT_APP_BASE_URL+"/api/tag/cloud")
        .then((r) => {setTags(r.data.data)})
    }, [])

    const [tags, setTags] = useState(null);
    



    return (tags === null ? null : <><TagCloud tags={tags} minSize={12} maxSize={35}/></>)
}