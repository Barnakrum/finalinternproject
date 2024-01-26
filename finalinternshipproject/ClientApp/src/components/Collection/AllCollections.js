import axios from "axios";
import { useEffect, useState } from "react"
import Collection from "./Collection";

export default function AllCollections() {

    const [collections, setCollections] = useState([]);

    useEffect(() => {
        
        
        try {

            axios.get(process.env.REACT_APP_BASE_URL+"/api/collection/getall")
            .then((r) => {
            setCollections(r.data.data);

            })
        } catch (error) {
            console.log(error)
        }
    

    }, [])
    
    function handleClick() {
        console.log(collections)
    }
    
    //if(collections.length<1) return <>x</>; else return collections.map((collection, index) => <></>)

    return (
        <>
        <button onClick={handleClick}></button>
        {collections.map((collection, index) => 

        <Collection
        variant="short"
        key={index}
        collectionData={collection}/>)}
        </>
    )
}